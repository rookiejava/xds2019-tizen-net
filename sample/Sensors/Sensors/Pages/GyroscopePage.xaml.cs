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
    public partial class GyroscopePage : CirclePage
    {
        public GyroscopePage()
        {
            Model = new GyroscopeModel
            {
                IsSupported = Tizen.Sensor.Gyroscope.IsSupported,
                SensorCount = Tizen.Sensor.Gyroscope.Count,
            };

            InitializeComponent();

            if (Model.IsSupported)
            {
                Gyroscope.ReadingChanged += Gyroscope_DataUpdated;

                canvas.ChartScale = 1000;
                canvas.Series = new List<Series>()
                {
                    new Series()
                    {
                        Color = SKColors.Red,
                        Name = "X",
                        FormattedText = "X={0:f2}°/s",
                    },
                    new Series()
                    {
                        Color = SKColors.Green,
                        Name = "Y",
                        FormattedText = "Y={0:f2}°/s",
                    },
                    new Series()
                    {
                        Color = SKColors.Blue,
                        Name = "Z",
                        FormattedText = "Z={0:f2}°/s",
                    },
                };
            }
        }

        public GyroscopeModel Model { get; private set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Gyroscope.Start(SensorSpeed.Default);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Gyroscope.Stop();
        }

        private void Gyroscope_DataUpdated(object sender, GyroscopeChangedEventArgs e)
        {
            var data = e.Reading;
            Model.X = data.AngularVelocity.X;
            Model.Y = data.AngularVelocity.Y;
            Model.Z = data.AngularVelocity.Z;

            long ticks = DateTime.UtcNow.Ticks;
            foreach (var serie in canvas.Series)
            {
                switch (serie.Name)
                {
                    case "X":
                        serie.Points.Add(new Extensions.Point() { Ticks = ticks, Value = data.AngularVelocity.X });
                        break;

                    case "Y":
                        serie.Points.Add(new Extensions.Point() { Ticks = ticks, Value = data.AngularVelocity.Y });
                        break;

                    case "Z":
                        serie.Points.Add(new Extensions.Point() { Ticks = ticks, Value = data.AngularVelocity.Z });
                        break;
                }
            }
            canvas.InvalidateSurface();
        }
    }
}