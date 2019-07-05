using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

namespace Samples.Tizen
{
    class Program : FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();

            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            if (Device.Idiom == TargetIdiom.Watch)
                global::Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();

            Xamarin.Essentials.Platform.MapServiceToken = "inesert your map key";

            app.Run(args);
        }
    }
}
