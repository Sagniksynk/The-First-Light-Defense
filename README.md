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

<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Built With


* [![Unity][Unity.com]][Unity-url]
* [![C#][CSharp.net]][CSharp-url]


<p align="right">(<a href="#readme-top">back to top</a>)</p>

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





<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/othneildrew/Best-README-Template.svg?style=for-the-badge
[contributors-url]: https://github.com/othneildrew/Best-README-Template/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/othneildrew/Best-README-Template.svg?style=for-the-badge
[forks-url]: https://github.com/othneildrew/Best-README-Template/network/members
[stars-shield]: https://img.shields.io/github/stars/othneildrew/Best-README-Template.svg?style=for-the-badge
[stars-url]: https://github.com/othneildrew/Best-README-Template/stargazers
[issues-shield]: https://img.shields.io/github/issues/othneildrew/Best-README-Template.svg?style=for-the-badge
[issues-url]: https://github.com/othneildrew/Best-README-Template/issues
[license-shield]: https://img.shields.io/github/license/othneildrew/Best-README-Template.svg?style=for-the-badge
[license-url]: https://github.com/othneildrew/Best-README-Template/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/othneildrew
[product-screenshot]: images/screenshot.png
[Next.js]: https://img.shields.io/badge/next.js-000000?style=for-the-badge&logo=nextdotjs&logoColor=white
[Next-url]: https://nextjs.org/
[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[React-url]: https://reactjs.org/
[Vue.js]: https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D
[Vue-url]: https://vuejs.org/
[Angular.io]: https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white
[Angular-url]: https://angular.io/
[Svelte.dev]: https://img.shields.io/badge/Svelte-4A4A55?style=for-the-badge&logo=svelte&logoColor=FF3E00
[Svelte-url]: https://svelte.dev/
[Laravel.com]: https://img.shields.io/badge/Laravel-FF2D20?style=for-the-badge&logo=laravel&logoColor=white
[Laravel-url]: https://laravel.com
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white
[JQuery-url]: https://jquery.com 
[Unity.com]: https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white
[Unity.com]: https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white
[Unity-url]: https://unity.com/

[CSharp.net]: https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white
[CSharp-url]: https://learn.microsoft.com/en-us/dotnet/csharp/
