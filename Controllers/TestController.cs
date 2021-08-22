using FilesUrl.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FilesUrl.Controllers
{
    /// <summary>
    /// 测试用
    /// </summary>
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {

        private IRedisService redisService;

        public TestController(IRedisService redisService)
        {
            this.redisService = redisService;
        }

        //只需要在对象成员前面加上[Dependency]，
        //就是把构造函数去掉，成员对象上面加[Dependency]注入
        //[Dependency]
        //public IRedisService _redisService { get; set; }

        /// <summary>
        /// 方法注入-加[InjectionMethod]属性
        /// </summary>
        /// <param name="IRedisService"></param>
        //[InjectionMethod]
        //public void SetInjection(IRedisService redisService)
        //{
        //    _redisService = redisService;
        //}

        [HttpGet]
        [AllowAnonymous]
        [Route("GetDITestValue")]
        public IHttpActionResult GetDITestValue()
        {

            return Ok(redisService.getString());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GenerateCode")]
        public IHttpActionResult GenerateCode()
        {

            redisService.generateCode("random", Guid.NewGuid().ToString());

            return Ok("ABCDEFG");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetCode")]
        public IHttpActionResult GetCode(String key)
        {

            string code = redisService.getCode(key);

            return Ok(code);
        }

    }
}
