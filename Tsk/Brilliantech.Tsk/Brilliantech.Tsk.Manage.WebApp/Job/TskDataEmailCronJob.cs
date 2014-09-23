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
            try
            {
                List<User> users = null;
                using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
                {
                    IUserRep userRep = new UserRep(unitOfWork);
                    users = userRep.ListWithEmail();
                }

                SmtpClient smtpServer = new SmtpClient("smtp.163.com");
                smtpServer.Credentials = new System.Net.NetworkCredential("iwangsong@163.com", "iwangsong520520");
                smtpServer.Port = 25;

                foreach (User user in users)
                {
                    LogUtil.Logger.Info(user.Email);
                    try
                    {
                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress("iwangsong@163.com");
                            mail.To.Add(user.Email);
                            mail.Subject = "Inspect Data File";
                            List<UserInspect> inspects = new List<UserInspect>();
                            using (IUnitOfWork unitOfWork = new TskDataDataContext(DbUtil.ConnectionString))
                            {
                                IUserInspectRep userInspectRep = new UserInspectRep(unitOfWork);
                                inspects = userInspectRep.ListByUserId(user.Id);
                            }

                            var filename = "Inspect" + Guid.NewGuid().ToString() + DateTime.Now.ToString("yyyyMMddHHmm") + ".csv";
                            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InspectFileTmp", filename);
                            using (FileStream csv = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
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
                            Attachment att = new Attachment(filePath);
                            mail.Attachments.Add(att);
                            smtpServer.Send(mail);
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
    }
}