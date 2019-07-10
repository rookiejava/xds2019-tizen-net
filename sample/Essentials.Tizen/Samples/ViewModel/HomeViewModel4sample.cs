﻿using System.Collections.Generic;
using System.Linq;
using Samples.Model;
using Samples.View;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Samples.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        bool alreadyAppeared;
        SampleItem[] samples;
        IEnumerable<SampleItem> filteredItems;
        string filterText;

        public HomeViewModel()
        {
            alreadyAppeared = false;
            GetSamples();
            filteredItems = samples;
            filterText = string.Empty;
        }

        void GetSamples()
        {
            var list = new List<SampleItem>();

            if (Device.Idiom == TargetIdiom.Watch)
            {
                list.Add(new SampleItem(
                "📏",
                "Accelerometer",
                typeof(CircleAccelerometerPage),
                "Retrieve acceleration data of the device in 3D space.",
                new[] { "accelerometer", "sensors", "hardware", "device" }));

                list.Add(new SampleItem(
                "📏",
                "Barometer",
                typeof(CircleBarometerPage),
                "Easily detect pressure level, via the device barometer.",
                new[] { "barometer", "hardware", "device", "sensor" }));

                list.Add(new SampleItem(
                "📶",
                "Connectivity",
                typeof(CircleConnectivityPage),
                "Check connectivity state and detect changes.",
                new[] { "connectivity", "internet", "wifi" }));

                list.Add(new SampleItem(
                "📁",
                "File System",
                typeof(CircleFileSystemPage),
                "Easily save files to app data.",
                new[] { "files", "directory", "filesystem", "storage" }));

                list.Add(new SampleItem(
                "📍",
                "Geocoding",
                typeof(CircleGeocodingPage),
                "Easily geocode and reverse geocoding.",
                new[] { "geocoding", "geolocation", "position", "address", "mapping" }));

                list.Add(new SampleItem(
                "📍",
                "Geolocation",
                typeof(CircleGeolocationPage),
                "Quickly get the current location.",
                new[] { "geolocation", "position", "address", "mapping" }));

                list.Add(new SampleItem(
                "📏",
                "Gyroscope",
                typeof(CircleGyroscopePage),
                "Retrieve rotation around the device's three primary axes.",
                new[] { "gyroscope", "sensors", "hardware", "device" }));

                list.Add(new SampleItem(
                "📞",
                "Phone Dialer",
                typeof(CirclePhoneDialerPage),
                "Easily open the phone dialer.",
                new[] { "phone", "dialer", "communication", "call" }));

                list.Add(new SampleItem(
                "⚙️",
                "Preferences",
                typeof(CirclePreferencesPage),
                "Quickly and easily add persistent preferences.",
                new[] { "settings", "preferences", "prefs", "storage" }));

                list.Add(new SampleItem(
                "🔒",
                "Secure Storage",
                typeof(CircleSecureStoragePage),
                "Securely store data.",
                new[] { "settings", "preferences", "prefs", "security", "storage" }));

                list.Add(new SampleItem(
                "💬",
                "SMS",
                typeof(CircleSMSPage),
                "Easily send SMS messages.",
                new[] { "sms", "message", "text", "communication", "share" }));

                list.Add(new SampleItem(
                "🔊",
                "Text To Speech",
                typeof(CircleTextToSpeechPage),
                "Vocalize text on the device.",
                new[] { "text", "message", "speech", "communication" }));

                list.Add(new SampleItem(
                "📳",
                "Vibration",
                typeof(CircleVibrationPage),
                "Quickly and easily make the device vibrate.",
                new[] { "vibration", "vibrate", "hardware", "device" }));
            }
            else
            {
                if (DeviceInfo.Idiom != DeviceIdiom.TV)
                {
                    list.Add(new SampleItem(
                    "📏",
                    "Accelerometer",
                    typeof(AccelerometerPage),
                    "Retrieve acceleration data of the device in 3D space.",
                    new[] { "accelerometer", "sensors", "hardware", "device" }));

                    list.Add(new SampleItem(
                    "📏",
                    "All Sensors",
                    typeof(AllSensorsPage),
                    "Have a look at the accelerometer, barometer, compass, gyroscope, magnetometer, and orientation sensors.",
                    new[] { "accelerometer", "barometer", "compass", "gyroscope", "magnetometer", "orientation", "sensors", "hardware", "device" }));

                    list.Add(new SampleItem(
                    "📦",
                    "App Info",
                    typeof(AppInfoPage),
                    "Find out about the app with ease.",
                    new[] { "app", "info" }));

                    list.Add(new SampleItem(
                    "📏",
                    "Barometer",
                    typeof(BarometerPage),
                    "Easily detect pressure level, via the device barometer.",
                    new[] { "barometer", "hardware", "device", "sensor" }));

                    list.Add(new SampleItem(
                    "🌐",
                    "Browser",
                    typeof(BrowserPage),
                    "Quickly and easily open a browser to a specific website.",
                    new[] { "browser", "web", "internet" }));
                
                    list.Add(new SampleItem(
                    "📏",
                    "Compass",
                    typeof(CompassPage),
                    "Monitor compass for changes.",
                    new[] { "compass", "sensors", "hardware", "device" }));
                }

                list.Add(new SampleItem(
                    "📶",
                    "Connectivity",
                    typeof(ConnectivityPage),
                    "Check connectivity state and detect changes.",
                    new[] { "connectivity", "internet", "wifi" }));

                if (DeviceInfo.Idiom != DeviceIdiom.TV)
                {
                    list.Add(new SampleItem(
                    "📧",
                    "Email",
                    typeof(EmailPage),
                    "Easily send email messages.",
                    new[] { "email", "share", "communication", "message" }));
                }

                list.Add(new SampleItem(
                "📁",
                "File System",
                typeof(FileSystemPage),
                "Easily save files to app data.",
                new[] { "files", "directory", "filesystem", "storage" }));

                if (DeviceInfo.Idiom != DeviceIdiom.TV)
                {
                    list.Add(new SampleItem(
                    "🔦",
                    "Flashlight",
                    typeof(FlashlightPage),
                    "A simple way to turn the flashlight on/off.",
                    new[] { "flashlight", "torch", "hardware", "flash", "device" }));
                }

                list.Add(new SampleItem(
                    "📍",
                    "Geocoding",
                    typeof(GeocodingPage),
                    "Easily geocode and reverse geocoding.",
                    new[] { "geocoding", "geolocation", "position", "address", "mapping" }));

                if (DeviceInfo.Idiom != DeviceIdiom.TV)
                {
                    list.Add(new SampleItem(
                    "📍",
                    "Geolocation",
                    typeof(GeolocationPage),
                    "Quickly get the current location.",
                    new[] { "geolocation", "position", "address", "mapping" }));
                
                    list.Add(new SampleItem(
                    "📏",
                    "Launcher",
                    typeof(LauncherPage),
                    "Launch other apps via Uri",
                    new[] { "launcher", "app", "run" }));

                    list.Add(new SampleItem(
                    "📏",
                    "Gyroscope",
                    typeof(GyroscopePage),
                    "Retrieve rotation around the device's three primary axes.",
                    new[] { "gyroscope", "sensors", "hardware", "device" }));

                    list.Add(new SampleItem(
                    "📏",
                    "Magnetometer",
                    typeof(MagnetometerPage),
                    "Detect device's orientation relative to Earth's magnetic field.",
                    new[] { "compass", "magnetometer", "sensors", "hardware", "device" }));

                    list.Add(new SampleItem(
                    "📍",
                    "Launch Maps",
                    typeof(MapsPage),
                    "Easily launch maps with coordinates.",
                    new[] { "geocoding", "geolocation", "position", "address", "mapping", "maps", "route", "navigation" }));

                    list.Add(new SampleItem(
                    "📏",
                    "Orientation Sensor",
                    typeof(OrientationSensorPage),
                    "Retrieve orientation of the device in 3D space.",
                    new[] { "orientation", "sensors", "hardware", "device" }));

                    list.Add(new SampleItem(
                    "📞",
                    "Phone Dialer",
                    typeof(PhoneDialerPage),
                    "Easily open the phone dialer.",
                    new[] { "phone", "dialer", "communication", "call" }));
                }

                list.Add(new SampleItem(
                "⚙️",
                "Preferences",
                typeof(PreferencesPage),
                "Quickly and easily add persistent preferences.",
                new[] { "settings", "preferences", "prefs", "storage" }));

                list.Add(new SampleItem(
                "🔒",
                "Secure Storage",
                typeof(SecureStoragePage),
                "Securely store data.",
                new[] { "settings", "preferences", "prefs", "security", "storage" }));

                if (DeviceInfo.Idiom != DeviceIdiom.TV)
                {
                    list.Add(new SampleItem(
                    "📲",
                    "Share",
                    typeof(SharePage),
                    "Send text, website uris and files to other apps.",
                    new[] { "data", "transfer", "share", "communication" }));

                    list.Add(new SampleItem(
                    "💬",
                    "SMS",
                    typeof(SMSPage),
                    "Easily send SMS messages.",
                    new[] { "sms", "message", "text", "communication", "share" }));
                }

                list.Add(new SampleItem(
                "🔊",
                "Text To Speech",
                typeof(TextToSpeechPage),
                "Vocalize text on the device.",
                new[] { "text", "message", "speech", "communication" }));

                if (DeviceInfo.Idiom != DeviceIdiom.TV)
                {
                    list.Add(new SampleItem(
                    "📳",
                    "Vibration",
                    typeof(VibrationPage),
                    "Quickly and easily make the device vibrate.",
                    new[] { "vibration", "vibrate", "hardware", "device" }));
                }
            }

            samples = new SampleItem[list.Count];
            var index = 0;
            foreach (var item in list)
            {
                samples[index++] = item;
            }
        }

        public IEnumerable<SampleItem> FilteredItems
        {
            get => filteredItems;
            private set => SetProperty(ref filteredItems, value);
        }

        public string FilterText
        {
            get => filterText;
            set
            {
                SetProperty(ref filterText, value);
                FilteredItems = Filter(samples, value);
            }
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            if (!alreadyAppeared)
            {
                alreadyAppeared = true;

                if (VersionTracking.IsFirstLaunchEver)
                {
                    DisplayAlertAsync("Welcome to the Samples! This is the first time you are launching the app!");
                }
                else if (VersionTracking.IsFirstLaunchForCurrentVersion)
                {
                    var count = VersionTracking.VersionHistory.Count();
                    DisplayAlertAsync($"Welcome to the NEW Samples! You have tried {count} versions.");
                }
            }
        }

        static IEnumerable<SampleItem> Filter(IEnumerable<SampleItem> samples, string filterText)
        {
            if (!string.IsNullOrWhiteSpace(filterText))
            {
                var lower = filterText.ToLowerInvariant();
                samples = samples.Where(s =>
                {
                    var tags = s.Tags
                        .Union(new[] { s.Name })
                        .Select(t => t.ToLowerInvariant());
                    return tags.Any(t => t.Contains(lower));
                });
            }

            return samples.OrderBy(s => s.Name);
        }
    }
}
