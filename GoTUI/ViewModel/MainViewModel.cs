using GalaSoft.MvvmLight;
using GoT.Data;
using GoT.Geography;

namespace GoTUI.ViewModel
{
  /// <summary>
  /// This class contains properties that the main View can data bind to.
  /// </summary>
  public class MainViewModel : ViewModelBase
  {
    public Map Map { get; set; }

    /// <summary>
    /// Initializes a new instance of the MainViewModel class.
    /// </summary>
    public MainViewModel(IGameData gameData)
    {
      Map = new Map(gameData);
    }
  }
}