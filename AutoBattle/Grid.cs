using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
			foreach (var direction in PossibleDirections)
			{
				if (TryGetNeighbour(box, direction, out var neighbour) && neighbour.Occupied) return true;
			}

			return false;
		}

		public bool TryGetNeighbour(GridBox position, PossibleDirection direction, out GridBox neighbour)
		{
			var hasNeighbour = false;
			var neighbourPosition = 0;
			switch (direction)
			{
				case PossibleDirection.UpperLeft:
					hasNeighbour = BoxHasUpwardsNeighbour(position) && BoxHasLeftNeighbour(position);
					neighbourPosition = position.Index - columnCount - 1;
					break;
				case PossibleDirection.Up:
					hasNeighbour = BoxHasUpwardsNeighbour(position);
					neighbourPosition = position.Index - columnCount;
					break;
				case PossibleDirection.UpperRight:
					hasNeighbour = BoxHasUpwardsNeighbour(position) && BoxHasRightNeighbour(position);
					neighbourPosition = position.Index - columnCount + 1;
					break;
				case PossibleDirection.Left:
					hasNeighbour = BoxHasLeftNeighbour(position);
					neighbourPosition = position.Index - 1;
					break;
				case PossibleDirection.Right:
					hasNeighbour = BoxHasRightNeighbour(position);
					neighbourPosition = position.Index + 1;
					break;
				case PossibleDirection.LowerLeft:
					hasNeighbour = BoxHasDownwardsNeighbour(position) && BoxHasLeftNeighbour(position);
					neighbourPosition = position.Index + columnCount - 1;
					break;
				case PossibleDirection.Down:
					hasNeighbour = BoxHasDownwardsNeighbour(position);
					neighbourPosition = position.Index + columnCount;
					break;
				case PossibleDirection.LowerRight:
					hasNeighbour = BoxHasDownwardsNeighbour(position) && BoxHasRightNeighbour(position);
					neighbourPosition = position.Index + columnCount + 1;
					break;
			}
			
			if (hasNeighbour)
			{
				neighbour = grids[neighbourPosition];
				return true;
			}

			neighbour = null;
			return false;
		}

		bool BoxHasLeftNeighbour(GridBox box)
		{
			return box.Index % columnCount != 0;
		}
		
		bool BoxHasRightNeighbour(GridBox box)
		{
			return box.Index % columnCount != columnCount - 1;
		}
		
		bool BoxHasUpwardsNeighbour(GridBox box)
		{
			return box.Index >= columnCount;
		}
		
		bool BoxHasDownwardsNeighbour(GridBox box)
		{
			return box.Index < (lineCount - 1) * columnCount;
		}
	}
}
