using System;
using static AutoBattle.Character;
using static AutoBattle.Grid;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
	class Program
	{
		static void Main(string[] args)
		{
			var grid = new Grid(5, 5);
			var allPlayers = new List<Character>();
			var currentTurn = 0;
			var numberOfPossibleTiles = grid.grids.Count;
			
			var characterClass = GetPlayerChoice();
			var playerCharacter = CreatePlayerCharacter(characterClass);
			var enemyCharacter = CreateEnemyCharacter();
			
			//populates the character variables and targets
			enemyCharacter.Target = playerCharacter;
			playerCharacter.Target = enemyCharacter;
			
			playerCharacter.CurrentBox = GetAvailablePositionFromGrid(grid);
			enemyCharacter.CurrentBox = GetAvailablePositionFromGrid(grid);
			
			allPlayers.Add(playerCharacter);
			allPlayers.Add(enemyCharacter);
			
			// Making the first one to move be random
			allPlayers = allPlayers.Shuffle();

			do
			{
				grid.DrawBattlefield();
				foreach (var character in allPlayers)
				{
					character.StartTurn(grid);
				}
				currentTurn++;
				
				WaitInputToNextTurn();
			} while (!HasGameFinished());
			
			void WaitInputToNextTurn()
			{
				Console.Write(Environment.NewLine + Environment.NewLine);
				Console.WriteLine($"Click on any key to start the next turn (currently on {currentTurn})...{Environment.NewLine}");
				Console.Write(Environment.NewLine + Environment.NewLine);
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
				Console.WriteLine($"Choose Between One of this Classes:{Environment.NewLine}");
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

		static Character CreatePlayerCharacter(CharacterClass characterClass)
		{
			Console.WriteLine($"Player Class Choice: {characterClass}");
			return new Character(characterClass)
			{
				Health = 100,
				BaseDamage = 20
			};
		}

		static Character CreateEnemyCharacter()
		{
			//randomly choose the enemy class and set up vital variables
			var rand = new Random();
			var possibleClasses = Enum.GetValues(typeof(CharacterClass));
			var enemyClass = (CharacterClass)possibleClasses.GetValue(rand.Next(possibleClasses.Length))!;
			Console.WriteLine($"Enemy Class Choice: {enemyClass}");
			return new Character(enemyClass)
			{
				Health = 100,
				BaseDamage = 20
			};
		}

		int GetRandomInt(int min, int max)
		{
			var rand = new Random();
			var index = rand.Next(min, max);
			return index;
		}

		static GridBox GetAvailablePositionFromGrid(Grid grid)
		{
			var rand = new Random();
			// TODO remove infinite loop
			while (true)
			{
				var random = rand.Next(grid.grids.Count);
				var randomLocation = grid.grids.ElementAt(random);
				Console.Write($"{random}{Environment.NewLine}");
				if (!randomLocation.Occupied)
				{
					return randomLocation;
				}
			}
		}
	}

	internal static class ExtensionsUtils
	{
		public static List<T> Shuffle<T>(this List<T> list)
		{
			var rng = new Random();
			return list.OrderBy(a => rng.Next()).ToList();
		}
	}
}
