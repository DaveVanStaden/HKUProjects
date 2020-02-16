using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Parser = TVDM.MicroControllers.SerialCommandParser;
using Connector = TVDM.MicroControllers.SerialConnector;

namespace TVDM
{
    namespace MicroControllers
    {
        public class MicroControllerComponentBase : MonoBehaviour
        {
            #region Events
            // These are the supported parameters(single parameter dynamic input)
            [System.Serializable]
            public class UnityFloatEvent : UnityEvent<float> { }
            [HideInInspector]
            public UnityFloatEvent
                floatEvent = new UnityFloatEvent();

            [System.Serializable]
            public class UnityBoolEvent : UnityEvent<bool> { }
            [HideInInspector]
            public UnityBoolEvent
                boolEvent = new UnityBoolEvent();

            [System.Serializable]
            public class UnityIntEvent : UnityEvent<int> { }
            [HideInInspector]
            public UnityIntEvent
                intEvent = new UnityIntEvent();

            [System.Serializable]
            public class UnityStringEvent : UnityEvent<string> { }
            [HideInInspector]
            public UnityStringEvent
                stringEvent = new UnityStringEvent();
            [HideInInspector]
            public UnityEvent
                simpleEvent = new UnityEvent();

            #endregion


            #region ControllerSettings
            [Header("ControllerSettings")]
            public string
                identifier;

            public enum PinSubscription { UART, D2, D3, D4, D5, D6, D7, D8, A0, A1, A2, A3, IC2W, IC2X, IC2Y, IC2Z }
            [HideInInspector]
            public PinSubscription
                pinSubscription = PinSubscription.D2;

            public enum PinMode { Default, Input, Output }
            [HideInInspector]
            public PinMode
                pinMode;

            [HideInInspector]
            public MicroControllerTypeDefinitionBase.MC_TYPE
                MC_Type;

            [HideInInspector]
            public MicroControllerTypeDefinitionBase.MC_Information
                typeBaseInformation;

            [HideInInspector]
            public bool
                advancedInformation;

            [HideInInspector]
            public bool
                subscribed = false;
            [HideInInspector]
            public string
                incommingCommand;
            [HideInInspector]
            public string
                incomingValue;
            [HideInInspector]
            public string
                outgoingValue;
            #endregion

            public enum IncomingValueType { Int, Float, String, Bool }
            [HideInInspector]
            public IncomingValueType
                incomingValueType;

            public enum NodeReactionType { Action, ValueTransfer, Measure }
            [HideInInspector]
            public NodeReactionType 
                nodeReactionType;

            [HideInInspector]
            public bool
                dynamicAction;

            [HideInInspector]
            public float
                floatThreshold;
            [HideInInspector]
            public int
                intThreshold;
            [HideInInspector]
            public string
                stringThreshold;
            [HideInInspector]
            public bool
                boolThreshold;

            private void Start()
            {
                typeBaseInformation = MicroControllerTypeDefinitionBase.GenerateMCInformation(MC_Type);
            }

            private void LateUpdate()
            {
                if (!subscribed && MC_Type != MicroControllerTypeDefinitionBase.MC_TYPE.LCD)
                {
                    Subscribe();
                }
            }

            public virtual void Initialize()
            {

            }

            public virtual void Subscribe()
            {
                Initialize();
                // Initialization of pins for correct Unitybound components
                //Debug.Log("MicroControllerComponentBase -> Subscribe");
                Parser.BindPinToComponent(pinSubscription, this, pinMode);
                subscribed = true;
            }

            public virtual void UnSubscribe()
            {
                // Denitialization of pins for correct Unitybound components
                if (Connector._instance.stream.IsOpen)
                {
                    //Debug.Log("MicroControllerComponentBase -> Unsubscribe ( " + identifier + " )");
                    Parser.UnBindPinFromComponent(pinSubscription, this);
                }
                subscribed = false;
            }

            private void OnDisable()
            {
                UnSubscribe();
            }
            
