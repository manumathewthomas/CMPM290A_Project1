// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule
{
    /// <summary>
    /// Very simple class that implements basic logic for a trigger button.
    /// </summary>
    public class TriggerButton : MonoBehaviour, IInputHandler
    {


        public CSVParsing smScript;
        public GameObject step;
        public GameObject hr;
        public GameObject week1;
        public GameObject week2;
        public GameObject week3;

        public int mode;
        public int weekID;

        /// <summary>
        /// Indicates whether the button is clickable or not.
        /// </summary>
        [Tooltip("Indicates whether the button is clickable or not.")]
        public bool IsEnabled = true;

        public event Action ButtonPressed;


        /// <summary>
        /// Press the button programmatically.
        /// </summary>

        public void Start()
        {
            step.GetComponent<TextMesh>().color = Color.red;
            hr.GetComponent<TextMesh>().color = new Color(1.0f, 1.0f, 1.0f);

            week1.GetComponent<TextMesh>().color = Color.yellow;
            week2.GetComponent<TextMesh>().color = new Color(1.0f, 1.0f, 1.0f);
            week3.GetComponent<TextMesh>().color = new Color(1.0f, 1.0f, 1.0f);

        }
        public void Press()
        {
            if (IsEnabled)
            {
                ButtonPressed.RaiseEvent();
            }
        }

        void IInputHandler.OnInputDown(InputEventData eventData)
        {
            // Nothing.

        }

        void IInputHandler.OnInputUp(InputEventData eventData)
        {
            if (IsEnabled && eventData.PressType == InteractionSourcePressInfo.Select)
            {
                ButtonPressed.RaiseEvent();
                eventData.Use(); // Mark the event as used, so it doesn't fall through to other handlers.



                if (eventData.selectedObject.name == "StepParent")
                {
                    mode = 0;
                    step.GetComponent<TextMesh>().color = Color.red;
                    hr.GetComponent<TextMesh>().color = new Color(1.0f, 1.0f, 1.0f);
                }
                else if (eventData.selectedObject.name == "HRParent")
                {
                    mode = 1;
                    step.GetComponent<TextMesh>().color = new Color(1.0f, 1.0f, 1.0f);
                    hr.GetComponent<TextMesh>().color = Color.red;
                }

                if (eventData.selectedObject.name == "Week1Parent")
                {
                    weekID = 0;
                    week1.GetComponent<TextMesh>().color = Color.yellow;
                    week2.GetComponent<TextMesh>().color = new Color(1.0f, 1.0f, 1.0f);
                    week3.GetComponent<TextMesh>().color = new Color(1.0f, 1.0f, 1.0f);
                }
                else if (eventData.selectedObject.name == "Week2Parent")
                {
                    weekID = 1;
                    week1.GetComponent<TextMesh>().color = new Color(1.0f, 1.0f, 1.0f);
                    week2.GetComponent<TextMesh>().color = Color.yellow;
                    week3.GetComponent<TextMesh>().color = new Color(1.0f, 1.0f, 1.0f);
                }
                else if (eventData.selectedObject.name == "Week3Parent")
                {
                    weekID = 2;
                    week1.GetComponent<TextMesh>().color = new Color(1.0f, 1.0f, 1.0f);
                    week2.GetComponent<TextMesh>().color = new Color(1.0f, 1.0f, 1.0f);
                    week3.GetComponent<TextMesh>().color = Color.yellow;
                }

               
                smScript.visualize(mode, weekID);


            }
        }
    }
}
