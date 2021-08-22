using FilesUrl.Common.Helper;
using FilesUrl.Services.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace FilesUrl.Controllers
{
    [RoutePrefix("api/VerifyCode")]
    public class VerifyCodeController : ApiController
    {

        private IRedisService _redisService;

        public VerifyCodeController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GenerateCode")]
        public HttpResponseMessage GenerateCode()
        {

            //生成验证码
            string code = "";
            System.IO.MemoryStream ms = VerifyCodeHelper.Create(out code);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                //StreamContent 前端显示错误
                //Content = new StreamContent(ms)
                Content = new ByteArrayContent(ms.ToArray())
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(@"image/jpeg");

            //生成Token将验证码存入Redis
            string token = "Verify-" + Guid.NewGuid().ToString();
            _redisService.generateCode(token, code);

            //感动中国，他妈的
            //需配置响应头
            //奇淫技巧
            //<add name="Access-Control-Expose-Headers" value="Content-Disposition" />
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue(token); ;

            #region 返回Cookie
            //坑的一批
            //将Token以Cookie的形式返回给前端
            //CookieHeaderValue chv = new CookieHeaderValue("codeToken", token);
            //chv.Expires = DateTimeOffset.Now.AddMinutes(5);
            //chv.Domain = Request.RequestUri.Host;
            ////httpOnly是服务器可访问 cookie, 默认是 true。
            ////禁止javascript操作cookie（为避免跨域脚本(xss)攻击，通过javascript的document.cookie无法访问带有HttpOnly标记的cookie。
            ////chv.HttpOnly = true;
            //chv.HttpOnly = false;
            //chv.Path = "/";
            //response.Headers.AddCookies(new CookieHeaderValue[] { chv });

            //跨域，坑的一匹，若要前端能保存并请求Cookie，前端需配置withCredentials:true,并且需指定响应头
            //如果加了这个就会产生重复响应头
            //response.Content.Headers.Add("Access-Control-Allow-Origin", "http://localhost:8080");
            #endregion

            return response;
        }

    }
}
