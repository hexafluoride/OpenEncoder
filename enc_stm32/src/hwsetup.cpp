#include "hwsetup.h"

ADC_HandleTypeDef hadc;
int conv_start = 0;
int buf_index = 0;
uint16_t buf[RAW_BUFFER_SIZE];

void setup_adc() {
    hadc.Instance = ADC1;
    hadc.Init.ScanConvMode = ADC_SCAN_DISABLE;
    hadc.Init.ContinuousConvMode = DISABLE;
    hadc.Init.DiscontinuousConvMode = DISABLE;
    hadc.Init.ExternalTrigConv = ADC_SOFTWARE_START;
    hadc.Init.DataAlign = ADC_DATAALIGN_RIGHT;
    hadc.Init.NbrOfConversion = 1;

    if (HAL_ADC_Init(&hadc) != HAL_OK)
    {
    }
}

void read_burst(uint32_t channel, int count, int calibrate = 8) {
// HAL_ADC_Init(&hadc);

     ADC_ChannelConfTypeDef adc_config = {0};
     adc_config.Channel = channel;
     adc_config.Rank = ADC_REGULAR_RANK_1;
     adc_config.SamplingTime = ADC_SAMPLETIME_1CYCLE_5;

//     HAL_ADC_ConfigChannel(&hadc, &adc_config);

//     HAL_ADCEx_Calibration_Start(&hadc);

    conv_start = buf_index;

    // for (int q = 0; q < 8; q++) { 

    // HAL_ADC_Start(&hadc);
    //     HAL_ADC_PollForConversion(&hadc, 1);
    // }
    hadc.Instance = ADC1;
    hadc.Init.ScanConvMode = ADC_SCAN_DISABLE;
    hadc.Init.ContinuousConvMode = DISABLE;
    hadc.Init.DiscontinuousConvMode = DISABLE;
    hadc.Init.ExternalTrigConv = ADC_SOFTWARE_START;
    hadc.Init.DataAlign = ADC_DATAALIGN_RIGHT;
    hadc.Init.NbrOfConversion = 1;
HAL_ADC_Init(&hadc);
    HAL_ADC_ConfigChannel(&hadc, &adc_config);

    for (int q = 0; q < calibrate; q++) {

    HAL_ADCEx_Calibration_Start(&hadc);
    }

    for (int i = 0; i < count; i++) {

    HAL_ADCEx_Calibration_Start(&hadc);
    HAL_ADC_Start(&hadc);
        if (HAL_ADC_PollForConversion(&hadc, 1) == HAL_OK) {
            buf[buf_index] = HAL_ADC_GetValue(&hadc);
            buf_index++;

            if (buf_index >= RAW_BUFFER_SIZE) {
                buf_index = 0;
            }
        }
    HAL_ADC_Stop(&hadc);
    }
    HAL_ADC_DeInit(&hadc);

    // HAL_ADC_Stop(&hadc);
    // HAL_ADC_DeInit(&hadc);
}

