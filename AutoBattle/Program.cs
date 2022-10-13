using System;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
	class Program
	{
		static void Main(string[] args)
		{
			var grid = new Grid(10, 10);
			var allPlayers = new List<Character>();
			var currentTurn = 0;
			
			var characterClass = GetPlayerChoice();
			var playerCharacter = new Character(characterClass)
			{
				Name = "Clark Kent"
			};
			var enemyCharacter = CreateEnemyCharacter();

			PuPlayerOnGrid(playerCharacter, grid);
			PuPlayerOnGrid(enemyCharacter, grid);
			
			enemyCharacter.Target = playerCharacter;
			playerCharacter.Target = enemyCharacter;
			
			allPlayers.Add(playerCharacter);
			allPlayers.Add(enemyCharacter);
			
			// Making the first one to move/attack be random
			allPlayers = allPlayers.Shuffle();
			grid.DrawBattlefield();

			do
			{
				WaitInputToNextTurn();
				
				var hasAnyCharacterMoved = false;
				foreach (var character in allPlayers)
				{
					hasAnyCharacterMoved |= character.ApplyTurn(grid);
				}
				
				if (hasAnyCharacterMoved)
				{
					grid.DrawBattlefield();
				}
				
				currentTurn++;
			} while (!HasGameFinished());

			if (playerCharacter.HasDied)
			{
				Console.WriteLine($"Player {enemyCharacter.Name} has won the game!");
			}
			else
			{
				Console.WriteLine($"Player {playerCharacter.Name} has won the game!");
			}
			
			void WaitInputToNextTurn()
			{
				Console.WriteLine();
				Console.WriteLine($"Click on any key to start the next turn (currently on {currentTurn})...");
				Console.WriteLine();
				_ = Console.ReadKey();
			}

			bool HasGameFinished()
			{
				return !allPlayers.TrueForAll(player => player.Health > 0);
			}
		}

		static CharacterClass GetPlayerChoice()
		{
			while (true)
			{
				//asks for the player to choose between for possible classes via console.
				Console.WriteLine("Choose Between One of this Classes:");
				Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
				//store the player choice in a variable
				var typedClass = Console.ReadLine();
				
				var hasParsed = uint.TryParse(typedClass, out var choice);
				if (string.IsNullOrEmpty(typedClass) || !hasParsed) continue;
				
				var isValueDefined = Enum.IsDefined(typeof(CharacterClass), choice);
				if (!isValueDefined) continue;

				return (CharacterClass)choice;
			}
		}

		static Character CreateEnemyCharacter()
		{
			//randomly choose the enemy class and set up vital variables
			var possibleClasses = Enum.GetValues(typeof(CharacterClass));
			var enemyClass = (CharacterClass)possibleClasses.GetValue(
				ExtensionsUtils.RandomGenerator.Next(possibleClasses.Length))!;
			Console.WriteLine($"Enemy Class Choice: {enemyClass}");
			return new Character(enemyClass)
			{
				Name = "Lex Luthor"
			};
		}

		static void PuPlayerOnGrid(Character character, Grid battlefield)
		{
			if (battlefield.AvailableBoxIndexes.Count == 0)
			{
				throw new Exception("There are not enough available spaces on the battlefield for a player to join");
			}

			var random = ExtensionsUtils.RandomGenerator.Next(battlefield.AvailableBoxIndexes.Count);
			var randomLocation = battlefield.AvailableBoxIndexes[random];
			character.WalkTo(battlefield, battlefield.Grids[randomLocation]);
		}
	}

	internal static class ExtensionsUtils
	{
		public static readonly Random RandomGenerator = new Random();
		public static List<T> Shuffle<T>(this List<T> list)
		{
			return list.OrderBy(a => RandomGenerator.Next()).ToList();
		}
	}
}
