# Prographers Precomputed Culling
![Precomputed_Culling_Logo](https://user-images.githubusercontent.com/5092066/170501889-39cf1828-8557-4f1b-9e9c-2856f41bd3d2.png)

Pixel perfect occlusion system that will significantly improve the performance of your scenes, without almost any overhead.

Using time on your computer, precompute/bake your scenes to gain a significant performance boost. The occlusion culling algorithm will find all visible meshes from each point on your scene, using the color-coding algorithm. Then it will save them efficiently and load only when needed.

Heavily occluded scenes like a two-story house with the interior on all floors gained us up to 240% in frame rate!. From 30fps to 72fps on Quest 2.
Open world scenes with flat chunked terrain, gained us 10-25%. From 47fps to 60fps on low-end mobile.

# Sitemap

[Documentation](articles/intro.html)

[API Reference](api/index.html)