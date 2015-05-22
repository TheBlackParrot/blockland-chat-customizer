new AudioProfile(ChatSound)
{
	fileName = "./sounds/chat.wav";
	description = AudioGui;
	preload = true;
};
new AudioProfile(SelfChatSound)
{
	fileName = "./sounds/selfchat.wav";
	description = AudioGui;
	preload = true;
};

if(isFile("config/client/chat/vars.cs"))
{

	exec("config/client/chat/vars.cs");

}
else{

	$Chat::FontFamily = "Palatino Linotype";
	$Chat::FontSize_ = 16;
	$Chat::NameColor = "FFFF00";
	$Chat::SelfNameColor = "FFFF99";
	$Chat::ClanColor = "777777";
	$Chat::MessageColor = "FFFFFF";
	$Chat::SelfMessageColor = "CCCCCC";
	$Chat::Enable_NameColor = 1;
	$Chat::Enable_SelfNameColor = 1;
	$Chat::Enable_ClanColor = 1;
	$Chat::Enable_MessageColor = 1;
	$Chat::Enable_SelfMessageColor = 1;
	$Chat::ShowClanTags = 1;
	$Chat::Echo = 0;
	$Chat::Log = 0;
	$Chat::Sound = 0;
	$Chat::Bold = 0;
	$Chat::Italic = 0;
	$Chat::ChooserR = 255;
	$Chat::ChooserG = 128;
	$Chat::ChooserB = 0;

	$Chat::Shadow = 0;
	$Chat::ShadowColor = "000000";
	$Chat::ShadowOpacity = 255;

	$Chat::Time = 0;
	$Chat::TimeColor = "FFFFFF";
	$Chat::Enable_TimeColor = 0;

	$Chat::ShadowX = 2;
	$Chat::ShadowY = 2;

}

export("$Chat::*","config/client/chat/vars.cs",false);


//for the gui to help choose colors.
function convertRGBToHex(%dec)
{

	%str = "0123456789ABCDEF";
	%final = "";
	
	while(%dec != 0)
	{
	
		%hexn = %dec % 16;
		%dec = mFloor(%dec / 16);
		%hex = getSubStr(%str,%hexn,1) @ %hex;
		
	}
	
	if(strLen(%hex) == 1) { %hex = "0" @ %hex; }
	if(strLen(%hex) == 0) { %hex = "00"; }
	
	return %hex;
	
}

// GUI STUFF *START*

if(isObject(ExampleChat)) { ExampleChat.setVisible(0); }
if(isObject(OPT_ChatSize0)) { OPT_ChatSize0.setVisible(0); }
if(isObject(OPT_ChatSize1)) { OPT_ChatSize1.setVisible(0); }
if(isObject(OPT_ChatSize2)) { OPT_ChatSize2.setVisible(0); }
if(isObject(OPT_ChatSize3)) { OPT_ChatSize3.setVisible(0); }
if(isObject(OPT_ChatSize4)) { OPT_ChatSize4.setVisible(0); }
if(isObject(OPT_ChatSize5)) { OPT_ChatSize5.setVisible(0); }
if(isObject(OPT_ChatSize6)) { OPT_ChatSize6.setVisible(0); }
if(isObject(OPT_ChatSize7)) { OPT_ChatSize7.setVisible(0); }
if(isObject(OPT_ChatSize8)) { OPT_ChatSize8.setVisible(0); }
if(isObject(OPT_ChatSize9)) { OPT_ChatSize9.setVisible(0); }
if(isObject(OPT_ChatSize10)) { OPT_ChatSize10.setVisible(0); }

function addChatSettingsButton()
{

	//really wish badspot would correctly name everything that would be great. >_>

	%why = new GuiSwatchCtrl(HideChatSizeText)
	{
		profile = "GuiDefaultProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "313 149";
		extent = "59 17";
		minExtent = "59 17";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		color = "243 243 243 255";
	};
	OptGraphicsPane.add(%why);
	
	//will be a parent of the graphics pane unfortunately. wtf.
	%button = new GuiBitmapButtonCtrl(ChatSettingsButton)
	{
		profile = "BlockButtonProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "389 122";
		extent = "140 30";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		command = "canvas.pushDialog(ChatSettingsGUI);";
		text = "Chat Settings";
		groupNum = "-1";
		buttonType = "PushButton";
		bitmap = "base/client/ui/button1";
		lockAspectRatio = "0";
		alignLeft = "0";
		alignTop = "0";
		overflowImage = "0";
		mKeepCached = "0";
		mColor = "255 255 255 255";
	};
	OptGraphicsPane.add(%button);
	
}
if(!isObject(ChatSettingsButton)) { addChatSettingsButton(); }


