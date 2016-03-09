using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using WebChat.WebUI.Models;
using WebChat.DataAccess.Concrete.DataBase;
using WebChat.DataAccess.Concrete.Entities.Identity;
using Microsoft.Owin.Security;
using WebChat.DataAccess.Abstract;
using WebChat.DataAccess.Concrete;
using WebChat.DataAccess.Managers;
using Microsoft.Owin.Security.Facebook;
using WebChat.BusinessLogic;
using WebChat.BusinessLogic.Managers;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Twitter;

namespace WebChat.WebUI
{
    public partial class Startup
    {
        // Дополнительные сведения о настройке проверки подлинности см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Настройка контекста базы данных, диспетчера пользователей и диспетчера входа для использования одного экземпляра на запрос
            app.CreatePerOwinContext(WebChatDbContext.GetInstance);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppSignInManager>(AppSignInManager.Create);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);
            app.CreatePerOwinContext<IDataService>(EfUnitOfWork.GetInstance);

            app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ApplicationCookie);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Позволяет приложению проверять метку безопасности при входе пользователя.
                    // Эта функция безопасности используется, когда вы меняете пароль или добавляете внешнее имя входа в свою учетную запись.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<AppUserManager, AppUser, long>
                    (
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager),
                        getUserIdCallback: (user) => (user.GetUserId<long>())
                    )
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

           var x = new FacebookAuthenticationOptions();
            x.Scope.Add("email");
            x.AppId = "1509836725980556";
            x.AppSecret = "7f18acb2f48a5e5e6b2e17aa7b179076";
            x.Provider = new FacebookAuthenticationProvider()
            {
                OnAuthenticated = (context) =>
                {
                    context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));
                    return Task.FromResult(true);
                }
            };
            x.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;
            app.UseFacebookAuthentication(x);


            app.UseTwitterAuthentication(new TwitterAuthenticationOptions
            {
                ConsumerKey = "owRihJ8kuYIMDAa6XaK88O7Vj",
                ConsumerSecret = "YTloVLtW2CDBhrFrzNbrZOJkaB2mxPBqZ36IWtxu3BNtN7td1u",
                BackchannelCertificateValidator = new CertificateSubjectKeyIdentifierValidator(
                new[]
                {
                        "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2
                        "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3
                        "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5
                        "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4
                        "4eb6d578499b1ccf5f581ead56be3d9b6744a5e5", // VeriSign Class 3 Primary CA - G5
                        "5168FF90AF0207753CCCD9656462A212B859723B", // DigiCert SHA2 High Assurance Server C‎A 
                        "B13EC36903F8BF4701D498261A0802EF63642BC3" // DigiCert High Assurance EV Root CA
                }),
                Provider = new TwitterAuthenticationProvider()
                {
                    OnAuthenticated = (context) =>
                    {
                        context.Identity.AddClaim(new System.Security.Claims.Claim("TwitterAccessToken", context.AccessToken));
                        context.Identity.AddClaim(new System.Security.Claims.Claim("TwitterAccessTokenSecret", context.AccessTokenSecret));
                        return Task.FromResult(true);
                    }
                }
            });

            //app.UseGoogleAuthentication(
            //    clientId: "282134184660-4f551vurl3s9goa52b7712se7ag6m2qe.apps.googleusercontent.com",
            //    clientSecret: "9JXf7o7EcxKicS5geVyaVsX8"
            //);

        }
    }
}