using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControllerHintUtilities {

    [Serializable]
    public enum ButtonNames {
        A = 1,
        B = 2,
        Trigger = 3,
        Grip = 4,
        Joystick = 5,
        Menu = 6,
        X = 7,
        Y = 8
    }

    /// <summary>
    /// This struct is comprised of all objects needed to
    /// create a buttin hint for a controller.
    /// </summary>
    [Serializable]
    public struct ButtonTip {
        public ButtonNames b_name;       // button name, A,B,Trigger,etc
        public string b_text;       // text for action achieved by button
        public GameObject b_gameObj;    // button game object
        public Connector b_connector;   // line renderer and panel
        public Renderer b_renderer;
        public Material b_originalMaterial; // material of button, material changes when highlighting.
    }

    [Serializable]
    public struct RequestButtonTip {
        public ButtonNames b_name;       // button name, A,B,Trigger,etc
        public string b_text;       // text for action achieved by button
        public RequestButtonTip(ButtonNames v1, string v2) : this() {
            this.b_name = v1;
            this.b_text = v2;
        }
    }
}
