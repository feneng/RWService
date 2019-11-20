using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace RWService
{
    public partial class HSPD_RW_Service02 : ServiceBase
    {
        private EventLog _el;
        private string _serviceSource = "";
        private string _webServiceUrl = "";
        private int _port;
        private string _printId = "";

        public HSPD_RW_Service02()
        {
            InitializeComponent();
        }


        protected override void OnStart(string[] args)
        {
            readWeight.Interval = 500;    //0.5秒写入一次
            readWeight.Enabled = true;

            try
            {
                var xmlFileName = "Setting.xml";
                var path = Application.StartupPath;
                if (File.Exists(path + "\\" + xmlFileName))
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(path + "\\" + xmlFileName);

                    var xmlNodeList = xmlDoc.GetElementsByTagName("ServiceName");
                    if (xmlNodeList.Count > 0)
                    {
                        _serviceSource = xmlNodeList[0].InnerText;
                    }

                    xmlNodeList = xmlDoc.GetElementsByTagName("WebServiceUrl");
                    if (xmlNodeList.Count > 0)
                    {
                        _webServiceUrl = xmlNodeList[0].InnerText;
                    }

                    xmlNodeList = xmlDoc.GetElementsByTagName("Port");
                    if (xmlNodeList.Count > 0)
                    {
                        _port = int.Parse(xmlNodeList[0].InnerText);
                    }

                    xmlNodeList = xmlDoc.GetElementsByTagName("PrintId");
                    if (xmlNodeList.Count > 0)
                    {
                        _printId = xmlNodeList[0].InnerText;
                    }

                    if (string.IsNullOrEmpty(_webServiceUrl) || _port == 0||string.IsNullOrEmpty(_printId))
                    {
                        Stop();
                    }

                    _el = new EventLog
                    {
                        Source = _serviceSource
                    };
                }
                else
                {
                    Stop();
                }
            }
            catch (Exception ex)
            {
                _el.Log = ex.ToString();
                _el.WriteEntry(_el.Log, EventLogEntryType.Error);
            }
        }

        protected override void OnStop()
        {
        }

        private void GenUseList_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var service = new WeighService.WeighService
            {
                Url = _webServiceUrl,
                Timeout = 30000
            };
            try
            {
                SetWeigh(this._port, this._printId, service);
            }
            catch
            {
                // ignored
            }
        }

        private void SetWeigh(int port,string printId, WeighService.WeighService service)
        {
            string weight = "0";

            UdpClient udpClient = null;
            try
            {
                var remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);
                var localEndpoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[1], port);
                udpClient = new UdpClient(localEndpoint);
                var receiveBytes = udpClient.Receive(ref remoteEndpoint);
                string data = Encoding.Default.GetString(receiveBytes, 0, receiveBytes.Length);
                weight = data;
                _el.WriteEntry($"获取到重量{data}", EventLogEntryType.Warning);
            }
            catch
            {
                return;
            }
            finally
            {
                udpClient?.Close();
            }


            if (weight.IndexOf('-') > 0)
            {
                weight = "-" + weight.Replace("=", "")
                             .Replace("(kg)", "")
                             .Replace("ST,NT,", "")
                             .Replace("+", "")
                             .Replace("-", "")
                             .Replace("kg", "").Trim();
            }
            else
            {
                weight = weight.Replace("=", "")
                    .Replace("(kg)", "")
                    .Replace("ST,NT,", "")
                    .Replace("+", "")
                    .Replace("-", "")
                    .Replace("kg", "").Trim(); //ST,NT,+   9.75kg
            }

            if (weight.IndexOf('.') > 0)
            {
                try
                {
                    string str = weight.Substring(weight.IndexOf('.') + 1, 1);
                    if (int.Parse(str) <= 2)
                    {
                        weight = weight.Substring(0, weight.IndexOf('.'));
                    }

                    double wg = double.Parse(weight) * 10;
                    wg = Math.Floor(wg) * 0.1;

                    weight = wg.ToString("0.###");
                }
                catch
                {
                    //ignore
                }
            }


            var infoMag = service.SetWeight(printId, float.Parse(weight));
            if (infoMag == 0) return;
            _el.WriteEntry($"重量记录写入失败:{infoMag},服务端接收数据:{weight},客户端:{printId},端口:{port}", EventLogEntryType.Error);
        }
    }
}
