using System;
using static AutoBattle.Types;

namespace AutoBattle
{
	public class Character
	{
		const int DEFAULT_BASE_HEALTH = 100;
		const int DEFAULT_BASE_DAMAGE = 20;
		
		public string Name { get; set; }
		public float Health;
		public float BaseDamage;
		public float DamageMultiplier { get; set; }
		public GridBox CurrentBox;
		public int PlayerIndex { get; }
		public Character Target { get; set; }

		static int _nextAvailableIndex = 0;
		
		public Character(CharacterClass characterClass)
		{
			PlayerIndex = _nextAvailableIndex++;
			Health = DEFAULT_BASE_HEALTH;
			BaseDamage = DEFAULT_BASE_DAMAGE;
		}

		bool TakeDamage(float amount)
		{
			if (!((Health -= amount) <= 0)) return false;
			
			Die();
			return true;
		}

		void Die()
		{
			Console.WriteLine($"Player {PlayerIndex} has died{Environment.NewLine}");
		}

		public void WalkTo(bool canWalk)
		{
		}

		/**
		 * <summary> Applies the movement/attack to a character </summary>
		 * <param name="battlefield"> The grid this character is going to move in </param>
		 * <returns> A boolean indicating if the character changed its position </returns>
		 */
		public bool ApplyTurn(Grid battlefield)
		{
			if (battlefield.AreNeighboursOccupied(CurrentBox)) 
			{
				Attack(Target);
				return false;
			}
			
			// if there is no target close enough, calculates in which direction this character should move
			// to be closer to a possible target
			if (CurrentBox.lineIndex > Target.CurrentBox.lineIndex)
			{
				MoveTo(CurrentBox.Index - 1);
				Console.WriteLine($"Player {PlayerIndex} walked left");
				return true;
			}
			
			if (CurrentBox.lineIndex < Target.CurrentBox.lineIndex)
			{
				MoveTo(CurrentBox.Index + 1);
				Console.WriteLine($"Player {PlayerIndex} walked right");
				return true;
			}

			if (CurrentBox.columnIndex > Target.CurrentBox.columnIndex)
			{
				MoveTo(CurrentBox.Index - battlefield.lineCount);
				Console.WriteLine($"Player {PlayerIndex} walked up");
				return true;
			}

			MoveTo(CurrentBox.Index + battlefield.lineCount);
			Console.WriteLine($"Player {PlayerIndex} walked down");
			return true;

			void MoveTo(int index)
			{
				Console.WriteLine($"Moving player {PlayerIndex} from {CurrentBox.Index} to {index}");
				CurrentBox.Occupied = false;
				CurrentBox = battlefield.grids[index];
				CurrentBox.Occupied = true;
			}
		}

		void Attack(Character target)
		{
			var rand = new Random();
			target.TakeDamage(rand.Next(0, (int)BaseDamage));
			Console.WriteLine($"Player {PlayerIndex} is attacking the player {Target.PlayerIndex} and did {BaseDamage} damage{Environment.NewLine}");
		}
	}
}
