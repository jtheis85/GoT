using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Linq;
using GoT;

namespace GoTIntegrationTests
{
  /// <summary>
  /// 
  /// </summary>
  [TestClass]
  [DeploymentItem(@"Data\Houses.xml", @"Data\")]
  [DeploymentItem(@"Data\Game.xml", @"Data\")]
  public class SetupTests
  {
    #region Test Initialization
    Game TestGame;

    /// <summary>
    /// 
    /// </summary>
    [TestInitialize]
    public void InitializeTests()
    {
      TestGame = new Game();
    } 
    #endregion

    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public void StartingPowerSuccessTest()
    {
      TestGame.Setup(playerCount: 5);

      foreach (var player in TestGame.GetPlayers())
      {
        player.CurrentPower.Should().Be(5, "because each player should start the game with 5 power");
      }
    }

    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public void WildlingThreatSuccessTest()
    {
      TestGame.Setup(playerCount: 3);
      TestGame.WildlingThreat.Should().Be(2, "because the game should start with a Wildling Threat Level of 2");
    }

    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public void NPlayerValidHousesTest()
    {
      for (int i = 3; i <= 6; i++)
      {
        int playerCount = i;
        TestGame.Setup(playerCount);

        TestGame.GetPlayers().Should().OnlyContain(p => p.MinPlayers <= playerCount);
        TestGame.GetPlayers().Count().Should().Be(playerCount);

        #region Old Version
        //private void VerifyValidHouses(int playerCount)
        //{
        //  TestGame.GetPlayers().Count(p => p.House == HouseName.Baratheon)
        //    .Should().Be(1, "because a {0} player game must contain exactly one house named Baratheon", playerCount);
        //  TestGame.GetPlayers().Count(p => p.House == HouseName.Lannister)
        //    .Should().Be(1, "because a {0} player game must contain exactly one house named Lannister", playerCount);
        //  TestGame.GetPlayers().Count(p => p.House == HouseName.Stark)
        //    .Should().Be(1, "because a {0} player game must contain exactly one house named Stark", playerCount);

        //  if (playerCount >= 4)
        //  {
        //    TestGame.GetPlayers().Count(p => p.House == HouseName.Greyjoy)
        //      .Should().Be(1, "because a {0} player game must contain exactly one house named Tyrell", playerCount);
        //  }
        //  if (playerCount >= 5)
        //  {
        //    TestGame.GetPlayers().Count(p => p.House == HouseName.Tyrell)
        //      .Should().Be(1, "because a {0} player game must contain exactly one house named Greyjoy", playerCount);
        //  }
        //  if (playerCount == 6)
        //  {
        //    TestGame.GetPlayers().Count(p => p.House == HouseName.Martell)
        //      .Should().Be(1, "because a {0} player game must contain exactly one house named Martell", playerCount);
        //  }
        //} 
        #endregion
      }
    }

    /// <summary>
    /// Tests that players begin the game with a non-zero supply amount
    /// </summary>
    [TestMethod]
    public void StartingSupplySuccessTest()
    {
      for (int i = 3; i <= 6; i++)
      {
        int playersVerified = 0;
        int playerCount = i;
        TestGame.Setup(playerCount);

        foreach (var player in TestGame.GetPlayers())
        {
          // TODO: Publicly exposing SupplyTrack seems wrong. 
          TestGame.SupplyTrack.GetSupply(player).Should().BeGreaterOrEqualTo(1, "because players should start with at least 1 supply");
          TestGame.SupplyTrack.GetSupply(player).Should().BeLessOrEqualTo(6, "because no player should start with more than 6 supply");
          playersVerified++;

          #region Old Version
          //if (player.House == HouseName.Baratheon)
          //{
          //  player.CurrentSupply.Should().Be(2, "because House Baratheon starts the game with 2 supply");
          //  playersVerified++;
          //}

          //else if (player.House == HouseName.Stark)
          //{
          //  player.CurrentSupply.Should().Be(1, "because House Stark starts the game with 1 supply");
          //  playersVerified++;
          //}

          //else if (player.House == HouseName.Lannister)
          //{
          //  player.CurrentSupply.Should().Be(2, "because House Lannister starts the game with 2 supply");
          //  playersVerified++;
          //}

          //else if (player.House == HouseName.Tyrell)
          //{
          //  player.CurrentSupply.Should().Be(2, "because House Tyrell starts the game with 2 supply");
          //  playersVerified++;
          //}

          //else if (player.House == HouseName.Greyjoy)
          //{
          //  player.CurrentSupply.Should().Be(2, "because House Greyjoy starts the game with 2 supply");
          //  playersVerified++;
          //}

          //else if (player.House == HouseName.Martell)
          //{
          //  player.CurrentSupply.Should().Be(2, "because House Martell starts the game with 2 supply");
          //  playersVerified++;
          //} 
          #endregion
        }

        playersVerified.Should().Be(playerCount, String.Format("because {0} players must be set up with starting supply.", playerCount));
      }
    }

    /// <summary>
    /// Tests that players begin the game with influence positions between 1
    /// and the number of players
    /// </summary>
    [TestMethod]
    public void StartingInfluenceTracksSuccessTest()
    {
      for (int i = 3; i <= 6; i++)
      {
        int playerCount = i;
        int playersVerified = 0;
        TestGame.Setup(playerCount);

        foreach (var player in TestGame.GetPlayers())
        {
          foreach (var track in TestGame.GetInfluenceTracks())
          {
            track.Rank(player).Should().BeGreaterOrEqualTo(1, "because players cannot be better than position 1");
            track.Rank(player).Should().BeLessOrEqualTo(playerCount, "because players cannot be worse than last");
          }

          playersVerified++;

          #region Old Version
          //player.IronThroneRank.Should().BeGreaterOrEqualTo(1, "because players cannot be better than position 1");
          //player.IronThroneRank.Should().BeLessOrEqualTo(playerCount, "because players cannot be worse than last");
          //player.KingsCourtRank.Should().BeGreaterOrEqualTo(1, "because players cannot be better than position 1");
          //player.KingsCourtRank.Should().BeLessOrEqualTo(playerCount, "because players cannot be worse than last");
          //player.FiefdomsRank.Should().BeGreaterOrEqualTo(1, "because players cannot be better than position 1");
          //player.FiefdomsRank.Should().BeLessOrEqualTo(playerCount, "because players cannot be worse than last");

          //if (player.House == HouseName.Baratheon)
          //{
          //  VerifyRank(player, InfluenceTrack.IronThrone, playerCount, expectedRank: 1);

          //  if (playerCount == 3)
          //  {
          //    VerifyRank(player, InfluenceTrack.KingsCourt, playerCount, expectedRank: 3);
          //    VerifyRank(player, InfluenceTrack.Fiefdoms, playerCount, expectedRank: 2);

          //  }
          //  else if (playerCount == 4)
          //  {
          //    VerifyRank(player, InfluenceTrack.KingsCourt, playerCount, expectedRank: 7);
          //    VerifyRank(player, InfluenceTrack.Fiefdoms, playerCount, expectedRank: 7);
          //  }
          //  else if (playerCount == 5)
          //  {
          //    VerifyRank(player, InfluenceTrack.KingsCourt, playerCount, expectedRank: 7);
          //    VerifyRank(player, InfluenceTrack.Fiefdoms, playerCount, expectedRank: 7);
          //  }
          //  else if (playerCount == 6)
          //  {
          //    VerifyRank(player, InfluenceTrack.KingsCourt, playerCount, expectedRank: 7);
          //    VerifyRank(player, InfluenceTrack.Fiefdoms, playerCount, expectedRank: 7);
          //  }

          //  playersVerified++;
          //}
          //else if (player.House == HouseName.Stark)
          //{
          //  VerifyRank(player, InfluenceTrack.IronThrone, playerCount, expectedRank: 3);
          //  VerifyRank(player, InfluenceTrack.KingsCourt, playerCount, expectedRank: 2);

          //  if (playerCount == 3)
          //    VerifyRank(player, InfluenceTrack.Fiefdoms, playerCount, expectedRank: 1);
          //  else if (playerCount == 4)
          //    VerifyRank(player, InfluenceTrack.Fiefdoms, playerCount, expectedRank: 3);
          //  else if (playerCount == 5)
          //    VerifyRank(player, InfluenceTrack.Fiefdoms, playerCount, expectedRank: 4);
          //  else if (playerCount == 6)
          //    VerifyRank(player, InfluenceTrack.Fiefdoms, playerCount, expectedRank: 4);

          //  playersVerified++;
          //}
          //else if (player.House == HouseName.Lannister)
          //{
          //  VerifyRank(player, InfluenceTrack.IronThrone, playerCount, expectedRank: 2);
          //  VerifyRank(player, InfluenceTrack.KingsCourt, playerCount, expectedRank: 1);

          //  if (playerCount == 3)
          //    VerifyRank(player, InfluenceTrack.Fiefdoms, playerCount, expectedRank: 3);
          //  else if (playerCount == 4)
          //    VerifyRank(player, InfluenceTrack.Fiefdoms, playerCount, expectedRank: 4);
          //  else if (playerCount == 5)
          //    VerifyRank(player, InfluenceTrack.Fiefdoms, playerCount, expectedRank: 5);
          //  else if (playerCount == 6)
          //    VerifyRank(player, InfluenceTrack.Fiefdoms, playerCount, expectedRank: 6);

          //  playersVerified++;
          //}
          //else if (player.House == HouseName.Tyrell)
          //{
          //  if (playerCount == 5)
          //  {
          //    VerifyRank(player, InfluenceTrack.IronThrone, playerCount, expectedRank: 5);
          //    VerifyRank(player, InfluenceTrack.KingsCourt, playerCount, expectedRank: 5);
          //    VerifyRank(player, InfluenceTrack.Fiefdoms, playerCount, expectedRank: 2);
          //  }
          //  else if (playerCount == 6)
          //  {
          //    VerifyRank(player, InfluenceTrack.IronThrone, playerCount, expectedRank: 6);
          //    VerifyRank(player, InfluenceTrack.KingsCourt, playerCount, expectedRank: 5);
          //    VerifyRank(player, InfluenceTrack.Fiefdoms, playerCount, expectedRank: 2);
          //  }

          //  playersVerified++;
          //}
          //else if (player.House == HouseName.Greyjoy)
          //{
          //  VerifyRank(player, InfluenceTrack.Fiefdoms, playerCount, expectedRank: 1);

          //  if (playerCount == 4)
          //  {
          //    VerifyRank(player, InfluenceTrack.IronThrone, playerCount, expectedRank: 4);
          //    VerifyRank(player, InfluenceTrack.KingsCourt, playerCount, expectedRank: 4);
          //  }
          //  else if (playerCount == 5)
          //  {
          //    VerifyRank(player, InfluenceTrack.IronThrone, playerCount, expectedRank: 5);
          //    VerifyRank(player, InfluenceTrack.KingsCourt, playerCount, expectedRank: 5);
          //  }
          //  else if (playerCount == 6)
          //  {
          //    VerifyRank(player, InfluenceTrack.IronThrone, playerCount, expectedRank: 5);
          //    VerifyRank(player, InfluenceTrack.KingsCourt, playerCount, expectedRank: 6);
          //  }
          //  playersVerified++;
          //}
          //else if (player.House == HouseName.Martell)
          //{
          //  VerifyRank(player, InfluenceTrack.IronThrone, playerCount, expectedRank: 4);
          //  VerifyRank(player, InfluenceTrack.KingsCourt, playerCount, expectedRank: 3);
          //  VerifyRank(player, InfluenceTrack.Fiefdoms, playerCount, expectedRank: 3);
          //  playersVerified++;
          //} 

          //private void VerifyRank(Player player, InfluenceTrack track, int playerCount, int expectedRank)
          //{
          //  player.GetRank(track).Should().Be(expectedRank, String.Format(
          //    "because {0} starts the game in position {1} on the {2} in a {3} player game.", player.House, expectedRank, track, playerCount));
          //}
          #endregion
        }

        playersVerified.Should().Be(playerCount, String.Format("because {0} players must be set up on the influence tracks.", playerCount));
      }
    }

    /// <summary>
    /// Ensures that each player exists only once on each influence track
    /// </summary>
    [TestMethod]
    public void PlayerOncePerInfluenceTrackSuccessTest()
    {
      for (int i = 3; i <= 6; i++)
      {
        int playerCount = i;
        int placesVerified = 0;
        TestGame.Setup(playerCount);

        foreach (var track in TestGame.GetInfluenceTracks())
        {
          for (int j = 1; j <= playerCount; j++)
          {
            int trackPosition = j;
            TestGame.GetPlayers()
              .Count(p => track.Rank(p) == trackPosition)
              .Should().Be(1, "because each spot on the influence track should be occupied by exactly 1 player");
            placesVerified++;
          }
        }

        int tracksCount = TestGame.GetInfluenceTracks().Count();
        int placesCount = playerCount * tracksCount;
        placesVerified.Should().Be(placesCount, String.Format(
          "because {0} players must be set up only once on {1} influence tracks.", playerCount, tracksCount));
      } 

      #region Old Version
      //for (int i = 3; i <= 6; i++)
      //{
      //  int playerCount = i;
      //  int placesVerified = 0;
      //  TestGame.Setup(playerCount);

      //  foreach (var track in InfluenceTracks.GetTracks())
      //  {
      //    for (int j = 1; j <= playerCount; j++)
      //    {
      //      int trackPosition = j;
      //      TestGame.GetPlayers()
      //        .Count(p => p.GetRank(track) == trackPosition)
      //        .Should().Be(1, "because each spot on the influence track should be occupied by exactly 1 player");

      //      placesVerified++;
      //    }
      //  }

      //  int tracksCount = Enum.GetValues(typeof(InfluenceTrack)).Length;
      //  int placesCount = playerCount * tracksCount;
      //  placesVerified.Should().Be(placesCount, String.Format(
      //    "because {0} players must be set up in {1} places on {2} influence tracks.", playerCount, placesCount, tracksCount));
      //} 
      #endregion
    }

    /// <summary>
    /// Verifies that each influence token is held by one and only one player
    /// at the start of the game.
    /// 
    /// Encompasses 12 individual tests, 4 player count configurations by 3
    /// influence tracks
    /// </summary>
    [TestMethod]
    public void InfluenceLeaderTokensSuccessTest()
    {
      for (int i = 3; i <= 6; i++)
      {
        int playerCount = i;
        TestGame.Setup(playerCount);

        foreach (var track in TestGame.GetInfluenceTracks())
        {
          track.GetLeadPlayer().Should().NotBeNull("because the leader token for each track ({0}) should be posessed by exactly one player", track.TrackType.ToString());

        }
      }
    }
  }
}
