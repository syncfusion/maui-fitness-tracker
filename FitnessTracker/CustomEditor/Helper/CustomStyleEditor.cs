using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.Core;
#if ANDROID
using PlatformView = Android.Widget.EditText;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.Controls.TextBox;
#endif
using PointerEventArgs = Syncfusion.Maui.Core.Internals.PointerEventArgs;

namespace FitnessTracker
{
    public class CustomStyleEditor : Editor
    {
        protected override void OnHandlerChanged()
        {
            // Hide editor border and underline.
            var platformView = this.Handler?.PlatformView as PlatformView;
            if (platformView != null)
            {
                this.ApplyTextBoxStyle(platformView);
            }

            base.OnHandlerChanged();
        }

        private void ApplyTextBoxStyle(PlatformView? platformView)
        {
            if (platformView != null)
            {
#if WINDOWS
                var textBoxStyle = new Microsoft.UI.Xaml.Style(typeof(Microsoft.UI.Xaml.Controls.TextBox));
                textBoxStyle.Setters.Add(new Microsoft.UI.Xaml.Setter() { Property = Microsoft.UI.Xaml.Controls.Control.BorderBrushProperty, Value = new Microsoft.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0)) });
                textBoxStyle.Setters.Add(new Microsoft.UI.Xaml.Setter() { Property = Microsoft.UI.Xaml.Controls.Control.BorderThicknessProperty, Value = new Thickness(0) });

                platformView.Resources.Add(typeof(Microsoft.UI.Xaml.Controls.TextBox), textBoxStyle);
#else
                platformView.Background = null;
                platformView.SetPadding(0, 0, 0, 0);
#endif
            }
        }
    }

    internal class SfEffectsViewAdv : SfEffectsView, ITouchListener, IGestureListener
    {
        public new void OnTouch(PointerEventArgs e)
        {
            if (e.Action == PointerActions.Entered)
            {
                this.ApplyEffects(SfEffects.Highlight, RippleStartPosition.Default, new System.Drawing.Point((int)e.TouchPoint.X, (int)e.TouchPoint.Y), false);
            }
            else if (e.Action == PointerActions.Released || e.Action == PointerActions.Cancelled || e.Action == PointerActions.Exited)
            {
                this.Reset();
            }
            else if (e.Action == PointerActions.Pressed)
            {
                this.ApplyEffects(SfEffects.Ripple, RippleStartPosition.Default, new System.Drawing.Point((int)e.TouchPoint.X, (int)e.TouchPoint.Y), false);
            }
        }
    }
}
