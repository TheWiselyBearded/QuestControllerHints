# QuestControllerHints
An example project with button highlighting. Developer can activate hints on button input or run the user through each button in 'tutorial mode'.

###### *Using Unity 2019.3 and XR Interaction Toolkit*

---

## Features:
![Features](https://i.imgur.com/nBFga9g.gifv)
![Features](https://i.imgur.com/SaJavJx.gif)
* Tutorial mode to run user through controls.
* Button Highlighting
---

## Configuration:

### *WHEN IMPORTING PACKAGE, MAKE SURE Android BUILD PLATFORM IS SET.*

## Adding Hints/Tips to Quest Controller:
![Inspector](https://imgur.com/ZEzzep0.jpg)
![Code](https://i.imgur.com/LPwuV9a.png)

As a developer, simply add **ButtonController** scripts to specify different button configurations. 
* To configure hints, create your own script or use the **TutorialExample** in the inspector to specify the controls/ 

* The **HintManager** is responsible for activating hint instances and calculating the state of the tutorial based on the ordered list provided by the user.
* The **TutorialExample** script shows how we can setup an inspector component to allow us to coordinate a sequence of tips. If we want to activate tutorial mode, simply toggle the checkbox.
---

# How it works:

#### *HintManager.cs*
---
This script is assigned is configured using the Left/Right Quest Controller prefab. 
Basically, every single button utilizes the **ButtonController** script to fire _click/press_ events to progress the tutorial mode.
The script is documented and provides the developer with extensibility. Simply define a Button Press/Hold event and assign one of the HintManager methods in the Unity inspector.

---


### Author

**Alireza Bahremand**

* [Website/Alireza Bahremand](https://www.alirezabahremand.com/)
* [twitter/lirezaBahremand](https://twitter.com/lirezabahremand)

**Dylan Kerr**
* [Website/Dylan Kerr](https://www.djkerrdesign.com/)

### License

Copyright © 2020, [Alireza Bahremand](https://github.com/TheWiselyBearded).
Released under the [MIT license](LICENSE).