/**
 * VR-OR Connection to Arduino
 *
 * @file Arduino.cs
 * @author Uwe Gruenefeld
 * @version 2017-10-07
 **/
﻿using System;
using WebSocketSharp;

namespace UnityEngine
{
	/// <summary>
	/// Implements MonoBehaviour and connects to server via websocket
	/// </summary>
	public class Arduino : MonoBehaviour
	{
		[HeaderAttribute("Connection")]
		public string IP = "localhost";
		public int PORT = 8080;

		// Variables for websocket connection
		private WebSocket webSocket;

		// Use this for initialization
		void Start () {
			
		}
		
		/// <summary>
		/// Update is called once per frame by Unity
		/// </summary>
		void Update () 
		{
			// Check connection
			if (!this.ProcessConnection ())
				return;
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

		// Handling a response
		private void Response(string message)
		{
			Debug.Log ("Recieved: " + message);
		}
	}
}
