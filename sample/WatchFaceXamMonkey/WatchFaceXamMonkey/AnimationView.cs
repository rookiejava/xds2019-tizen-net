using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace WatchFaceXamMonkey
{
    public class AnimationView : View
    {
        public static readonly BindableProperty LoopProperty = BindableProperty.Create(nameof(Loop), typeof(bool),
            typeof(AnimationView), default(bool));

        public static readonly BindableProperty IsPlayingProperty = BindableProperty.Create(nameof(IsPlaying),
            typeof(bool), typeof(AnimationView), default(bool));

        public static readonly BindableProperty DurationProperty = BindableProperty.Create(nameof(Duration),
            typeof(TimeSpan), typeof(AnimationView), default(TimeSpan));

        public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation),
            typeof(string), typeof(AnimationView), default(string));


        public static readonly BindableProperty SpeedProperty = BindableProperty.Create(nameof(Speed),
            typeof(float), typeof(AnimationView), 1.0f);

        public static readonly BindableProperty PlaybackStartedCommandProperty = BindableProperty.Create(nameof(PlaybackStartedCommand),
            typeof(ICommand), typeof(AnimationView));

        public static readonly BindableProperty PlaybackStoppedCommandProperty = BindableProperty.Create(nameof(PlaybackStoppedCommand),
            typeof(ICommand), typeof(AnimationView));

        public static readonly BindableProperty PlaybackFinishedCommandProperty = BindableProperty.Create(nameof(PlaybackFinishedCommand),
            typeof(ICommand), typeof(AnimationView));

        public string Animation
        {
            get { return (string)GetValue(AnimationProperty); }
            set { SetValue(AnimationProperty, value); }
        }

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public bool Loop
        {
            get { return (bool)GetValue(LoopProperty); }
            set { SetValue(LoopProperty, value); }
        }

        public bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            set { SetValue(IsPlayingProperty, value); }
        }

        public float Speed
        {
            get { return (float)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        public ICommand PlaybackStartedCommand
        {
            get { return (ICommand)GetValue(PlaybackStartedCommandProperty); }
            set { SetValue(PlaybackStartedCommandProperty, value); }
        }

        public ICommand PlaybackStoppedCommand
        {
            get { return (ICommand)GetValue(PlaybackStoppedCommandProperty); }
            set { SetValue(PlaybackStoppedCommandProperty, value); }
        }

        public ICommand PlaybackFinishedCommand
        {
            get { return (ICommand)GetValue(PlaybackFinishedCommandProperty); }
            set { SetValue(PlaybackFinishedCommandProperty, value); }
        }

        public event EventHandler OnPlay;

        public void Play()
        {
            OnPlay?.Invoke(this, new EventArgs());
            ExecuteCommandIfPossible(PlaybackStartedCommand);
        }

        public event EventHandler OnStop;

        public void Stop()
        {
            OnStop?.Invoke(this, new EventArgs());
            ExecuteCommandIfPossible(PlaybackStoppedCommand);
        }

        public event EventHandler OnFinish;

        public void PlaybackFinished()
        {
            OnFinish?.Invoke(this, EventArgs.Empty);
            ExecuteCommandIfPossible(PlaybackFinishedCommand);
        }

        private void ExecuteCommandIfPossible(ICommand command)
        {
            if (command != null && command.CanExecute(null))
            {
                command.Execute(null);
            }
        }
    }
}
