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

		void TakeDamage(float amount)
		{
			if (!((Health -= amount) <= 0))
			{
				Console.WriteLine($"{Name} took {amount} damage ({Health} health left)");
				return;
			}
			
			Console.WriteLine($"{Name} took {amount} damage and died");
		}

		public void WalkTo(Grid battlefield, GridBox nextBox)
		{
			if (CurrentBox != null)
			{
				CurrentBox.Occupied = false;
				CurrentBox.CharacterInitial = GridBox.EMPTY_CHARACTER_INITIAL;
				battlefield.AvailableBoxIndexes.Add(CurrentBox.Index);
			}

			CurrentBox = nextBox;
			CurrentBox.CharacterInitial = Initial;
			CurrentBox.Occupied = true;
			battlefield.AvailableBoxIndexes.Remove(CurrentBox.Index);
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
			// to be closer to a possible target. Variable 'nextBox' should never be null in this situation.
			var directionToMove = GetTargetsDirection();
			battlefield.TryGetNeighbour(CurrentBox, directionToMove, out var nextBox);
			WalkTo(battlefield, nextBox);
			return true;
		}

		PossibleDirection GetTargetsDirection()
		{
			var shouldMoveLeft = CurrentBox.lineIndex > Target.CurrentBox.lineIndex;
			var shouldMoveRight = CurrentBox.lineIndex < Target.CurrentBox.lineIndex;
			var shouldMoveUp = CurrentBox.columnIndex > Target.CurrentBox.columnIndex;
			var shouldMoveDown = CurrentBox.columnIndex < Target.CurrentBox.columnIndex;

			if (shouldMoveLeft)
			{
				if (shouldMoveUp) return PossibleDirection.UpperLeft;
				if (shouldMoveDown) return PossibleDirection.LowerLeft;
				return PossibleDirection.Left;
			}
			if (shouldMoveRight)
			{
				if (shouldMoveUp) return PossibleDirection.UpperRight;
				if (shouldMoveDown) return PossibleDirection.LowerRight;
				return PossibleDirection.Right;
			}
			return shouldMoveUp ? PossibleDirection.Up : PossibleDirection.Down;
		}

		void Attack(Character target)
		{
			target.TakeDamage(GetRandomDamage());
		}
		
		int GetRandomDamage()
		{
			return (int) (_rand.NextDouble() * BaseDamage * DamageMultiplier);
		}
	}
}
