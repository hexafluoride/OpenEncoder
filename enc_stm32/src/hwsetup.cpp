#include "hwsetup.h"

ADC_HandleTypeDef hadc;
int conv_start = 0;
int buf_index = 0;
uint16_t buf[512];

void setup_adc() {
    hadc.Instance = ADC1;
    hadc.Init.ScanConvMode = ADC_SCAN_DISABLE;
    hadc.Init.ContinuousConvMode = DISABLE;
    hadc.Init.DiscontinuousConvMode = ENABLE;
    hadc.Init.ExternalTrigConv = ADC_SOFTWARE_START;
    hadc.Init.DataAlign = ADC_DATAALIGN_RIGHT;
    hadc.Init.NbrOfConversion = 1;

    if (HAL_ADC_Init(&hadc) != HAL_OK)
    {
    }
}

void read_burst(uint32_t channel, int count) {
    ADC_ChannelConfTypeDef adc_config = {0};
    adc_config.Channel = channel;
    adc_config.Rank = ADC_REGULAR_RANK_1;
    adc_config.SamplingTime = ADC_SAMPLETIME_1CYCLE_5;

    HAL_ADC_ConfigChannel(&hadc, &adc_config);

    HAL_ADC_Start(&hadc);

    conv_start = buf_index;

    for (int i = 0; i < count; i++) {
        if (HAL_ADC_PollForConversion(&hadc, 1) == HAL_OK) {
            buf[buf_index] = HAL_ADC_GetValue(&hadc);
            buf_index++;

            if (buf_index >= 512) {
                buf_index = 0;
            }
        }
    }
}

