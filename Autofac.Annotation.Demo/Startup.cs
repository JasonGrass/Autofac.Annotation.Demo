using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Autofac.Annotation.Demo
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
            // 替换默认的 Controller 容器为通用的 IOC 容器。（默认 Controller 是独立管理的。）
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacAnnotationModule(Assembly.GetExecutingAssembly()));
            AutowiredProperties(builder);
        }

        private void AutowiredProperties(ContainerBuilder builder)
        {
            /*
             * 为以下 ControllerBase 进行属性的自动注入
             *
             * P.S 需要被自动注入的属性，必须标记 Autowired Attribute.
             */

            bool Predicate(Type t) => typeof(ControllerBase).IsAssignableFrom(t) && t != typeof(ControllerBase);

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(Predicate)
                .PropertiesAutowired(new AutowiredPropertySelector())
                ;

        }
    }


}
