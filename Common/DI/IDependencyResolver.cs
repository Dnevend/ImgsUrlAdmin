using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

//System已经定义此接口，不要实现
namespace FilesUrl.Other
{

    /// <summary>
    /// 定义一个IDependencyResolever用来解析依赖项目
    /// </summary>
    public interface IDependencyResolver : IDependencyScope, IDisposable
    {

        IDependencyScope BeginScope();

    }

    /// <summary>
    /// 当ASP.NET Web API创建一个controller实例的时候，它首先调用IDependencyResolver的GetService方法，传回一个Controller实例，你可以使用一个扩展的钩子去创建控制器并且解析依赖。
    /// 假如GetService方法返回NULL，ASP.NET Web API将查找一个无参的构造函数。
    /// </summary>
    public interface IDependencyScope
    {
        object GetService(Type serviceType);
        IEnumerable<object> GetServices(Type servicesType);
    }

}
