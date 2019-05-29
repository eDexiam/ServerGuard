# ServerGuard

ServerGuard is a plugin to disallow access to prevent troublemakers from joining your server

# How does it work?

It checks if a the user is in a ban database and kicks them if they are

# Translations? Configs? Anything?

Just get the correct translation for your server and rename the file to ServerGuard (remove the language name in the file).
To contribute to the translations fork the repository do your changes and make a pull request!

__Configs__

|Config name|Default value|Value Type|Description|
|---|---|---|---|
|sg_webhookurl|none|String|The webhook URL for Discord logging (The auto kick system must be disabled for this to work)|
|sg_enableautokick|true|Bool|Disallow access to users on the troublemakers database|
|sg_notifyroles|none|List|Put a list of staff roles to notify in-game when a troublemaker has joined (The auto kick system must be disabled for this to work)|
|sg_doorhackdetection|false|Bool|Phasing trough door protection (May impact server performance heavily further testing required)|

# How to report people to be banned like this?

Right now the only way you can report players is by hopping on the Discord for this plugin! https://discord.gg/NDeZzyz

# How to install?

Well pretty easy just grab the latest release and put in the "sm_plugins" folder, also don't forget the to dependency in the dependency folder.


*Also feel free to suggest features, please try to keep it in the theme of server security.*