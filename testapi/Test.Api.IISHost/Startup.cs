
namespace Test.Api.IISHost
{
    using System;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Http;

    using Microsoft.Owin;
    using Microsoft.Owin.Cors;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    using Test.Api.Authentication;
    using Test.Api.Services;

    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private const string TokenExpireTimeSpan = "TokenExpireTimeSpan";
        private const string TokenEndPointPath = "TokenEndPointPath";
        /// <summary>
        /// This method enables the application to setup the token end point and providers to generate the token.
        /// </summary>
        /// <param name="app">
        /// IAppBuilder builder.
        /// </param>
        public void Configuration(IAppBuilder app)
        {
            // TODO: This is a hack to get 401 response codes when authentication fails.  It should be fixed if
            // TODO: Owin/Katana ever come up with a better way of doing it.
            app.Use<InvalidAuthenticationMiddleware>();

           // IUserService _userService = new UserService();

                  GlobalConfiguration.Configure(x => x.MapHttpAttributeRoutes());
            WebApiConfiguration.Configure(GlobalConfiguration.Configuration);
            var config = GlobalConfiguration.Configuration;
            var gg = config.DependencyResolver.GetService(typeof(IUserService));

            app.UseCors(CorsOptions.AllowAll);
            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                ApplicationCanDisplayErrors = true,
                TokenEndpointPath = new PathString(ConfigurationManager.AppSettings[TokenEndPointPath]),
                AccessTokenExpireTimeSpan =
                    TimeSpan.FromDays(Convert.ToDouble(ConfigurationManager.AppSettings[TokenExpireTimeSpan])),
                Provider = new CustomAuthorizationProvider((IUserService)config.DependencyResolver.GetService(typeof(IUserService))),
                AuthenticationType = "Bearer",
                AccessTokenFormat = new TokenFormatter(),
                AllowInsecureHttp = true,
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}