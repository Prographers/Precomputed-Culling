# What is occlusion culling

Occlusion culling is the process of removing objects from the render pipeline that are not visible to the camera. Out of the box, Unity provides simple frustum testing algorithm. It is done by testing the object's bounding volume against the frustum of the camera. If the object is not within the camera's frustum, it is considered to be occluded and is not rendered.

# How frustum culling differs form precomputed occlusion culling

Frustum culling is a technique used to cull objects that are not visible from the current viewpoint. Precomputed part of the occlusion culling suggests that the processing of culling is done before the game is built for final release. This principle is leveraging an idea that developers will have a lot of time and processing power to generate occlusion data, and reduces the complexity of detecting what is occluded in the runtime.

Frustum culling does not remove from the rendering objects that are behind of other objects.

| Type | Visualsation | 
| --- | --- |
| **Precomputed culling off** | <img src="../images/culing_off.png" width="200" />|
| **Precomputed culling on** | <img src="../images/culing_on.png" width="200" /> |

# When should I use precomputed culling

Precomputed culling should be used for virtualy any 3D project. From fully open worlds, to closed, corridor based scenes. Scene with hundreds or thousands of objects ussualy without any culling algorithm can have significant GPU impact. Using precomputed culling can save a lot of GPU resources.

For **open world games**, in long view distant scenes you can save between **10-25% of GPU usage** ,depending on the scenery and occlusion level.

For **indor levels**, you can save up to **90% of the GPU usage**, depending on the scenery and occlusion level.

# How often should I bake culling data

Culling data while using precomputed culling should be baked for a few main reasons.

 - Before **EVERY** release.
 - Scene changed significantly and artifacts are showing up during the development (and disabling culling for development is not an option).
 - You want to test performance of your scene
 - You want to test occlusion bugs

We recommend setting up CI pipeline that would bake culling after any scene changes to always keep the latest data in the build, to not forget to run the pipeline. Baking everything before the release might not be the best option as bakes can last everywhere from few seconds to few hours.

# Why I should use Precomputed Culling over Unity's Umbra or other asset from asset store?

We created this asset for 3 main reasons.

1. Umbra doesn't support multi-scene setups, or addative scene setups.
2. Umbra bakes are low quality and bleed a lot of objects.
3. We often had issues with baking in batch mode.

<iframe width="560" height="315" src="https://www.youtube.com/embed/5deUO-gQKPM" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

With our solution we tried to address those issues and improve upon them.

We added features like:
 - Variants bake
 - Quality controll with pixel visiblity
 - Multiple areas with priorities
 - and many more...

