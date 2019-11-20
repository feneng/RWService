using System.ServiceProcess;

namespace RWService
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        private static void Main()
        {
            var servicesToRun = new ServiceBase[]
            {
                new HSPD_RW_Service02()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
