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
            // �滻Ĭ�ϵ� Controller ����Ϊͨ�õ� IOC ��������Ĭ�� Controller �Ƕ�������ġ���
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
             * Ϊ���� ControllerBase �������Ե��Զ�ע��
             *
             * P.S ��Ҫ���Զ�ע������ԣ������� Autowired Attribute.
             */

            bool Predicate(Type t) => typeof(ControllerBase).IsAssignableFrom(t) && t != typeof(ControllerBase);

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(Predicate)
                .PropertiesAutowired(new AutowiredPropertySelector())
                ;

        }
    }


}
