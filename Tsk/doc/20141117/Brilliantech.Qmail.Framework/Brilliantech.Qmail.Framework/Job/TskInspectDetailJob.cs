using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Brilliantech.Framwork.Utils.LogUtil;
using System.Configuration;
using System.Data.SqlClient;
using Brilliantech.Qmail.Framework.Util;
using System.Data;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;

namespace Brilliantech.Qmail.Framework.Job
{
    public class TskInspectDetailJobTrigger
    {
        // 设置触发器组，唯一
        private static string groupId = "TskDataEmailCronGroup";
        public TskInspectDetailJobTrigger()
        {
            // 从配置文件 App.config中读取TskDataEmailCrons的值,并通过符号“;”分隔
            // 值组成方式是： 运行时间,存储过程名;运行时间,存储过程名;运行时间,存储过程名
            string[] crons=ConfigurationManager.AppSettings["TskDataEmailCrons"].Split(';');
            for (var i = 0; i < crons.Length; i++)
            {
                // 通过符号“,”分隔每个任务
                string[] cron = crons[i].Split(',');
                // 新建触发器,触发器名称需保持不同
                string triggerName="TskDataEmailCronGroupT"+i.ToString();
                ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                                                 .WithIdentity(triggerName, groupId) //
                                                                 .WithCronSchedule(cron[0])
                                                                 .Build();
                // 新建任务
                string jobName = "TskDataEmailCronJob" + i.ToString();
                IJobDetail job = JobBuilder.Create<TskInspectDetailJob>()
                    .WithIdentity(jobName, groupId)
                    .Build();
                
                // 添加任务参数,将存储过程名传给任务
                job.JobDataMap.Add("CommandText",cron[1]);
                // 将任务及触发器安排到系统中
                QmailRunner.Scheduler.ScheduleJob(job, trigger);
                // 日志记录
                LogUtil.Logger.Info("测试数据文件邮件任务启动成功");
            }
        }
    }

    public class TskInspectDetailJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                // 从文件TskInspectDetailJob.txt中初始化参数
                List<List<string>> parameters = ParameterUtil.GetParameterFromFile("TskInspectDetailJob.txt");

