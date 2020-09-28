using System;

using BizHawk.Common;

namespace BizHawk.Client.EmuHawk
{
	public sealed class CellList : SortedList<Cell>
	{
		public int? FirstRowIndex => Count == 0 ? null : this[0].RowIndex;

		public int? LastRowIndex => Count == 0 ? null : this[Count - 1].RowIndex;

		// restore Set semantics, I doubt this is necessary TBH --yoshi
		public override void Add(Cell item)
		{
			var i = _list.BinarySearch(item);
			if (i >= 0)
			{
				Console.WriteLine("Yoshi didn't think this print statement would ever get hit, please screencap this and send it to him.");
				Console.WriteLine(item == null ? "`CellList.Add(null)`" : $"`CellList.Add(Cell {{ r = {item.RowIndex}, c = \"{item.Column?.Name}\" }})`");
				return;
			}
			_list.Insert(~i, item);
		}
	}

	/// <summary>
	/// Represents a single cell of the <seealso cref="InputRoll"/>
	/// </summary>
	public class Cell : IComparable<Cell>
	{
		public RollColumn Column { get; internal set; }
		public int? RowIndex { get; internal set; }
		public string CurrentText { get; internal set; }

		public Cell() { }

		public Cell(Cell cell)
		{
			Column = cell.Column;
			RowIndex = cell.RowIndex;
		}

		public int CompareTo(Cell other)
		{
			if (other == null)
			{
				return 1;
			}

			if (this.RowIndex.HasValue)
			{
				if (other.RowIndex.HasValue)
				{
					int row = this.RowIndex.Value.CompareTo(other.RowIndex.Value);
					return row == 0
						? this.Column.Name.CompareTo(other.Column.Name)
						: row;
				}

				return 1;
			}

			if (other.RowIndex.HasValue)
			{
				return -1;
			}

			return this.Column.Name.CompareTo(other.Column.Name);
		}

		public override bool Equals(object obj)
		{
			var cell = obj as Cell;
			if (cell != null)
			{
				return Column == cell.Column && RowIndex == cell.RowIndex;
			}

			return false;
		}

		public override int GetHashCode()
		{
			return Column.GetHashCode() + RowIndex.GetHashCode();
		}

		public static bool operator ==(Cell a, Cell b)
		{
			return a?.Equals(b) ?? ReferenceEquals(b, null);
		}

		public static bool operator !=(Cell a, Cell b)
		{
			return !(a == b);
		}
	}

	public static class CellExtensions
	{
		public static bool IsDataCell(this Cell cell)
		{
			return cell != null && cell.RowIndex != null && cell.Column != null;
		}
	}
}
