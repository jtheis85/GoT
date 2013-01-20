using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GoT.Data;

namespace GoT.Geography
{
  /// <summary>
  /// Holds information about a map, including the features, regions, and
  /// geographical relationships.
  /// </summary>
  public class Map
  {
    /// x,y format
    /// <summary>
    /// The dimensions of the map background art in pixels
    /// x,y format
    /// </summary>
    /// <remarks>
    /// TODO: Refactor UI-specific Background Dimensions out of Map class
    /// Something seems off. This is clearly a UI concern. What is it doing here?
    /// </remarks>
    public Tuple<int,int> BackgroundDimensions { get; set; }
    public ObservableCollection<Region> Regions { get; private set; }

    private IGameData gameData;
    private IList<RegionAdjacency> adjacency;

    public Map(IGameData gameData)
    {
      this.gameData = gameData;
      Regions = new ObservableCollection<Region>(gameData.GetRegions());
      adjacency = gameData.GetRegionAdjacencies();
      //BackgroundDimensions = gameData.GetBackgroundDimensions();
    }

    public int RegionCount
    {
      get { return Regions.Count(); }
    }

    /// Region adjacency, order-independent
    /// <summary>
    /// Determines whether a pair of regions are adjacent regardless 
    /// of adjacency type. Order-independent
    /// </summary>
    /// <param name="firstRegionID">The first region ID</param>
    /// <param name="secondRegionID">The second region ID</param>
    /// <returns>Whether they are adjacent by the given type</returns>
    public bool AreAdjacent(int firstRegionID, int secondRegionID)
    {
      return adjacency.Contains(new RegionAdjacency() { FirstRegion = firstRegionID, SecondRegion = secondRegionID });
    }

    /// Region adjacency by type, order-independent
    /// <summary>
    /// Determines whether a pair of regions are adjacent with a specific 
    /// adjacency type. Order-independent
    /// </summary>
    /// <param name="adjacencyType">The type of adjacency between the regions</param>
    /// <param name="firstRegionID">The first region ID</param>
    /// <param name="secondRegionID">The second region ID</param>
    /// <returns>Whether they are adjacent by the given type</returns>
    public bool AreAdjacentBy(AdjacencyType adjacencyType, int firstRegionID, int secondRegionID)
    {
      return adjacency.Contains(new RegionAdjacency() { FirstRegion = firstRegionID, SecondRegion = secondRegionID, AdjacencyType = adjacencyType });
    }

    public Region Region(int RegionId)
    {
      return Regions.First(r => r.ID == RegionId);
    }
  }
}
