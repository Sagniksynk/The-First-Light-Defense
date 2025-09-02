# The First Light Defense

![Gameplay GIF or Screenshot](https://via.placeholder.com/800x450.png?text=Gameplay+GIF+Here)

The First Light Defense is a classic 2D tower defense game built from the ground up in Unity. Players must strategically build and manage resources to defend their Headquarters (HQ) from increasingly difficult waves of enemies. The project is designed with a scalable architecture using Scriptable Objects and demonstrates a wide range of Unity features, including a responsive UI, dynamic audio systems, and post-processing effects.

## ✨ Core Features

### Gameplay & Strategy
* **Dynamic Wave System:** Survive endless waves of enemies that increase in number over time (`EnemyWaveManager.cs`).
* **Resource Management:** Construct resource generators to gather materials needed for your defenses (`ResourceManager.cs`, `ResourceGenerator.cs`).
* **Diverse Building Types:** Place a variety of structures, including resource generators and defensive towers, each with unique costs and abilities (`BuildingTypeSO.cs`).
* **Strategic Placement:** A robust building system checks for construction radius, proximity to other buildings, and clear terrain, with a visual ghost preview (`BuildingManager.cs`, `BuildingGhost.cs`).
* **Intelligent Enemies:** Enemies automatically target the nearest building or the main HQ, forcing strategic tower placement (`Enemy.cs`).
* **Defensive Towers:** Build towers that automatically detect and fire projectiles at incoming enemies (`Tower.cs`, `Arrow.cs`).

### UI & User Experience
* **Responsive UI:** The user interface is built to scale across different screen resolutions (`Canvas Scaler`).
* **Intuitive Building Menu:** A clean UI allows players to easily select and place buildings, with tooltips showing construction costs (`BuilidingTypeSelectUI.cs`, `TooltipUI.cs`).
* **Real-time Information:** Keep track of your resources, the current wave number, and a countdown to the next enemy attack (`ResourceUI.cs`, `EnemyWaveUI.cs`).
* **Game Over & Options:** A game over screen displays your performance, and an in-game options menu provides volume controls and the ability to pause or return to the main menu (`GameOverUI.cs`, `OptionsMenu.cs`).

### Audio & Visuals
* **Dynamic Audio System:** The game features a comprehensive sound manager for sound effects (`SoundManager.cs`) and a background music player that changes tracks based on the scene (`BackgroundMusicPlayer.cs`).
* **Zoom-based Ambient Sound:** The ambient sounds from buildings fade in and out based on the camera's zoom level (`BuildingSoundPlayer.cs`).
* **Advanced Camera Controls:** A Cinemachine-powered camera allows for smooth panning (keyboard or edge-scrolling) and zooming (`CameraHandler.cs`).
* **Juicy Feedback & Effects:** Visual feedback is enhanced with screen shake on explosions and chromatic aberration effects on key events (`ScreenShake.cs`, `PostProcessing_ChromaticAberration.cs`).
* **Day/Night Cycle:** A simple yet effective day/night cycle changes the global lighting over time (`DayNightCycle.cs`).
* **Dynamic Sorting:** Sprites are automatically sorted based on their Y-position to create a sense of depth (`SpritePositionSortingOrder.cs`).

## 🛠️ Project Architecture

This project is built around a data-driven architecture, making it easy to extend and modify.

* **Scriptable Objects:** Most game entities (buildings, resources) are defined using `ScriptableObject`s (`BuildingTypeSO.cs`, `ResourceTypeSO.cs`). This allows for easy creation of new content without changing code.
* **Singleton Managers:** Core systems like the `BuildingManager`, `ResourceManager`, and `EnemyWaveManager` are implemented as singletons for easy access and clear control over the game state.
* **Event-Driven Logic:** The code makes extensive use of C# events (`event EventHandler`) to decouple systems. For example, the UI updates automatically when resource amounts change, without being directly told to by the resource generator.

## 🚀 Getting Started

### Prerequisites
* **Unity 2022.3.x (LTS)** or a newer version is recommended.
* The project uses the following Unity packages, which should be installed via the Package Manager:
    * **Cinemachine** (`com.unity.cinemachine`)
    * **Universal RP (URP)** (`com.unity.render-pipelines.universal`)
    * **TextMeshPro** (`com.unity.textmeshpro`)

### Installation
1.  Clone the repository to your local machine:
    ```bash
    git clone https://github.com/Sagniksynk/The-First-Light-Defense.git
    ```
2.  Open Unity Hub and add the cloned project folder.
3.  Launch the project in the Unity Editor.
4.  The main scenes are located in the `Assets/Scenes` folder:
    * `MainMenuScene`: The entry point of the game.
    * `GameScene`: The core gameplay scene.

## 📱 Build Targets

The project is structured for multi-platform deployment. With the provided code, it can be built for:
* **PC (Standalone):** The default control scheme uses a keyboard and mouse.
* **WebGL:** Runs in a web browser.


## 📄 License

This project is licensed under the MIT License. See the `LICENSE` file for more details.
