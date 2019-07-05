using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Tizen.Applications;
using Tizen.Wearable.CircularUI.Forms;

namespace WatchFaceXamMonkey
{
    public class WatchViewModel : INotifyPropertyChanged
    {
        string _date;
        string _time;
        FunAnimationView _animationView;
        WatchView _watchView;

        public WatchViewModel(WatchView view)
        {
            _watchView = view;
        }

        public string Date
        {
            get => _date;
            set
            {
                if (_date == value) return;
                _date = value;
                OnPropertyChanged();
            }
        }

        public string Time
        {
            get => _time;
            set
            {
                if (_time == value) return;
                _time = value;
                OnPropertyChanged();
            }
        }

        public FunAnimationView FunAnimationView
        {
            get => _animationView;
            set
            {
                _animationView = value;
                OnPropertyChanged();
            }
        }

        public Command ShowLottieAnimationCommand => new Command(ShowLottieAnimation);

        public Command AnimateMonkeyCommand => new Command(AnimateMonkey);

        public Command AnimationPlaybackFinishedCommand => new Command(AnimationPlaybackFinished);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void ShowLottieAnimation()
        {
            Tizen.Log.Error("AAA", "@@@ ShowLottieAnimation @@@");
            var animationView = new FunAnimationView();
            FunAnimationView = animationView;
            FunAnimationView.LView.OnFinish += (s, e) =>
            {
                Tizen.Log.Error("AAA", "@@@ FINISH @@@");
                animationView.LView.Stop();
                animationView = null;
            };
        }

        void AnimateMonkey()
        {
            Tizen.Log.Error("AAA", "@@@ AnimateMonkey @@@");
            var parentAnimation = new Animation();
            var scaleUpAnimation = new Animation(v => _watchView.Monkey.Scale = v, 1, 2, Easing.SpringIn);
            var rotateAnimation = new Animation(v => _watchView.Monkey.Rotation = v, 0, 360);
            var scaleDownAnimation = new Animation(v => _watchView.Monkey.Scale = v, 2, 1, Easing.SpringOut);
            parentAnimation.Add(0, 0.5, scaleUpAnimation);
            parentAnimation.Add(0, 1, rotateAnimation);
            parentAnimation.Add(0.5, 1, scaleDownAnimation);

            parentAnimation.Commit(_watchView, "MultiAnimation", 16, 3000, null);

        }

        void AnimationPlaybackFinished()
        {
            Tizen.Log.Error("AAA", "@@@ AnimationPlaybackFinished @@@");
        }

    }
}
