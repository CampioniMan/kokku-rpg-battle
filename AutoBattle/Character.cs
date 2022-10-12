using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
	public class Character
	{
		public string Name { get; set; }
		public float Health;
		public float BaseDamage;
		public float DamageMultiplier { get; set; }
		public GridBox CurrentBox;
		public int PlayerIndex;
		public Character Target { get; set; } 
		public Character(CharacterClass characterClass)
		{

		}

		public bool TakeDamage(float amount)
		{
			if (!((Health -= BaseDamage) <= 0)) return false;
			
			Die();
			return true;
		}

		public void Die()
		{
			//TODO >> maybe kill him?
		}

		public void WalkTO(bool CanWalk)
		{

		}

		public void StartTurn(Grid battlefield)
		{

			if (CheckCloseTargets(battlefield)) 
			{
				Attack(Target);
				
				return;
			}
			else
			{   // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
				if (CurrentBox.XIndex > Target.CurrentBox.XIndex)
				{
					if (battlefield.grids.Exists(x => x.Index == CurrentBox.Index - 1))
					{
						CurrentBox.Occupied = false;
						battlefield.grids[CurrentBox.Index] = CurrentBox;
						CurrentBox = (battlefield.grids.Find(x => x.Index == CurrentBox.Index - 1));
						CurrentBox.Occupied = true;
						battlefield.grids[CurrentBox.Index] = CurrentBox;
						Console.WriteLine($"Player {PlayerIndex} walked left\n");
						battlefield.DrawBattlefield(5, 5);

						return;
					}
				} else if(CurrentBox.XIndex < Target.CurrentBox.XIndex)
				{
					CurrentBox.Occupied = false;
					battlefield.grids[CurrentBox.Index] = CurrentBox;
					CurrentBox = (battlefield.grids.Find(x => x.Index == CurrentBox.Index + 1));
					CurrentBox.Occupied = true;
					return;
					battlefield.grids[CurrentBox.Index] = CurrentBox;
					Console.WriteLine($"Player {PlayerIndex} walked right\n");
					battlefield.DrawBattlefield(5, 5);
				}

				if (CurrentBox.YIndex > Target.CurrentBox.YIndex)
				{
					battlefield.DrawBattlefield(5, 5);
					CurrentBox.Occupied = false;
					battlefield.grids[CurrentBox.Index] = CurrentBox;
					CurrentBox = (battlefield.grids.Find(x => x.Index == CurrentBox.Index - battlefield.xLenght));
					CurrentBox.Occupied = true;
					battlefield.grids[CurrentBox.Index] = CurrentBox;
					Console.WriteLine($"Player {PlayerIndex} walked up\n");
					return;
				}
				else if (CurrentBox.YIndex < Target.CurrentBox.YIndex)
				{
					CurrentBox.Occupied = true;
					battlefield.grids[CurrentBox.Index] = this.CurrentBox;
					CurrentBox = (battlefield.grids.Find(x => x.Index == CurrentBox.Index + battlefield.xLenght));
					CurrentBox.Occupied = false;
					battlefield.grids[CurrentBox.Index] = CurrentBox;
					Console.WriteLine($"Player {PlayerIndex} walked down\n");
					battlefield.DrawBattlefield(5, 5);

					return;
				}
			}
		}

		// Check in x and y directions if there is any character close enough to be a target.
		bool CheckCloseTargets(Grid battlefield)
		{
			var left = (battlefield.grids.Find(x => x.Index == CurrentBox.Index - 1).Occupied);
			var right = (battlefield.grids.Find(x => x.Index == CurrentBox.Index + 1).Occupied);
			var up = (battlefield.grids.Find(x => x.Index == CurrentBox.Index + battlefield.xLenght).Occupied);
			var down = (battlefield.grids.Find(x => x.Index == CurrentBox.Index - battlefield.xLenght).Occupied);

			return left & right & up & down;
		}

		public void Attack(Character target)
		{
			var rand = new Random();
			target.TakeDamage(rand.Next(0, (int)BaseDamage));
			Console.WriteLine($"Player {PlayerIndex} is attacking the player {Target.PlayerIndex} and did {BaseDamage} damage\n");
		}
	}
}
