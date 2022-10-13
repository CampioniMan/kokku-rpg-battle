using System;
using static AutoBattle.Types;

namespace AutoBattle
{
	public class Character
	{
		const int DEFAULT_HEALTH = 100;
		const int DEFAULT_BASE_DAMAGE = 20;
		const int DEFAULT_DAMAGE_MULTIPLIER = 1;

		static int _nextAvailableIndex = 0;

		readonly Random _rand = new Random();

		string _name = "DefaultPlayerName";
		Character _target;
		
		public float Health;
		public float BaseDamage;
		public GridBox CurrentBox;

		public bool HasDied => Health <= 0;

		public Character Target
		{
			get => _target;
			set
			{
				_target = value;
				
				// Some difference between classes so it becomes funnier to play
				if ((Class == CharacterClass.Archer && _target.Class == CharacterClass.Cleric) ||
				    (Class == CharacterClass.Cleric && _target.Class == CharacterClass.Paladin) ||
				    (Class == CharacterClass.Paladin && _target.Class == CharacterClass.Warrior) ||
				    (Class == CharacterClass.Warrior && _target.Class == CharacterClass.Archer))
				{
					DamageMultiplier += 0.25f;
				}
			}
		}
		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				Initial = string.IsNullOrWhiteSpace(_name) ? 'D' : _name[0];
			}
		}
		char Initial { get; set; }
		public CharacterClass Class { get; }
		public float DamageMultiplier { get; set; }
		public int PlayerIndex { get; }
		
		public Character(CharacterClass characterClass)
		{
			PlayerIndex = _nextAvailableIndex++;
			Health = DEFAULT_HEALTH;
			BaseDamage = DEFAULT_BASE_DAMAGE;
			DamageMultiplier = DEFAULT_DAMAGE_MULTIPLIER;
			Class = characterClass;
		}

		bool TakeDamage(float amount)
		{
			if (!((Health -= amount) <= 0))
			{
				Console.WriteLine($"{Name} took {amount} damage ({Health} health left)");
				return false;
			}
			
			Console.WriteLine($"{Name} took {amount} damage and died");
			return true;
		}

		public void WalkTo(Grid battlefield, int index)
		{
			if (CurrentBox != null)
			{
				CurrentBox.Occupied = false;
				CurrentBox.CharacterInitial = GridBox.EMPTY_CHARACTER_INITIAL;
			}

			CurrentBox = battlefield.grids[index];
			CurrentBox.CharacterInitial = Initial;
			CurrentBox.Occupied = true;
		}

		/**
		 * <summary> Applies the movement/attack to a character </summary>
		 * <param name="battlefield"> The grid this character is going to move in </param>
		 * <returns> A boolean indicating if the character changed its position </returns>
		 */
		public bool ApplyTurn(Grid battlefield)
		{
			if (HasDied) return false;
			
			if (battlefield.AreNeighboursOccupied(CurrentBox)) 
			{
				Attack(Target);
				return false;
			}
			
			// if there is no target close enough, calculates in which direction this character should move
			// to be closer to a possible target
			if (CurrentBox.lineIndex > Target.CurrentBox.lineIndex)
			{
				WalkTo(battlefield, CurrentBox.Index - 1);
				Console.WriteLine($"{Name} walked left");
				return true;
			}
			
			if (CurrentBox.lineIndex < Target.CurrentBox.lineIndex)
			{
				WalkTo(battlefield, CurrentBox.Index + 1);
				Console.WriteLine($"{Name} walked right");
				return true;
			}

			if (CurrentBox.columnIndex > Target.CurrentBox.columnIndex)
			{
				WalkTo(battlefield, CurrentBox.Index - battlefield.columnCount);
				Console.WriteLine($"{Name} walked up");
				return true;
			}

			if (CurrentBox.columnIndex < Target.CurrentBox.columnIndex)
			{
				WalkTo(battlefield, CurrentBox.Index + battlefield.columnCount);
				Console.WriteLine($"{Name} walked down");
				return true;
			}

			return false;
		}

		void Attack(Character target)
		{
			Console.WriteLine($"{Name} will attack {Target.Name}");
			target.TakeDamage(GetRandomDamage());
		}
		
		int GetRandomDamage()
		{
			return (int) (_rand.NextDouble() * BaseDamage * DamageMultiplier);
		}
	}
}
