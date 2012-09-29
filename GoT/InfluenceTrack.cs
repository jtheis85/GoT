using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoT
{
  public class InfluenceTrack
  {
    public readonly InfluenceTrackType TrackType;
    public readonly LeaderTokenType LeaderToken;

    private List<Player> rankedPlayers;

    /// <summary>
    /// Expects players ranked in order
    /// </summary>
    /// <param name="trackType"></param>
    /// <param name="rankedPlayers"></param>
    public InfluenceTrack(InfluenceTrackType trackType, LeaderTokenType leaderTokenType, IList<Player> rankedPlayers)
    {
      TrackType = trackType;
      LeaderToken = leaderTokenType;
      this.rankedPlayers = rankedPlayers as List<Player>;
    }

    public int Rank(Player player)
    {
      // +1 because List is zero-based and the tracks are not
      int temp = rankedPlayers.FindIndex(p => p.Equals(player)) + 1;
      return temp;
    }

    public IList<Player> GetRankedPlayers()
    {
      return rankedPlayers.AsReadOnly();
    }

    public Player GetLeadPlayer()
    {
      return rankedPlayers[0];
    }

    /// <summary>
    /// Reorders players on an influence track in the order provided, starting
    /// with the top position of the track.
    /// </summary>
    /// <remarks>
    /// TODO: Find a cleaner solution
    /// Params makes it easy to pass in the
    /// players needing to be reordered, and has the side benefit (I think)
    /// of leaving players who don't need to be reordered in place. However,
    /// </remarks>
    /// <param name="reorderedPlayers"></param>
    public void Reorder(params Player[] reorderedPlayers)
    {
      // Validate that all players to be reordered exist on the track BEFORE
      // reordering any of them
      foreach (var player in reorderedPlayers)
      {
        if (!rankedPlayers.Contains(player))
        {
          throw new ArgumentException(
            String.Format("player {0} does not exist on this {1} track.", 
              player.House, 
              TrackType), 
              "reorderedPlayers");
        }
      }

      for (int i = 0; i < reorderedPlayers.Length; i++)
      {
        rankedPlayers.Remove(reorderedPlayers[i]);
        rankedPlayers.Insert(i, reorderedPlayers[i]);
      }
    }
  }

  public enum LeaderTokenType
  {
    None,
    ValyrianSteelBlade,
    IronThrone,
    MessengerRaven
  }

  public enum InfluenceTrackType
  {
    None,
    IronThrone,
    Fiefdoms,
    KingsCourt
  }
}
