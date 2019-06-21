using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Tizen.Applications;
using Tizen.Wearable.CircularUI.Forms;

namespace WatchFaceXamDevSummit
{
    public class ClockViewModel : INotifyPropertyChanged
    {
        public static readonly Color DEFAULT_BACKGROUND_COLOR = Color.Gray;
        public static readonly Color DEFAULT_HOUR_COLOR = Color.FromHex("#3498DB");
        public static readonly Color DEFAULT_MINUTE_COLOR = Color.White;

        readonly string SETTING_KEY_BACKGROUND_COLOR = "BackgroundColor";
        readonly string SETTING_KEY_HOUR_COLOR = "HourColor";
        readonly string SETTING_KEY_MINUTE_COLOR = "MinuteColor";
        readonly string SETTING_KEY_BACKGROUND_INAGE = "BackgroundImage";
        readonly string DEFAULT_BACKGROUND_IMAGE = "XamDevSummitMonkey.png";
        readonly string SECOND_BACKGROUND_IMAGE = "XamDevSummitBG.png";
        readonly int DEFAULT_HOUR_FONT_SIZE = 14;
        readonly int DEFAULT_MINUTE_FONT_SIZE = 14;
        readonly int SECOND_HOUR_FONT_SIZE = 30;
        readonly int SECOND_MINUTE_FONT_SIZE = 30;
        readonly Rectangle DEFAULT_HOUR_BOUNDS = new Rectangle(100, 170, 60, 40);
        readonly Rectangle DEFAULT_MINUTE_BOUNDS = new Rectangle(200, 170, 60, 40);
        readonly Rectangle SECOND_HOUR_BOUNDS = new Rectangle(120, 50, 120, 80);
        readonly Rectangle SECOND_MINUTE_BOUNDS = new Rectangle(120, 130, 120, 80);

        bool _settingEnabled;
        Color _backgroundColor;
        Color _hourColor;
        Color _minuteColor;
        int _hour;
        int _minute;
        int _hourFontSize;
        int _minuteFontSize;
        string _backgroundImage;
        Rectangle _hourBounds;
        Rectangle _minuteBounds;
        SettingView _settingView;
        ActionButtonItem _actionButtonItem;
        IRotaryFocusable _rotarayFocusObject;

        public ClockViewModel()
        {
            _backgroundColor = DEFAULT_BACKGROUND_COLOR;
            _hourColor = DEFAULT_HOUR_COLOR;
            _minuteColor = DEFAULT_MINUTE_COLOR;
            _backgroundImage = DEFAULT_BACKGROUND_IMAGE;
            _hourFontSize = DEFAULT_HOUR_FONT_SIZE;
            _minuteFontSize = DEFAULT_MINUTE_FONT_SIZE;
            _hourBounds = DEFAULT_HOUR_BOUNDS;
            _minuteBounds = DEFAULT_MINUTE_BOUNDS;
        }

        public int Hour
        {
            get => _hour;
            set
            {
                if (_hour == value) return;
                _hour = value;
                OnPropertyChanged();
            }
        }

        public int Minute
        {
            get => _minute;
            set
            {
                if (_minute == value) return;
                _minute = value;
                OnPropertyChanged();
            }
        }

        public string BackgroundImage
        {
            get => _backgroundImage;
            set
            {
                if (_backgroundImage == value) return;
                _backgroundImage = value;
                if (_backgroundImage == DEFAULT_BACKGROUND_IMAGE)
                {
                    HourFontSize = DEFAULT_HOUR_FONT_SIZE;
                    HourBounds = DEFAULT_HOUR_BOUNDS;
                    MinuteFontSize = DEFAULT_MINUTE_FONT_SIZE;
                    MinuteBounds = DEFAULT_MINUTE_BOUNDS;
                }
                else
                {
                    HourFontSize = SECOND_HOUR_FONT_SIZE;
                    HourBounds = SECOND_HOUR_BOUNDS;
                    MinuteFontSize = SECOND_MINUTE_FONT_SIZE;
                    MinuteBounds = SECOND_MINUTE_BOUNDS;
                }
                OnPropertyChanged();
            }
        }

        public bool SettingEnabled
        {
            get => _settingEnabled;
            set
            {
                if (_settingEnabled == value) return;
                _settingEnabled = value;
                OnPropertyChanged();
            }
        }

        public SettingView SettingView
        {
            get => _settingView;
            set
            {
                _settingView = value;
                OnPropertyChanged();
            }
        }

