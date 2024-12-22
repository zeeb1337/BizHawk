﻿using System.Linq;
using System.Text;
using BizHawk.Emulation.Common;

namespace BizHawk.Client.Common
{
	/// <summary>
	/// Generates a display friendly version of the input log entry
	/// using .bk2 mnemonics as the basis for display
	/// </summary>
	public static class Bk2InputDisplayGenerator
	{
		public static string Generate(IController source)
		{
			if (source.Definition.MnemonicsCache is null)
				throw new InvalidOperationException("Can't generate input display string with empty mnemonics cache");

			var sb = new StringBuilder();
			var controls = source.Definition.ControlsOrdered.ToList();
			// index 0 is for controls that don't belong to any indexed player
			if (controls.ElementAtOrDefault(0) != null)
			{
				controls.Add(controls[0]);
				controls.RemoveAt(0);
			}

			foreach ((string buttonName, AxisSpec? axisSpec) in controls.SelectMany(x => x))
			{
				if (axisSpec.HasValue)
				{
					int val = source.AxisValue(buttonName);

					if (val == axisSpec.Value.Neutral)
					{
						sb.Append("      ");
					}
					else
					{
						sb.Append(val.ToString().PadLeft(5, ' ')).Append(',');
					}
				}
				else
				{
					sb.Append(source.IsPressed(buttonName)
						? source.Definition.MnemonicsCache[buttonName]
						: ' ');
				}
			}

			return sb.ToString();
		}
	}
}
