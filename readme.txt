# Auto-Battle RPG Game
This is a simple turn based RPG game, it was developed as a test for Kokku.

## Platforms Used
- IntelliJ Rider (2022.1.1)

## Documentation
The project already had a few of the procedures and some of the business logic already implemented, here goes a little bit of what I did to improve it:

### Setup
Here I will document the changes I've made before actually coding anything in particular.

1. Changed the indentation from spaces to tabs;
	The project was small enough and it was early in the stages of its implementation, so it wasn't a big change.
2. Applied a code style of my own choice;

### Code
This is a description of which code-related changes I think are important to talk about.

1. I started by reviewing the code inside the main file 'Program.cs':
	- A lot of the methods were doing stuff they weren't supposed to do, instead of a the Main() method calling the necessary functions in the correct order, the methods themselves were calling each other when they were done, which is not a good practice sinse they're doing way more then what they were supposed to do;
	- Replaced the recursive calls by loops, which prevents stack overflows and makes the infinite loop problems more evident;
	- Changed the way 'GetPlayerChoice()' was implemented, making it just return a value of the selected character;
	- Changed the way 'CreatePlayerCharacter()' was implemented, making it just return a new Character;
	- Changed the way the class 'Character' is constructed so there's no need for the programmer to care about the variable Index, preventing future programming mistakes since it's supposed to be unique for each Character;
	- Changed 'CreateEnemyCharacter()' so there's no need to change this function in the future if more classes are added or removed from the CharacterClass enum;
	- Removed both 'GetPlayerPosition()' and 'GetEnemyPosition()' and replaced them with 'GetAvailablePositionFromGrid()' which is a generic implementation for its purpose;
	- Replaced the usages of '\n' to 'Environment.NewLine';
	- Added a 'ExtensionsUtils' static class so I could add some generic extra methods/variables when needed;
2. Then I changed the 'Grid' class to make it work and to also be more generic:
	- Modified 'DrawBattlefield()' so it uses the grid information from the specific instance;
3. About the 'Character' class
	- I decided to leave its constructor with the 'CharacterClass' variable because it sound like it will be useful in the future. I used it for a business logic I invented but it doesn't need to be used that way;
	- Removed the usage of 'battlefield.grids.Find()' when using 'CheckCloseTargets()' since it isn't necessary for a simple function like this. It had transformed a simple O(1) function into a O(n*m), n and m being the #lines and #columns respectively;
	- Moved the function 'CheckCloseTargets()' to the Grid class and renamed it to 'AreNeighboursOccupied()' so it becomes more generic while also belonging to the class related to the battlefield;
	- Showing the moves of the characters using their names instead of their indexes;
4. About the 'Types' file
	- Separated it into many files, each one with its own context;
	- Transformed 'GridBox' from a struct into a class to prevent future mistakes from happening (wrong references because someone forgot to change the original reference instead of a copy);
	- I decided to remove both 'CharacterClassSpecific' and 'CharacterSkills' since these classes define features in a very detailed way, but they're not gonna be implemented by now, so there's no need to maintain those definitions;
	- Added an initial for the character in question so it's possible to figure out which character is the player and which one is the enemy;
5. Generally
    - I added the 8 direction move functionality; 
    - I decided not to use Linq since I'm using .NET 3.1 and I'm not sure if Linq has its newest optimizations already implemented in this specific version;


### Business Logic
Here are some decision I took based on what I was able to understand from the given requisites:

`Each team should have one move per turn (except when the move places the character in attack range of an opposing team character)`

When I read this for the first time I was able to understand that the character could only change its position if it won't attack in the current turn, but it could also mean that the player attacks and changes its position in the same turn depending on the definition of the word "move" in this context (in the game Civilization VI in some cases you're able to attack and move in the same turn, for example). I ended up going with the first option.

I also added some business logic when a character's target is set so the game becomes more fun to watch.

I made an earlier decision when looking at the code and seeing the requirements for this project: I will only add support for the 1v1 auto-battle. I wasn't looking forward into adding multiple characters per team because of time constraints and the code reflects that;

It's more a design thing, but I decided to get rid of most of the new lines that were printed, since there were a lot of WriteLines with multiple new lines.

I set the default player names as 'Clark Kent' and 'Lex Luthor' which is a reference to Superman.
