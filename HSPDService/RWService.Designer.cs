﻿namespace RWService
{
    partial class RwService
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
            this.readWeight = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.readWeight)).BeginInit();
            // 
            // readWeight
            // 
            this.readWeight.Interval = 3000D;
            this.readWeight.Elapsed += new System.Timers.ElapsedEventHandler(this.GenUseList_Elapsed);
            // 
            // RWService
            // 
            this.ServiceName = "HSJQ_RW_Service";
            ((System.ComponentModel.ISupportInitialize)(this.readWeight)).EndInit();

        }

        #endregion

        private System.Timers.Timer readWeight;
    }
}