function updateChatPreview()
{

	%font = $Chat::FontFamily;
	strReplace(%font,"Bold","");
	strReplace(%font,"Italic","");
	if($Chat::Bold) { %font = %font SPC "Bold"; }
	if($Chat::Italic) { %font = %font SPC "Italic"; }
	if($Chat::Shadow) { %shadow = "<shadow:" @ $Chat::ShadowX @ ":" @ $Chat::ShadowY @ "><shadowcolor:" @ $Chat::ShadowColor @ convertRGBtoHex($Chat::ShadowOpacity) @ ">"; } else { %shadow = ""; }
	%size = $Chat::FontSize_;
	
	if($Chat::Enable_NameColor) { %colName = $Chat::NameColor; } else { %colName = "FFFF00"; }
	if($Chat::Enable_SelfNameColor) { %colSelfName = $Chat::SelfNameColor; } else { %colSelfName = %colName; }
	if($Chat::Enable_ClanColor) { %colClan = $Chat::ClanColor; } else { %colClan = "666666"; }
	if($Chat::Enable_MessageColor) { %colMessage = $Chat::MessageColor; } else { %colMessage = "FFFFFF"; }
	if($Chat::Enable_SelfMessageColor) { %colSelfMessage = $Chat::SelfMessageColor; } else { %colSelfMessage = %colMessage; }
	if($Chat::Enable_TimeColor) { %colTime = $Chat::TimeColor; } else { %colTime = "00FF00"; }

	if($Chat::ShowClanTags)
	{

		%cP = $Pref::Player::ClanPrefix;
		%cS = $Pref::Player::ClanPrefix;

		if(%cP $= "") { %cP = "pre"; }
		if(%cS $= "") { %cS = "suf"; }

	}
	else
	{

		%cP = ""; %cS = "";

	}

	if($Chat::Time) { %time = "<color:" @ %colTime @ ">" @ getWord(getDateTime(),1); } else { %time = ""; }

	$ChatPreviewSTR = %shadow @ "<font:" @ %font @ ":" @ %size @ ">" @ %time SPC "<color:" @ %colClan @ ">" @ %cP @ "<color:" @ %colName @ ">Blockhead37619<color:" @ %colClan @ ">" @ %cS @ "<color:ffffff>:" SPC "<color:" @ %colMessage @ ">How is everyone? :D<br>" @ %time SPC "<color:" @ %colClan @ ">" @ %cP @ "<color:" @ %colSelfName @ ">" @ $pref::Player::NetName @ "<color:" @ %colClan @ ">" @ %cS @ "<color:ffffff>:" SPC "<color:" @ %colSelfMessage @ ">Great! :)";
	ChatPreview.setText($ChatPreviewSTR);

}

if(!isObject(ChatSettingsGUI))
{

	%size = $Chat::FontSize_;

	if(%size == 16 || %size == 17) { OPT_SetChatSize(0); }
	else if(%size >= 18 && %size < 20) { OPT_SetChatSize(1); }
	else if(%size >= 20 && %size < 22) { OPT_SetChatSize(2); }
	else if(%size >= 22 && %size < 24) { OPT_SetChatSize(3); }
	else if(%size >= 24 && %size < 26) { OPT_SetChatSize(4); }
	else if(%size >= 26 && %size < 28) { OPT_SetChatSize(5); }
	else if(%size >= 28 && %size < 30) { OPT_SetChatSize(6); }
	else if(%size >= 30 && %size < 32) { OPT_SetChatSize(7); }
	else if(%size >= 32 && %size < 34) { OPT_SetChatSize(8); }
	else if(%size >= 34) { OPT_SetChatSize(9); }

	exec("./ChatSettingsGUI.gui");
}
else 
{
	ChatSettingsGUI.delete();
	exec("./ChatSettingsGUI.gui");
}

if(!isObject(ChatSettingsGUI_ColorHelper))
{
	exec("./ChatSettingsGUI_ColorHelper.gui");
}
else 
{
	ChatSettingsGUI_ColorHelper.delete();
	exec("./ChatSettingsGUI_ColorHelper.gui");
}

function saveChatSettings()
{

	%size = $Chat::FontSize_;

	strReplace($Chat::FontFamily,"Bold","");
	strReplace($Chat::FontFamily,"Italic","");

	export("$Chat::*","config/client/chat/vars.cs",false);
	exec("config/client/chat/vars.cs");
	
	warn("Saved and applied chat settings.");
	canvas.popDialog(ChatSettingsGUI);

	if(%size == 16 || %size == 17) { OPT_SetChatSize(0); }
	else if(%size >= 18 && %size < 20) { OPT_SetChatSize(1); }
	else if(%size >= 20 && %size < 22) { OPT_SetChatSize(2); }
	else if(%size >= 22 && %size < 24) { OPT_SetChatSize(3); }
	else if(%size >= 24 && %size < 26) { OPT_SetChatSize(4); }
	else if(%size >= 26 && %size < 28) { OPT_SetChatSize(5); }
	else if(%size >= 28 && %size < 30) { OPT_SetChatSize(6); }
	else if(%size >= 30 && %size < 32) { OPT_SetChatSize(7); }
	else if(%size >= 32 && %size < 34) { OPT_SetChatSize(8); }
	else if(%size >= 34) { OPT_SetChatSize(9); }

	updateChatPreview();

}

