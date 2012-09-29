using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoT
{
  public class Game 
  {
    public int WildlingThreat { get; set; }



    public SupplyTrack SupplyTrack
    {
      get { return supplyTrack; }
      private set { supplyTrack = value; }
    }
    private SupplyTrack supplyTrack;
    

    private readonly List<Player> players;
    private readonly List<InfluenceTrack> influenceTracks;
    private int playerCount;

    /// <summary>
    /// 
    /// </summary>
    public Game()
    {
      players = new List<Player>();
      influenceTracks = new List<InfluenceTrack>();
      supplyTrack = new SupplyTrack(GameData.GetSupplyLimits());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerCount"></param>
    public void Setup(int playerCount)
    {
      players.Clear();

      this.playerCount = playerCount;
      WildlingThreat = GameData.GetStartingWildlingThreat();

      foreach (var house in GameData.GetValidHouses(playerCount))
      {
        var player = new Player(house);
        supplyTrack.Set(player, GameData.GetStartingSupply(house));
        player.AddPower(GameData.GetStartingPower());

        players.Add(player);
      }

      // TODO: Influence Tracks should have their token inferred from their type based on GameData
      influenceTracks.Add(new InfluenceTrack(InfluenceTrackType.IronThrone, LeaderTokenType.IronThrone,         players));
      influenceTracks.Add(new InfluenceTrack(InfluenceTrackType.Fiefdoms,   LeaderTokenType.ValyrianSteelBlade, players));
      influenceTracks.Add(new InfluenceTrack(InfluenceTrackType.KingsCourt, LeaderTokenType.MessengerRaven,     players));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Player> GetPlayers()
    {
      return players;
    }

    public IEnumerable<InfluenceTrack> GetInfluenceTracks()
    {
      return influenceTracks.AsReadOnly();
    }
  }
}
