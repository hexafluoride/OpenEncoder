#include <Arduino.h>
#include <HardwareSerial.h>

#define ANALOG_START_PIN PA0
#define LED_START_PIN PB3
#define CHANNEL_COUNT 6

#define RAW_BUFFER_SIZE 512

HardwareSerial s1(PA10, PA9);

void setup() {
  s1.begin(2000000);

  for (int i = 0; i < CHANNEL_COUNT; i++) {
    pinMode(LED_START_PIN + i, OUTPUT);
    pinMode(ANALOG_START_PIN + i, INPUT_ANALOG);
  }
}

uint16_t raw_buf[RAW_BUFFER_SIZE];
int last_written_raw = 0;

int period = 5;
int delay_us = 0;
int inner_delay_us = 0;
int switch_delay_us = 0;
int switch_time = 2;

void readChannelState(int channel) {
  int p = switch_time;
  int start_index_r = last_written_raw;
  int turn_on_index = 0;
  
  bool state = false;
  
  while (true) {
    auto val = analogRead(ANALOG_START_PIN + channel);
    raw_buf[last_written_raw] = val;
    last_written_raw = (last_written_raw + 1) % RAW_BUFFER_SIZE;
    p++;
    
    if (p > period) {
      p = 0;
      state = !state;
      digitalWrite(LED_START_PIN + channel, state ? HIGH : LOW);
  
      if (state) {
        if (switch_delay_us)
          delayMicroseconds(switch_delay_us);
        turn_on_index = last_written_raw - start_index_r;

        if (turn_on_index < 0) {
          turn_on_index += RAW_BUFFER_SIZE;
        }
      }

      if (!state) {break;}
    }

    if (inner_delay_us)
      delayMicroseconds(inner_delay_us);
  }

  digitalWrite(LED_START_PIN + channel, LOW);

  int raw_count = last_written_raw - start_index_r;
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
    s1.write(((uint8_t*)&raw_buf) + (_index * 2), 2);
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

      analogRead(next);

      if (delay_us)
        delayMicroseconds(delay_us);
  }

  g++;

  if (g % 20 == 0) {
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
      }
    }
  }
}