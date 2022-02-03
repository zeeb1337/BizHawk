﻿using System;
using System.ComponentModel;

using BizHawk.Emulation.Cores.Consoles.Nintendo.NDS;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace BizHawk.Client.Common
{
	[Description("Functions specific to NDSHawk (functions may not run when an NDS game is not loaded)")]
	public sealed class NDSLuaLibrary : LuaLibraryBase
	{
		public NDSLuaLibrary(IPlatformLuaLibEnv luaLibsImpl, ApiContainer apiContainer, Action<string> logOutputCallback)
			: base(luaLibsImpl, apiContainer, logOutputCallback) {}

		public override string Name => "nds";

		private NDS.NDSSettings Settings
		{
			get => APIs.Emulation.GetSettings() as NDS.NDSSettings ?? new NDS.NDSSettings();
			set => APIs.Emulation.PutSettings(value);
		}

		[LuaMethodExample("if ( nds.getscreenlayout( ) ) then\r\n\tconsole.log( \"Returns which screen layout is active\" );\r\nend;")]
		[LuaMethod("getscreenlayout", "Returns which screen layout is active")]
		[return: LuaEnumStringParam]
		public string GetScreenLayout()
			=> Settings.ScreenLayout.ToString();

		[LuaMethodExample("if ( nds.getscreeninvert( ) ) then\r\n\tconsole.log( \"Returns whether screens are inverted\" );\r\nend;")]
		[LuaMethod("getscreeninvert", "Returns whether screens are inverted")]
		public bool GetScreenInvert()
			=> Settings.ScreenInvert;

		[LuaMethodExample("if ( nds.getscreenrotation( ) ) then\r\n\tconsole.log( \"Returns how screens are rotated\" );\r\nend;")]
		[LuaMethod("getscreenrotation", "Returns how screens are rotated")]
		[return: LuaEnumStringParam]
		public string GetScreenRotation()
			=> Settings.ScreenRotation.ToString();

		[LuaMethodExample("if ( nds.getscreengap( ) ) then\r\n\tconsole.log( \"Returns the gap between the screens\" );\r\nend;")]
		[LuaMethod("getscreengap", "Returns the gap between the screens")]
		public int GetScreenGap()
			=> Settings.ScreenGap;

		[LuaMethodExample("if ( nds.getaccurateaudiobitrate( ) ) then\r\n\tconsole.log( \"Returns whether the audio bitrate is set to accurate mode\" );\r\nend;")]
		[LuaMethod("getaccurateaudiobitrate", "Returns whether the audio bitrate is in accurate mode")]
		public bool GetAccurateAudioBitrate()
			=> Settings.AccurateAudioBitrate;

		[LuaMethodExample("nds.setscreenlayout( \"Vertical\" );")]
		[LuaMethod("setscreenlayout", "Sets which screen layout is active")]
		public void SetScreenLayout([LuaEnumStringParam] string value)
		{
			var s = Settings;
			s.ScreenLayout = (NDS.ScreenLayoutKind)Enum.Parse(typeof(NDS.ScreenLayoutKind), value, true);
			Settings = s;
		}

		[LuaMethodExample("nds.setscreeninvert( false );")]
		[LuaMethod("setscreeninvert", "Sets whether screens are inverted")]
		public void SetScreenInvert(bool value)
		{
			var s = Settings;
			s.ScreenInvert = value;
			Settings = s;
		}

		[LuaMethodExample("nds.setscreenrotation( \"Rotate0\" );")]
		[LuaMethod("setscreenrotation", "Sets how screens are rotated")]
		public void SetScreenRotation([LuaEnumStringParam] string value)
		{
			var s = Settings;
			s.ScreenRotation = (NDS.ScreenRotationKind)Enum.Parse(typeof(NDS.ScreenRotationKind), value, true);
			Settings = s;
		}

		[LuaMethodExample("nds.setscreengap( 0 );")]
		[LuaMethod("setscreengap", "Sets the gap between the screens")]
		public void GetScreenGap(int value)
		{
			var s = Settings;
			s.ScreenGap = value;
			Settings = s;
		}

		[LuaMethodExample("nds.setaccurateaudiobitrate( true );")]
		[LuaMethod("setaccurateaudiobitrate", "Sets whether the audio bitrate is in accurate mode")]
		public void SetAccurateAudioBitrate(bool value)
		{
			var s = Settings;
			s.AccurateAudioBitrate = value;
			Settings = s;
		}
	}
}
