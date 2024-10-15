###### shadowWhite

# State Pattern

## Understanding the State Pattern

### What is a Design Pattern?

Before we get into the **State Pattern**, let’s quickly recap what a **design pattern** is:

**Design Patterns** are reusable solutions to common problems that arise in software development. They help structure your code in a way that's organized, efficient, and easy to maintain.

The **State Pattern** is one such design pattern.

---

## The State Pattern

### What is the State Pattern?

The **State Pattern** is a behavioral design pattern that allows an object to change its behavior based on its internal state. In simpler terms, the object can behave differently depending on what "state" it's in.

### Why Use the State Pattern?

Imagine you're playing a video game where your character can be in different modes, like "normal," "powered-up," or "damaged." Each mode (or state) affects how the character behaves. The State Pattern helps you manage these different behaviors neatly.

### Key Components of the State Pattern

The State Pattern involves three main components:

### **1. Context**

- **What it is:** The **Context** is the object whose behavior changes based on its state.
- **What it does:** It maintains a reference to the current state and delegates behavior to the state object.
- **Example:** In a video game, the character itself is the Context. It can be in different states like "normal" or "powered-up."

### **2. State Interface**

- **What it is:** The **State Interface** defines a set of methods that all concrete states must implement.
- **What it does:** It provides a blueprint for how different states should behave.
- **Example:** In our game, the State Interface might include methods like `move()`, `attack()`, and `defend()` that different states need to implement.

### **3. Concrete States**

- **What it is:** **Concrete States** are the different possible states the Context can be in. Each state implements the State Interface and defines specific behavior for the Context.
- **What it does:** It alters the behavior of the Context by implementing methods from the State Interface in a way that fits the particular state.
- **Example:** The "normal," "powered-up," and "damaged" modes of the character are Concrete States. Each mode has a different way of implementing the `move()`, `attack()`, and `defend()` methods.

### How the State Pattern Works Together

1. **Context starts in a default state:** The Context (e.g., game character) begins in one of the states (e.g., "normal").
2. **State influences behavior:** The Context behaves according to its current state. For example, if the character is "normal," it might move at regular speed and deal normal damage.
3. **State changes:** When an event occurs (like collecting a power-up), the Context changes its state (e.g., from "normal" to "powered-up").
4. **New state defines new behavior:** The Context now behaves according to the new state. For example, in "powered-up" mode, the character might move faster and deal more damage.
5. **Repeat as necessary:** The Context can continue to change states as different events occur, and its behavior will update accordingly.

### Simple Analogy

Think of a **traffic light**:

- **Context:** The **traffic light** itself is the Context.
- **State Interface:** The **State Interface** might define methods like `changeLight()`.
- **Concrete States:** The different light colors—**red, yellow, and green**—are the Concrete States. Each color represents a different state with its own behavior:
  - **Red:** The light stops cars.
  - **Yellow:** The light warns cars to slow down.
  - **Green:** The light lets cars go.

As the traffic light changes color, its behavior (what it instructs cars to do) changes as well.

#### Some other state examples:
	- Aircraft => grounded-takingoff-flying-landing
	- Gun => shooting-reload-idle
	- Primitive Shape Scale => scaling up-max scale-scaling down-init scale
	- *shape color by mouse pos => outside shape border : color1-hover over shape : color2-clicked:color3
	- vehicle states => stopped-driving-braking-reversing
	- npc => patrol-chase-idle-attack

---

## When to Use the State Pattern

The State Pattern is especially useful when:

1. **An object has several different states:** And the behavior of the object changes based on its state.
2. **State-specific behavior is complex:** And you want to avoid cluttering your code with lots of `if-else` or `switch` statements.
3. **You anticipate adding new states:** The pattern makes it easier to add new states without modifying existing code.

### Example Scenarios

- **Game Development:** Managing different states of a character (normal, powered-up, invincible, etc.).
- **Text Editor:** Different states of a text editor like "Editing Mode," "Read-Only Mode," etc.
- **Media Player:** Different states like "Playing," "Paused," "Stopped."

---

## Summary Table

| Aspect                    | State Pattern                                                  |
|---------------------------|----------------------------------------------------------------|
| **Context**               | Object whose behavior changes based on its state              |
| **State Interface**       | Defines methods that all concrete states must implement       |
| **Concrete States**       | Different possible states the context can be in, each with specific behavior |
| **Behavior**              | Varies depending on the current state of the context          |
| **Best For**              | Situations where an object’s behavior changes with its state  |

---

## Conclusion

The **State Pattern** is a powerful design pattern that helps manage an object's behavior as it changes over time. By separating out the different states and encapsulating their behaviors, your code becomes cleaner, more organized, and easier to extend.

Understanding the State Pattern is a great tool for any developer, especially when working on applications where objects can be in multiple states.

---

**Any questions or need more examples?**