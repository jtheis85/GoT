using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoT.Data;

namespace GoT
{
  public class UnitFactory
  {
    private IGameData gameData;
    private IList<UnitType> unitTypes;

    public UnitFactory(IGameData gameData)
    {
      this.gameData = gameData;
      unitTypes = gameData.GetUnitTypes();
    }

    public UnitType GetUnit(int ID)
    {
      return unitTypes[ID];
    }
  }
}
