using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace ProjectDemo.WebAPICore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ////注册Swagger生成器，定义一个和多个Swagger 文档
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            //});

            //注册Swagger生成器，定义一个和多个Swagger 文档
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info
            //    {
            //        Version = "v1",
            //        Title = "yilezhu's API",
            //        Description = "A simple example ASP.NET Core Web API",
            //        TermsOfService = "None",
            //        Contact = new Contact
            //        {
            //            Name = "依乐祝",
            //            Email = string.Empty,
            //            Url = "http://www.baidu.com"
            //        },
            //        License = new License
            //        {
            //            Name = "许可证名字",
            //            Url = "http://www.baidu.com"
            //        }
            //    });
            //    // 为 Swagger JSON and UI设置xml文档注释路径
            //    var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
            //    var xmlPath = Path.Combine(basePath, "WebApiCoreDemo.xml");
            //    c.IncludeXmlComments(xmlPath);
            //});


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "WebApiCoreDemo Api",
                    TermsOfService = "None"
                });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "ProjectDemo.WebAPICore.xml");
                //var xmlPathBll = Path.Combine(basePath, "WebApiCoreDemo.BLL.xml");
                //var xmlPathEntity = Path.Combine(basePath, "WebApiCoreDemo.Entity.xml");
                c.IncludeXmlComments(xmlPath);
                //c.IncludeXmlComments(xmlPathBll);
                //c.IncludeXmlComments(xmlPathEntity);
            }
            );
        }

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="app"></param>
        ///// <param name="env"></param>
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }
        //    else
        //    {
        //        app.UseHsts();
        //    }
        //    //启用中间件服务生成Swagger作为JSON终结点
        //    app.UseSwagger();
        //    //启用中间件服务对swagger-ui，指定Swagger JSON终结点
        //    app.UseSwaggerUI(c =>
        //    {
        //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        //    });

        //    app.UseHttpsRedirection();
        //    app.UseMvc();
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoApi");
            });
            app.UseMvc();
            //重写路由
            app.Map("/api/WebSocketHandler", SocketHandler.Map);
        }
    }
}
