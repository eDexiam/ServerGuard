using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Net;
using Newtonsoft.Json;
using System.Linq;

namespace ServerGuard
{
	class RoundEventHandler : IEventHandlerPlayerJoin
	{
		private readonly ServerGuard plugin;
        private string result;

		public RoundEventHandler(ServerGuard plugin) => this.plugin = plugin;
        public void OnPlayerJoin(PlayerJoinEvent ev)
        {
            plugin.Info("Checking data for " + ev.Player.Name);
            try
            {
                using (var client = new WebClient())
                {
                    result = client.DownloadString("http://151.80.185.9/?steamid=" + ev.Player.SteamId); // Gets the data from the server
                    plugin.Info("Printing data" + result);
                }
            } catch(WebException ex)
            {
                if(ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if(resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        plugin.Info("No data found for " + ev.Player.Name + " skipping...");
                        return;
                    } else
                    {
                        plugin.Error("Database error");
                    }
                    
                }
            }

            DataRead userdata = JsonConvert.DeserializeObject<DataRead>(result);
            if(userdata.isbanned)
            {
                ev.Player.Disconnect("You are banned by ServerGuard.");
                plugin.Info("Player is in list, ejecting...");
                return;
            }
            plugin.Info("Player is not banned");

            if (plugin.GetConfigList("sg_notifyroles").Length != 0)
            {
                foreach (Player player in plugin.Server.GetPlayers())
                {
                    if (plugin.GetConfigList("sg_notifyroles").Contains(player.GetRankName()))
                    {
                        player.PersonalBroadcast(5, "Warning troublemaker detected. Name: " + player.Name, false);
                    }
                }
            }

            if (plugin.GetConfigString("sg_webhookurl").Length > 0 )
            {
                using (WebClient webclient = new WebClient())
                {
                    if (!userdata.isbanned) return;
                    webclient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    WebhookGeneration jsondata = new WebhookGeneration();
                    jsondata.content = "Warning! A troublemaker has been detected! " + ev.Player.Name + " (" + ev.Player.SteamId + ")";
                    string json = JsonConvert.SerializeObject(jsondata);
                    webclient.UploadString(plugin.GetConfigString("sg_webhookurl"), "POST", json);
                    plugin.Info("Webhook sent");
                    // return;
                }
            }
        }
        class DataRead
        {
            // public string IPAdress;
            public string Reason;
            public bool isbanned;
        }
        class WebhookGeneration
        {
            public string content;
        }
    }
}
