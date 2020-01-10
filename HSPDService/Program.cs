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
                new HSJQ_RW_Service01()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
