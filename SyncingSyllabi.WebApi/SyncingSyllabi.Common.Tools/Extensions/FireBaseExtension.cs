using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SyncingSyllabi.Common.Tools.Extensions
{
    public static class FireBaseExtension
    {
        public static void FireBaseAdmin(this IServiceCollection services)
        {
            // Set Default App for Firebase
            var defaultApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sdk\\key.json"))
            });

        }
    }
}
