# Space Shooter by Pol Vega

## Architecture
The architecture has been chosen to have a code base easy to scale and to be able to work with many people, all without losing performance. But, also limiting its scope to an accessible implementation within a short period of time.

It's a mixture between an approach to an MVC architecture and some principles of Clean Architecture by Robert C. Martin. It also adds some functionalities to maximize the control of the information.

This are some of the benefits of this election:

- Highly deployed from the engine. It's not likely that a game will change the engine but it makes less traumatic to change some key systems in a project. This is possible because the game logic won't depend on those specifics.

- Allows to create unit tests easier since the logic is quite isolated.

- Allows your code base to be agnostic of where your data comes from (server, local, etc).

- It enforces the features to be decoupled from each other since the logic is encapsulated in small blocks instead of big directors that have a lot of responsibility.

Obviously, it also has some cons that need to be taken in to account:

- As everything is quite decoupled, sometimes it is less easy to follow the execution order.

- It adds some boilerplate since the architecture needs to follow a clear structure, which on the other hand can be useful for big teams to make sure the codebase keeps uniformed.

- It's not super fast to create new features so it's usually bad for prototyping.

-------------------------------

## Implementation

Let's take a look on how it works in this project:

### Objects
The reason for the existence of Objects is to encapsulate the logic of each feature in a small block. Each feature will be handled by four principal classes, following an approach of MVC architecture adapted to Unity. Those are Mediator, Model, View, and Data.

#### Model
It contains and operates the data of the object. It will perform minimal logic operations and its functionality is mainly to store the values.

#### View
Just plane MonoBehaviours with the needed components that will be updated referenced. Is the responsible on operating directly on the Unity Engine.

#### Mediator
As the name says, is the one in charge of mediating between Model and View. Observes the changes and acts in consequence. It does not store data, and it does not directly touch the engine. It just mediates, so all the main logic is here.

#### Data
The differentiating factor for each Instance of the same Object. Contains all values that will be directly handled by the Model. Velocity, appearance, type of projectiles... Here is where the user can define how he wants to create this instance.

It uses ScriptableObject as a support, but it will not be a big deal to migrate it to a server.

#### Representation
The class that wraps it all. It ensures Model, View and Mediator stick together, so the classes that want to interactuate with this block can easily deal with it.

However, its main reason of existence is to maximize poolability, so when an object is pooled, it maintains the reference to its Mediator and Model, avoiding creating them each time an object makes dispose and its consequential garbage collector execution.

#### Repository
The Service is called when someone wants to create (or recover from the Pool) a new Object. Is a simple MonoBehaviour that just has a reference to the View of the Object, and receives the desired Data as a parameter.

#### Factory
The engine behind the object's functionality. Is a camouflaged pool in charge of handling the creation and retrieving of objects.

### Flow
A custom manager that handles Update and Collisions of all Objects. This layer could be removed and Unity will handle its functionality. However, it is created to avoid the usage of nonoptimal Unity's Update and Collision systems, and gain control over the execution order.

#### IUpdatable
Each inheritor from an Updatable interface will be updated from the MainFlow.

#### ICollider
Each inheritor from a Collider interface will be notified in case it receives a collision from another object in its same layer. The collision layers system is based on Unity's one, but with the usage of custom editors it searches to be totally independent. In the CollisionMatrix file the user can set the relationship between the different collision layers.