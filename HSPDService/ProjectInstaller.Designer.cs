﻿namespace RWService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceProcessInstall1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstall1
            // 
            this.serviceProcessInstall1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstall1.Password = null;
            this.serviceProcessInstall1.Username = null;
            this.serviceProcessInstall1.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.ServiceProcessInstall1_AfterInstall);
            // 
            // serviceInstaller1
            // 
            this.serviceInstaller1.Description = "获取电子秤重量01(假期)";
            this.serviceInstaller1.DisplayName = "HSJQ_RW_Service01";
            this.serviceInstaller1.ServiceName = "HSJQ_RW_Service01";
            this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.serviceInstaller1.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.ServiceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstall1,
            this.serviceInstaller1});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstall1;
        private System.ServiceProcess.ServiceInstaller serviceInstaller1;
    }
}