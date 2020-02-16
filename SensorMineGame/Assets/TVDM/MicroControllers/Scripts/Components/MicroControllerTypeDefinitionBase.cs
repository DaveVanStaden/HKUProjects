using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TVDM
{
    namespace MicroControllers
    {
        public class MicroControllerTypeDefinitionBase : MonoBehaviour
        {
            public enum MC_TYPE { Custom, Potentio, RoundForce, Button, LCD, DigitalDisplay, ThumbStick, VibrationMotor, Servo }

            [System.Serializable]
            public class MC_Information
            {
                public virtual T Rebind_Normalized<T>(T _input, T IOMin, T IOMax)
                {
                    return ExtendedFunctions.Math.Normalize(_input, IOMin, IOMax);
                }

                public virtual T Remap<T>(T _input)
                {
                    return _input;
                }

                new public virtual MicroControllerComponentBase.IncomingValueType GetType()
                {
                    return MicroControllerComponentBase.IncomingValueType.Float;
                }
            }
            
            public class Potentio : MC_Information
            {
                public override T Remap<T>(T _input)
                {
                    return Rebind_Normalized((dynamic)_input, 0, 1023);
                }

                public override MicroControllerComponentBase.IncomingValueType GetType()
                {
                    return MicroControllerComponentBase.IncomingValueType.Float;
                }
            }

            public class RoundForce : MC_Information
            {
                public override T Remap<T>(T _input)
                {

                    return Rebind_Normalized((dynamic)_input, 0f, 600f); //It has something to do with this 'ere gabbin. Default is 600
                }

                public override MicroControllerComponentBase.IncomingValueType GetType()
                {
                    return MicroControllerComponentBase.IncomingValueType.Float;
                }
            }

            public class Custom : MC_Information
            {
                public override T Remap<T>(T _input)
                {
                    return Rebind_Normalized((dynamic)_input, 0f, 600f);
                }
            }

            public class Button: MC_Information
            {
                public override T Remap<T>(T _input)
                {
                    return _input;
                }
            }

            public static MC_Information GenerateMCInformation(MC_TYPE _type)
            {
                switch (_type)
                {
                    case MC_TYPE.Potentio:
                        return new Potentio();
                    case MC_TYPE.RoundForce:
                        return new RoundForce();
                    case MC_TYPE.Button:
                        return new Button();
                    default:
                        return null;
                }
            }
        }  
    }
}
