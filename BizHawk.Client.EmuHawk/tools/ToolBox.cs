﻿using System;
using System.Drawing;
using System.Windows.Forms;

using BizHawk.Client.Common;
using BizHawk.Emulation.Cores.Calculators;
using BizHawk.Emulation.Cores.Nintendo.NES;
using BizHawk.Emulation.Cores.Nintendo.SNES;

namespace BizHawk.Client.EmuHawk
{
	public partial class ToolBox : Form, IToolForm
	{
		public ToolBox()
		{
			InitializeComponent();
		}

		private void ToolBox_Load(object sender, EventArgs e)
		{
			Location = new Point(
				GlobalWin.MainForm.Location.X + GlobalWin.MainForm.Size.Width,
				GlobalWin.MainForm.Location.Y
			);

			HideShowIcons();
		}

		public bool AskSave() { return true;  }
		public bool UpdateBefore { get { return false; } }
		public void UpdateValues() { }

		public void Restart()
		{
			HideShowIcons();
		}

		private void HideShowIcons()
		{
			NesPPUToolbarItem.Visible =
				NesDebuggerToolbarItem.Visible =
				NesGameGenieToolbarItem.Visible =
				NesNameTableToolbarItem.Visible =
				Global.Emulator is NES;

			TI83KeypadToolbarItem.Visible = Global.Emulator is TI83;

			SNESGraphicsDebuggerToolbarItem.Visible =
			SNESGameGenieToolbarItem.Visible =
				Global.Emulator is LibsnesCore;

			GGGameGenieToolbarItem.Visible =
				Global.Game.System == "GG";

			GBGameGenieToolbarItem.Visible = Global.Game.System == "GB";

			Size = new Size(Size.Width, ToolBoxStrip.Size.Height + 50);
		}

		private void CheatsToolBarItem_Click(object sender, EventArgs e)
		{
			GlobalWin.Tools.Load<Cheats>();
		}

		private void RamWatchToolbarItem_Click(object sender, EventArgs e)
		{
			GlobalWin.Tools.LoadRamWatch(true);
		}

		private void RamSearchToolbarItem_Click(object sender, EventArgs e)
		{
			GlobalWin.Tools.Load<RamSearch>();
		}

		private void HexEditorToolbarItem_Click(object sender, EventArgs e)
		{
			GlobalWin.Tools.Load<HexEditor>();
		}

		private void LuaConsoleToolbarItem_Click(object sender, EventArgs e)
		{
			GlobalWin.MainForm.OpenLuaConsole();
		}

		private void NesPPUToolbarItem_Click(object sender, EventArgs e)
		{
			GlobalWin.Tools.Load<NESPPU>();
		}

		private void NesDebuggerToolbarItem_Click(object sender, EventArgs e)
		{
			GlobalWin.Tools.Load<NESDebugger>();
		}

		private void NesGameGenieToolbarItem_Click(object sender, EventArgs e)
		{
			GlobalWin.MainForm.LoadGameGenieEc();
		}

		private void NesNameTableToolbarItem_Click(object sender, EventArgs e)
		{
			GlobalWin.Tools.Load<NESNameTableViewer>();
		}

		private void TI83KeypadToolbarItem_Click(object sender, EventArgs e)
		{
			if (Global.Emulator is TI83)
			{
				GlobalWin.Tools.Load<TI83KeyPad>();
			}
		}

		private void TAStudioToolbarItem_Click(object sender, EventArgs e)
		{
			GlobalWin.MainForm.LoadTAStudio();
		}

		private void SNESGraphicsDebuggerToolbarItem_Click(object sender, EventArgs e)
		{
			if (Global.Emulator is LibsnesCore)
			{
				GlobalWin.Tools.Load<SNESGraphicsDebugger>();
			}
		}

		private void VirtualpadToolbarItem_Click(object sender, EventArgs e)
		{
			GlobalWin.Tools.Load<VirtualPadForm>();
		}

		private void SNESGameGenieToolbarItem_Click(object sender, EventArgs e)
		{
			GlobalWin.MainForm.LoadGameGenieEc();
		}

		private void GGGameGenieToolbarItem_Click(object sender, EventArgs e)
		{
			GlobalWin.MainForm.LoadGameGenieEc();
		}

		private void GBGameGenieToolbarItem_Click(object sender, EventArgs e)
		{
			GlobalWin.MainForm.LoadGameGenieEc();
		}
	}
}
