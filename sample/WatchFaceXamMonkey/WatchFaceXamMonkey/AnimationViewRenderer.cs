using WatchFaceXamMonkey;
using System;
using Xamarin.Forms.Platform.Tizen;
using ElottieSharp;
using XForms = Xamarin.Forms.Platform.Tizen.Forms;

[assembly: ExportRenderer(typeof(AnimationView), typeof(AnimationViewRenderer))]
namespace WatchFaceXamMonkey
{
    public class AnimationViewRenderer : ViewRenderer<AnimationView, LottieAnimationView>
    {
        public AnimationViewRenderer()
        {
            RegisterPropertyHandler(AnimationView.LoopProperty, UpdateLoop);
            RegisterPropertyHandler(AnimationView.AnimationProperty, UpdateAnimation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Element != null)
                {
                    Element.OnPlay -= OnPlay;
                    Element.OnStop -= OnStop;
                }

                if (Control != null)
                {
                    Control.Started -= OnStarted;
                    Control.Stopped -= OnStopped;
                    Control.Finished -= OnFinished;
                }
            }
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AnimationView> e)
        {
            if (Control == null)
            {
                var animationView = new LottieAnimationView(XForms.NativeParent);
                animationView.Started += OnStarted;
                animationView.Stopped += OnStopped;
                animationView.Finished += OnFinished;
                SetNativeControl(animationView);
            }

            if (e.OldElement != null)
            {
                e.OldElement.OnPlay -= OnPlay;
                e.OldElement.OnStop -= OnStop;
            }

            if (e.NewElement != null)
            {
                e.NewElement.OnPlay += OnPlay;
                e.NewElement.OnStop += OnStop;
            }
            base.OnElementChanged(e);
        }

        private void OnPlay(object sender, EventArgs e)
        {
            Control?.Play();
            Element.IsPlaying = true;
        }

        private void OnStop(object sender, EventArgs e)
        {
            Control?.Stop();
            Element.IsPlaying = false;
        }

        private void OnStarted(object sender, EventArgs e)
        {
            Element.IsPlaying = true;
        }

        private void OnStopped(object sender, EventArgs e)
        {
            Element.IsPlaying = false;
        }

        private void OnFinished(object sender, EventArgs e)
        {
            Element?.PlaybackFinished();
        }

        private void UpdateAnimation()
        {
            if (!string.IsNullOrEmpty(Element.Animation))
            {
                Control.SetAnimation(ResourcePath.GetPath(Element.Animation));
                Element.Duration = TimeSpan.FromMilliseconds(Control.DurationTime);
                if (Element.IsPlaying)
                    Control.Play();
            }
        }

        private void UpdateLoop()
        {
            Control.AutoRepeat = Element.Loop;
        }
    }
}