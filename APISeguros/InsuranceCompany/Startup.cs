using Autofac;
using Autofac.Integration.WebApi;
using InsuranceCompany.AuthorizationProviders;
using InsuranceCompany.Entities;
using InsuranceCompany.Services.Implementations;
using InsuranceCompany.Services.Interfaces;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Configuration;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(InsuranceCompany.Startup))]

namespace InsuranceCompany
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            MappingConfigs.SetupMappings();
            SetupAuthorization(app);
            SetupDependencies(app);
        }

        public static class MappingConfigs
        {
            public static void SetupMappings()
            {
                AutoMapper.Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Entities.Client, Models.DTO.ClientDto>();
                    cfg.CreateMap<Entities.Policy, Models.DTO.PolicyDto>();
                    cfg.CreateMap<Models.DTO.ClientDto, Entities.Client>();
                    cfg.CreateMap<Models.DTO.PolicyDto, Entities.Policy>();
                });
            }
        }

        private static void SetupAuthorization(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            var authProvider = new AuthorizationProvider();
            var options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(5),
                Provider = authProvider
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private static void SetupDependencies(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            var config = new HttpConfiguration();
            var connectionString = ConfigurationManager.AppSettings["DbConnectionString"];

            builder.RegisterType<InsuranceCompanyContext>().WithParameter(new NamedParameter("connString", connectionString));
            builder.RegisterType<ClientRepository>().As<IClientRepository>();
            builder.RegisterType<PolicyRepository>().As<IPolicyRepository>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Register the Autofac middleware FIRST, then the Autofac Web API middleware,
            // and finally the standard Web API middleware.
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);

            SwaggerConfig.Register(config);

            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }

    }
}
