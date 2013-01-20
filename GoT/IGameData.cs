using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoT.Geography;

namespace GoT.Data
{
  public interface IGameData
  {
    int GetVictoryPointLimit();

    IEnumerable<Region> GetRegions();

    IList<RegionAdjacency> GetRegionAdjacencies();

    IList<UnitType> GetUnitTypes();

    Tuple<int, int> GetBackgroundDimensions();
  }
}
