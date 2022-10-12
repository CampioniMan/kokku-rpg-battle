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
			Console.WriteLine("The battle field has been created\n");
			for (var i = 0; i < lines; i++)
			{
				for (var j = 0; j < columns; j++)
				{
					var newBox = new GridBox(j, i, false, columns * i + j);
					grids.Add(newBox);
					Console.Write($"{newBox.Index}\n");
				}
			}
		}

		// prints the matrix that indicates the tiles of the battlefield
		public void DrawBattlefield()
		{
			Console.Clear();
			for (var i = 0; i < lineCount; i++)
			{
				for (var j = 0; j < columnCount; j++)
				{
					if (grids[columnCount * i + j].Occupied)
					{
						//if()
						Console.Write("[X]\t");
					}
					else
					{
						Console.Write($"[ ]\t");
					}
				}
				Console.Write(Environment.NewLine + Environment.NewLine);
			}
			Console.Write(Environment.NewLine + Environment.NewLine);
		}
	}
}
