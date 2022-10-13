namespace AutoBattle
{
	public static class Types
	{
		public class GridBox
		{
			public int lineIndex;
			public int columnIndex;
			public bool Occupied;
			public char CharacterInitial;
			public int Index;
			
			public const char EMPTY_CHARACTER_INITIAL = ' ';

			public GridBox(int line, int column, bool occupied, int index)
			{
				lineIndex = line;
				columnIndex = column;
				Occupied = occupied;
				Index = index;
				CharacterInitial = EMPTY_CHARACTER_INITIAL;
			}
		}

		public enum CharacterClass : uint
		{
			Paladin = 1,
			Warrior = 2,
			Cleric = 3,
			Archer = 4
		}
	}
}
