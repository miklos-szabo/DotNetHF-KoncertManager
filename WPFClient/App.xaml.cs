using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Identity.Client;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly string Tenant = "ee96xy.onmicrosoft.com";
        private static readonly string AzureAdB2CHostname = "ee96xy.b2clogin.com";
        //private static readonly string ClientId = "81e71386-8817-4b58-b790-4cb6abb449a8";
        private static readonly string ClientId = "c6fc50e7-b639-4676-8a5f-bc7ef1dbbc6e";
        public static string PolicySignUpSignIn = "B2C_1_dotNetLabSingupSignin1";
        public static string PolicyEditProfile = "B2C_1_dotNetLab_Profileediting1";
        public static string PolicyResetPassword = "B2C_1_DotNetLab_PasswordReset1";

        public static string[] ApiScopes = { "https://ee96xy.onmicrosoft.com/dotNetLab/demo.read" };
        public static string ApiEndpoint = "https://localhost:5001/weatherforecast";
        private static string AuthorityBase = $"https://{AzureAdB2CHostname}/tfp/{Tenant}/";
        public static string AuthoritySignUpSignIn = $"{AuthorityBase}{PolicySignUpSignIn}";
        public static string AuthorityEditProfile = $"{AuthorityBase}{PolicyEditProfile}";
        public static string AuthorityResetPassword = $"{AuthorityBase}{PolicyResetPassword}";

        private static readonly string RedirectUri = "https://ee96xy.b2clogin.com/oauth2/nativeclient";
        public static IPublicClientApplication PublicClientApp { get; private set; }

        static App()
        {
            PublicClientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithB2CAuthority(AuthoritySignUpSignIn)
                .WithRedirectUri(RedirectUri)
                .WithLogging(Log, LogLevel.Verbose, false) //PiiEnabled set to false
                .Build();

            TokenCacheHelper.Bind(PublicClientApp.UserTokenCache);
        }
        private static void Log(LogLevel level, string message, bool containsPii)
        {
            string logs = ($"{level} {message}");
            StringBuilder sb = new StringBuilder();
            sb.Append(logs);
            File.AppendAllText(System.Reflection.Assembly.GetExecutingAssembly().Location + ".msalLogs.txt", sb.ToString());
            sb.Clear();
        }
    }
}
