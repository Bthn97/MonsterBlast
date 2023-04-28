# Monster Blast

# Still in Progress...

Monster Blast is a 2D grid-based matching game where players tap adjacent monster sprites to create matches of three or more of the same type. The game is built using Unity and follows various design patterns to ensure a clean and maintainable codebase.

<img width="278" alt="Screenshot 2023-04-28 at 12 22 01" src="https://user-images.githubusercontent.com/34586769/235109292-19f124ce-58d8-40a6-9e22-b9823a5a6a50.png">


## Features 🚁

- Tap adjacent monster sprites to create matches
- Matches of 3 or more of the same monster type are removed from the board
- New monsters spawn at the top of the board to replace removed ones
- Utilizes a grid system to manage the board and check for matches
- Intuitive and easy-to-use UI

## Design Patterns 🧑‍🚀

The following design patterns are implemented in this project to improve code organization and maintainability:

- **Singleton Pattern**: Used for managing the unique instances of various manager classes, such as InputManager and BoardStateManager.
- **Object Pooling Pattern**: Efficiently reuses game objects (e.g., monster sprites) to minimize memory usage and optimize performance.
- **Command Pattern**: Encapsulates user inputs as objects, allowing for easy management and possible undo/redo functionality.
- **State Pattern**: Manages the game's different states (e.g., initializing, idle, matching) to ensure appropriate behaviors and transitions.
- **Observer Pattern**: Allows for decoupling of components by using events and listeners, ensuring a flexible and modular architecture.

## How to Play ✈️

1. Clone or download this repository.
2. Open the project in Unity (version 2021.1.9 or later recommended).
3. Open the `Scenes` folder in the `Assets` folder and double-click the `Level` to load the game.
4. Press the Play button in the Unity Editor to start the game.
5. Click and drag adjacent monster sprites to swap their positions and create matches.

## Contributing

Pull requests and bug reports are welcome. For major changes, please open an issue first to discuss what you would like to change.


 
