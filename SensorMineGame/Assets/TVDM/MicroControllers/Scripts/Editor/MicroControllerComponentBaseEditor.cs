using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TVDM
{
    namespace MicroControllers
    {
        [CustomEditor(typeof(MicroControllerComponentBase))]
        public class MicroControllerComponentBaseEditor : Editor
        {
            SerializedProperty
                identifier,
                pinSubscription,
                advancedInformation,
                incomingValueType,
                dynamicAction,
                nodeReactionType,
                MC_Type,
                typeBaseInformation,
                pinMode,
                subscribed;


            int prevousSelection;

            private void OnEnable()
            {
                identifier = serializedObject.FindProperty("identifier");
                pinSubscription = serializedObject.FindProperty("pinSubscription");
                MC_Type = serializedObject.FindProperty("MC_Type");
                subscribed = serializedObject.FindProperty("subscribed");
                advancedInformation = serializedObject.FindProperty("advancedInformation");
                incomingValueType = serializedObject.FindProperty("incomingValueType");
                pinMode = serializedObject.FindProperty("pinMode");
                typeBaseInformation = serializedObject.FindProperty("typeBaseInformation");
                dynamicAction = serializedObject.FindProperty("dynamicAction");
                nodeReactionType = serializedObject.FindProperty("nodeReactionType");
            }

            public override void OnInspectorGUI()
            {
                serializedObject.Update();

                if(subscribed.boolValue == false)
                {
                    EditorGUILayout.PropertyField(identifier);
                    EditorGUILayout.PropertyField(pinSubscription);
                    EditorGUILayout.PropertyField(MC_Type);

                    if (MC_Type.enumValueIndex == 0)
                    {
                        EditorGUILayout.PropertyField(pinMode);
                        EditorGUILayout.PropertyField(incomingValueType);
                    }
                    else
                    {
                        // MC_TYPE { Custom, Potentio, RoundForce, Button, LCD, DigitalDisplay, ThumbStick, VibrationMotor, Servo }
                        //              0-------1----------2----------3-----4---------5------------6-------------7------------8

                        // public enum IncomingValueType { Int, Float, String, Bool }
                        //                                  0-----1------2--------3

                        // PinMode { Default, Input, Output }
                        //             0--------1--------2
                        switch (MC_Type.enumValueIndex)
                        {
                            // Potentio
                            case 1:
                                incomingValueType.enumValueIndex = 1; // Float
                                pinMode.enumValueIndex = 1;
                                break;
                            // RoundForce
                            case 2:
                                incomingValueType.enumValueIndex = 1; // Float
                                pinMode.enumValueIndex = 1;
                                break;
                            // Button
                            case 3:
                                incomingValueType.enumValueIndex = 3; // Boolean
                                pinMode.enumValueIndex = 1;
                                break;
                            // LCD
                            case 4:
                                incomingValueType.enumValueIndex = 2; // ?????
                                pinMode.enumValueIndex = 2;
                                break;
                            // DigitalDisplay
                            case 5:
                                incomingValueType.enumValueIndex = 0; // ?????
                                pinMode.enumValueIndex = 2;
                                break;
                            // Thumbstick
                            case 6:
                                incomingValueType.enumValueIndex = 1; // ?????
                                pinMode.enumValueIndex = 1;
                                break;
                            // VibrationMotor
                            case 7:
                                incomingValueType.enumValueIndex = 1; // ?????
                                pinMode.enumValueIndex = 2;
                                break;
                            // Servo
                            case 8:
                                incomingValueType.enumValueIndex = 1; // ?????
                                pinMode.enumValueIndex = 2;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("-- Identifier: " + identifier.stringValue);
                    EditorGUILayout.LabelField("-- Pin Subscription: " + ((MicroControllerComponentBase.PinSubscription)pinSubscription.enumValueIndex).ToString());
                    EditorGUILayout.LabelField("-- MicroController Type: " + ((MicroControllerTypeDefinitionBase.MC_TYPE)MC_Type.enumValueIndex).ToString());
                }

                EditorGUILayout.PropertyField(nodeReactionType);

                switch (nodeReactionType.enumValueIndex)
                {
                    //Action
                    case 0:
                        EditorGUILayout.PropertyField(dynamicAction);
                        if (dynamicAction.boolValue == false)
                        {
                            DrawThresholds();
                            EditorGUILayout.PropertyField(serializedObject.FindProperty("simpleEvent"));
                        }
                        else
                        {
                            DrawThresholds();
                            DrawCustomEvents();
                        }
                        break;
                    //Valuetransfer
                    case 1:
                            DrawCustomEvents();
                        break;
                    //Measure
                    case 2:
                        break;
                }

                EditorGUILayout.PropertyField(advancedInformation);
                if (advancedInformation.boolValue == true)
                {

                    EditorGUILayout.PropertyField(serializedObject.FindProperty("subscribed"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("incommingCommand"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("incomingValue"));
                }

                prevousSelection = MC_Type.enumValueIndex;

                serializedObject.ApplyModifiedProperties();
            }

            public void DrawThresholds()
            {
                if (pinMode.enumValueIndex != 2)
                {
                    switch (incomingValueType.enumValueIndex)
                    {
                        case 0:
                            EditorGUILayout.PropertyField(serializedObject.FindProperty("intThreshold"));
                            break;
                        case 1:
                            EditorGUILayout.PropertyField(serializedObject.FindProperty("floatThreshold"));
                            break;
                        case 2:
                            EditorGUILayout.PropertyField(serializedObject.FindProperty("stringThreshold"));
                            break;
                        case 3:
                            EditorGUILayout.PropertyField(serializedObject.FindProperty("boolThreshold"));
                            break;
                    }
                }
            }

            public void DrawCustomEvents()
            {
                if (pinMode.enumValueIndex != 2)
                {
                    switch (incomingValueType.enumValueIndex)
                    {
                        case 0:
                            EditorGUILayout.PropertyField(serializedObject.FindProperty("intEvent"));
                            break;
                        case 1:
                            EditorGUILayout.PropertyField(serializedObject.FindProperty("floatEvent"));
                            break;
                        case 2:
                            EditorGUILayout.PropertyField(serializedObject.FindProperty("stringEvent"));
                            break;
                        case 3:
                            EditorGUILayout.PropertyField(serializedObject.FindProperty("boolEvent"));
                            break;
                    }
                }
                else
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("outgoingValue"));
                }
            }
        }  
    }
}
