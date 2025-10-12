# 🏎️ Window Dash  

**Window Dash** (also released as *GroceryDash* on itch.io) is a fast-paced **Unity** game built in 48 hours during the **University of Waterloo Game Jam** 🎮.  

In this chaotic grocery pickup simulator, players race against time to fulfill car order tickets! Each car pulls up with a grocery list,  you’ll need to dash into the store, grab the correct items, manage your limited carrying weight, and deliver everything back before the timer runs out.  

Strategy meets speed,  plan your runs, pick lighter or heavier items wisely, and stay calm under pressure to become the ultimate grocery runner! 🛒  

**Demo:** [Youtube Video](https://youtu.be/V1NB5lXH1gQ)  

---

## 🎮 Play the Game  

- 🧺 **Mac Version:** [Play on itch.io](https://sianqian27gmailcom.itch.io/grocerydash)  
- 🛻 **Windows Version:** [Play on itch.io](https://andijingzhitse.itch.io/grocerydash)  

---

## 🕹️ Gameplay Overview  

- 🚗 **Cars with Orders:** Each car comes with a grocery ticket listing items needed.  
- 🛒 **Collect Groceries:** Move through the store to grab each item.  
- ⚖️ **Weight Limit:**  
  - Every item has a weight value (e.g., a can of pop is light, a bag of potatoes is heavy).  
  - You can only carry up to **3 total weight units** at once.  
  - Plan your routes,  multiple trips may be required for heavier orders!  
- ⏱️ **Timed Gameplay:** Deliver as many accurate orders as possible before the timer runs out.  
- 🌆 **Level Progression:** Higher levels feature more items, heavier combinations, and tighter time limits.  

---

## ✨ Core Features  

### 🚙 Car & Order System  
- **`Car.cs`** – Controls car behavior, highlighting, and interactions.  
- **`Ordering.cs`** – Handles generation and tracking of grocery order tickets.  
- **`SubmitOrderTrigger.cs`** – Validates completed orders and updates the score.  

### 🛍️ Item & Inventory System  
- **`Item.cs`** – Defines item attributes (name, type, and weight).  
- **`Inventory.cs`** – Manages carried items and enforces the 3-weight limit.  

### 🧩 Environment & Interaction  
- **`Garbage Bin.cs` / `Garbage Bin Trigger.cs`** – Manage object cleanup and item disposal.  
- **`Player.cs`** – Handles player movement, interactions, and carrying mechanics.  

### 🕒 Game Flow  
- **`GameTimerScript.cs`** – Controls countdowns and triggers end screens.  
- **`Level Select.cs` / `Main Menu.cs` / `StartScreenScript.cs`** – Manage menus, navigation, and UI transitions.  

---

## 🧠 Technical Highlights  

- Built in **Unity** using **C#**  
- Implements **Finite State Machine (FSM)** for smooth car/player behavior  
- Weight-based inventory system for resource management  
- Modular systems for order validation, inventory, and timer control  

---

## 👩‍💻 Team Credits  

**Developers:** Kelvin, Varsha, Sian, and Andi  
**Art:** Amanda and Jacob  
**Made at:** *Game Jam @ University of Waterloo*  
