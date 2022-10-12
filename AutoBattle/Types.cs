namespace AutoBattle
{
	public static class Types
	{
		public struct CharacterClassSpecific
		{
			CharacterClass _characterClass;
			float _hpModifier;
			float _classDamage;
			CharacterSkills[] _skills;
		}

		public class GridBox
		{
			public int lineIndex;
			public int columnIndex;
			public bool Occupied;
			public int Index;

			public GridBox(int line, int column, bool occupied, int index)
			{
				lineIndex = line;
				columnIndex = column;
				Occupied = occupied;
				Index = index;
			}
		}

		public struct CharacterSkills
		{
			string _name;
			float _damage;
			float _damageMultiplier;
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
