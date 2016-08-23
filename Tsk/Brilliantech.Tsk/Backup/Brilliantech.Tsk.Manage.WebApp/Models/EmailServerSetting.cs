using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Brilliantech.Tsk.Manage.WebApp.Models
{
    public class EmailServerSetting
    {
        private static string smtpHost;
        private static int smtpPort;
        private static string emailAddress;
        private static string emailUser;
        private static string emailPwd;


        static EmailServerSetting()
        {
            var appSettings = ConfigurationManager.AppSettings;
            smtpHost = appSettings["SmtpHost"];
          //  smtpPort = int.Parse(appSettings["SmtpPort"]);
            emailAddress = appSettings["EmailAddress"];
            emailUser = appSettings["EmailUser"];
            emailPwd = appSettings["EmailPwd"];
        }


        public static string SmtpHost
        {
            get { return EmailServerSetting.smtpHost; }
            set { EmailServerSetting.smtpHost = value; }
        }

        public static int SmtpPort
        {
            get { return EmailServerSetting.smtpPort; }
            set { EmailServerSetting.smtpPort = value; }
        }

        public static string EmailAddress
        {
            get { return EmailServerSetting.emailAddress; }
            set { EmailServerSetting.emailAddress = value; }
        }

        public static string EmailUser
        {
            get { return EmailServerSetting.emailUser; }
            set { EmailServerSetting.emailUser = value; }
        }

        public static string EmailPwd
        {
            get { return EmailServerSetting.emailPwd; }
            set { EmailServerSetting.emailPwd = value; }
        }
        public static void Save()
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var appSettings = configFile.AppSettings.Settings;
            appSettings["SmtpHost"].Value = smtpHost;
          //  appSettings["SmtpPort"].Value = smtpPort.ToString();
            appSettings["EmailAddress"].Value = EmailAddress;
            appSettings["EmailPwd"].Value = emailPwd;
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
    }
}