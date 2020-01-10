using System;
using System.Collections;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Windows.Forms;

namespace RWServiceControler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private readonly string _serviceFilePath = $"{Application.StartupPath}\\RWService.exe";

        private void Button1_Click(object sender, EventArgs e)
        {
            label1.Text = "服务安装中...";
            if (IsServiceExisted(textBox1.Text.Trim()))
            {
                UninstallService(_serviceFilePath);
            }

            InstallService(_serviceFilePath);
            label1.Text = "服务安装完成！";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            label1.Text = "服务启动中...";
            if (this.IsServiceExisted(textBox1.Text.Trim())) this.ServiceStart(textBox1.Text.Trim());
            label1.Text = "服务启动完成！";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            label1.Text = "服务停止中...";
            if (this.IsServiceExisted(textBox1.Text.Trim())) this.ServiceStop(textBox1.Text.Trim());
            label1.Text = "服务停止完成！";
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            label1.Text = "服务卸载中...";
            if (this.IsServiceExisted(textBox1.Text.Trim()))
            {
                this.ServiceStop(textBox1.Text.Trim());
                this.UninstallService(_serviceFilePath);
            }
            label1.Text = "服务卸载完成！";
        }

        //判断服务是否存在
        private bool IsServiceExisted(string service)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController sc in services)
            {
                if (sc.ServiceName.ToLower() == service.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        //安装服务
        private void InstallService(string servicePath)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = servicePath;
                IDictionary savedState = new Hashtable();
                installer.Install(savedState);
                installer.Commit(savedState);
            }
        }

        //卸载服务
        private void UninstallService(string servicePath)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = servicePath;
                installer.Uninstall(null);
            }
        }
        //启动服务
        private void ServiceStart(string service)
        {
            using (ServiceController control = new ServiceController(service))
            {
                if (control.Status == ServiceControllerStatus.Stopped)
                {
                    control.Start();
                }
            }
        }

        //停止服务
        private void ServiceStop(string service)
        {
            using (ServiceController control = new ServiceController(service))
            {
                if (control.Status == ServiceControllerStatus.Running)
                {
                    control.Stop();
                }
            }
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
