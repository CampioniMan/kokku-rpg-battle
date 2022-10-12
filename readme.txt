# Auto-Battle RPG Game
This is a simple turn based RPG game, it was developed as a test for Kokku.

## Platforms Used
- IntelliJ Rider (2022.1.1)

## Documentation
The project already had a few of the procedures and business logics already implemented, here goes a little bit of what I did to improve it:

### Setup
Here I will document the changes I've made before actually coding anything in particular.

1. Changed the indentation from spaces to tabs;
	The project was small enough and it was early in the stages of its implementation, so it wasn't a big change.
2. Applied a code style of my own choice;

### Code
This is a description of which code-related changes I think are important to talk about.

1. I started by reviewing the code inside the main file 'Program.cs'
	- A lot of the methods were doing stuff they weren't supposed to do, instead of a the Main() method calling the necessary functions in the correct order, the methods themselves were calling each other when they were done, which is not a good practice sinse they're doing way more then what they were supposed to do.;
	- Changed the way 'GetPlayerChoice()' was implemented, making it just return a value of the selected character;
	- Changed the way 'CreatePlayerCharacter()' was implemented, making it just return a new Character;
	- Changed the way the class 'Character' is constructed so there's no need for the programmer to care about the variable Index, preventing future programming mistakes since it's supposed to be unique for each Character;
	- Changed 'CreateEnemyCharacter()' so there's no need to change this function in the future if more classes are added or removed from the CharacterClass enum;
	- Removed both 'GetPlayerPosition()' and 'GetEnemyPosition()' and replaced them with 'GetAvailablePositionFromGrid()' which is a generic implementation for its purpose.
	- Replaced the usages of '\n' to 'Environment.NewLine'
2. Then I changed the 'Grid' class to make it work and to also be more generic
	- Modified 'DrawBattlefield()' so it uses the grid information from the specific instance
	