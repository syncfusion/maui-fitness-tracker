using System.Globalization;

namespace FitnessTracker
{
    public class TooltipValueConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DataPoint model)
            {
                switch (parameter?.ToString())
                {
                    case "Name":
                        return model.Label;
                    case "Value":
                        return model.Value;
                }
            }
            return value;
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
