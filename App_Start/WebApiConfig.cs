using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using FilesUrl.Common.DI;
using FilesUrl.Common.Redis;
using FilesUrl.Services;
using FilesUrl.Services.IServices;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Unity;

namespace FilesUrl
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // 将 Web API 配置为仅使用不记名令牌身份验证。
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API 路由
            config.MapHttpAttributeRoutes();

            //允许跨域
            // 直接在WebConfig中配置，此处重复会导致前端返回错误
            //config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            //依赖注入容器
            var container = new UnityContainer();
            container.RegisterType<IRedisCacheManager, RedisCacheManager>();//注入Redis管理类
            container.RegisterType<IRedisService, RedisService>();//注入Redis服务类
            config.DependencyResolver = new UnityResolver(container);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
