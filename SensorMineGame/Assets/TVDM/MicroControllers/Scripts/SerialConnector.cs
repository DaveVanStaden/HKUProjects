/* ArduinoConnector by Alan Zucconi
 * http://www.alanzucconi.com/?p=2979
 */
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace TVDM
{
    namespace MicroControllers
    {
        public class SerialConnector : MonoBehaviour
        {
            public static SerialConnector
                _instance;

            public float 
                ThreadMessage;
            Thread 
                processingThread;
            bool 
                process = false;
            
            List<string>
                commandList = new List<string>();

            /* The serial port where the MicroController is connected. */
            [Tooltip("The serial port where the MicroController is connected")]
            public string 
                port = "COM1";
            /* The baudrate of the serial port. */
            [Tooltip("The baudrate of the serial port")]
            public int 
                baudrate = 9600;

            public int 
                commandBufferSize = 64;

            public SerialPort 
                stream;
            public string 
                serialMessage;

            public Dictionary<string, MicroControllerComponentBase>
                subscriptions = new Dictionary<string, MicroControllerComponentBase>();

            public bool
                Analyse = false;

            private void Awake()
            {
                if (_instance == null)
                {
                    _instance = this;
                }
                else
                {
                    Destroy(this);
                }
            }

            private void Start()
            {
                Open();
            }

            public void Dequeue(string _command)
            {
                string fullString = _command;
                if (Analyse)
                {
                    Debug.Log(_command);
                }
                string[] collection = fullString.Split(' ');
                if (collection.Length > 3)
                {
                    if (subscriptions.ContainsKey(collection[2]))
                    {
                        if (subscriptions[collection[2]] != null)
                        {
                            subscriptions[collection[2]].incommingCommand = _command;
                            subscriptions[collection[2]].incomingValue = collection[3];
                        }
                    }
                }
            }

            public void Open()
            {
                // Opens the serial port
                stream = new SerialPort(port, baudrate);
                stream.ReadTimeout = 50;
                stream.Open();
            }

            // Use this to write custom commands to the arduino _message is the command the arduino will recieve, for Pin bound communication use the subscription calls for components to enable with or disable from pin
            public void WriteToStream(string _message)
            {
                // Send the request
                //Debug.Log("Trying to write: " + _message);
                commandList.Add(_message);
            }

            public void SubscribeToStream(MicroControllerComponentBase _component, string _message)
            {
                if (subscriptions.ContainsKey(_component.identifier))
                {
                    //Debug.Log("SerialConnector -> SubscribeToStream() -> Double entry found: Adding instanceID");
                    _component.identifier += _component.transform.GetInstanceID().ToString();
                }

                subscriptions.Add(_component.identifier, _component);

                // Send the request
                WriteToStream(_message);
            }
            
            public void UnSubscribeFromStream(MicroControllerComponentBase _component, string _message)
            {
                if (subscriptions.ContainsKey(_component.identifier))
                {
                    subscriptions.Remove(_component.identifier);
                }
                // Send the request
                WriteToStream(_message);
            }

            public string ReadFromArduino(int timeout = 0)
            {
                stream.ReadTimeout = timeout;
                try
                {
                    return stream.ReadLine();
                }
                catch (TimeoutException)
                {
                    return null;
                }
            }

            public void Close()
            {
                // B reak C onnection and R eset
                WriteToStream("BCR");
                stream.Close();
            }

            private void OnApplicationQuit()
            {
                process = false;
                processingThread.Abort();
                // B reak C onnection and R eset
                WriteToStream("BCR");
                Close();
            }

            private void Update()
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    WriteToStream(serialMessage);
                }
                if (stream.IsOpen && process == false)
                {
                    Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

                    process = true;

                    processingThread = new Thread(delegate () {
                        Thread.Yield();
                        while (process)
                        {
                            string dataString = null;

                            if (stream.IsOpen)
                            {
                                try
                                {
                                    dataString = stream.ReadLine();
                                }
                                catch (TimeoutException)
                                {
                                    dataString = null;
                                }
                            }
                            if (dataString != null)
                            {
                                Dequeue(dataString);
                            }
                            Thread.Yield();
                        }

                    });

                    processingThread.Priority = System.Threading.ThreadPriority.BelowNormal;

                    processingThread.Start();
                }
            }

            int 
                currentFrameCount = 0;

            private void LateUpdate()
            {
                // Send command combined in frame of 64 bytes per frame
                for (int i = 0; i < commandList.Count;)
                {
                    if(commandList[i].Length > commandBufferSize)
                    {
                        Debug.LogError("COMMAND TO BIG FOR BUFFER WILL NOT PROCESS -> " + "Command cannot exceed commandBufferSize: " + commandBufferSize.ToString() + " || current commandSize: " + commandList[i].Length+ "\n" + commandList[i]);
                        commandList.RemoveAt(0);
                    }
                    if (currentFrameCount + commandList[i].Length < commandBufferSize)
                    {
                        stream.WriteLine(commandList[i]);
                        currentFrameCount += commandList[i].Length;
                        commandList.RemoveAt(0);
                    }
                    else
                    {
                        currentFrameCount = 0;
                        i++;
                    }
                }
                stream.BaseStream.Flush();
            }
        }  
    }
}