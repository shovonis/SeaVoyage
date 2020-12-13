using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Valve.VR;


namespace DWP2.ShipController
{
    [System.Serializable]
    public class InputStates
    {
        private InputBindings inputBindings;

        private float bowThruster;
        public float BowThruster => bowThruster;

        private float sternThruster;
        public float SternThruster => sternThruster;

        private float rudder;
        public float Rudder => rudder;

        private float leftThrottle;
        public float LeftThrottle => leftThrottle;

        private float rightThrottle;
        public float RightThrottle => rightThrottle;

        private float throttle;
        public float Throttle => throttle;

        private bool engineStartStop;
        public bool EngineStartStop
        {
            get { return engineStartStop; }
            set { engineStartStop = value; }
        }

        private bool anchorDropWeigh;
        public bool AnchorDropWeigh
        {
            get { return anchorDropWeigh; }
            set { anchorDropWeigh = value; }
        }

        // [Header("GUI (optional)")]
        // public Slider throttleSlider;
        // public Slider leftThrottleSlider;
        // public Slider rightThrottleSlider;
        // public Slider rudderSlider;
        // public Slider bowThrusterSlider;
        // public Slider sternThrusterSlider;
        // public Button engineStartStopButton;
        // public Button anchorDropWeighButton;

        public void Initialize(AdvancedShipController sc)
        {
            inputBindings = new InputBindings();
            inputBindings.Initialize(sc);
            
            sc.EngineStartOff.AddOnStateDownListener(TriggerDown, sc.handType);
            sc.EngineStartOff.AddOnStateUpListener(TriggerUp, sc.handType);
            sc.Move.AddOnAxisListener(GetAxisValue, sc.handType);

        }

       
        public void PostFixedUpdate()
        {
            engineStartStop = false;
            anchorDropWeigh = false;
        }

        public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            engineStartStop = false;
        }
    
        public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            engineStartStop = true;
        }
        
        private void GetAxisValue(SteamVR_Action_Vector2 fromaction, SteamVR_Input_Sources fromsource, Vector2 axis, Vector2 delta)
        {
            throttle = axis.y;
            rudder = axis.x;
        }
        
        public void SetAxis(ref float axisValue, string name)
        {
            float value = inputBindings.GetAxis(name);
            axisValue = value;
        }

        public void SetButtonDown(ref bool isPressed, string name)
        {
            if (inputBindings.GetButtonDown(name)) isPressed = true;
        }

    }
}