                foreach (var parameter in parameters)
                {
                    // 设置文件名及路径
                    string fileName = "测试台数据_" + DateTime.Now.ToString("yyyyMMddHHmm") + "_" + Guid.NewGuid().ToString("N") + ".xlsx";
                    string filePath = FileUtil.GetExcelPath(fileName);
                    Console.WriteLine(filePath);

                    bool blankExcel = true;
                    // 建立数据库连接
                    // Leoni_Tsk_JNConnectionString 是App.config 配置的数据库连接方式
                    // 如果扩展其它数据库，添加并使用配置即可
                    using (SqlConnection conn = SQLUtil.GetConnection("Leoni_Tsk_JNConnectionString"))
                    {
                        // 从任务中获取存储过程名，并初始化数据库执行命令
                        using (SqlCommand cmd = new SqlCommand(context.JobDetail.JobDataMap.Get("CommandText").ToString(), conn))
                        {
                            // 打开数据库连接
                            conn.Open();
                            // 命令类型为存储过程
                            cmd.CommandType = CommandType.StoredProcedure;
                            // 设置参数
                            SqlParameter p = new SqlParameter("@UserTskNo", SqlDbType.VarChar, 2000);
                            p.Value = parameter[1];
                            cmd.Parameters.Add(p);
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                using (DataSet ds = new DataSet())
                                {
                                    // 填充数据
                                    da.Fill(ds);
                                    // 关闭数据库连接
                                    conn.Close();
                                    // 新建Excel
                                    using (ExcelPackage excel = new ExcelPackage(new FileInfo(filePath)))
                                    {
                                        // 统计表
                                        ExcelWorksheet totalSheet = excel.Workbook.Worksheets.Add("Total");
                                        string[] totalHeader = new string[] { "PartNo.","总计","机台","小计"};
                                        for (int i = 0; i < totalHeader.Length; i++)
                                        {
                                            // 设置列宽
                                            totalSheet.Column(i + 1).Width = 18;
                                            // 设置横向居中
                                            totalSheet.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            totalSheet.Cells[1, i + 1].Value = totalHeader[i];
                                            // 设置字体加粗
                                            totalSheet.Cells[1, i + 1].Style.Font.Bold = true;
                                        }

                                        // 获取数据库统计表数据，通过算法写到Excel中
                                        DataTable totalDT = ds.Tables[0];
                                        if (totalDT.Rows.Count > 0)
                                        {
                                            blankExcel = false;
                                            int rollUpRowIndex = 0;
                                            int rowSpan = 0;
                                            int partNoNum = 0;
                                            for (int i = 0; i < totalDT.Rows.Count; i++)
                                            {
                                                if (rollUpRowIndex == i)
                                                {
                                                    if (i != 0)
                                                    {
                                                        partNoNum++;
                                                    }
                                                    rowSpan = int.Parse(totalDT.Rows[rollUpRowIndex][3].ToString());

                                                    totalSheet.Cells[2 + rollUpRowIndex - partNoNum, 1].Value = totalDT.Rows[rollUpRowIndex][0].ToString();
                                                    totalSheet.Cells[2 + rollUpRowIndex - partNoNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    totalSheet.Cells[2 + rollUpRowIndex - partNoNum, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    totalSheet.Cells[2 + rollUpRowIndex - partNoNum, 1, 1 + rollUpRowIndex + rowSpan - partNoNum, 1].Merge = true;

                                                    totalSheet.Cells[2 + rollUpRowIndex - partNoNum, 2].Value = totalDT.Rows[rollUpRowIndex][2].ToString();
                                                    totalSheet.Cells[2 + rollUpRowIndex - partNoNum, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    totalSheet.Cells[2 + rollUpRowIndex - partNoNum, 2].Style.VerticalAlignment =ExcelVerticalAlignment.Center;

                                                    totalSheet.Cells[2 + rollUpRowIndex - partNoNum, 2, 1 + rollUpRowIndex - partNoNum + rowSpan, 2].Merge = true;
                                                    rollUpRowIndex += rowSpan+1;
                                                  
                                                }
                                                else
                                                {
                                                    totalSheet.Cells[i + 1 - partNoNum, 3].Value = totalDT.Rows[i][1].ToString();
                                                    totalSheet.Cells[i + 1 - partNoNum, 4].Value = totalDT.Rows[i][2].ToString();
                                                }
                                            }
                                        }

                                        // 详细表
                                        ExcelWorksheet detailSheet = excel.Workbook.Worksheets.Add("Detail");
                                        // 写表头
                                        string[] detailHeader=new string[]{"TskNo","LeoniNo","CusNo","ClipScanNo","ClipScanTime1","ClipScanTime2","TskScanNo","TskScanTime3","Time3MinTime2","OkOrNot","CreatedAt"};
                                        for (int i = 0; i < detailHeader.Length; i++) {
                                            detailSheet.Column(i+1).Width = 18;
                                            detailSheet.Cells[1, i+1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            detailSheet.Cells[1, i + 1].Value = detailHeader[i];
                                            detailSheet.Cells[1, i + 1].Style.Font.Bold = true;
                                        }
                                        DataTable detailDT = ds.Tables[1];
                                        if (detailDT.Rows.Count > 0)
                                        {
                                            blankExcel = false;
                                            for (int i = 0; i < detailDT.Rows.Count; i++)
                                            {
                                                for (int j = 0; j < detailDT.Rows[i].ItemArray.Length; j++)
                                                {
                                                    detailSheet.Cells[i + 2, j + 1].Value = detailDT.Rows[i][j].ToString();
                                                }
                                            }
                                        }
                                        // 保存 Excel,默认文件夹是当前程序中的 ExcelTmp
                                        excel.Save();
                                    }

                                }
                            }
                        }
                    }
                    // 判断Excel是否有数据
                    if (!blankExcel)
                    {
                        // 如果有，则发送邮件
                        EmailUtil.Send("电测台测试数据", parameter[0], filePath);
                        LogUtil.Logger.Info("【邮件发送成功】" + parameter[0] + ", File: " + fileName);
                    }
                    else {
                        LogUtil.Logger.Error("【测试数据为空，邮件未发送】" + parameter[0] + ", File: " + fileName);
                    }
                }
            }
            catch (Exception e) {
                LogUtil.Logger.Error(e.Message);
                throw e;
            }
        }
    }
}
