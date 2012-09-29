using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoT.Data;

namespace GoT.Geography
{
  public class Map
  {
    private IGameData gameData;
    private IList<Region> regions;
    private IList<RegionAdjacency> adjacency;

    public Map(IGameData gameData)
    {
      this.gameData = gameData;
      regions = gameData.GetRegions();
      adjacency = gameData.GetRegionAdjacencies();
    }

    public int RegionCount
    {
      get { return regions.Count(); }
    }

    public bool AreAdjacent(int firstRegionID, int secondRegionID)
    {
      return adjacency.Contains(new RegionAdjacency() { FirstRegion = firstRegionID, SecondRegion = secondRegionID });
    }

    public bool AreAdjacentBy(AdjacencyType adjacencyType, int firstRegionID, int secondRegionID)
    {
      return adjacency.Contains(new RegionAdjacency() { FirstRegion = firstRegionID, SecondRegion = secondRegionID, AdjacencyType = adjacencyType });
    }
  }
}
