using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;
using ElmSharp;
using Native = Xamarin.Forms.Platform.Tizen.Native;
using XPage = Xamarin.Forms.Page;
using XButton = Xamarin.Forms.Button;
using XColor = Xamarin.Forms.Color;
using EColor = ElmSharp.Color;
using EButton = ElmSharp.Button;

namespace WatchFaceXamDevSummit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingView : ContentView
    {
        ColorTypeConverter _converter;

        public SettingView()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<XPage, ActionSheetArguments>(this, XPage.ActionSheetSignalName, ActionSheetSignalNameHandler);
            _converter = new ColorTypeConverter();
        }

        public void UnsubscribeMessage()
        {
            MessagingCenter.Unsubscribe<Xamarin.Forms.Page, ActionSheetArguments>(this, "Xamarin.ShowActionSheet");
        }

        public XButton BGColorButton => BackgroundColorButton;

        public XButton HHColorButton => HourColorButton;

        public XButton MMColorButton => MinuteColorButton;

        public IRotaryFocusable RotaryFocusObject => CircleScrollView;

        public void UpdateAllButtonTextColor()
        {
            FillTextColor(BackgroundColorButton);
            FillTextColor(HourColorButton);
            FillTextColor(MinuteColorButton);
        }

        async void OnColorButtonClicked(object sender, EventArgs e)
        {
            if (sender is XButton button)
            {
                string[] buttons = {
                    "Aqua", "Azure", "Beige", "Bisque", "Black", "Blue", "BlueViolet", "Brown",  "Chocolate",
                    "Coral", "Crimson","Cyan", "DarkBlue", "DarkCyan", "DarkGoldenrod", "DarkGray", "DarkGreen", "DarkKhaki", "DarkMagenta", "DarkOliveGreen", "DarkOrange",
                    "DarkOrchid", "DarkRed", "DarkSalmon", "DarkSeaGreen", "DarkSlateBlue", "DarkSlateGray", "DarkTurquoise", "DarkViolet", "DeepPink", "DeepSkyBlue",
                    "FloralWhite", "Fuchsia", "Gold", "Gray", "Green", "GreenYellow", "Honeydew", "HotPink", "IndianRed", "Indigo",
                    "Ivory", "Khaki", "Lavender", "LightBlue", "LightCoral", "LightPink", "LightSkyBlue", "Lime", "Magenta", "MidnightBlue",
                    "Navy", "Olive", "Orange", "OrangeRed", "Orchid", "PaleGreen", "Pink", "Purple", "Red", "Silver",
                    "SkyBlue", "Transparent", "Violet", "Wheat", "White", "Yellow", "YellowGreen"
                };
                string ret = await Application.Current.MainPage.DisplayActionSheet("Choose Color", "Cancel", "Default", buttons);
                XColor color = (XColor)(_converter.ConvertFromInvariantString(ret));
                if (color.IsDefault)
                {
                    ApplyDefaultColor(button);
                }
                else
                {
                    button.BackgroundColor = color;
                }
            }
        }

        void OnResetColorButtonClicked(object sender, EventArgs e)
        {
            BackgroundColorButton.BackgroundColor = ClockViewModel.DEFAULT_BACKGROUND_COLOR;
            HourColorButton.BackgroundColor = ClockViewModel.DEFAULT_HOUR_COLOR;
            MinuteColorButton.BackgroundColor = ClockViewModel.DEFAULT_MINUTE_COLOR;
            UpdateAllButtonTextColor();
        }

        void ActionSheetSignalNameHandler(XPage sender, ActionSheetArguments arguments)
        {
            Native.Dialog dialog = Native.Dialog.CreateDialog(Forms.NativeParent);
            dialog.Title = arguments.Title;

            Box box = new Box(dialog);

            if (null != arguments.Destruction)
            {
                Native.Button destruction = new Native.Button(dialog)
                {
                    Text = arguments.Destruction,
                    Style = ButtonStyle.Text,
                    TextColor = EColor.Red,
                    AlignmentX = -1
                };
                destruction.Clicked += (s, evt) =>
                {
                    arguments.SetResult(arguments.Destruction);
                    dialog.Dismiss();
                };
                destruction.Show();
                box.PackEnd(destruction);
            }

            foreach (string buttonName in arguments.Buttons)
            {
                Native.Button button = new Native.Button(dialog)
                {
                    Text = buttonName,
                    BackgroundColor = buttonName.ToColor(),
                    AlignmentX = -1,
                    MinimumWidth=400
                };

                if (button.BackgroundColor==EColor.White || button.BackgroundColor==ColorExtensions.Azure)
                {
                    button.TextColor = EColor.Black;
                }

                button.Clicked += (s, evt) =>
                {
                    arguments.SetResult(buttonName);
                    dialog.Dismiss();
                };
                button.Show();
                box.PackEnd(button);
            }

            box.Show();
            dialog.Content = box;

            if (null != arguments.Cancel)
            {
                EButton cancel = new EButton(dialog) { Text = arguments.Cancel };
                dialog.NegativeButton = cancel;
                cancel.Clicked += (s, evt) =>
                {
                    dialog.Dismiss();
                };
            }

            dialog.BackButtonPressed += (s, evt) =>
            {
                dialog.Dismiss();
            };

            dialog.Show();
        }

        void FillTextColor(XButton button)
        {
            if (button.BackgroundColor == XColor.White || button.BackgroundColor == XColor.Azure)
            {
                button.TextColor = XColor.Black;
            }
        }

        void ApplyDefaultColor(XButton button)
        {
            if (button.Text == BackgroundColorButton.Text)
            {
                button.BackgroundColor = ClockViewModel.DEFAULT_BACKGROUND_COLOR;
            }
            else if (button.Text == HourColorButton.Text)
            {
                button.BackgroundColor = ClockViewModel.DEFAULT_HOUR_COLOR;
            }
            else if (button.Text == MinuteColorButton.Text)
            {
                button.BackgroundColor = ClockViewModel.DEFAULT_MINUTE_COLOR;
            }
        }
    }
}