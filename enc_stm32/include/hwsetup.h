#ifndef HWSETUP_H
#define HWSETUP_H
#include "main.h"

#define RAW_BUFFER_SIZE 4096

void setup_adc();
void read_burst(uint32_t channel, int count, int calibrate);

extern int conv_start ;
extern int buf_index;
extern uint16_t buf[RAW_BUFFER_SIZE];
#endif