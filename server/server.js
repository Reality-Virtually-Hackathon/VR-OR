#!/bin/node

/**
 * Connector between Unity and Arduino
 *
 * @file server.js
 * @author Uwe Gruenefeld
 * @version 2017-10-07
 **/

 var config		= require('./config.json');

 /*
  * Connection to Unity
  */
 var websocket = require("nodejs-websocket");
 var unity	   = websocket.createServer(function(connection)
 {
 	console.log("New socket connection");

  // Handle event for incoming text
  connection.on("text", function(message)
  {

  }

  // Handle event for close connection
	connection.on("close", function()
	{
		console.log("Socket connection closed");
	});
});
unity.listen(config.port);
console.log('Unity connection is running on ws://localhost:' + config.unityPort + '/');

/*
 * Connection to Arduino
 */
var serial    = require("serialport").SerialPort;
var arduino   = new SerialPort("/dev/tty.usbmodem" + config.arduinoPort);
arduino.on('open', function()
{
  arduino.on('data', function(data)
	{
      console.log(data[0]);
  });
});
console.log('Arduino connection is running on http://localhost:' + conf.arduinoPort + '/');
