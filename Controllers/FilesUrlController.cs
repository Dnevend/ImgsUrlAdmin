using FilesUrl.Common;
using FilesUrl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FilesUrl.Controllers
{

    [Authorize]
    [RoutePrefix("api/FilesUrl")]
    public class FilesUrlController : ApiController
    {

        ApplicationDbContext db;

        public FilesUrlController()
        {
            db = new ApplicationDbContext();
        }

        /// <summary>
        /// 获取文件地址列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAllFilesUrlList")]
        public IHttpActionResult GetFilesUrlList(FilesUrlFilterModel model)
        {

            if (!ModelState.IsValid)
            {
                return Ok(MessageModel<string>.Fail("请求参数错误！"));
            }

            Expression<Func<Models.FilesUrl,bool>> func = x => !x.Deleted;

            var result = db.FilesUrls.Where(f => !f.Deleted);

            if (model.startDate != null)
            {
                //TODO
                result = result.Where(r => r.UpTime >= model.startDate);
            }

            if (model.stopDate != null)
            {
                //TODO
                result = result.Where(r => r.UpTime <= model.stopDate);
            }

            var total = result.Count();
            var array = result
                .OrderByDescending(r => r.UpTime)
                .Skip((model.page - 1) * model.pageSize)
                            .Take(model.pageSize)
                            .ToArray();

            //var total = db.FilesUrls.Where(f => !f.Deleted).Count();
            //var array = db.FilesUrls
            //                .Where(f => !f.Deleted)
            //                .OrderByDescending(f => f.UpTime)
            //                .ToList()
            //                .Skip((model.page - 1) * model.pageSize)
            //                .Take(model.pageSize)
            //                .ToArray();

            return Ok(MessageModel<FilesUrlListModel>.Success("ImgUrlList", new FilesUrlListModel(total, array)));
        }

        /// <summary>
        /// 根据id删除记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("RemoveUrlById")]
        public IHttpActionResult RemoveUrlById(int id)
        {

            var url = db.FilesUrls.FirstOrDefault(f => f.id == id);
            if (url != null)
            {
                url.Deleted = true;
                db.SaveChanges();
            }

            return Ok(MessageModel<int>.Success("Deleted", id));
        }

    }
}
