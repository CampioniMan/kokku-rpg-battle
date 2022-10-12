namespace AutoBattle
{
	public class Types
	{
		public struct CharacterClassSpecific
		{
			CharacterClass _characterClass;
			float _hpModifier;
			float _classDamage;
			CharacterSkills[] _skills;

		}

		public struct GridBox
		{
			public int XIndex;
			public int YIndex;
			public bool Occupied;
			public int Index;

			public GridBox(int x, int y, bool occupied, int index)
			{
				XIndex = x;
				YIndex = y;
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
