using Android.Content;
using Android.Graphics.Drawables;
using NoteTakingApp.Droid.Renderers;
using NoteTakingApp.Views.Custom;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace NoteTakingApp.Droid.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gradientDrawable = new GradientDrawable();
                gradientDrawable.SetColor(global::Android.Graphics.Color.Transparent);
                this.Control.SetBackground(gradientDrawable);
            }
        }
    }
}
