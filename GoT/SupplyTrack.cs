using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoT
{
  public class SupplyTrack
  {
    private Dictionary<Player, int> players;
    private List<ArmySizeSupplyLimit> limits;

    public SupplyTrack(IEnumerable<ArmySizeSupplyLimit> armySizeSupplyLimit)
    {
      players = new Dictionary<Player, int>();
      limits = new List<ArmySizeSupplyLimit>();
      if(armySizeSupplyLimit != null)
      {
        limits.AddRange(armySizeSupplyLimit);  
      }
    }

    public void Add(Player player, int supply)
    {
      players.Add(player, supply);
    }

    public int GetSupply(Player player)
    {
      return players[player];
    }

    public void Set(Player player, int supply)
    {
      //Validate that supply is non-negative
      if (supply < 0)
      {
        throw new ArgumentOutOfRangeException("supply", "must be non-negative");
      }

      players[player] = supply;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="supply"></param>
    /// <param name="size"></param>
    /// <returns>
    /// The number of armies of that size allowed at that supply level, or null
    /// if there is no limit (e.g. for size 1 armies in a standard game). 
    /// </returns>
    public int? GetArmyCountLimit(int supply, int size)
    {
      ArmySizeSupplyLimit limit = limits.FirstOrDefault(l => l.Supply == supply && l.Size == size);
      if (limit != null)
      {
        return limit.ArmyCountLimit;
      }
      // TODO: Write a test to cover a null army limit
      else
      {
        return null;
      }
    }
  }

  public class ArmySizeSupplyLimit
  {
    public int Supply { get; set; }
    public int Size { get; set; }
    public int? ArmyCountLimit { get; set; }
  }
}
