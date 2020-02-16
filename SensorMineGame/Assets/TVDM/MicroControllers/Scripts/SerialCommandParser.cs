using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector = TVDM.MicroControllers.SerialConnector;

namespace TVDM
{
    namespace MicroControllers
    {
        public static class SerialCommandParser
        {

            public static void AnalogWrite(string _pin, string[] _parameters)
            {
                // A nalog W rite
                string command = "AW ";
                command += _pin + " ";
                for (int i = 0; i < _parameters.Length; i++)
                {
                    command += " " + _parameters[i];
                }
                Connector._instance.WriteToStream(command);
            }

            public static void DigitalWrite(string _pin, string[] _parameters)
            {
                // D igital W rite
                string command = "DW ";
                command += _pin + " ";
                for (int i = 0; i < _parameters.Length; i++)
                {
                    command += " " + _parameters[i];
                }
                Connector._instance.WriteToStream(command);
            }

            public static void IC2Write(string _pin, string[] _parameters)
            {
                // IC2W rite
                string command = "IC2W ";
                command += _pin + " ";
                for (int i = 0; i < _parameters.Length; i++)
                {
                    command += " " + _parameters[i];
                }
                Connector._instance.WriteToStream(command);
            }

            public static void DisplayWrite(string _message)
            {
                // DIS play W rite
                string command = "DISW ";
                command += _message;
                Connector._instance.WriteToStream(command);
            }

            public static void LCDWrite(string _message)
            {
                // LCD W rite
                string command = "LCDW ";
                command += _message;
                Connector._instance.WriteToStream(command);
            }

            public static void BindPinToComponent(MicroControllerComponentBase.PinSubscription _pinRequest, MicroControllerComponentBase _component, MicroControllerComponentBase.PinMode _pinMode)
            {
                // B ind
                string command = "B ";
                command += ConvertPinSubscriptionToPinIndex(_pinRequest) + " ";
                command += " " + _component.identifier;
                if(_pinMode != MicroControllerComponentBase.PinMode.Default)
                {
                    command += " " + _pinMode.ToString();
                }
                
                Connector._instance.SubscribeToStream(_component, command);
            }

            public static void UnBindPinFromComponent(MicroControllerComponentBase.PinSubscription _pinRequest, MicroControllerComponentBase _component)
            {

                // U n B ind
                string command = "UB ";
                command += ConvertPinSubscriptionToPinIndex(_pinRequest) + " ";
                command += " " + _component.identifier;

                Connector._instance.UnSubscribeFromStream(_component, command);
            }

            public static int ConvertPinSubscriptionToPinIndex(MicroControllerComponentBase.PinSubscription _subscription)
            {
                int returnInt = 0;
                switch (_subscription)
                {
                    // Digital port ints from 2 (inclusive) to 7 (inclusive)
                    case MicroControllerComponentBase.PinSubscription.D2:
                        returnInt = 2;
                        break;
                    case MicroControllerComponentBase.PinSubscription.D3:
                        returnInt = 3;
                        break;
                    case MicroControllerComponentBase.PinSubscription.D4:
                        returnInt = 4;
                        break;
                    case MicroControllerComponentBase.PinSubscription.D5:
                        returnInt = 5;
                        break;
                    case MicroControllerComponentBase.PinSubscription.D6:
                        returnInt = 6;
                        break;
                    case MicroControllerComponentBase.PinSubscription.D7:
                        returnInt = 7;
                        break;

                    // Analog port ints from 8 (inclusive) to 11 (inclusive)
                    case MicroControllerComponentBase.PinSubscription.A0:
                        returnInt = 8;
                        break;
                    case MicroControllerComponentBase.PinSubscription.A1:
                        returnInt = 9;
                        break;
                    case MicroControllerComponentBase.PinSubscription.A2:
                        returnInt = 10;
                        break;
                    case MicroControllerComponentBase.PinSubscription.A3:
                        returnInt = 11;
                        break;

                    // IC2 port ints from 12 (inclusive) to 15 (inclusive)
                    case MicroControllerComponentBase.PinSubscription.IC2W:
                        returnInt = 12;
                        break;
                    case MicroControllerComponentBase.PinSubscription.IC2X:
                        returnInt = 13;
                        break;
                    case MicroControllerComponentBase.PinSubscription.IC2Y:
                        returnInt = 14;
                        break;
                    case MicroControllerComponentBase.PinSubscription.IC2Z:
                        returnInt = 15;
                        break;
                }
                return returnInt;
            }
        }  
    }
}
