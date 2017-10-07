#!/bin/node

/**
 * Connector between Unity and Arduino
 *
 * @file server.js
 * @author Uwe Gruenefeld
 * @version 2017-10-07
 **/

var config = require('./config.json');

/*
 * Connection to Unity
 */
if(config.unity.active)
{
  var Websocket   = require("nodejs-websocket");
  var unity	      = Websocket.createServer(function(connection)
  {
    if(config.debug)
      console.log("New socket connection");

    // Fake it till you make it
    if(config.unity.fake)
    {
      function rand() {
        return Math.floor(Math.random() * config.arduino.servo);
      }

      function fake() {
        connection.sendText(rand() + ' ' + rand() + ' ' + rand());
        setTimeout(fake, 1000);
      }
      fake();
    }

    // Handle event for incoming text
    connection.on("text", function(message)
    {

    });

    // Handle event for close connection
  	connection.on("close", function()
  	{
      if(config.debug)
  		  console.log("Socket connection closed");
  	});
  });
  unity.listen(config.unity.port);
  if(config.debug)
    console.log('Unity connection is running on ws://localhost:' + config.unity.port);
}

/*
 * Connection to Arduino
 */
if(config.arduino.active)
{
  var SerialPort = require("serialport");
  var arduino = new SerialPort("/dev/tty.usbmodem" + config.arduinoPort, {
    baudrate: 9600,
    dataBits: 8,
    parity: 'none',
    stopBits: 1,
    flowControl: false
  });

  arduino.on('open', function()
  {
    arduino.on('data', function(data)
  	{
      if(config.debug)
        console.log(data[0]);
    });
  });
  if(config.debug)
    console.log('Arduino connection is running on /dev/tty.usbmodem' + config.arduino.port);
}
