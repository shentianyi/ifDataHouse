using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text; 
using Brilliantech.Tsk.Data.CL.Model;
using Brilliantech.Tsk.Service.Wcf.Config;
using Brilliantech.Tsk.Data.CL.Repository.Interface;
using Brilliantech.Tsk.Data.CL.Repository.Implement;
using Brilliantech.Framwork.Utils.LogUtil;
using Brilliantech.Framwork.Message;

namespace Brilliantech.Tsk.Service.Wcf
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class InspectService : IInspectService
    {
        public ProcessMessage CreateInspect(string text)
        {
            ProcessMessage message = new ProcessMessage();
            try
            {
                using (IUnitOfWork unitOfWork = new TskDataDataContext(MSSqlConfig.ConnectionString))
                {
                    IInspectOriginRep inspectOriginRep = new InspectOriginRep(unitOfWork);
                    InspectOrigin inspectOrigin = new InspectOrigin()
                    {
                        Id = Guid.NewGuid(),
                        Text = text,
                        CreatedAt = DateTime.Now
                    };

                    if (text == null || text.Length == 0 || text.Split(TskConfig.DataSpliter).Length != TskConfig.DataCount)
                    {
                        message.Result = false;
                        message.Messages.Add("数据为空或数据格式不存在");
                        if (!string.IsNullOrEmpty(text))
                        {
                            message.Messages.Add("数据属性长度为：" + text.Split(TskConfig.DataSpliter).Length.ToString());
                            message.Messages.Add("分隔符为：" + TskConfig.DataSpliter.ToString());
                        }
                        LogUtil.Logger.Error(message.GetMessageContent());
                    }
                    else
                    {

                        IInspectRep inspectRep = new InspectRep(unitOfWork);
                        string[] data = text.Split(TskConfig.DataSpliter);
                        Inspect inspect = new Inspect()
                        {
                            Id = Guid.NewGuid(),
                            TskNo = data[0],
                            LeoniNo = data[1],
                            CusNo = data[2],
                            ClipScanNo = data[3],
                          //  ClipScanTime1 = data[4],
                           // ClipScanTime2 = data[6],
                            TskScanNo = data[7],
                           // TskScanTime3 = data[8],
                           // Time3MinTime2 = data[9],
                            OkOrNot = data[10],
                            CreatedAt = DateTime.Now,
                            OriginId = inspectOrigin.Id
                        };
                        DateTime clipScanTime1 = DateTime.Now;
                        if (DateTime.TryParse(data[4], out clipScanTime1))
                        {
                            inspect.ClipScanTime1 = clipScanTime1;
                        }

                        DateTime clipScanTime2 = DateTime.Now;
                        if (DateTime.TryParse(data[6], out clipScanTime2))
                        {
                            inspect.ClipScanTime2 = clipScanTime2;
                        }

                        DateTime tskScanTime3 = DateTime.Now;
                        if (DateTime.TryParse(data[8], out tskScanTime3))
                        {
                            inspect.TskScanTime3 = tskScanTime3;
                        }

                        float time3MinTime2 = 0;
                        if (float.TryParse(data[9], out time3MinTime2)) {
                            inspect.Time3MinTime2 = time3MinTime2;
                        }

                        inspectRep.Create(inspect);
                        message.Messages.Add("数据处理成功");
                        message.Result = true;
                    }

                    inspectOrigin.ProcessResult = message.Result;
                    inspectOrigin.ProcessMessage = message.GetMessageContent();
                    inspectOriginRep.Create(inspectOrigin);

                    unitOfWork.Submit();
                }
                return message;
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error(e.Message);
                throw e;
            }
        }
    }
}
