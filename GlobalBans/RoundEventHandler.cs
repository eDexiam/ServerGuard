using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Net;
using Newtonsoft.Json;

namespace GlobalBans
{
	class RoundEventHandler : IEventHandlerPlayerJoin
	{
		private readonly GlobalBans plugin;

		public RoundEventHandler(GlobalBans plugin) => this.plugin = plugin;

        public void OnPlayerJoin(PlayerJoinEvent ev)
        {
            string result;
            using (var client = new WebClient())
            {
                result = client.DownloadString("http://151.80.185.9/" + ev.Player.SteamId + ".json"); // Gets the data from the server
            }
            if(result == null)
            {
                plugin.Info("No data found for " + ev.Player.Name + " skipping...");
            }
            DataRead userdata = JsonConvert.DeserializeObject<DataRead>(result);
            if(userdata.isbanned)
            {
                ev.Player.Disconnect("You have been global banned (Global Banning Plugin Only)");
            }
        }
        class DataRead
        {
            // public string IPAdress;
            public string Reason;
            public bool isbanned;
        }
    }
}
