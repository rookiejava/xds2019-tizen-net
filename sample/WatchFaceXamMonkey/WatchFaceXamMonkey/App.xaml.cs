using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WatchFaceXamMonkey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public WatchView WatchView => WView;

        public App()
        {
            InitializeComponent();
        }
    }
}