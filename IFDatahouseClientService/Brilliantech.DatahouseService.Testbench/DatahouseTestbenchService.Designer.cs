namespace Brilliantech.DatahouseService.Testbench
{
    partial class DatahouseTestbenchService
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
            this.datahouseTestEventLog = new System.Diagnostics.EventLog();
            this.scanTimer = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.datahouseTestEventLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanTimer)).BeginInit();
            // 
            // datahouseTestEventLog
            // 
            this.datahouseTestEventLog.Log = "ifdatahouse.testbench.source.new";
            this.datahouseTestEventLog.Source = "ifdatahouse.testbench.log.source";
            // 
            // scanTimer
            // 
            this.scanTimer.Enabled = true;
            this.scanTimer.Interval = 5000D;
            this.scanTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.scanTimer_Elapsed);
            // 
            // DatahouseTestbenchService
            // 
            this.ServiceName = "DatahouseTestbenchService";
            ((System.ComponentModel.ISupportInitialize)(this.datahouseTestEventLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanTimer)).EndInit();

        }

        #endregion

        private System.Diagnostics.EventLog datahouseTestEventLog;
        private System.Timers.Timer scanTimer;
    }
}
