/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Sensors.Extensions;
using Sensors.Model;
using SkiaSharp;
using System;
using System.Collections.Generic;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace Sensors.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PressurePage : CirclePage
    {
        public PressurePage()
        {
            Model = new PressureModel
            {
                IsSupported = Tizen.Sensor.PressureSensor.IsSupported,
                SensorCount = Tizen.Sensor.PressureSensor.Count,
            };

            InitializeComponent();

            if (Model.IsSupported)
            {
                Barometer.ReadingChanged += Pressure_DataUpdated;

                canvas.ChartScale = 2500;
                canvas.Series = new List<Series>()
                {
                    new Series()
                    {
                        Color = SKColors.Red,
                        Name = "Pressure",
                        FormattedText = "Pressure={0}hPa",
                    },
                };
            }
        }

        public PressureModel Model { get; private set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Barometer.Start(SensorSpeed.Default);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Barometer.Stop();
        }

        private void Pressure_DataUpdated(object sender, BarometerChangedEventArgs e)
        {
            Model.Pressure = (float)e.Reading.PressureInHectopascals;
            canvas.Series[0].Points.Add(new Extensions.Point() { Ticks = DateTime.UtcNow.Ticks, Value = (float)e.Reading.PressureInHectopascals });
            canvas.InvalidateSurface();
        }
    }
}