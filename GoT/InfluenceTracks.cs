using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoT
{
  #region Old
  ///// <summary>
  ///// Encapuslates all Influence Tracks and related operations
  ///// </summary>
  ///// <remarks>
  ///// The intention is to keep the number and behavior of influence
  ///// tracks decoupled from the rest of the program
  ///// </remarks>
  //public class InfluenceTrack
  //{
  //  public readonly InfluenceTrackType TrackType;
  //  public readonly LeaderTokenType TokenType;

  //  private SortedSet<Player> players;

  //  public InfluenceTrack(InfluenceTrackType trackType, IEnumerable<Player> players)
  //  {
  //    this.TrackType = trackType;
  //    players = new SortedSet<Player>(players);


  //    switch (trackType)
  //    {
  //      case InfluenceTrackType.IronThrone:
  //        return LeaderTokenType.IronThrone;
  //      case InfluenceTrackType.Fiefdoms:
  //        return LeaderTokenType.ValyrianSteelBlade;
  //      case InfluenceTrackType.KingsCourt:
  //        return LeaderTokenType.MessengerRaven;
  //      default:
  //        throw new ArgumentException("trackType");
  //    }
  //  }

  //  /// <summary>
  //  /// Pushes all players as far down the influence tracks as possible, leaving no space
  //  /// </summary>
  //  public static void AdjustInfluenceTracks(IEnumerable<Player> players)
  //  {
  //    foreach (var track in InfluenceTracks.GetTracks())
  //    {
  //      for (int i = 1; i <= players.Count(); i++)
  //      {
  //        int trackPosition = i;

  //        // If no player occupies the next lowest position on the track
  //        while (players.Count(p => p.GetRank(track) == trackPosition) == 0)
  //        {
  //          foreach (var player in players.Where(p => p.GetRank(track) > trackPosition))
  //          {
  //            // Move all players farther up the track down by one until someone does
  //            player.SetRank(track, player.GetRank(track) - 1);
  //          }
  //        }
  //      }
  //    }
  //  }

  //  public void GiveToken()
  //  {

  //  }
  //} 
  #endregion
}