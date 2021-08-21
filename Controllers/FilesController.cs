using FilesUrl.Common;
using FilesUrl.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;

namespace FilesUrl.Controllers
{
    [Authorize]
    [RoutePrefix("api/Files")]
    public class FilesController : ApiController
    {

        ApplicationDbContext db;

        public FilesController()
        {
            db = new ApplicationDbContext();
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("UploadImg")]
        public async Task<IHttpActionResult> UploadImgAsync()
        {

            //检查是否是 multipart/form-data 
            if (!Request.Content.IsMimeMultipartContent())
            {
                //throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                return Ok(MessageModel<string>.Fail("系统繁忙！"));
            }

            //参数
            var settingParams = db.SettingParams.ToList();

            var isAuth = Request.Headers.Authorization;
            var upAddress = ((HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            if (null == isAuth || String.IsNullOrEmpty(isAuth.ToString()) || isAuth.ToString().Trim().Equals("Bearer"))
            {
                //请求用户IP地址
                var preDayUpedCount = db.FilesUrls.Where(s => s.UpTime >= DateTime.Today && s.Uploader == upAddress).Count();
                var preDayUpCount = int.Parse(settingParams.Where(s => s.Param == "preCountLimit").Select(s => s.Value).First());
                if (preDayUpedCount >= preDayUpCount)
                {
                    return Ok(MessageModel<string>.Fail($"超过单日上传数量 {preDayUpCount} 张限制！"));
                }

                var dayUpedCount = db.FilesUrls.Where(s => s.UpTime >= DateTime.Today).Count();
                var dayUpCount = int.Parse(settingParams.Where(s => s.Param == "countLimit").Select(s => s.Value).First());
                if (dayUpedCount >= dayUpCount)
                {
                    return Ok(MessageModel<string>.Fail($"超过单日上传总量 {dayUpCount} 限制！"));
                }
            }

            string root = System.Web.HttpContext.Current.Server.MapPath("~/imgsUrl");// 绝对路径
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    string filename = file.Headers.ContentDisposition.FileName.Trim('"');
                    //获取对应文件后缀名
                    string fileExt = filename.Substring(filename.LastIndexOf('.'));

                    //类型限制
                    string imgType = settingParams.Where(s => s.Param == "imgType").Select(s => s.Value.ToUpper()).First();
                    string[] types = imgType.Split(',');
                    if (!types.Contains(fileExt.ToUpper()))
                    {
                        return Ok(MessageModel<string>.Fail("不支持的文件类型！"));
                    }

                    var obj = file.Headers;

                    FileInfo fileinfo = new FileInfo(file.LocalFileName);

                    if(fileinfo.Length > double.Parse(settingParams.Where(s => s.Param == "fileSize").Select(s => s.Value).First()) * 1024 * 1024)
                    {
                        return Ok(MessageModel<string>.Fail("文件大小超出限制！"));
                    }

                    string fileNewname = Guid.NewGuid().ToString().Replace("-", "") + fileExt;

                    string rootPath = "~/imgsUrl";

                    string filePath = System.Web.HttpContext.Current.Server.MapPath(rootPath + "/");// 绝对路径
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    //保存图片
                    fileinfo.MoveTo(filePath + fileNewname);

                    #region 缩略图
                    //System.Drawing.Image image = System.Drawing.Image.FromStream(fileinfo.OpenWrite());
                    System.Drawing.Image image = System.Drawing.Image.FromFile(fileinfo.FullName);

                    Bitmap bitmap = new Bitmap(image);
                    int width = image.Width;
                    int height = image.Height;

                    var bitmapWidth = width;
                    var bitmapHeight = height;
                    
                    if (height > width && height > 300)
                    {
                        bitmapHeight = 300;
                        bitmapWidth = 300 * width / height;
                    }
                    if (width > height && width > 300)
                    {
                        bitmapWidth = 300;
                        bitmapHeight = 300 * height / width;
                    }

                    //Bitmap smallBitmap = new Bitmap(bitmapWidth, bitmapHeight);
                    Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(() => false);
                    Image targetImg = bitmap.GetThumbnailImage(bitmapWidth, bitmapHeight, callb, IntPtr.Zero);
                    string smallPath = filePath + "small/";
                    if (!Directory.Exists(smallPath))
                    {
                        Directory.CreateDirectory(smallPath);
                    }
                    //smallBitmap.Save(smallPath + fileNewname);
                    targetImg.Save(smallPath + fileNewname);
                    #endregion

                    string imgPath = System.Web.Configuration.WebConfigurationManager.AppSettings["ImgPath"];// 返回路径
                    string imgUrl = imgPath + fileNewname;

                    FilesUrl.Models.FilesUrl filesUrl = new FilesUrl.Models.FilesUrl();
                    filesUrl.Type = fileExt;
                    filesUrl.Size = fileinfo.Length;
                    filesUrl.Url = fileNewname;
                    filesUrl.UpTime = DateTime.Now;
                    filesUrl.Uploader = upAddress;
                    db.FilesUrls.Add(filesUrl);
                    await db.SaveChangesAsync();

                    return Ok(MessageModel<string>.Success("Image", imgUrl));
                }

            }
            catch (Exception ex)
            {
                return Ok(MessageModel<string>.Fail(ex.Message));
            }

            return Ok();
        }

    }
}