function cancelChatSettings()
{

	exec("config/client/chat/vars.cs");

	canvas.popDialog(ChatSettingsGUI);
	warn("Chat settings not saved.");

	updateChatPreview();

}

function resetChatSettings()
{

	$Chat::FontFamily = "Palatino Linotype";
	$Chat::FontSize_ = 16;
	$Chat::NameColor = "FFFF00";
	$Chat::SelfNameColor = "FFFF99";
	$Chat::ClanColor = "666666";
	$Chat::MessageColor = "FFFFFF";
	$Chat::SelfMessageColor = "CCCCCC";
	$Chat::Enable_NameColor = 1;
	$Chat::Enable_SelfNameColor = 1;
	$Chat::Enable_ClanColor = 1;
	$Chat::Enable_MessageColor = 1;
	$Chat::Enable_SelfMessageColor = 1;
	$Chat::ShowClanTags = 1;
	$Chat::Echo = 0;
	$Chat::Log = 0;
	$Chat::Sound = 0;
	$Chat::Bold = 0;
	$Chat::Italic = 0;
	$Chat::Shadow = 0;
	$Chat::ShadowColor = "000000";
	$Chat::ShadowOpacity = 255;
	$Chat::Time = 0;
	$Chat::TimeColor = "00FF00";
	$Chat::Enable_TimeColor = 0;
	warn("Reset chat settings to their default state.");
	updateChatPreview();
	canvas.popDialog(ChatSettingsGUI);
	canvas.pushDialog(ChatSettingsGUI);

}


function updateChooserColor()
{

	$Chat::ChooserR = mFloor($Chat::ChooserR);
	$Chat::ChooserG = mFloor($Chat::ChooserG);
	$Chat::ChooserB = mFloor($Chat::ChooserB);

	%red = convertRGBtoHex($Chat::ChooserR);
	%green = convertRGBtoHex($Chat::ChooserG);
	%blue = convertRGBtoHex($Chat::ChooserB);
	$Chat::ChooserStr = %red @ %green @ %blue;
	
	ChatSettingsGUI_ColorHelperHexTextPrev.setText("<just:center><color:ffffff><font:Arial Bold:14>" @ $Chat::ChooserStr);
	ChatSettingsGUI_ColorHelperPreview.setColor($Chat::ChooserR SPC $Chat::ChooserG SPC $Chat::ChooserB SPC "255");
	
}

function snapChooserSliders()
{
	
	ChatSettingsGUI_ColorHelperRSlider.setValue($Chat::ChooserR);
	ChatSettingsGUI_ColorHelperGSlider.setValue($Chat::ChooserG);
	ChatSettingsGUI_ColorHelperBSlider.setValue($Chat::ChooserB);
	
}
	
	

function copyChooserStr()
{

	setClipboard($Chat::ChooserStr);
	messageBoxOK("",$Chat::ChooserStr SPC "was copied to the clipboard.");
	canvas.popDialog(ChatSettingsGUI_ColorHelper);
	
}

function ChatSettingsGUI_ColorHelper::onWake(%gui)
{

	updateChooserColor();
	
}
function ChatSettingsGUI::onWake(%gui)
{

	updateChatPreview();
	
}
	

	

//GUI STUFF *END*


