using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchFaceXamMonkey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FunAnimationView : ContentView
    {
        public AnimationView LView => LottieView;

        public FunAnimationView()
        {
            InitializeComponent();
        }
    }
}