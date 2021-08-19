using FilesUrl.Common;
using FilesUrl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FilesUrl.Controllers
{

    [Authorize]
    [RoutePrefix("api/setting")]
    public class SettingController : ApiController
    {

        ApplicationDbContext db;

        public SettingController()
        {
            db = new ApplicationDbContext();
        }

        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAllSettingParams")]
        public IHttpActionResult GetAllSettingParams()
        {

            var settings = db.SettingParams.ToList().ToArray();

            return Ok(MessageModel<Array>.Success("settings", settings));

        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetParam")]
        public IHttpActionResult SetParam(SetValueModel model)
        {

            var setting = db.SettingParams.Where(s => s.Param.Equals(model.Param)).FirstOrDefault();

            if (setting != null)
            {
                setting.Value = model.Value;
                db.SaveChanges();
            }
           
            return Ok(MessageModel<string>.Success("OK"));
        }

    }
}
