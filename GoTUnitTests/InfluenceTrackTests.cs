using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoT;
using FluentAssertions;

namespace GoTUnitTests
{
  [TestClass]
  public class InfluenceTrackTests
  {
    /// <summary>
    /// Simulates setup of the standard King's Court influence track, which has
    /// Baratheon leading, followed by Lannister, followed by Stark.
    /// </summary>
    [TestMethod]
    public void TestSimplePlayerRanking()
    {
      var rankedPlayers = new List<Player>();

      rankedPlayers.Add(new Player(HouseName.Baratheon));
      rankedPlayers.Add(new Player(HouseName.Lannister));
      rankedPlayers.Add(new Player(HouseName.Stark));

      var track = new InfluenceTrack(InfluenceTrackType.KingsCourt, LeaderTokenType.MessengerRaven, rankedPlayers);

      track.GetRankedPlayers()[0].House.Should().Be(HouseName.Baratheon);
      track.GetRankedPlayers()[1].House.Should().Be(HouseName.Lannister);
      track.GetRankedPlayers()[2].House.Should().Be(HouseName.Stark);
    }

    /// <summary>
    /// Simulates setup of the standard King's Court influence track, which has
    /// the Messenger Raven as its leader token.
    /// </summary>
    [TestMethod]
    public void TestSimpleLeaderTokenAssociation()
    {
      var rankedPlayers = new List<Player>();
      rankedPlayers.Add(new Player(HouseName.Baratheon));
      rankedPlayers.Add(new Player(HouseName.Lannister));
      rankedPlayers.Add(new Player(HouseName.Stark));

      var track = new InfluenceTrack(InfluenceTrackType.KingsCourt, LeaderTokenType.MessengerRaven, rankedPlayers);

      track.LeaderToken.Should().Be(LeaderTokenType.MessengerRaven);
    }

    /// <summary>
    /// Simulates setup of the standard Iron Throne influence track. The player
    /// added to the track first is the lead player, who has the leader token
    /// for that track.
    /// </summary>
    [TestMethod]
    public void TestSimplePlayerLeaderToken()
    {
      var rankedPlayers = new List<Player>();
      rankedPlayers.Add(new Player(HouseName.Baratheon));
      rankedPlayers.Add(new Player(HouseName.Lannister));
      rankedPlayers.Add(new Player(HouseName.Stark));

      var track = new InfluenceTrack(InfluenceTrackType.IronThrone, LeaderTokenType.IronThrone, rankedPlayers);

      track.GetLeadPlayer().House.Should().Be(HouseName.Baratheon);
    }

    /// <summary>
    /// Simulates a complete reordering of an influence track. This will happen
    /// often after the Game of Thrones card is played.
    /// </summary>
    [TestMethod]
    public void TestReorderPlayers()
    {
      var baratheon = new Player(HouseName.Baratheon);
      var lannister = new Player(HouseName.Lannister);
      var stark = new Player(HouseName.Stark);

      var rankedPlayers = new List<Player>();
      rankedPlayers.Add(baratheon);
      rankedPlayers.Add(lannister);
      rankedPlayers.Add(stark);

      var track = new InfluenceTrack(InfluenceTrackType.IronThrone, LeaderTokenType.IronThrone, rankedPlayers);

      track.Reorder(stark, baratheon, lannister);

      track.GetLeadPlayer().House.Should().Be(HouseName.Stark);
      track.GetRankedPlayers()[0].House.Should().Be(HouseName.Stark);
      track.GetRankedPlayers()[1].House.Should().Be(HouseName.Baratheon);
      track.GetRankedPlayers()[2].House.Should().Be(HouseName.Lannister);
    }

    /// <summary>
    /// Simulates a partial reordering of the top of an influence track. 
    /// This will happen often after the Game of Thrones card is played.
    /// </summary>
    [TestMethod]
    public void TestPartialTopReorder()
    {
      var baratheon = new Player(HouseName.Baratheon);
      var lannister = new Player(HouseName.Lannister);
      var stark = new Player(HouseName.Stark);
      var tyrell = new Player(HouseName.Tyrell);
      var greyjoy = new Player(HouseName.Greyjoy);
      var martell = new Player(HouseName.Martell);

      var rankedPlayers = new List<Player>();
      rankedPlayers.Add(baratheon);
      rankedPlayers.Add(lannister);
      rankedPlayers.Add(stark);
      rankedPlayers.Add(tyrell);
      rankedPlayers.Add(greyjoy);
      rankedPlayers.Add(martell);

      var track = new InfluenceTrack(InfluenceTrackType.IronThrone, LeaderTokenType.IronThrone, rankedPlayers);

      track.Reorder(stark, baratheon, lannister);

      track.GetLeadPlayer().House.Should().Be(HouseName.Stark);
      track.GetRankedPlayers()[0].House.Should().Be(HouseName.Stark);
      track.GetRankedPlayers()[1].House.Should().Be(HouseName.Baratheon);
      track.GetRankedPlayers()[2].House.Should().Be(HouseName.Lannister);
      track.GetRankedPlayers()[3].House.Should().Be(HouseName.Tyrell);
      track.GetRankedPlayers()[4].House.Should().Be(HouseName.Greyjoy);
      track.GetRankedPlayers()[5].House.Should().Be(HouseName.Martell);
    }

