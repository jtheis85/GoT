using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using GoTUI.ViewModel;

namespace GoTUI
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    /// <summary>
    /// Initializes a new instance of the MainWindow class.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        Closing += (s, e) => ViewModelLocator.Cleanup();
    }
  }

  [ValueConversion(typeof(Tuple<int, int, int, int>), typeof(string))]
  public class TupleToMarginConverter : IValueConverter
  {

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var margin = (Tuple<int, int, int, int>)value;
      return String.Format("{0} {1} {2} {3}",
        margin.Item1.ToString(),
        margin.Item2.ToString(),
        margin.Item3.ToString(),
        margin.Item4.ToString());
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}