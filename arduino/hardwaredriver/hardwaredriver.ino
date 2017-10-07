int potPin = 2;
int potPin2 = 3;
int potPin3 = 4;    // select the input pin for the potentiometer

int ledPin = 13;   // select the pin for the LED

int val = 0;
int val1 = 0;       // variable to store the value coming from the sensor
int val2 = 0;

void setup() {
	Serial.begin(9600); 
  	pinMode(ledPin, OUTPUT);  // declare the ledPin as an OUTPUT
}

void loop() {
	  val = analogRead(potPin);    // read the value from the sensor
	  val1 = analogRead(potPin2);
	  val2 = analogRead(potPin3);

	  // digitalWrite(ledPin, HIGH);  // turn the ledPin on
	  // delay(val);                  // stop the program for some time
	  // digitalWrite(ledPin, LOW);   // turn the ledPin off
	  // delay(val);                  // stop the program for some time

	  Serial.print(val);
	  Serial.print(" ");
	  Serial.print(val1);
	  Serial.print(" ");
	  Serial.print(val2);
	  Serial.println();
	  delay(100);
}