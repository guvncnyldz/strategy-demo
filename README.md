## 🕹️ Controls

- **Middle Mouse Button** — Move Camera  
- **Left Click** — Select Unit or Building  
- **Right Click** — Give Order  

---

## 🧱 Grid System

Using the **Factory Pattern** and **ScriptableObjects**, we can add, remove, or change grid types following the **Open/Closed Principle**. Currently, visual and non visual grids can be found.

📁 `Scripts/Grid/Factory`

---

## 🖥️ GPU Instancing
GPU instancing was implemented for visual grids. While not necessary for sprites, it is used here as an example.

📁 `Scripts/Services/GPUInstancing`

---
## 🏗️ Building System

Buildings are created using the **Builder Pattern** and **Object Pooling** instead of a prefab.
Additionally, the **Factory Pattern** and **Factory Method** allow for a flexible and generic way to choose which builder to use.

📁 `Scripts/Building/Builder`

---

## 🔌 Interfaces & Modularity

With a clean interface-based architecture, features like movement, attacking, production, and showing information panels are extendable and replaceable.  
This allows, for example, a unit that can produce other units or a building that can move.

📁 `Scripts/Interfaces`

---

## 🎮 Input System

Each input action maps to an interface.  
Classes implementing an input interface **subscribe** to the `InputManager`. When a specific input is triggered, the corresponding methods are called.

This system uses the **Unity New Input System**, with a simple **state machine** inside the `InputManager` (stateless transitions).

📄 `InputManager.cs`

---

## 🧠 Unit AI (FSM)

Unit behaviors are implemented using an **FSM (Finite State Machine)** via Unity's **Animator system**.

📁 `Scripts/Unit/FSM`  
📁 `Assets/FSM`

---

## 🏹 Combat System

There are two unit types: **Archer** and **Swordsman**.  
A simple **abstraction** in the `CombatSystem` enables easy integration of both **Melee** and **Ranged** combat.

📁 `Scripts/Unit/Combat`

---

## 🧩 Extensions

Various utility and helper methods are implemented via **C# Extension Methods**.

📁 `Scripts/Extensions`

---

## 🛠️ Singleton & Services

You’ll find usage of **Singleton** throughout the project, but also a cleaner alternative via **Service Locator**, which provides more flexibility.

📁 `Scripts/Services`

---

## ♻️ Object Pooling

A generic pooling system is accessed through **Service Locator**.

📁 `Scripts/Services/Pooling`

---

## 🖼️ UI Optimizations

UI movement and animations are handled using **DoTween**.  
Inside `UIProductionMenu` and `UIProductionPanel`, image and text parents are reassigned after instantiation to optimize **batching** and **reduce DrawCalls** significantly.
