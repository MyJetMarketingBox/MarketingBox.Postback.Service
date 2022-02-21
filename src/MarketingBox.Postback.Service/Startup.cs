using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Autofac;
using MyJetWallet.Sdk.GrpcSchema;
using MyJetWallet.Sdk.Service;
using Prometheus;
using MarketingBox.Postback.Service.Grpc;
using MarketingBox.Postback.Service.Modules;
using MarketingBox.Postback.Service.Services;
using SimpleTrading.ServiceStatusReporterConnector;
using MarketingBox.Postback.Service.Postgres;
using MyJetWallet.Sdk.Postgres;

namespace MarketingBox.Postback.Service
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.BindCodeFirstGrpc();

            services.AddHostedService<ApplicationLifetimeManager>();

            MyDbContext.LoggerFactory = Program.LogFactory;
            services.AddDatabase(DatabaseContext.Schema,
                Program.Settings.PostgresConnectionString,
                o => new DatabaseContext(o));
            
            services.AddMyTelemetry("SP-", Program.Settings.ZipkinUrl);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMetricServer();

            app.BindServicesTree(Assembly.GetExecutingAssembly());

            app.BindIsAlive();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcSchema<PostbackService, IPostbackService>();
                endpoints.MapGrpcSchema<EventReferenceLogService, IEventReferenceLogService>();

                endpoints.MapGrpcSchemaRegistry();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ClientModule>();
            builder.RegisterModule<SettingsModule>();
            builder.RegisterModule<ServiceModule>();
        }
    }
}