        public ActionButtonItem ActionButton
        {
            get => _actionButtonItem;
            set
            {
                _actionButtonItem = value;
                OnPropertyChanged();
            }
        }

        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                if (value.IsDefault)
                {
                    _backgroundColor = DEFAULT_BACKGROUND_COLOR;
                }
                else
                {
                    _backgroundColor = value;
                }
                OnPropertyChanged();
            }
        }

        public Color HourColor
        {
            get => _hourColor;
            set
            {
                if (value.IsDefault)
                {
                    _hourColor = DEFAULT_HOUR_COLOR;
                }
                else
                {
                    _hourColor = value;
                }
                OnPropertyChanged();
            }
        }

        public Color MinuteColor
        {
            get => _minuteColor;
            set
            {
                if(value.IsDefault)
                {
                    _minuteColor = DEFAULT_MINUTE_COLOR;
                }
                else
                {
                    _minuteColor = value;
                }
                OnPropertyChanged();
            }
        }

        public int HourFontSize
        {
            get => _hourFontSize;
            set
            {
                _hourFontSize = value;
                OnPropertyChanged();
            }
        }

        public int MinuteFontSize
        {
            get => _minuteFontSize;
            set
            {
                _minuteFontSize = value;
                OnPropertyChanged();
            }
        }

        public Rectangle HourBounds
        {
            get => _hourBounds;
            set
            {
                _hourBounds = value;
                OnPropertyChanged();
            }
        }

        public Rectangle MinuteBounds
        {
            get => _minuteBounds;
            set
            {
                _minuteBounds = value;
                OnPropertyChanged();
            }
        }


        public IRotaryFocusable RotarayFocusObject
        {
            get => _rotarayFocusObject;
            set
            {
                _rotarayFocusObject = value;
                OnPropertyChanged();
            }
        }

        public Command EnterSettingCommand => new Command(CreateSettingView);

        public Command ExitSettingCommand => new Command(ExitSettingView);

        public Command ChangeBackgroundImageCommand => new Command(ChangeBackgroundImage);

        public event PropertyChangedEventHandler PropertyChanged;

        public void SavePreference()
        {
            Preference.RemoveAll();
            Preference.Set(SETTING_KEY_BACKGROUND_COLOR, SettingView.BGColorButton.BackgroundColor.ToHex());
            Preference.Set(SETTING_KEY_HOUR_COLOR, SettingView.HHColorButton.BackgroundColor.ToHex());
            Preference.Set(SETTING_KEY_MINUTE_COLOR, SettingView.MMColorButton.BackgroundColor.ToHex());
            Preference.Set(SETTING_KEY_BACKGROUND_INAGE, BackgroundImage);
        }

        public void LoadPreference()
        {
            foreach (string key in Preference.Keys)
            {
                if (key == SETTING_KEY_BACKGROUND_COLOR)
                {
                    BackgroundColor = Color.FromHex(Preference.Get<string>(SETTING_KEY_BACKGROUND_COLOR));
                }
                else if (key == SETTING_KEY_HOUR_COLOR)
                {
                    HourColor = Color.FromHex(Preference.Get<string>(SETTING_KEY_HOUR_COLOR));
                }
                else if (key == SETTING_KEY_MINUTE_COLOR)
                {
                    MinuteColor = Color.FromHex(Preference.Get<string>(SETTING_KEY_MINUTE_COLOR));
                }
                else if (key == SETTING_KEY_BACKGROUND_INAGE)
                {
                    BackgroundImage = Preference.Get<string>(SETTING_KEY_BACKGROUND_INAGE);
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void CreateSettingView()
        {
            if (!SettingEnabled)
            {
                SettingEnabled = true;
                var setting = new SettingView();
                SettingView = setting;
                RotarayFocusObject = setting.RotaryFocusObject;
                setting.BGColorButton.BackgroundColor = BackgroundColor;
                setting.HHColorButton.BackgroundColor = HourColor;
                setting.MMColorButton.BackgroundColor = MinuteColor;
                setting.UpdateAllButtonTextColor();
                ActionButton = new ActionButtonItem
                {
                    Text = "OK",
                    Command = ExitSettingCommand
                };
            }
        }

        void ExitSettingView()
        {
            BackgroundColor = SettingView.BGColorButton.BackgroundColor;
            HourColor = SettingView.HHColorButton.BackgroundColor;
            MinuteColor = SettingView.MMColorButton.BackgroundColor;
            SettingEnabled = false;
            ActionButton.IsVisible = false;
            SavePreference();
            MessagingCenter.Unsubscribe<Page, ActionSheetArguments>(SettingView, Page.ActionSheetSignalName);
            SettingView = null;
        }

        void ChangeBackgroundImage()
        {
            if (BackgroundImage == DEFAULT_BACKGROUND_IMAGE)
            {
                BackgroundImage = SECOND_BACKGROUND_IMAGE;
            }
            else
            {
                BackgroundImage = DEFAULT_BACKGROUND_IMAGE;
            }
        }
    }
}
