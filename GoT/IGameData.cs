using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoT.Geography;

namespace GoT.Data
{
  public interface IGameData
  {
    IList<UnitType> GetUnitTypes();
    int GetVictoryPointLimit();

    IList<Region> GetRegions();

    IList<RegionAdjacency> GetRegionAdjacencies();
  }
}
