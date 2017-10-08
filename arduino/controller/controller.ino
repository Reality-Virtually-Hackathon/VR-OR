/*
 * Copyright (c) 2017, Uwe Gruenefeld
 * Controller for Connection to Unity
 */
#include <Wire.h>
#include <Adafruit_PWMServoDriver.h>
#include <string.h>

// called this way, it uses the default address 0x40
Adafruit_PWMServoDriver pwm = Adafruit_PWMServoDriver();
#define SERVOMIN  150 // this is the 'minimum' pulse length count (out of 4096)
#define SERVOMAX  350 // this is the 'maximum' pulse length count (out of 4096)

int tolerance = 1;
int pot0 = 0, pot1 = 0, pot2 = 0;

uint8_t baseservo = 0;
uint8_t arm0servo = 1;
uint8_t arm1servo = 2;

int potPin = 2;
int potPin2 = 3;
int potPin3 = 4;    // select the input pin for the potentiometer

int ledPin = 13;   // select the pin for the LED

int val = 0;
int val1 = 0;       // variable to store the value coming from the sensor
int val2 = 0;


bool check(int val1, int val2)
{
  if(abs(val1 - val2) > tolerance)
    return true;
  return false;
}

void setup() {
  Serial.begin(9600);
  pwm.begin();
  
  pwm.setPWMFreq(65);  // Analog servos run at ~60 Hz updates

  yield();
}

// you can use this function if you'd like to set the pulse length in seconds
// e.g. setServoPulse(0, 0.001) is a ~1 millisecond pulse width. its not precise!
void setServoPulse(uint8_t n, double pulse) {
  double pulselength;
  
  pulselength = 1000000;   // 1,000,000 us per second
  pulselength /= 60;   // 60 Hz
  Serial.print(pulselength); Serial.println(" us per period"); 
  pulselength /= 4096;  // 12 bits of resolution
  Serial.print(pulselength); Serial.println(" us per bit"); 
  pulse *= 1000;
  pulse /= pulselength;
  Serial.println(pulse);
  pwm.setPWM(n, 0, pulse);
}

void SetTensionServo(int joint, int tension) {
  int servonum = 0;
  if(joint == 0 ) servonum = baseservo;

  if(joint == 1 )
  {
    servonum = arm0servo;
    if(tension > 0) tension = 0;
    if (tension == 0) tension = 100;
  } 

  if(joint == 2 )
  {
    servonum = arm1servo;
    if(tension>0) tension = 0;
    if (tension == 0) tension = 100;
  } 

  int mapped_tension = map(tension, 1, 100, SERVOMIN, SERVOMAX);

 if (tension == 100) 
 {
  for (uint16_t pulselen = SERVOMIN; pulselen < SERVOMAX; pulselen++) {
    pwm.setPWM(servonum, 0, pulselen);
  }
 }
 else
 {
  for (uint16_t pulselen = SERVOMAX; pulselen > SERVOMIN; pulselen--) {
    pwm.setPWM(servonum, 0, pulselen);
  }
 }
  
}

String getValue(String data, char separator, int index)
{
  int found = 0;
  int strIndex[] = {0, -1};
  int maxIndex = data.length() - 1;
  for(int i=0; i<=maxIndex && found<=index; i++)
  {
    if(data.charAt(i)==separator || i==maxIndex)
    {
        found++;
        strIndex[0] = strIndex[1] + 1;
        strIndex[1] = (i == maxIndex) ? i + 1 : i;
    }
  }
  return found > index ? data.substring(strIndex[0], strIndex[1]) : "";
}


void parseResponse(String serialResponse) {
  String ten1 = getValue(serialResponse, ' ', 0);
  String ten2 = getValue(serialResponse, ' ', 1);
  String ten3 = getValue(serialResponse, ' ', 2);

  /*
  SetTensionServo(0, ten1.toInt());
  SetTensionServo(1, ten2.toInt());
  SetTensionServo(2, ten3.toInt());
  */
  
}

void loop() {
  int temp0 = analogRead(A0);
  int temp1 = 0; //analogRead(A1);
  int temp2 = 0; //analogRead(A2);

  val = analogRead(potPin);    // read the value from the sensor
  val1 = analogRead(potPin2);
  val2 = analogRead(potPin3);

  bool change = check(temp0, pot0);

  //if(change) 
  {
    pot0 = temp0, pot1 = temp1, pot2 = temp2;
    //Serial.println(val + (String)' ' + val1 + (String)' ' + val2);
  }

  String serialResponse;
   while (Serial.available()) {
    serialResponse = Serial.readStringUntil('\r\n');
  }

  if (serialResponse.length() >0) {
      parseResponse(serialResponse); //see what was received
    }
  
  delay(1);
}
