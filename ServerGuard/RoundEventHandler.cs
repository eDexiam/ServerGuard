﻿using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Net;
using Newtonsoft.Json;

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
                    result = client.DownloadString("http://151.80.185.9/" + ev.Player.SteamId + ".json"); // Gets the data from the server
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
        }
        class DataRead
        {
            // public string IPAdress;
            public string Reason;
            public bool isbanned;
        }
    }
}