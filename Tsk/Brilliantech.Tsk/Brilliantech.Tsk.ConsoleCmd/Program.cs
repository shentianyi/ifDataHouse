using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using Brilliantech.Tsk.ConsoleCmd.InspectService;
using System.Net.Mail;

namespace Brilliantech.Tsk.ConsoleCmd
{
    class Program
    {
        static void Main(string[] args)
        {

          //Console.WriteLine(  Convert.ToDateTime(""));
            //List<string> tsks = new List<string>() { "TSK044", "TSK544", "TSK045", "TSK046" };
            //List<string> leoniNos = new List<string>() { "91G40538", "6100982", "6100991", "91G40537" };
            //List<string> cusNos = new List<string>() { "-", "-", "-" };
            //List<string> clipScanNos = new List<string>() { "CS001", "CS002", "CS003" };
            //List<string> times = new List<string>() { "-", "2014-08-01 13:01:15", "-", "2014-08-02 02:02:00", "2014-08-02 12:01:15", "2014-08-03 11:02:03" };
            //List<string> tskScanNos = new List<string>() { "-", "S002", "-", "-", "-" };
            //List<string> mins = new List<string>() { "-", "-", "13", "-", "15" };
            //List<string> okOrnots = new List<string>() { "1", "1", "0", "0", "1" };
            ////string result = "TSK056;91G40538;23431905;23431905;2014-08-01 12:01:15;2014-08-01 12:01:15;20T1906234141213;2014-08-01 14:23:13;15;1";
            //try
            //{
            //    InspectServiceClient client = new InspectServiceClient();
            //    foreach (string tsk in tsks)
            //    {
            //        foreach (string leoniNo in leoniNos)
            //        {
            //            foreach (string cusNo in cusNos)
            //            {
            //                foreach (string clipScanNo in clipScanNos)
            //                {
            //                    foreach (string time in times)
            //                    {
            //                        foreach (string tskScanNo in tskScanNos)
            //                        {
            //                            foreach (string min in mins)
            //                            {
            //                                foreach (string okOrnot in okOrnots)
            //                                {
            //                                    string put = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}",
            //                                        tsk, leoniNo, cusNo, clipScanNo, time,time, tskScanNo,
            //                                        time, min, okOrnot);
            //                                    Console.WriteLine(put);
            //                                    client.CreateInspect(put);
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            SmtpClient smtpServer = new SmtpClient("smtp.163.com");
            smtpServer.Credentials = new System.Net.NetworkCredential("iwangsong@163.com", "iwangsong520520");
            smtpServer.Port = 25;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("iwangsong@163.com");
            mail.To.Add("iwangsong@163.com");
            mail.Subject = "Inspect Data File";
            Attachment att = new Attachment(@"C:\Excel\b.xml");
            mail.Attachments.Add(att);
            smtpServer.Send(mail);

            Console.WriteLine(DateTime.Now.ToString("yyyy/M/d HH:mm:ss"));
            Console.Read();
        }
    }
}
