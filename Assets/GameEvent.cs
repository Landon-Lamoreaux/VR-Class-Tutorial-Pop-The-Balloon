using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR;

public class GameEvent : MonoBehaviour
{
    List<InputDevice> inputDevices = new List<InputDevice>();

    private bool first = false; // Flag for the first time we see a device.

    private bool isPressed;

    public SpawnArea spawnArea;

    public Teleportation teleportation;
    private bool isPressedTrigger;

    private int points;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!first)
        {
            InputDeviceCharacteristics desiredCharateristics = InputDeviceCharacteristics.HeldInHand;
            InputDevices.GetDevicesWithCharacteristics(desiredCharateristics, inputDevices);

            if(inputDevices.Count > 0)
                first = true;
        }

        foreach (InputDevice device in inputDevices)
        {
            // Find features for this device.
            List<InputFeatureUsage> supportedFeatures = new List<InputFeatureUsage>();
            device.TryGetFeatureUsages(supportedFeatures);


            foreach (InputFeatureUsage feature in supportedFeatures)
            {
                bool state;
                device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out state);

                if (state) // Is the button down on this frame?
                {
                    if (!isPressed) // Was the button up on the last frame?
                    {
                        Debug.Log("Primary button is pressed.");
                        Reset();
                    }
                }
                isPressed = state; // Update its state

                device.TryGetFeatureValue(CommonUsages.triggerButton, out state);

                /* If only one controller is active, use it, or only look at right hand
                   characteristics are a bit mask. If this is not zero, it is a right controller.*/
                if (inputDevices.Count == 1 || (device.characteristics & InputDeviceCharacteristics.Right) > 0)
                {
                    // On up
                    if (!state)
                    {
                        if (isPressedTrigger)
                        {
                            teleportation.ShowLine(false);
                            teleportation.teleport();
                        }
                    }
                    else
                    {
                        if (!isPressedTrigger)
                        {
                            teleportation.ShowLine(true);
                        }
                    }
                    isPressedTrigger = state;
                }
            }
        }
    }

    public void Reset()
    {
        //To Do reset game.
        spawnArea.Reset();
    }

    public void AddPoint(int points)
    {
        this.points += points;
        Debug.Log("Score: " + this.points);
    }

    public GameObject GetScript( GameObject game )
    {
        game.GetComponent<BalloonCollisionEvents>().score = this;
        return game;
    }

    public GameObject GetScriptCylinder(GameObject game)
    {
        game.GetComponent<CylinderColision>().score = this;
        return game;
    }

    public GameObject GetScriptBox(GameObject game)
    {
        game.GetComponent<BoxCollision>().score = this;
        return game;
    }

    public GameObject GetScriptBall(GameObject game)
    {
        game.GetComponent<RedCollision>().score = this;
        return game;
    }
}