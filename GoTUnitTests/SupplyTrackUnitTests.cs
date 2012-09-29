using FluentAssertions;
using GoT;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GoTUnitTests
{
  [TestClass]
  public class SupplyTrackUnitTests
  {
    /// <summary>
    /// Tests the simplest function of the Supply Track, keeping track of a
    /// player's current supply level.
    /// </summary>
    [TestMethod]
    public void TestBasicSupplyAmount()
    {
      var baratheon = new Player(HouseName.Baratheon);
      var lannister = new Player(HouseName.Lannister);
      var stark = new Player(HouseName.Stark);

      var track = new SupplyTrack(null);

      track.Add(baratheon, supply: 2);
      track.Add(lannister, supply: 2);
      track.Add(stark,     supply: 1);

      track.GetSupply(baratheon).Should().Be(2);
      track.GetSupply(lannister).Should().Be(2);
      track.GetSupply(stark)    .Should().Be(1);
    }

    /// <summary>
    /// Tests a simple adjustment of a player's supply level
    /// </summary>
    [TestMethod]
    public void TestSimpleAdjustSupply()
    {
      var baratheon = new Player(HouseName.Baratheon);
      var lannister = new Player(HouseName.Lannister);
      var stark = new Player(HouseName.Stark);

      var track = new SupplyTrack(null);

      track.Add(baratheon, supply: 2);
      track.Add(lannister, supply: 2);
      track.Add(stark, supply: 1);

      track.Set(stark, supply: 3);
      track.Set(lannister, supply: 1);

      track.GetSupply(baratheon).Should().Be(2);
      track.GetSupply(lannister).Should().Be(1);
      track.GetSupply(stark)    .Should().Be(3);
    }

    /// <summary>
    /// Ensures that multiple players can all have the same supply level
    /// simultaneously.
    /// </summary>
    [TestMethod]
    public void TestSharedSupplyLevels()
    {
      var baratheon = new Player(HouseName.Baratheon);
      var lannister = new Player(HouseName.Lannister);
      var stark = new Player(HouseName.Stark);
      var tyrell = new Player(HouseName.Tyrell);
      var greyjoy = new Player(HouseName.Greyjoy);
      var martell = new Player(HouseName.Martell);

      var track = new SupplyTrack(null);

      track.Add(baratheon, supply: 2);
      track.Add(lannister, supply: 2);
      track.Add(stark,     supply: 2);
      track.Add(tyrell,    supply: 2);
      track.Add(greyjoy,   supply: 2);
      track.Add(martell,   supply: 2);

      track.GetSupply(baratheon).Should().Be(2);
      track.GetSupply(lannister).Should().Be(2);
      track.GetSupply(stark)    .Should().Be(2);
      track.GetSupply(tyrell)   .Should().Be(2);
      track.GetSupply(greyjoy)  .Should().Be(2);
      track.GetSupply(martell)  .Should().Be(2);
    }

    /// <summary>
    /// Ensures no player can ever have less than 0 supply. Attempting to set
    /// a player below this level should leave them as-is.
    /// </summary>
    [TestMethod]
    public void TestMinimumSupply()
    {
      var baratheon = new Player(HouseName.Baratheon);
      var lannister = new Player(HouseName.Lannister);
      var stark = new Player(HouseName.Stark);

      var track = new SupplyTrack(null);

      track.Add(baratheon, supply: 2);
      track.Add(lannister, supply: 2);
      track.Add(stark, supply: 1);

      Action set = () => track.Set(stark, supply: -1);
      set.ShouldThrow<ArgumentOutOfRangeException>("because 0 is the minimum supply level.");

      track.GetSupply(baratheon).Should().Be(2);
      track.GetSupply(lannister).Should().Be(2);
      track.GetSupply(stark).Should().Be(1);
    }

    /// <summary>
    /// Tests that supplied limits are followed.
    /// </summary>
    [TestMethod]
    public void TestBasicArmyCountLimits()
    {
      var track = new SupplyTrack(new List<ArmySizeSupplyLimit>()
                                    { new ArmySizeSupplyLimit() 
                                      { ArmyCountLimit = null, Size = 1, Supply = 0 }});

      track.GetArmyCountLimit(supply: 0, size: 1).Should().Be(null);
    }
  }
}
