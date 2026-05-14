#include <Keypad.h>
#include <Wire.h>
#include <LiquidCrystal_I2C.h>

int rowNum = 0;
LiquidCrystal_I2C lcd1(0x27, 16, 2);
const int buzzer = 10;
const byte ROWS = 4;  //four rows
const byte COLS = 4;  //four columns
char keys[ROWS][COLS] = {
  { '1', '2', 'A', 'B' },
  { '3', '4', 'C', 'D' },
  { '5', '6', 'E', 'F' },
  { '7', '8', 'G', 'H' }
};

byte rowPins[ROWS] = { 5, 4, 3, 2 };  //connect to the row pinouts of the keypad
byte colPins[COLS] = { 9, 8, 7, 6 };  //connect to the column pinouts of the keypad

Keypad keypad = Keypad(makeKeymap(keys), rowPins, colPins, ROWS, COLS);

void setup() {
  pinMode(12, OUTPUT);
  pinMode(buzzer, OUTPUT);
  lcd1.begin();
  lcd1.backlight();
  Wire.begin();
  Serial.begin(9600);
  Serial.println(" ");
  Serial.println("\nI2C Scanner");
  Serial.println("Tell your opponent what piece you are moving");
  Serial.println("If it doesnt say its your opponents turn or it does it to early redo it till it works it also wont indicate checkmate");
  Serial.println("Pick your column and number");
  Serial.println("The buzzer will go off twice and that will indicate a move swap");
  Serial.println("The led will be off at the start when it turns on it will be blacks move");
}

void loop() {
  char keys = keypad.getKey();

  if (keys) {
    lcd1.setCursor(0, rowNum);
    lcd1.print(keys);
    if (rowNum < 1) {
      rowNum++;
    } else {
      rowNum = 0;
      tone(buzzer, 1000);
      delay(500);
      noTone(buzzer);
      delay(500);
      digitalWrite(12, LOW);
      delay(1000);
      digitalWrite(12, HIGH);
      delay(1000);
    }
  }
}
