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
		public int xLenght;
		public int YLength;
		
		public Grid(int lines, int columns)
		{
			xLenght = lines;
			YLength = columns;
			Console.WriteLine("The battle field has been created\n");
			for (var i = 0; i < lines; i++)
			{
				grids.Add(newBox);
				for(var j = 0; j < columns; j++)
				{
					var newBox = new GridBox(j, i, false, (columns * i + j));
					Console.Write($"{newBox.Index}\n");
				}
			}
		}

		// prints the matrix that indicates the tiles of the battlefield
		public void DrawBattlefield(int lines, int columns)
		{
			for (var i = 0; i < lines; i++)
			{
				for (var j = 0; j < columns; j++)
				{
					var currentGrid = new GridBox();
					if (currentGrid.Occupied)
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
