# Unity Space Shooter - Advanced Architecture Showcase

> A sophisticated 2D space shooter demonstrating enterprise-grade architecture patterns in Unity, showcasing modern software engineering principles adapted for game development.

**Unity Version:** 2020.3.48f1  
**Architecture Style:** Custom MVC with Clean Architecture principles  
**Developer:** Pol Vega Díaz

## 🎯 Project Vision

This project represents my approach to solving one of Unity's core challenges: building scalable, maintainable game architecture without sacrificing performance. I've designed this system to demonstrate how enterprise software development patterns can be successfully adapted for game development, creating a framework that's both powerful and practical.

## 🏗️ Architectural Philosophy

### The Challenge I Set Out to Solve

Unity's component-based architecture, while powerful, can lead to tightly-coupled, hard-to-test code when building complex games. I wanted to prove that you could implement sophisticated software architecture patterns in Unity without sacrificing performance or development velocity.

### My Solution: Hybrid MVC + Clean Architecture

I've created a unique architectural approach that combines:
- **Model-View-Controller patterns** adapted for Unity's component system
- **Clean Architecture principles** for dependency inversion and testability  
- **Custom performance optimizations** tailored for game development needs
- **Enterprise patterns** like Service Locator and Repository for scalability

## 🔧 Core Architecture Systems

### 1. The Objects Framework - My Custom MVC Implementation

Every game entity follows a consistent **five-class architecture pattern** I designed:

```
ObjectData (ScriptableObject)      ← Configuration & Design-time data
     ↓
ObjectModel                        ← Runtime data storage & minimal logic  
     ↓
ObjectMediator                     ← Business logic coordinator
     ↓ 
ObjectView (MonoBehaviour)         ← Unity engine interactions
     ↓
ObjectRepresentation               ← Lifecycle wrapper
     ↓
Repository                         ← Factory service
```

#### Why This Pattern?

- **Separation of Concerns**: Each class has a single, clear responsibility
- **Testability**: Business logic in Mediators can be unit tested independently
- **Unity Decoupling**: Game logic doesn't depend on MonoBehaviour specifics
- **Designer-Friendly**: ScriptableObject configuration allows non-programmers to modify game balance
- **Performance Optimization**: Representation enables efficient object pooling

### 2. Modular Assembly Architecture

I've structured the codebase using Unity Assembly Definitions to create a modular, scalable system:

```
Core Assembly (Game Logic)
    ↙    ↓    ↓    ↓    ↘
Events  Flow  Objects  Audio  ServiceRegister
    ↓    ↓      ↓      ↓         ↓
Array2DEditor (Custom Tools)
```

**Benefits of This Design:**
- **Compilation Speed**: Only modified assemblies recompile
- **Clear Dependencies**: Prevents circular references
- **Reusability**: Lower-level assemblies can be used in other projects
- **Team Scalability**: Clear boundaries for parallel development

### 3. Custom Performance Systems

#### Flow System - Centralized Game Loop

Instead of relying on Unity's distributed Update() calls, I've implemented a centralized update system:

```csharp
public class MainFlow : MonoBehaviour
{
    private readonly List<IUpdatable> _updatableObjects = new List<IUpdatable>();
    
    private void Update()
    {
        if (Pause) return;
        
        var dt = Time.deltaTime;
        for (var i = 0; i < _updatableObjects.Count; i++)
        {
            _updatableObjects[i].OnUpdate(dt);
        }
        CheckCollisions();
    }
}
```

**Why Custom Systems?**
- **Performance**: Single loop vs. hundreds of Update() calls
- **Control**: Precise execution order and pause/resume functionality
- **Cache Efficiency**: Better memory locality with centralized processing

#### Object Pooling Factory

I've implemented a pooling system to eliminates garbage collection spikes:

```csharp
public static class ObjectFactory
{
    public static T Get<T>(ObjectView prefab, ObjectData data) where T : ObjectRepresentation, new()
    {
        var representation = FactoryPool.Get<T>();
        if (representation != null)
        {
            representation.View.SetActive(true);
            representation.Configure(data);
            return (T)representation;
        }
        // Create new if pool empty...
    }
}
```

### 4. Type-Safe Event System

I've designed an event system that provides compile-time safety while maintaining flexibility:

```csharp
public class EventDispatcher : IEventDispatcher
{
    private readonly Dictionary<Type, dynamic> _events = new Dictionary<Type, dynamic>();

    public void Subscribe<T>(Action<T> callback) where T : IBaseEvent
    {
        var type = typeof(T);
        if (!_events.ContainsKey(type)) _events.Add(type, null);
        _events[type] += callback;
    }

    public void Dispatch<T>(T arg) where T : IBaseEvent
    {
        var type = typeof(T);
        if (_events.ContainsKey(typeof(T)))
        {
            var action = _events[type] as Action<T>;
            action?.Invoke(arg);
        }
    }
}
```

### 5. Service Locator Pattern

I've implemented a dependency injection system adapted for Unity's constraints:

