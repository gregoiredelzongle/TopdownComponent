# TopdownComponent
Top-down Component for 2D games in Unity

![alt tag](images/github_tdcomponent01.gif?raw=true "Title")

The Top-downComponent uses a transformation matrix to simulate a 3D position into a 2D world. Notes that it's still experimental.

**TopdownComponent**
Drag  this component into a GameObject.
Then instead of "transform.position", you can now change the position in TopDownComponent, adjusting it's height with position.y like a 3D object
A custom inspector is included with custom handles, multi editing and multi move is supported
You can check the height of the object directly on scene by selecting it and enabling "show floor" on the inspector.

**TopdownDynamicLayerSorting**
Drag this component into any GameObject with a Renderer and a TopdownComponent to sort the sprite using the position.z axis in the TopdownComponent

Check the scene in the Samples folder for more informations

Art by Kenney (http://kenney.nl/)

