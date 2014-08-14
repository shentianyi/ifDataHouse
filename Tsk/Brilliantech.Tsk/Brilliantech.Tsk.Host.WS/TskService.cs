using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using Brilliantech.Framwork.Utils.LogUtil;

namespace Brilliantech.Tsk.Host.WS
{
    public partial class TskService : ServiceBase
    {
        private ServiceHost inspectService;

        public TskService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try {
                inspectService = new ServiceHost(typeof(Tsk.Service.Wcf.InspectService));
                
                inspectService.Open();
                EventLog.WriteEntry("服务启动成功");
                LogUtil.Logger.Info("服务启动成功");
            }
            catch (Exception e) {
                EventLog.WriteEntry("服务启动失败:" + e.Message);
                LogUtil.Logger.Error("服务启动失败:" + e.Message);
                this.Stop();
            }
        }

        protected override void OnStop()
        {
            LogUtil.Logger.Warn("服务已停止");
            EventLog.WriteEntry("服务已停止");
            if (inspectService != null) {
                inspectService.Close();
            }
        }
    }
}
