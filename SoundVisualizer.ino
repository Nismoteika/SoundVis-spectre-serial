#include "LedControl.h"

#define NUM_OF_COLUMNS 16
//значения яркости от 0 до 15   
#define BRIGHTNESS 5

//LedControl(int dataPin, int clkPin, int csPin, int numDevices);
LedControl matrix = LedControl(16, 15, 14, 2);

byte val_cols[NUM_OF_COLUMNS];

void setup() 
{
  Serial.begin(19200);
  matrix.shutdown(0, false);
  matrix.shutdown(1, false); 

  //задаём яркость
  matrix.setIntensity(0, BRIGHTNESS);
  matrix.setIntensity(1, BRIGHTNESS);

  //очищаем матрицы
  matrix.clearDisplay(0);
  matrix.clearDisplay(1);
}

void loop()
{
  if (Serial.available() > 0) {
    Serial.readBytes(val_cols, NUM_OF_COLUMNS);
    //колонки
    ////строки
    for(int i = 0; i < 8; i++) {
      for(int j = 0; j < val_cols[i]; j++)
        matrix.setLed(0, i, j, true);//7-i
    }
    for(int i = 0; i < 8; i++) {
      for(int j = 0; j < val_cols[i+8]; j++)//i
        matrix.setLed(1, i, j, true);
    }
    Serial.flush();
  }
  
  matrix.clearDisplay(0);
  matrix.clearDisplay(1);
}
