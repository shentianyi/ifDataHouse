﻿namespace Brilliantech.PackSysDataService
{
    partial class PackSysDbDataService
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
            this.readDataTimer = new System.Timers.Timer();
            this.packDataSysEventLog = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.readDataTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.packDataSysEventLog)).BeginInit();
            // 
            // readDataTimer
            // 
            this.readDataTimer.Enabled = true;
            this.readDataTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.readDataTimer_Elapsed);
            // 
            // packDataSysEventLog
            // 
            this.packDataSysEventLog.Log = "packdataSys.source.new";
            this.packDataSysEventLog.Source = "packdataSys.log.source";
            // 
            // PackSysDbDataService
            // 
            this.ServiceName = "PackSysDbDataService";
            ((System.ComponentModel.ISupportInitialize)(this.readDataTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.packDataSysEventLog)).EndInit();

        }

        #endregion

        private System.Timers.Timer readDataTimer;
        private System.Diagnostics.EventLog packDataSysEventLog;

    }
}
