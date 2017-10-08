#!/bin/node

/**
 * Connector between Unity and Arduino
 *
 * @file server.js
 * @author Uwe Gruenefeld
 * @version 2017-10-07
 **/

console.log("Please assign 192.168.43.200 to ur computer an connect to Hotspot.")

var config = require('./config.json');
var msgFromUnity = null, msgFromArduino = null;

/*
 * Connection to Unity
 */
if(config.unity.active) {
  var Websocket   = require("nodejs-websocket");
  var unity	      = Websocket.createServer(function(connection) {
    if(config.debug)
      console.log("New socket connection");

    function rand() {
      return Math.floor(Math.random() * config.arduino.pot);
    }

    function loop() {
      // Fake it till you make it
      try {
        if(config.unity.fake)
          connection.sendText(rand() + ' ' + rand() + ' ' + rand());
        else if(msgFromArduino != null) {
          connection.sendText(String(msgFromArduino));
          msgFromArduino = null;
        }
        setTimeout(loop, 100);
      } catch(exc) {}
    }
    loop();

    // Handle event for incoming text
    connection.on("text", function(message) {
      if(config.debug)
        console.log("Unity: " + message);
      msgFromUnity = message;
    });

    // Handle event for close connection
  	connection.on("close", function() {
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
if(config.arduino.active) {
  const SerialPort = require("serialport");
  const Readline = SerialPort.parsers.Readline;
  const port = new SerialPort('/dev/tty.usbmodem' + config.arduino.port);
  const parser = new Readline();
  port.pipe(parser);
  parser.on('data', function(data) {
    if(config.debug)
      console.log("Arduino: " + data);
    msg = data;
  });

  function rand() {
    return Math.floor(Math.random() * config.arduino.servo);
  }

  function loop() {
    // Fake it till you make it
    try {
      if(config.arduino.fake)
        connection.sendText(rand() + ' ' + rand() + ' ' + rand());
      else if(msgFromUnity != null) {
        connection.sendText(String(msgFromUnity));
        msgFromUnity = null;
      }
      setTimeout(loop, 100);
    } catch(exc) {}
  }
  loop();

  if(config.debug)
    console.log('Arduino connection is running on /dev/tty.usbmodem' + config.arduino.port);
}
