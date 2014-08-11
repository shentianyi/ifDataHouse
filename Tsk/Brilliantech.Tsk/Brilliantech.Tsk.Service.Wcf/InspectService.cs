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
                        Id=Guid.NewGuid(),
                        Text = text,
                        CreatedAt = DateTime.Now
                    };
                    try
                    {
                        if (text == null || text.Length == 0 || text.Split(TskConfig.DataSpliter).Length != TskConfig.DataCount)
                        {
                            message.Result = false;
                            message.Messages.Add("数据为空或数据格式不存在");
                            message.Messages.Add("数据属性长度为：" + text.Split(TskConfig.DataSpliter).Length.ToString());
                            message.Messages.Add("分隔符为：" + TskConfig.DataSpliter.ToString());
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
                                ClipScanTime1 = data[4],
                                ClipScanTime2 = data[5],
                                TskScanNo = data[6],
                                TskScanTime3 = data[7],
                                Time3MinTime2 = data[8],
                                OkOrNot = data[9],
                                CreatedAt = DateTime.Now,
                                OriginId = inspectOrigin.Id
                            };

                            inspectRep.Create(inspect);
                            message.Result = true;
                        }
                    }
                    catch (Exception e) {
                        LogUtil.Logger.Error(e.Message);
                        message.Messages.Add(e.GetType().ToString());
                        message.Messages.Add(e.Message);
                    }
                    inspectOrigin.ProcessResult = message.Result;
                    inspectOrigin.ProcessMessage = message.GetMessageContent();
                    inspectOriginRep.Create(inspectOrigin);

                    unitOfWork.Submit();
                }
            }
            catch(Exception e) {
                LogUtil.Logger.Error(e.Message);
                message.Messages.Add(e.GetType().ToString());
                message.Messages.Add(e.Message);
            }
            return message;
        }
    }
}
