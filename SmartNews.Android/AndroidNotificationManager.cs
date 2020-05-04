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
using SmartNews.Models;
using Android.Support.V4.App;
using Xamarin.Forms;
using Square.OkHttp;
using AndroidApp = Android.App.Application;
using Android.Graphics;
using Android.Media;
using Android.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

[assembly: Dependency(typeof(SmartNews.Droid.AndroidNotificationManager))]
namespace SmartNews.Droid
{
    public class AndroidNotificationManager : INotificationManager
    {

        private Context mContext;
        private NotificationManager mNotificationManager;
        private NotificationCompat.Builder mBuilder;
        public static String NOTIFICATION_CHANNEL_ID = "10023";
        public const string API_KEY = "AAAAnhj66S4:APA91bES1qZmlzWnPzu7ivLI9d0hLlxyw-IYYjfo25rDbGZYLWBR-19n_9ShXJ9uz9QMECUPsLO9P368ALx_4J5MpzfmnTMZnSvzHX5Siwyk-pl55ad05K50tuc9I7tVwsL09cUr9FXu";
        public const string TITLE = "Hello, Xamarin!";
        public const string MESSAGE = "Hello, Xamarin Notification!";

        public AndroidNotificationManager()
        {
            mContext = global::Android.App.Application.Context;
        }

        public void ReceiveNotification(String title, String message)
        {
            try
            {
                var intent = new Intent(mContext, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                intent.PutExtra(title, message);
                var pendingIntent = PendingIntent.GetActivity(mContext, 0, intent, PendingIntentFlags.OneShot);

                mBuilder = new NotificationCompat.Builder(mContext);
                mBuilder.SetSmallIcon(Resource.Drawable.xamarin_logo);
                mBuilder.SetContentTitle(title)
                        .SetAutoCancel(true)
                        .SetContentTitle(title)
                        .SetContentText(message)
                        .SetChannelId(NOTIFICATION_CHANNEL_ID)
                        .SetPriority((int)NotificationPriority.High)
                        .SetVibrate(new long[0])
                        .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate)
                        .SetVisibility((int)NotificationVisibility.Public)
                        .SetSmallIcon(Resource.Drawable.xamarin_logo)
                        .SetContentIntent(pendingIntent);

                NotificationManager notificationManager = mContext.GetSystemService(Context.NotificationService) as NotificationManager;

                if (global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.O)
                {
                    NotificationImportance importance = global::Android.App.NotificationImportance.High;

                    NotificationChannel notificationChannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, title, importance);
                    notificationChannel.EnableLights(true);
                    notificationChannel.EnableVibration(true);
                    notificationChannel.SetShowBadge(true);
                    notificationChannel.Importance = NotificationImportance.High;
                    notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });

                    if (notificationManager != null)
                    {
                        mBuilder.SetChannelId(NOTIFICATION_CHANNEL_ID);
                        notificationManager.CreateNotificationChannel(notificationChannel);
                    }
                }

                notificationManager.Notify(0, mBuilder.Build());
            }
            catch (Exception ex)
            {
                //
            }
        }

        public void PushNotification(string mess)
        {
            // Mes mes = new Mes(MyFirebaseIIDService.token, new Noti("great", "yes"));
            // string json = JsonConvert.SerializeObject(mes);
            // Log.Error("json", json);
            // OkHttpClient client = new OkHttpClient();
            // RequestBody body = RequestBody.Create(
            // MediaType.Parse("application/json; charset=utf-8"), json);
            // Request request = new Request.Builder()
            //.Url("https://fcm.googleapis.com/fcm/send")
            //.Post(body)
            //.AddHeader("Authorization", "key=AAAAnhj66S4:APA91bES1qZmlzWnPzu7ivLI9d0hLlxyw-IYYjfo25rDbGZYLWBR-19n_9ShXJ9uz9QMECUPsLO9P368ALx_4J5MpzfmnTMZnSvzHX5Siwyk-pl55ad05K50tuc9I7tVwsL09cUr9FXu")
            //.Build();
            var jGcmData = new JObject();
            var jData = new JObject();
            var jNotification = new JObject();
            jData.Add("title", TITLE);
            jData.Add("body", mess);
            jNotification.Add("title", TITLE);
            jNotification.Add("body", mess);
            jGcmData.Add("to", "c9eBpZD6TpA:APA91bE__KSWoh-TtGaMjbsNQh6FeLTcjCK2-D-AoUUvNyJGSRpnb_BEIZvaPKvQAVW2mypAXkv4N32VUU6X4J6b3NOdHofOv9pEZj1W3RaIpt1EVJmyWSaGYgtL1OTe7cE2iUFsL48V");
            jGcmData.Add("notification", jData);
            jGcmData.Add("data", jData);

            var url = new Uri("https://fcm.googleapis.com/fcm/send");
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                        "Authorization", "key=" + API_KEY);

                    Task.WaitAll(client.PostAsync(url,
                        new StringContent(jGcmData.ToString(), System.Text.Encoding.Default, "application/json"))
                            .ContinueWith(response =>
                            {
                                Console.WriteLine(response);
                                Console.WriteLine("Message sent: check the client device notification tray.");
                            }));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to send FCM message:");
                Console.Error.WriteLine(e.StackTrace);
            }
        }
    }
}