using Syncfusion.Maui.Toolkit.Charts;
using System.Collections.ObjectModel;

namespace FitnessTracker
{
    /// <summary>
    /// Helper class to display chart data when only a single segment is present in ItemsSource
    /// </summary>
    public class ColumnSeriesExt : ColumnSeries
    {
        internal bool IsSingleSegment { get; set; }
        protected override ChartSegment CreateSegment()
        {
            return new ColumnSegmentExt();
        }
        protected override void DrawSeries(ICanvas canvas, ReadOnlyObservableCollection<ChartSegment> segments, RectF clipRect)
        {
            IsSingleSegment = segments.Count == 1;

            base.DrawSeries(canvas, segments, clipRect);
        }
    }

    public class ColumnSegmentExt : ColumnSegment
    {
        protected override void Draw(ICanvas canvas)
        {
            if (Series is ColumnSeriesExt series)
            {
                if (series.IsSingleSegment)
                {
                    var centerX = (Left + Right) / 2;

                    //You can customize the segment width based on requirement for mobile and desktop applications
                    var rect = new RectF(centerX, Top, 64, (Bottom - Top));
                    CornerRadius cornerRadius = series.CornerRadius;
                    canvas.SetFillPaint(base.Fill, rect);
                    canvas.FillRoundedRectangle(rect, cornerRadius.TopLeft, cornerRadius.TopRight, cornerRadius.BottomLeft, cornerRadius.BottomRight);

                }
                else
                {
                    base.Draw(canvas);
                }

            }

        }
    }
}
