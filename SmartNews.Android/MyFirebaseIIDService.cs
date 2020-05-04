using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Iid;

namespace SmartNews.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    [Obsolete]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";
        public static string token;
        [Obsolete]
        public override void OnTokenRefresh()
        {
            token = FirebaseInstanceId.Instance.Token;
            SendRegistrationToServer(token);
        }
        void SendRegistrationToServer(string token)
        {

            // send this token to server
        }
    }
}