#ifndef HWSETUP_H
#define HWSETUP_H
#include "main.h"

void setup_adc();
void read_burst(uint32_t channel, int count);

extern int conv_start ;
extern int buf_index;
extern uint16_t buf[512];
#endif