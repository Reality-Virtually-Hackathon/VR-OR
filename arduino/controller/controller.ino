/*
 * Copyright (c) 2017, Uwe Gruenefeld
 * Controller for Connection to Unity
 */
int tolerance = 1;
int pot0 = 0, pot1 = 0, pot2 = 0;

bool check(int val1, int val2)
{
  if(abs(val1 - val2) > tolerance)
    return true;
  return false;
}

void setup() {
  Serial.begin(9600);
}

void loop() {
  int temp0 = analogRead(A0);
  int temp1 = 0; //analogRead(A1);
  int temp2 = 0; //analogRead(A2);

  bool change = check(temp0, pot0);

  if(change) {
    pot0 = temp0, pot1 = temp1, pot2 = temp2;
    Serial.println(pot0 + (String)' ' + pot1 + (String)' ' + pot2);
  }
  
  delay(1);
}
