using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
	public class Grid
	{
		public List<GridBox> grids = new List<GridBox>();
		public readonly int lineCount;
		public readonly int columnCount;
		
		public Grid(int lines, int columns)
		{
			lineCount = lines;
			columnCount = columns;
			for (var i = 0; i < lines; i++)
			{
				for (var j = 0; j < columns; j++)
				{
					var newBox = new GridBox(j, i, false, columns * i + j);
					grids.Add(newBox);
				}
			}
			Console.WriteLine($"The battle field has been created");
		}

		// prints the matrix that indicates the tiles of the battlefield
		public void DrawBattlefield()
		{
			for (var i = 0; i < lineCount; i++)
			{
				for (var j = 0; j < columnCount; j++)
				{
					Console.Write($"[{grids[columnCount * i + j].CharacterInitial}]\t");
				}
				Console.Write(Environment.NewLine + Environment.NewLine);
			}
			Console.Write(Environment.NewLine + Environment.NewLine);
		}

		public bool AreNeighboursOccupied(GridBox box)
		{
			var hasLeftIndex = box.Index % columnCount != 0;
			if (hasLeftIndex && grids[box.Index - 1].Occupied) return true;
			
			var hasRightIndex = box.Index % columnCount != columnCount - 1;
			if (hasRightIndex && grids[box.Index + 1].Occupied) return true;
			
			var hasIndexAbove = box.Index >= columnCount;
			if (hasIndexAbove && grids[box.Index - columnCount].Occupied) return true;
			
			var hasIndexBelow = box.Index < (lineCount - 1) * columnCount;
			if (hasIndexBelow && grids[box.Index + columnCount].Occupied) return true;

			return false;
		}
	}
}
