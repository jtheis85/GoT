using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoT.Data;

namespace GoT
{
  public class VictoryTrack
  {
    public int VictoryPointLimit { get; private set; }

    private Dictionary<Player, int> playerVictoryPoints;
    private IGameData gameData;

    public VictoryTrack(IGameData gameData)
    {
      playerVictoryPoints = new Dictionary<Player, int>();
      this.gameData = gameData;
      VictoryPointLimit = gameData.GetVictoryPointLimit();
    }

    public void Add(Player player, int victoryPoints)
    {
      playerVictoryPoints.Add(player, victoryPoints);
    }

    public int GetVictoryPoints(Player player)
    {
      return playerVictoryPoints[player];
    }
  }
}