package ChatExtensions
{

	function newChatSO::addLine(%a,%msg)
	{

		%font = $Chat::FontFamily;

		if($Chat::Bold)
		{

			%font = %font SPC "Bold";

		}

		if($Chat::Italic)
		{

			%font = %font SPC "Italic";

		}

		if($Chat::Shadow)
		{

			%shadow = "<shadow:" @ $Chat::ShadowX @ ":" @ $Chat::ShadowY @ "><shadowcolor:" @ $Chat::ShadowColor @ convertRGBtoHex($Chat::ShadowOpacity) @ ">";

		}
		else
		{

			%shadow = "";

		}

		%msg = strReplace(%msg,"\c0","<color:ff0040>");
		%msg = strReplace(%msg,"\c1","<color:4040ff>");
		%msg = strReplace(%msg,"\c2","<color:00ff00>");
		%msg = strReplace(%msg,"\c3","<color:ffff00>");
		%msg = strReplace(%msg,"\c4","<color:0000ff>");
		%msg = strReplace(%msg,"\c5","<color:ff00ff>");
		%msg = strReplace(%msg,"\c6","<color:ffffff>");
		%msg = strReplace(%msg,"\c7","<color:606060>");
		%msg = strReplace(%msg,"\c8","<color:000000>");

		%add =  %shadow @ "<font:" @ %font @ ":" @ $Chat::FontSize_ @ ">";
		%msg = %add @ %msg;

		parent::addLine(%a,%msg);

	}

	function clientCmdChatMessage(%a,%b,%c,%fmsg,%cp,%name,%cs,%msg)
	{

		if( %msg $= "" )
		{
			parent::clientCmdChatMessage( %a, %b, %c, %fmsg, %cp, %name, %cs, %msg );
		}
		else
		{

			if($Chat::Echo)
			{

				echo(%name @ ":" SPC %msg);

			}

			if($Chat::Log)
			{

				%fp = new FileObject();
	
				%date = strReplace(getWord(getDateTime(),0),"/",".");
				%time = getWord(getDateTime(),1);
	
				%filename = "config/client/chatlogs/" @ %date;
		
				%fp.openForAppend(%filename);
				%fp.writeLine("[" @ %time @ "] ::" SPC %name @ ":" SPC %msg);
		
				%fp.close();
				%fp.delete();

			}

			if($Chat::Sound)
			{

				if(%msg !$= "" && getSimTime()-100 > $lastMessageTime && %name !$= $Pref::Player::NetName)
				{
					$lastMessageTime = getSimTime();
					alxPlay(ChatSound);
				}
				
				if(%msg !$= "" && getSimTime()-100 > $lastMessageTime && %name $= $Pref::Player::NetName)
				{
					$lastMessageTime = getSimTime();
					alxPlay(SelfChatSound);
				}
					

			}

			if($Chat::ShowClanTags)
			{

				if($Chat::Enable_ClanColor)
				{

					%cp = "<color:" @ $Chat::ClanColor @ ">" @ %cp;
					%cs = "<color:" @ $Chat::ClanColor @ ">" @ %cs;

				}
				else
				{

					%cp = "\c7" @ %cp;
					%cs = "\c7" @ %cs;

				}

			}
			else
			{

				%cp = "";
				%cs = "";

			}

			if(%name $= $Pref::Player::NetName)
			{
		
				if($Chat::Enable_SelfNameColor)
				{	

					%name = "<color:" @ $Chat::SelfNameColor @ ">" @ %name;


				}
				else
				{

					if($Chat::Enable_NameColor)
					{

						%name = "<color:" @ $Chat::NameColor @ ">" @ %name;

					}
					else
					{

						%name = "\c3" @ %name;

					}

				}

				if($Chat::Enable_SelfMessageColor)
				{

					%msg = "<color:" @ $Chat::SelfMessageColor @ ">" @ %msg;

				}
				else
				{

					if($Chat::Enable_MessageColor)
					{

					%msg = "<color:" @ $Chat::MessageColor @ ">" @ %msg;

					}
					else
					{

						%msg = "\c6" @ %msg;

					}

				}

			}

			if($Chat::Enable_NameColor && %name !$= $Pref::Player::NetName)
			{

				%name = "<color:" @ $Chat::NameColor @ ">" @ %name;

			}
			else if(%name !$= $Pref::Player::NetName)
			{

				%name = "\c3" @ %name;

			}

			if($Chat::Enable_MessageColor && %name !$= $Pref::Player::NetName)
			{

				%msg = "<color:" @ $Chat::MessageColor @ ">" @ %msg;

			}
			else if(%name !$= $Pref::Player::NetName)
			{

				%msg = "\c6" @ %msg;

			}

			if($Chat::Time)
			{

				if($Chat::Enable_TimeColor)
				{

					%time = "<color:" @ $Chat::TimeColor @ ">" @ getWord(getDateTime(),1) @ " ";

				}
				else
				{

					%time = "<color:00ff00>" @ getWord(getDateTime(),1) @ " ";

				}

			}
			else
			{

				%time="";

			}

			//%font = "<font:" @ $Chat::FontFamily @ ":" @ $Chat::FontSize_ @ ">";

			//%fmsg = %font @ %cp @ %name @ %cs @ "\c6:" SPC %msg;
			%fmsg = %time @ %cp @ %name @ %cs @ "<color:ffffff>:" SPC %msg;

			Parent::clientCmdChatMessage(%a,%b,%c,%fmsg,%cp,%name,%cs,%msg);
		}

	}

};
activatePackage(ChatExtensions);
warn("Custom Chat has been started.");