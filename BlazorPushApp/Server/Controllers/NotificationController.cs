using BlazorPushApp.Shared;
using BlazorPushApp.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebPush;

namespace BlazorPWA.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private static List<NotificationSubscription> _subscriptions = new();

        [HttpPost]
        [Route("subscribe")]
        public int Post(NotificationSubscription notificationSubscription)
        {
            _subscriptions.Add(notificationSubscription);
            return _subscriptions.Count();
        }

        [HttpGet]
        [Route("sendall")]
        public async Task<int> Get()
        {
            //Replace with your generated public/private key
            var publicKey = "BPodXwNKsIZ5HyRyFm6Xx4WmSeoq8zZJnjakgJUgaXbz1lvKnTfYBS5rqBpcFUnlm-1tTSE-122i5qjb4Br6Z7o";
            var privateKey = "aXH4Dc9rB_1fF0iLKRNXdQUIOsALMkrlZgjBQg8tO2U";

            //give a website URL or mailto:your-mail-id
            var vapidDetails = new VapidDetails("mailto:silam@hotmail.com", publicKey, privateKey);
            var webPushClient = new WebPushClient();

            foreach (var subscription in _subscriptions)
            {
                var pushSubscription = new PushSubscription(subscription.Url, subscription.P256dh, subscription.Auth);

                try
                {
                    var payload = JsonSerializer.Serialize(new
                    {
                        message = "this text is from server",
                        url = "open this URL when user clicks on notification"
                    });
                    await webPushClient.SendNotificationAsync(pushSubscription, payload, vapidDetails);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Error sending push notification: " + ex.Message);
                    //return -1;
                }
            }

            return _subscriptions.Count();
        }
    }
}