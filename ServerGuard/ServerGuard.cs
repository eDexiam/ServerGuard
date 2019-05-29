using Smod2;
using Smod2.API;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.Lang;
using Smod2.Piping;
using System.Collections.Generic;

namespace ServerGuard
{
	[PluginDetails( // Don't touch that, IM WATCHING YOU
		author = "lucasboss45",
		name = "ServerGuard",
		description = "A banning system for troublemakers",
		id = "lucas.serverguard.plugin",

		version = "1.2",
		SmodMajor = 3,
		SmodMinor = 4,
		SmodRevision = 0
		)]
	public class ServerGuard : Plugin
	{
		public override void OnDisable()
		{
			this.Info("ServerGuard activated");
		}

		public override void OnEnable()
		{
			this.Info("ServerGuard deactivated");
		}

        public override void Register()
        {
            this.AddEventHandlers(new RoundEventHandler(this));
            this.AddConfig(new ConfigSetting("sg_webhookurl", "", true, "The webhook URL for Discord logging (The auto kick system must be disabled for this to work)"));
            this.AddConfig(new ConfigSetting("sg_enableautokick", true, true, "Disallow access to users on the troublemakers database"));
            this.AddConfig(new ConfigSetting("sg_notifyroles", new string[] { }, true, "Put a list of staff roles to notify in-game when a troublemaker has joined (The auto kick system must be disabled for this to work)"));
            string lang = "ServerGuard";
            this.AddTranslation(new LangSetting("kickmessage", "You are banned by ServerGuard.", lang));
            this.AddTranslation(new LangSetting("ingamemsg:", "Warning troublemaker detected. Name:", lang));
            this.AddTranslation(new LangSetting("webhookmsg:", "Warning! A troublemaker has been detected!", lang));
        }
	}
}
