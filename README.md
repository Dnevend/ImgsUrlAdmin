# ImgsUrlAdmin
 图床网站管理后台.Net WebAPI程序
 <br/>
项目演示地址:<a href="http://81.68.146.67:8008/" target="_blank">点击访问</a>
<br/>
前端仓库地址:<a href="https://github.com/Dnevend/ImgsUrlWeb" target="_blank">Github_ImgsUrlWeb</a>

 数据库可根据Models创建迁移生成
 Add-Migration "InitDB"
 或直接使用已有迁移文件更新数据库
 Update-Database

2021.08.22项目升级
<br/>
1.应用UnityContainer实现依赖注，未来新增实现都会使用依赖注入
<br/>
2.应用Redis缓存实现验证码缓存
<br/>
3.增加登录验证码环节，防止黑客暴力破解
<br/>

其他：由于前后端分离架构，前端无法建立在同一个Session，或由于跨域导致Set-Cookie无效，致使无法获取图片验证码的Token以至于影响验证码校验。
最后采用Content-Disposition的方式来传递Token。另外要注意增加显示Desc的响应头，详细使用已在代码中进行注解。
并且Chorme80+对跨域的请求更加严格。无法对SameSite by default cookies进行配置，此处跨域不是普普通通坑。丢
