namespace Brilliantech.DatahouseService.PackSys
{
    partial class DatahousePackSysService
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
            this.scanTimer = new System.Timers.Timer();
            this.datahousePackEventLog = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.scanTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datahousePackEventLog)).BeginInit();
            // 
            // scanTimer
            // 
            this.scanTimer.Enabled = true;
            this.scanTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.scanTimer_Elapsed);
            // 
            // datahousePackEventLog
            // 
            this.datahousePackEventLog.Log = "pack.ifdatahouse.source.new";
            this.datahousePackEventLog.Source = "pack.ifdatahouse.log.source";
            // 
            // DatahousePackSysService
            // 
            this.ServiceName = "DatahousePackSysService";
            ((System.ComponentModel.ISupportInitialize)(this.scanTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datahousePackEventLog)).EndInit();

        }

        #endregion

        private System.Timers.Timer scanTimer;
        private System.Diagnostics.EventLog datahousePackEventLog;

    }
}
