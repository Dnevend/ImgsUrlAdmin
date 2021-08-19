using FilesUrl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilesUrl.Common.Helper
{
    public class DbLogsHelper
    {

        ApplicationDbContext db;

        public DbLogsHelper()
        {
            db = new ApplicationDbContext();
        }

        public static DbLogsHelper Create()
        {
            return new DbLogsHelper();
        }

        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="subject">主题</param>
        /// <param name="content">内容</param>
        public void CreateLog(string subject, string content, string type)
        {
            LogsModel log = new LogsModel();
            log.datetime = DateTime.Now;
            log.subject = subject;
            log.content = content;
            log.type = type;
            db.Logs.Add(log);
            db.SaveChanges();
        }

    }
}