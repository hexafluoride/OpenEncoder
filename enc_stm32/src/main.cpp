#define USE_HAL_DRIVER
#include "main.h"
#include "hwsetup.h"

#define ANALOG_START_PIN PA0
#define LED_START_PIN PB3
#define CHANNEL_COUNT 6


HardwareSerial s1(PA10, PA9);

void setup() {

  for (int i = 0; i < CHANNEL_COUNT; i++) {
    pinMode(LED_START_PIN + i, OUTPUT);
    pinMode(ANALOG_START_PIN + i, INPUT_ANALOG);
    digitalWrite(LED_START_PIN + i, LOW);
  }

  setup_adc();

  s1.begin(2000000);

}

uint16_t raw_buf[RAW_BUFFER_SIZE];
int last_written_raw = 0;

int period = 10;
int delay_us = 0;
int inner_delay_us = 0;
int switch_delay_us = 0;
int switch_time = 0;
int calibrate = 5;

void readChannelState(int channel) {
  int p = switch_time;
  int start_index_r = buf_index;
  int turn_on_index = 0;
  
  bool state = false;

  digitalWrite(LED_START_PIN + channel, LOW);
  read_burst(channel, period - switch_time, calibrate);
  turn_on_index = buf_index - start_index_r;
  digitalWrite(LED_START_PIN + channel, HIGH);
  read_burst(channel, period, calibrate);
  digitalWrite(LED_START_PIN + channel, LOW);

  int raw_count = buf_index - start_index_r;
  if (raw_count < 0) {
    raw_count += RAW_BUFFER_SIZE;
  }

  s1.write(0xFE);
  s1.write(0xFE);
  s1.write(0xFE);
  s1.write(0xFE);
  s1.write(0xCC);
  s1.write(channel);
  s1.write(turn_on_index);
  s1.write(raw_count);

  for (int k = 0; k < raw_count; k++) {
    int _index = k + start_index_r;
    if (_index >= RAW_BUFFER_SIZE) { 
      _index = _index - RAW_BUFFER_SIZE;
    }
    s1.write(((uint8_t*)&buf) + (_index * 2), 2);
  }
}

int g = 0;

void loop() {
  for (int pin = 0; pin < CHANNEL_COUNT; pin++) {
      readChannelState(pin);
      auto next = ANALOG_START_PIN + pin + 1;
      if (pin == CHANNEL_COUNT - 1) {
        next = ANALOG_START_PIN;
      }

      //analogRead(next);

      if (delay_us)
        delayMicroseconds(delay_us);
  }

  g++;

  if (g % 3 == 0) {
    g = 0;

    if (s1.available() > 0) {
      int preamble_count = 0;

      while (s1.read() == 0xcc && preamble_count < 4)
        preamble_count++;

      if (preamble_count != 4) {
        while (s1.available()) {
          s1.read();
        }
        return;
      }

      int config_item = s1.read();
      switch (config_item) {
        case 0xb0:
          period = s1.read();
          break;
        case 0xb1:
          delay_us = s1.read();
          break;
        case 0xb3:
          switch_time = s1.read();
          if (switch_time > 127) {
            switch_time = -(switch_time - 127);
          }
          break;
        case 0xb4:
          inner_delay_us = s1.read();
          break;
        case 0xb5:
          switch_delay_us = s1.read();
          break;
        case 0xb6:
          calibrate = s1.read();
          break;
      }
    }
  }
}