namespace WebChat.WebUI.WebApp
{
    #region Using

    using System;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Owin;
    using Microsoft.Owin.Security;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security.Facebook;
    using Services.Common;
    using Microsoft.Owin.Security.Twitter;
    using Data.Storage.Managers;
    using Data.Storage.Identity;
    using Data.Storage;
    using Services.Interfaces;
    using Services.Interfaces.Settings;
    using AppStart;
    #endregion

    public partial class Startup
    {
        private IAuthSettings config = DependencyContainer.Current.GetService<IApplicationSettings>().AuthSettings;
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<WebChatDbContext>(DependencyContainer.Current.GetService<WebChatDbContext>);
            app.CreatePerOwinContext<AppUserManager>(DependencyContainer.Current.GetService<AppUserManager>);
            app.CreatePerOwinContext<AppSignInManager>(AppSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<AppUserManager, UserModel, long>
                    (
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager),
                        getUserIdCallback: (user) => (user.GetUserId<long>())
                    )
                }
            });

            app.SetDefaultSignInAsAuthenticationType("External");

            var fb = new FacebookAuthenticationOptions()
            {
                AppId = config.FacebookSettings.AppId,
                AppSecret = config.FacebookSettings.AppId,
                Provider = new FacebookAuthenticationProvider
                {
                    OnAuthenticated = (context) =>
                    {
                        context.Identity.AddClaim(new System.Security.Claims.Claim(AppClaimTypes.FacebookAccessToken, context.AccessToken));
                        return Task.FromResult(true);
                    }
                }               
            };
            fb.Scope.Add("email");
            fb.Scope.Add("public_profile");
            fb.SignInAsAuthenticationType = AppBuilderSecurityExtensions.GetDefaultSignInAsAuthenticationType(app);
            app.UseFacebookAuthentication(fb);

            app.UseTwitterAuthentication(new TwitterAuthenticationOptions
            {
                ConsumerKey = config.TwitterSettings.AppId,
                ConsumerSecret = config.TwitterSettings.AppSecret,
                BackchannelCertificateValidator = this.GetTwitterCertificateValidator(),
                Provider = new TwitterAuthenticationProvider()
                {
                    OnAuthenticated = (context) =>
                    {
                        context.Identity.AddClaim(new System.Security.Claims.Claim(AppClaimTypes.TwitterAccessToken, context.AccessToken));
                        context.Identity.AddClaim(new System.Security.Claims.Claim(AppClaimTypes.TwitterAccessTokenSecret, context.AccessTokenSecret));
                        return Task.FromResult(true);
                    }
                }
            });
        }
        
      
        private CertificateSubjectKeyIdentifierValidator GetTwitterCertificateValidator()
        {
            return new CertificateSubjectKeyIdentifierValidator(
                new[]
                {
                            "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2
                            "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3
                            "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5
                            "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4
                            "4eb6d578499b1ccf5f581ead56be3d9b6744a5e5", // VeriSign Class 3 Primary CA - G5
                            "5168FF90AF0207753CCCD9656462A212B859723B", // DigiCert SHA2 High Assurance Server C‎A 
                            "B13EC36903F8BF4701D498261A0802EF63642BC3" // DigiCert High Assurance EV Root CA
                });
        }
    }   
}