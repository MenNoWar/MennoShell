using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xaml;
using ManagedShell.WindowsTasks;

namespace CairoDesktop.SupportingClasses
{
    [ValueConversion(typeof(bool), typeof(Style))]
    public class TaskButtonStyleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {            
            if (values[0] is not FrameworkElement fxElement)
            {
                return null;
            }

            if (DesignerProperties.GetIsInDesignMode(fxElement)) return null;

            try
            {
                // Default style is Inactive...
                var fxStyle = fxElement.FindResource("CairoTaskbarButtonInactiveStyle");
                if (values[1] == DependencyProperty.UnsetValue || values[1] == null)
                {
                    // Default - couldn't get window state.
                    return fxStyle;
                }

                EnumUtility.TryCast(values[1], out ApplicationWindow.WindowState winState, ApplicationWindow.WindowState.Inactive);

                if (winState == ApplicationWindow.WindowState.Active)
                {
                    fxStyle = fxElement.FindResource("CairoTaskbarButtonActiveStyle");
                }

                return fxStyle;
            }
            catch
            {                
                return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