            private void Update()
            {
                if (pinMode == PinMode.Output)
                {
                    switch (MC_Type)
                    {
                        case MicroControllerTypeDefinitionBase.MC_TYPE.LCD:
                            if (!string.IsNullOrEmpty(outgoingValue))
                            {
                                Parser.LCDWrite(outgoingValue);
                            }
                            break;
                        case MicroControllerTypeDefinitionBase.MC_TYPE.DigitalDisplay:
                            if (!string.IsNullOrEmpty(outgoingValue))
                            {
                                Parser.DisplayWrite(outgoingValue);
                            }
                            break;
                    }
                }
                else
                {
                    if (nodeReactionType == NodeReactionType.ValueTransfer)
                    {
                        switch (incomingValueType)
                        {
                            case IncomingValueType.Float:
                                float parseFloat;
                                if (float.TryParse(incomingValue, out parseFloat))
                                {
                                    if (MC_Type != MicroControllerTypeDefinitionBase.MC_TYPE.Custom)
                                    {
                                        parseFloat = typeBaseInformation.Remap(parseFloat);
                                    }
                                    floatEvent.Invoke(parseFloat);
                                }
                                break;
                            case IncomingValueType.Int:
                                int parseInt;
                                if (int.TryParse(incomingValue, out parseInt))
                                {
                                    if (MC_Type != MicroControllerTypeDefinitionBase.MC_TYPE.Custom)
                                    {
                                        parseInt = typeBaseInformation.Remap(parseInt);
                                    }
                                    intEvent.Invoke(parseInt);
                                }
                                break;
                            case IncomingValueType.String:
                                stringEvent.Invoke(incomingValue);
                                break;
                            case IncomingValueType.Bool:
                                bool parseBool = false;
                                if (incomingValue == "0")
                                {
                                    parseBool = false;
                                }
                                else if(incomingValue == "1")
                                {
                                    parseBool = true;
                                }
                                if (MC_Type != MicroControllerTypeDefinitionBase.MC_TYPE.Custom)
                                {
                                    parseBool = typeBaseInformation.Remap(parseBool);
                                }
                                boolEvent.Invoke(parseBool);
                                
                                break;
                        }
                    }
                    else if (nodeReactionType == NodeReactionType.Action)
                    {
                        switch (incomingValueType)
                        {
                            case IncomingValueType.Float:
                                float parseFloat;
                                if (float.TryParse(incomingValue, out parseFloat))
                                {
                                    if (MC_Type != MicroControllerTypeDefinitionBase.MC_TYPE.Custom)
                                    {
                                        parseFloat = typeBaseInformation.Remap(parseFloat);
                                    }
                                    if (parseFloat > floatThreshold)
                                    {
                                        if (!dynamicAction)
                                        {
                                            simpleEvent.Invoke();
                                        }
                                        else
                                        {
                                            floatEvent.Invoke(parseFloat);
                                        }
                                    }
                                }
                                break;

                            case IncomingValueType.Int:
                                int parseInt;
                                if (int.TryParse(incomingValue, out parseInt))
                                {
                                    if (MC_Type != MicroControllerTypeDefinitionBase.MC_TYPE.Custom)
                                    {
                                        parseInt = typeBaseInformation.Remap(parseInt);
                                    }
                                    if (parseInt > intThreshold)
                                    {
                                        if (!dynamicAction)
                                        {
                                            simpleEvent.Invoke();
                                        }
                                        else
                                        {
                                            intEvent.Invoke(parseInt);
                                        }
                                    }
                                }

                                break;

                            case IncomingValueType.String:
                                string incomingString = incomingValue;
                                if (incomingString == stringThreshold)
                                {
                                    if (!dynamicAction)
                                    {
                                        simpleEvent.Invoke();
                                    }
                                    else
                                    {
                                        stringEvent.Invoke(incomingString);
                                    }
                                }
                                break;

                            case IncomingValueType.Bool:
                                bool parseBool;
                                if (bool.TryParse(incomingValue, out parseBool))
                                {
                                    if (incomingValue == "0")
                                    {
                                        parseBool = false;
                                    }
                                    else if (incomingValue == "1")
                                    {
                                        parseBool = true;
                                    }
                                    if (MC_Type != MicroControllerTypeDefinitionBase.MC_TYPE.Custom)
                                    {
                                        parseBool = typeBaseInformation.Remap(parseBool);
                                    }
                                    if (parseBool == boolThreshold)
                                    {
                                        if (!dynamicAction)
                                        {
                                            simpleEvent.Invoke();
                                        }
                                        else
                                        {
                                            boolEvent.Invoke(parseBool);
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }  
    }
}
