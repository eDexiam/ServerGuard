using Smod2;
using Smod2.API;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.Lang;
using Smod2.Piping;

namespace GlobalBans
{
	[PluginDetails( // Don't touch that, IM WATCHING YOU
		author = "lucasboss45",
		name = "GlobalBans",
		description = "A global banning system for troublemakers",
		id = "lucas.globalban.plugin",
		configPrefix = "gb",
		version = "1.0",
		SmodMajor = 3,
		SmodMinor = 4,
		SmodRevision = 0
		)]
	public class GlobalBans : Plugin
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
		}
	}
}
