using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using System.IO;
using Brilliantech.Tsk.Data.CL.Model;
using Brilliantech.Tsk.Manage.WebApp.Util;
using Brilliantech.Tsk.Data.CL.Repository.Interface;
using Brilliantech.Tsk.Data.CL.Repository.Implement;
using System.Net.Mail;
using Brilliantech.Framwork.Utils.LogUtil;
using System.Text;
using Brilliantech.Tsk.Manage.WebApp.Models;
using Ionic.Zip;

namespace Brilliantech.Tsk.Manage.WebApp.Job
{
    public class TskDataEmailCronJob : IJob
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(TskDataEmailCronJob));

        /// <summary>
        /// Called by the <see cref="IScheduler" /> when a
        /// <see cref="ITrigger" /> fires that is associated with the <see cref="IJob" />.
        /// </summary>
        public virtual void Execute(IJobExecutionContext context)
        {
            //TestFile(context);
            //return;
            try
            {
                JobDataMap job = context.JobDetail.JobDataMap;
                DateTime startQueryDate = (DateTime)job.Get("StartDate");
                DateTime endQueryDate = (DateTime)job.Get("EndDate");
               
                List<User> users = null;
                using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
                {
                    IUserRep userRep = new UserRep(unitOfWork);
                    users = userRep.ListWithEmail();
                }

                SmtpClient smtpServer = new SmtpClient(EmailServerSetting.SmtpHost);
                //smtpServer.Port = EmailServerSetting.SmtpPort;
                smtpServer.Credentials = new System.Net.NetworkCredential(EmailServerSetting.EmailUser, EmailServerSetting.EmailPwd);


                foreach (User user in users)
                {
                    try
                    {
                        List<UserInspect> inspects = new List<UserInspect>();
                        using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
                        {
                            IUserInspectRep userInspectRep = new UserInspectRep(unitOfWork);
                            inspects = userInspectRep.ListByUserId(user.Id, startQueryDate, endQueryDate);
                        }
                        if (inspects.Count > 0)
                        {
                            using (MailMessage mail = new MailMessage())
                            {
                                mail.From = new MailAddress(EmailServerSetting.EmailAddress);
                                mail.To.Add(user.Email);
                                mail.Subject = "电测台数据文件";


                                var filename = "Inspect" + DateTime.Now.ToString("yyyyMMddHHmm") + "_" + Guid.NewGuid().ToString("N");
                                var csvfilename = filename + ".csv";
                                //var zipfilename = filename + ".zip";

                                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InspectFileTmp");
                                if (!Directory.Exists(filePath))
                                {
                                    Directory.CreateDirectory(filePath);
                                }
                                var csvFilePath = Path.Combine(filePath, csvfilename);
                                //var zipFilePath = Path.Combine(filePath, zipfilename);

                                using (FileStream csv = new FileStream(csvFilePath, FileMode.Create, FileAccess.ReadWrite))
                                {
                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        using (StreamWriter sw = new StreamWriter(ms, Encoding.UTF8))
                                        {
                                            sw.WriteLine(string.Join(",", InspectQueryModel.CsvHead.ToArray()));
                                            foreach (UserInspect i in inspects)
                                            {
                                                List<string> ii = new List<string>();
                                                foreach (string field in InspectQueryModel.Fileds)
                                                {
                                                    var value = i.GetType().GetProperty(field).GetValue(i, null);
                                                    ii.Add(value == null ? "" : value.ToString());
                                                }
                                                sw.WriteLine(string.Join(",", ii.ToArray()));
                                            }

                                            csv.Write(ms.ToArray(), 0, (int)ms.Length);
                                        }
                                    }
                                }
                                //using (ZipFile zip = new ZipFile())
                                //{
                                //    zip.AddFile(csvFilePath);
                                //    zip.Save(zipfilename);
                                //}
                                Attachment att = new Attachment(csvFilePath);
                                mail.Attachments.Add(att);

                                smtpServer.Send(mail);
                                LogUtil.Logger.Info("Send Inspect Email to:" + user.Email + ", File: " + filename);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogUtil.Logger.Error(ex.Message);
                    }
                }
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error(e.Message);
            }
        }
        public void TestFile(IJobExecutionContext context)
        {
            try
            {
                IJobDetail job = context.JobDetail;

                var filename = "C:\\Excel\\" + DateTime.Now.ToString("yyyyMMddHHmmsss") + ".txt";
                //using (FileStream csv = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
                //{
                //    using (MemoryStream ms = new MemoryStream())
                //    {
                //        using (StreamWriter sw = new StreamWriter(ms, Encoding.UTF8))
                //        {
                //            sw.WriteLine("StartDate:" + job.JobDataMap.Get("StartDate").ToString());
                //            sw.WriteLine("EndDate:" + job.JobDataMap.Get("EndDate").ToString());
                //            sw.WriteLine("HAHA");

                //            LogUtil.Logger.Error(job.JobDataMap.Get("StartDate").ToString());
                //            csv.Write(ms.ToArray(), 0, (int)ms.Length);
                //        }
                //    }
                //}
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.WriteLine(job.Key);
                    sw.WriteLine("StartDate:" + job.JobDataMap.Get("StartDate").ToString());
                    sw.WriteLine("EndDate:" + job.JobDataMap.Get("EndDate").ToString());
                   
                }
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error(e.Message);
            }
        }
    }
}