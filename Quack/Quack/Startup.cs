using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Quack.Infrastructure;
using System.Web.Http;

[assembly: OwinStartup(typeof(Quack.Startup))]
namespace Quack
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            googleAuthOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "374089372061-g28ovvr2b4pps4gurf9aluohu3727c0b.apps.googleusercontent.com",
                ClientSecret = "qCxXtsiixm6UCfjKnunKstjV",
                Provider = new GoogleAuthProvider()
            };
            app.UseGoogleAuthentication(googleAuthOptions);

            //facebookAuthOptions = new FacebookAuthenticationOptions()
            //{
            //    AppId = "xxx",
            //    AppSecret = "xxx",
            //    Provider = new FacebookAuthProvider()
            //};
            //app.UseFacebookAuthentication(facebookAuthOptions);
        }

    }
}