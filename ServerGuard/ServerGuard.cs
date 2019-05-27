using Smod2;
using Smod2.API;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.Lang;
using Smod2.Piping;

namespace ServerGuard
{
	[PluginDetails( // Don't touch that, IM WATCHING YOU
		author = "lucasboss45",
		name = "ServerGuard",
		description = "A banning system for troublemakers",
		id = "lucas.serverguard.plugin",
		version = "1.1",
		SmodMajor = 3,
		SmodMinor = 4,
		SmodRevision = 0
		)]
	public class ServerGuard : Plugin
	{
		public override void OnDisable()
		{
			this.Info("GlobalBans activated");
		}

		public override void OnEnable()
		{
			this.Info("GlobalBans deactivated");
		}
		
		public override void Register()
		{
			this.AddEventHandlers(new RoundEventHandler(this));
            this.AddConfig(new ConfigSetting("sg_webhookurl", "", true, "The webhook URL for Discord logging"));
		}
	}
}
