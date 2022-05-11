# Precomputed Culling
Pixel perfect occlusion system that will significantly improve the performance of your scenes, without almost any overhead.

Using time of your computer, precompute/bake your scenes to gain a significant performance boost. The occlusion culling algorithm will find all visible meshes from each point on your scene, using the color-coding algorithm. Then it will save them efficiently and load only when needed.

Heavily occluded scenes like a two-story house with the interior on all floors gained us up to 240% in frame rate!. From 30fps to 72fps on Quest 2.
Open world scenes with flat chunked terrain, gained us 10-25%. From 47fps to 60fps on low-end mobile.

**Documentation:**

https://prographers.com/Precomputed-Culling/ (WIP)

https://github.com/Prographers/Precomputed-Culling/

**Asset Store:**

https://assetstore.unity.com/packages/slug/221372

### Features:
- Support for URP
- Support for multiple cameras
- Support for multiple scenes with multiple variants and dynamic loading and unloading
- Lightweight with an option for additional compression
- Supports transparency
- Supports LODs
- Supports Unity Terrain
- Supports 2D Sprites*
- Runtime precision control between quality and performance
- Support for all platforms including mobile, VR, AR, and WebGL
- API for custom building pipelines
- Everything is written in C#, full source code included
- Code documentation + external documentation with tutorials
- Easy to setup
- Examples and Demos
- Support via email or discord

*Not recommended to use in 2D games, intended use is for example leaves on the 3D trees or far away rocks.

### Limitations:
- Required to bake scenes and their variants before build.

### Roadmap:
- Support for Legacy (built-in) and HDRP
- Support for dynamic objects
- Support for occlusion portals
- Split baking between multiple computers
- Improve baking times
- Better debugging tools and statistics
- Dynamic lighting shadow
- and more...

## Installation:

1. In Unity Editor, Open Package manager
2. In "My assets" select `Precomputed Culling` and press "Install"
3. Done!

## Basic Usage:

For more advanced usages visit https://prographers.com/Precomputed-Culling/ or related section in the repository https://github.com/Prographers/Precomputed-Culling/

1. Open the main scene where you want to initialize your setup
2. In the top menu, press Tools->Precomputed Culling->Setup
3. Adjust Precomputed Volume/Area to match play area of you player (note you can have multiple areas)
4. Adjust cell density and camera controll (Find best setting between bake Quality and Bake time and performance).
4. Start bake either in Tools->Precomputed Culling->Bake or in Area
5. Wait for bake to finish
6. Enjoy increase in performance

## Recomendation:
### Open world areas: 
 - Chunk size should be around 5 meters width by 5 meters height by top jump height.
 - At least 3 cameras in the middle. At top of jump height; At eye level; At croutch eye level
 - Allow for taking data from neighbouring cells in disnace of 1 reduce agressivnes of the algorithm
 - Set runtime pixel requirement to 50-100 pixels
 - Allow for baking terrain
 - Allow for sprites baking (If envirment is not a pure 3D models)

### Small Areas (building):
 - Chunk size should be around 1 meter by 1 meter by height of the building
 - At least 18 cameras. All spreadout within the cell. At top of jump height; At eye level; At croutch eye level.
 - (Optional if experiencing artifacts) Allow for taking data from neighbouring cells in disnace of 1 reduce agressivnes of the algorithm
 - Set runtime pixel requirement to 0 - 25 pixels
 - Allow for baking transparents between 1 or 2 layers

More dense areas should have bigger priority then less dense areas.
