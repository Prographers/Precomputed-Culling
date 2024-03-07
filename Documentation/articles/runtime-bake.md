# Runtime Bake Guide

This guide will show you how to set up and use runtime occlusion culling.

## Setup

1. Create or open a scene on which you want to enable runtime baking.
2. From the top menu, press Tools > PrecomputedCulling > SetupRuntime or add elements manually:
    * Add PrecomputedSceneSettings to the scene.
    * Add PrecomputedCamera to the camera.
    * Add your implementation of BaseRuntimeBakeController or DemoRuntimeBakeController.
    * Add PrecomputedCullingRendererId to all renderers you want to be taken into account during the bake. Remember to set the correct id.
3. To trigger the bake, call BakeAll on your implementation of BaseRuntimeBakeController or DemoRuntimeBakeController.
4. To load data use LoadIndex on your IDataProvider.
5. Assign the PrecomputedData to the correct PrecomputedArea.
   See the DemoRuntimeBakeController for an example of how to use the runtime bake system.

**Note:** If you have a lot of objects to be considered during the bake, you can use `Tools > PrecomputedCulling > Setup meshes` to automatically add and setup PrecomputedCullingRendererId to all renderers in the scene.

## How it Works

Culling data generated during the bake is saved to a zip file located at StreamingAssets/PrecomputedData/PrecomputedData.zip. This file contains all the baked data separated into files for each bake. Data is serialized by a custom serializer called SimpleTextSerializer. Then, while loading, the data is unpacked and deserialized. The culling process works exactly the same as in the case of the pre-baked data.

## Customization

### Customizing the Data Storage, Serialization, and Deserialization

You are free to implement your custom provider and serializer that will fit your needs. To do this, implement IDataProvider and handle the serialization and deserialization of the data. Then you just have to pass your provider to the RuntimeBakeProcess.

### Customizing the Bake Process Indicator

You can, and probably should, also implement your own way of showing the progress of the bake. Our demo implementation uses a DemoRuntimeBakeProgress. You can swap it with your implementation of IBakeProgress. Then you have to pass it to the RuntimeBakeProcess as well.