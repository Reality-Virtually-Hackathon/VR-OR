/**
 * MIT License
 *
 * Copyright (c) 2017 Mo Kakwan, Uwe Gruenefeld, Helena Deus, Michal Leszczynski, Alisha Haris
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 **/
using UnityEngine;
using WebSocketSharp;
﻿using System;

namespace MagicTool
{
	public class NetworkController : MonoBehaviour
	{
		[HeaderAttribute("Connection")]
		public string IP = "localhost";
		public int PORT = 8080;

		// Variables for websocket connection
		private WebSocket webSocket;
		[HideInInspector]
		public Vector3 rotation;
		
		/// <summary>
		/// Update is called once per frame by Unity
		/// </summary>
		void Update () 
		{
			// Check connection
			//if (!this.ProcessConnection ())
			//	return;
		}

		/// <summary>
		/// Checks the websocket connection
		/// </summary>
		/// <returns><c>true</c>, if connection was established, <c>false</c> otherwise.</returns>
		private bool ProcessConnection()
		{
			// If the websocket is null
			if(this.webSocket == null)
				this.webSocket = new WebSocket ("ws://" + this.IP + ":" + this.PORT);

			// If the websocket is not alive
			// try to connect to the server
			if (!this.webSocket.IsAlive)
			{
				try
				{
					// Connect to the server
					this.webSocket.Connect ();


					// Method for handling incoming messages
					this.webSocket.OnMessage += (sender, message) => this.Response (message.Data);
				}
				catch(Exception exception) 
				{
					// Something went wrong
					Debug.Log (exception);
				}
			}

			// Return if websocket is alive
			return this.webSocket.IsAlive;
		}

		/// <summary>
		/// Handling responses coming in from network
		/// </summary>
		private void Response(string message)
		{
			//Debug.Log ("Recieved: " + message);

			// Response has format "angle1 angle2 angle3"
			string[] list = message.Split(' ');
			this.rotation = new Vector3 (
				Int32.Parse(list[0]) / 1024f * 360f,
				Int32.Parse(list[1]) / 1024f * 360f,
				Int32.Parse(list[2]) / 1024f * 360f
			);
		}

		/// <summary>
		/// Send messages over network
		/// </summary>
		private float fA, fB, fC;
		public void Request(float forceA, float forceB, float forceC)
		{
			if (fA == forceA && fB == forceB && fC == forceC)
				return;

			fA = forceA;
			fB = forceB;
			fC = forceC;

			String message = fA + ";" + fB + ";" + fC + "\n";

			this.ProcessConnection ();
			this.webSocket.Send (message);
			this.webSocket.Close ();
			//Debug.Log ("Requested: " + message);
		}
	}
}
