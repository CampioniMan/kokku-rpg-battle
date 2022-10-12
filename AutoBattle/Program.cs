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
			CharacterClass playerCharacterClass;
			GridBox playerCurrentLocation;
			GridBox enemyCurrentLocation;
			Character playerCharacter;
			Character enemyCharacter;
			var allPlayers = new List<Character>();
			var currentTurn = 0;
			var numberOfPossibleTiles = grid.grids.Count;
			Setup(); 
			
			void Setup()
			{
				GetPlayerChoice();
			}

			void GetPlayerChoice()
			{
				//asks for the player to choose between for possible classes via console.
				Console.WriteLine("Choose Between One of this Classes:\n");
				Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
				//store the player choice in a variable
				var choice = Console.ReadLine();

				switch (choice)
				{
					case "1":
						CreatePlayerCharacter(Int32.Parse(choice));
						break;
					case "2":
						CreatePlayerCharacter(Int32.Parse(choice));
						break;
					case "3":
						CreatePlayerCharacter(Int32.Parse(choice));
						break;
					case "4":
						CreatePlayerCharacter(Int32.Parse(choice));
						break;
					default:
						GetPlayerChoice();
						break;
				}
			}

			void CreatePlayerCharacter(int classIndex)
			{
				var characterClass = (CharacterClass)classIndex;
				Console.WriteLine($"Player Class Choice: {characterClass}");
				playerCharacter = new Character(characterClass)
				{
					Health = 100,
					BaseDamage = 20,
					PlayerIndex = 0
				};

				CreateEnemyCharacter();
			}

			void CreateEnemyCharacter()
			{
				//randomly choose the enemy class and set up vital variables
				var rand = new Random();
				var randomInteger = rand.Next(1, 4);
				var enemyClass = (CharacterClass)randomInteger;
				Console.WriteLine($"Enemy Class Choice: {enemyClass}");
				enemyCharacter = new Character(enemyClass)
				{
					Health = 100
				};
				playerCharacter.BaseDamage = 20;
				playerCharacter.PlayerIndex = 1;
				StartGame();
			}

			void StartGame()
			{
				//populates the character variables and targets
				enemyCharacter.Target = playerCharacter;
				playerCharacter.Target = enemyCharacter;
				allPlayers.Add(playerCharacter);
				allPlayers.Add(enemyCharacter);
				AllocatePlayers();
				StartTurn();
			}

			void StartTurn()
			{
				if (currentTurn == 0)
				{
					//AllPlayers.Sort();  
				}

				foreach (var character in allPlayers)
				{
					character.StartTurn(grid);
				}

				currentTurn++;
				HandleTurn();
			}

			void HandleTurn()
			{
				if (playerCharacter.Health == 0)
				{
					return;
				}
				else if (enemyCharacter.Health == 0)
				{
					Console.Write(Environment.NewLine + Environment.NewLine);

					// endgame?

					Console.Write(Environment.NewLine + Environment.NewLine);

					return;
				}
				else
				{
					Console.Write(Environment.NewLine + Environment.NewLine);
					Console.WriteLine("Click on any key to start the next turn...\n");
					Console.Write(Environment.NewLine + Environment.NewLine);

					var key = Console.ReadKey();
					StartTurn();
				}
			}

			int GetRandomInt(int min, int max)
			{
				var rand = new Random();
				var index = rand.Next(min, max);
				return index;
			}

			void AllocatePlayers()
			{
				AllocatePlayerCharacter();
			}

			void AllocatePlayerCharacter()
			{
				var random = 0;
				var randomLocation = grid.grids.ElementAt(random);
				Console.Write($"{random}\n");
				if (!randomLocation.Occupied)
				{
					var playerCurrentLocation = randomLocation;
					randomLocation.Occupied = true;
					grid.grids[random] = randomLocation;
					playerCharacter.CurrentBox = grid.grids[random];
					AllocateEnemyCharacter();
				}
				else
				{
					// TODO remove infinite loop
					AllocatePlayerCharacter();
				}
			}

			void AllocateEnemyCharacter()
			{
				var random = 24;
				var randomLocation = grid.grids.ElementAt(random);
				Console.Write($"{random}\n");
				if (!randomLocation.Occupied)
				{
					enemyCurrentLocation = randomLocation;
					randomLocation.Occupied = true;
					grid.grids[random] = randomLocation;
					enemyCharacter.CurrentBox = grid.grids[random];
					grid.DrawBattlefield(5 , 5);
				}
				else
				{
					// TODO remove infinite loop
					AllocateEnemyCharacter();
				}
			}
		}
	}
}
