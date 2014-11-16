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
        private static string groupId = "TskDataEmailCronGroup";
        public TskInspectDetailJobTrigger()
        {
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                                             .WithIdentity("TskDataEmailCronGroupT1", groupId)
                                                             .WithCronSchedule(ConfigurationManager.AppSettings["TskDataEmailCrons"])
                                                             .Build();
            IJobDetail job = JobBuilder.Create<TskInspectDetailJob>()
                .WithIdentity("job1", groupId)
                .Build();
            QmailRunner.Scheduler.ScheduleJob(job, trigger);

            LogUtil.Logger.Info("测试数据文件邮件任务启动成功");
        }
    }

    public class TskInspectDetailJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                // 初始化参数
                List<List<string>> parameters = ParameterUtil.GetParameterFromFile("TskInspectDetailJob.txt");

                foreach (var parameter in parameters)
                {
                    string fileName = "测试台数据_" + DateTime.Now.ToString("yyyyMMddHHmm") + "_" + Guid.NewGuid().ToString("N") + ".xlsx";
                    string filePath = FileUtil.GetExcelPath(fileName);
                    Console.WriteLine(filePath);

                    bool blankExcel = true;
                    // 建立数据库连接
                    using (SqlConnection conn = SQLUtil.GetConnection("Leoni_Tsk_JNConnectionString"))
                    {
                        // 初始化数据库执行命令
                        using (SqlCommand cmd = new SqlCommand("QueryUserTskDetailData", conn))
                        {
                            conn.Open();
                            cmd.CommandType = CommandType.StoredProcedure;// 命令类型为存储过程
                            SqlParameter p = new SqlParameter("@UserTskNo", SqlDbType.VarChar, 2000);
                            p.Value = parameter[1];
                            cmd.Parameters.Add(p);
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                using (DataSet ds = new DataSet())
                                {
                                    da.Fill(ds);
                                    conn.Close();
                                    using (ExcelPackage excel = new ExcelPackage(new FileInfo(filePath)))
                                    {
                                        // 统计表
                                        ExcelWorksheet totalSheet = excel.Workbook.Worksheets.Add("Total");
                                        DataTable totalDT = ds.Tables[0];
                                        if (totalDT.Rows.Count > 0)
                                        {
                                            blankExcel = false;
                                            for (int i = 0; i < totalDT.Rows.Count; i++)
                                            {
                                                for (int j = 0; j < totalDT.Rows[i].ItemArray.Length; j++)
                                                {
                                                    totalSheet.Cells[i+1, j+1].Value = totalDT.Rows[i][j].ToString();
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
                                        excel.Save();
                                    }

                                }
                            }
                        }
                    }

                    if (!blankExcel) {
                        EmailUtil.Send("电测台测试数据", parameter[0], filePath);
                        LogUtil.Logger.Info("Send Inspect Email to:" + parameter[0] + ", File: " + fileName);
                    }
                }
            }
            catch (Exception e) {
                throw e;
            }
        }
    }
}
