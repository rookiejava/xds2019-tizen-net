using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchFaceXamMonkey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchView : ContentView
    {
        public Image Monkey => MonkeyImage;

        public WatchView()
        {
            InitializeComponent();

            var parentAnimation = new Animation();
            var scaleUpAnimation = new Animation(v => Monkey.Scale = v, 1, 2, Easing.SpringIn);
            var rotateAnimation = new Animation(v => Monkey.Rotation = v, 0, 360);
            var scaleDownAnimation = new Animation(v => Monkey.Scale = v, 2, 1, Easing.SpringOut);
            parentAnimation.Add(0, 0.5, scaleUpAnimation);
            parentAnimation.Add(0, 1, rotateAnimation);
            parentAnimation.Add(0.5, 1, scaleDownAnimation);

            parentAnimation.Commit(this, "MultiAnimation", 16, 3000, null);
        }

    }
}