    /// <summary>
    /// Simulates a partial reordering of the middle of an influence track.
    /// This will happen often after the Game of Thrones card is played.
    /// </summary>
    [TestMethod]
    public void TestPartialMiddleReorder()
    {
      var baratheon = new Player(HouseName.Baratheon);
      var lannister = new Player(HouseName.Lannister);
      var stark = new Player(HouseName.Stark);
      var tyrell = new Player(HouseName.Tyrell);
      var greyjoy = new Player(HouseName.Greyjoy);
      var martell = new Player(HouseName.Martell);

      var rankedPlayers = new List<Player>();
      rankedPlayers.Add(baratheon);
      rankedPlayers.Add(lannister);
      rankedPlayers.Add(stark);
      rankedPlayers.Add(tyrell);
      rankedPlayers.Add(greyjoy);
      rankedPlayers.Add(martell);

      var track = new InfluenceTrack(InfluenceTrackType.IronThrone, LeaderTokenType.IronThrone, rankedPlayers);

      track.Reorder(tyrell, baratheon, stark);

      track.GetLeadPlayer().House.Should().Be(HouseName.Tyrell);
      track.GetRankedPlayers()[0].House.Should().Be(HouseName.Tyrell);
      track.GetRankedPlayers()[1].House.Should().Be(HouseName.Baratheon);
      track.GetRankedPlayers()[2].House.Should().Be(HouseName.Stark);
      track.GetRankedPlayers()[3].House.Should().Be(HouseName.Lannister);
      track.GetRankedPlayers()[4].House.Should().Be(HouseName.Greyjoy);
      track.GetRankedPlayers()[5].House.Should().Be(HouseName.Martell);
    }

    /// <summary>
    /// Simulates a partial reordering of the bottom of an influence track.
    /// This will happen often after the Game of Thrones card is played.
    /// </summary>
    [TestMethod]
    public void TestPartialBottomReorder()
    {
      var baratheon = new Player(HouseName.Baratheon);
      var lannister = new Player(HouseName.Lannister);
      var stark = new Player(HouseName.Stark);
      var tyrell = new Player(HouseName.Tyrell);
      var greyjoy = new Player(HouseName.Greyjoy);
      var martell = new Player(HouseName.Martell);

      var rankedPlayers = new List<Player>();
      rankedPlayers.Add(baratheon);
      rankedPlayers.Add(lannister);
      rankedPlayers.Add(stark);
      rankedPlayers.Add(tyrell);
      rankedPlayers.Add(greyjoy);
      rankedPlayers.Add(martell);

      var track = new InfluenceTrack(InfluenceTrackType.IronThrone, LeaderTokenType.IronThrone, rankedPlayers);

      track.Reorder(tyrell, greyjoy, martell);

      track.GetLeadPlayer().House.Should().Be(HouseName.Tyrell);
      track.GetRankedPlayers()[0].House.Should().Be(HouseName.Tyrell);
      track.GetRankedPlayers()[1].House.Should().Be(HouseName.Greyjoy);
      track.GetRankedPlayers()[2].House.Should().Be(HouseName.Martell);
      track.GetRankedPlayers()[3].House.Should().Be(HouseName.Baratheon);
      track.GetRankedPlayers()[4].House.Should().Be(HouseName.Lannister);
      track.GetRankedPlayers()[5].House.Should().Be(HouseName.Stark);
    }

    /// <summary>
    /// Ensures that players that don't exist on the track can't be
    /// accidentally reordered into existence. Additionally, the track
    /// shouldn't be partially reordered.
    /// </summary>
    [TestMethod]
    public void TestMissingPlayerReorder()
    {
      var baratheon = new Player(HouseName.Baratheon);
      var lannister = new Player(HouseName.Lannister);
      var stark = new Player(HouseName.Stark);
      var tyrell = new Player(HouseName.Tyrell);

      var rankedPlayers = new List<Player>();
      rankedPlayers.Add(baratheon);
      rankedPlayers.Add(lannister);
      rankedPlayers.Add(stark);

      var track = new InfluenceTrack(InfluenceTrackType.IronThrone, LeaderTokenType.IronThrone, rankedPlayers);

      Action reorder = () => track.Reorder(stark, baratheon, lannister, tyrell);
      reorder.ShouldThrow<ArgumentException>("because player to reorder was not found on this track");

      track.GetLeadPlayer().House.Should().Be(HouseName.Baratheon);
      track.GetRankedPlayers()[0].House.Should().Be(HouseName.Baratheon);
      track.GetRankedPlayers()[1].House.Should().Be(HouseName.Lannister);
      track.GetRankedPlayers()[2].House.Should().Be(HouseName.Stark);
    }
  }
}
