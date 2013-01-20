using GalaSoft.MvvmLight;
using GoT.Data;

namespace GoTUI.ViewModel
{
  public class ViewModelLocator
  {
    private MainViewModel viewModel;

    public ViewModelLocator()
    {
      if (ViewModelBase.IsInDesignModeStatic)
      {
        viewModel = new MainViewModel(new DesignGameData());
      }
      else
      {
        viewModel = new MainViewModel(new GameData());
      }
    }

    public MainViewModel Main
    {
        get { return viewModel; }
    }

    public static void Cleanup()
    {
      // Not implemented
    }
  }
}