```csharp
public class ServiceLocator
{
    public static ServiceLocator Instance => _instance ??= new ServiceLocator();
    private readonly Dictionary<Type, object> _services;
    
    public void RegisterService<T>(T service) { /* ... */ }
    public T GetService<T>() { /* ... */ }
}
```

## 🎮 Game-Specific Implementations

### Ship System - Complete Architecture Example

The Ship entity demonstrates the full architectural pattern in action:

**ShipMediator** coordinates between data and presentation:
```csharp
public class ShipMediator : ObjectMediator, IUpdatable
{
    public override void Configure()
    {
        _eventDispatcher.Subscribe<PowerUpActivatedEvent>(OnPowerUpActivated);
        _view.OnCollisionConfirmed += OnDamaged;
        _view.Sprite = _model.Sprite;
        _view.Radius = _model.Radius;
    }

    public void OnUpdate(float dt)
    {
        _view.UpdatePosition();
        if (_model.UpdateRecoil(dt))
        {
            _view.Shoot(_model.Bullet, _model.ActiveSpawnPoints, _model.BulletsPerSpawnPoint);
        }
    }
}
```

Notice how the Mediator:
- Coordinates between Model data and View operations
- Participates in the event system for decoupled communication
- Manages the update loop while maintaining separation of concerns

## 🛠️ Custom Editor Tools

I've created some editor tools to support the architecture:

- **Array2D Enemy Pattern Editor**: Visual level design tools
- **Collision Layer Matrix Editor**: Visual collision configuration
- **Audio Service Editor**: Sound management interface

These tools demonstrate my commitment to designer-friendly workflows, allowing non-programmers to configure complex behaviors through intuitive interfaces.

## 📊 Architecture Patterns Implemented

This project showcases my understanding of multiple software engineering patterns:

1. **Model-View-Controller (MVC)** - Modified for Unity with Mediator coordination
2. **Service Locator** - Dependency injection adapted for MonoBehaviour constraints
3. **Object Pool** - Performance optimization for frequent object creation
4. **Factory Method** - Abstracted object creation through repositories
5. **Observer Pattern** - Type-safe event system for decoupled communication
6. **Strategy Pattern** - Collision matrix configuration system
7. **Mediator Pattern** - Coordination between Model and View components
8. **Template Method** - Abstract base classes defining object lifecycle
9. **Coordinator Pattern** - High-level game state orchestration

## 🚀 Performance Benefits

### Memory Efficiency
- **Object Pooling**: Eliminates garbage collection spikes from frequent instantiation
- **Centralized Updates**: Better cache locality compared to distributed Update() calls
- **Minimal Allocations**: Reuses expensive MonoBehaviour components

### CPU Efficiency
- **Single Update Loop**: Replaces hundreds of individual Update() calls
- **Custom Collision Detection**: Simple sphere collision suitable for 2D games
- **Optimized Data Flow**: Minimal indirection between game logic and rendering

## 🎯 Design Decision Rationale

### Why Custom Update Loop?
**Problem**: Unity's Update() method called on every MonoBehaviour creates performance overhead  
**My Solution**: Centralized MainFlow manages all updates with predictable order  
**Trade-off**: More setup complexity for significant performance gains and better control

### Why Service Locator vs. Dependency Injection?
**Problem**: Constructor injection difficult with Unity's MonoBehaviour instantiation  
**My Solution**: Service Locator provides dependency inversion benefits while working with Unity's constraints  
**Trade-off**: Runtime dependency resolution vs. compile-time safety, but with clear registration patterns

### Why ScriptableObject Data Pattern?
**Problem**: Hard-coded game balance values make iteration slow  
**My Solution**: ScriptableObject-based configuration allows designers to modify values without programming  
**Trade-off**: More asset management overhead for significantly improved workflow

## 📈 Scalability Features

### Team Scaling
- **Clear Patterns**: Consistent five-class structure makes onboarding straightforward
- **Modular Assemblies**: Teams can work on different systems independently
- **Defined Interfaces**: Clear contracts between systems

### Feature Scaling
- **Template Architecture**: New game objects follow established patterns
- **Plugin System**: Custom editor tools support complex configurations
- **Event-Driven**: New features integrate without modifying existing systems

### Performance Scaling
- **Object Pooling**: Handles large numbers of dynamic objects efficiently
- **Centralized Systems**: Custom collision and update systems scale better than Unity defaults
- **Memory Management**: Minimal garbage generation even with complex object hierarchies

## 🎪 What This Project Demonstrates

This architecture showcase demonstrates my ability to:

- **Solve Complex Problems**: Address Unity's architectural limitations with creative solutions
- **Balance Trade-offs**: Optimize for both performance and maintainability
- **Apply Enterprise Patterns**: Successfully adapt software engineering principles to game development
- **Design for Scale**: Create systems that work for both small teams and large projects
- **Think Holistically**: Consider developer workflow, designer needs, and runtime performance

The result is a game architecture that could serve as the foundation for much larger, more complex projects while remaining accessible and performant.