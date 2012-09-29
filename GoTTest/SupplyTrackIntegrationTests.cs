using FluentAssertions;
using GoT;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoTIntegrationTests
{
  /// <summary>
  /// Contains integration tests related to the Supply track
  /// </summary>
  /// <remarks>
  /// TODO: Change GameData to allow tests to point to their own files
  /// </remarks>
  [TestClass]
  public class SupplyTrackIntegrationTests
  {
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// TODO: Need a way to test that a track DOESN'T contain other limits
    /// e.g. How do I know Supply 0 doesn't allow 4 armies of size 50 without
    /// explicitly testing a bunch of values. However, assuming the data file
    /// stays within the standard game maximum of size 4 armies and 6 supply,
    /// this isn't a major concern.
    /// </remarks>
    [TestMethod]
    public void TestLoadLimitsFromFile()
    {
      var track = new SupplyTrack(GameData.GetSupplyLimits());

      track.GetArmyCountLimit(supply: 0, size: 1).Should().Be(null);
      track.GetArmyCountLimit(supply: 0, size: 2).Should().Be(2);
      track.GetArmyCountLimit(supply: 0, size: 3).Should().Be(0);
      track.GetArmyCountLimit(supply: 0, size: 4).Should().Be(0);

      track.GetArmyCountLimit(supply: 1, size: 1).Should().Be(null);
      track.GetArmyCountLimit(supply: 1, size: 2).Should().Be(1);
      track.GetArmyCountLimit(supply: 1, size: 3).Should().Be(1);
      track.GetArmyCountLimit(supply: 1, size: 4).Should().Be(0);

      track.GetArmyCountLimit(supply: 2, size: 1).Should().Be(null);
      track.GetArmyCountLimit(supply: 2, size: 2).Should().Be(2);
      track.GetArmyCountLimit(supply: 2, size: 3).Should().Be(1);
      track.GetArmyCountLimit(supply: 2, size: 4).Should().Be(0);

      track.GetArmyCountLimit(supply: 3, size: 1).Should().Be(null);
      track.GetArmyCountLimit(supply: 3, size: 2).Should().Be(3);
      track.GetArmyCountLimit(supply: 3, size: 3).Should().Be(1);
      track.GetArmyCountLimit(supply: 3, size: 4).Should().Be(0);

      track.GetArmyCountLimit(supply: 4, size: 1).Should().Be(null);
      track.GetArmyCountLimit(supply: 4, size: 2).Should().Be(2);
      track.GetArmyCountLimit(supply: 4, size: 3).Should().Be(2);
      track.GetArmyCountLimit(supply: 4, size: 4).Should().Be(0);

      track.GetArmyCountLimit(supply: 5, size: 1).Should().Be(null);
      track.GetArmyCountLimit(supply: 5, size: 2).Should().Be(2);
      track.GetArmyCountLimit(supply: 5, size: 3).Should().Be(1);
      track.GetArmyCountLimit(supply: 5, size: 4).Should().Be(1);

      track.GetArmyCountLimit(supply: 6, size: 1).Should().Be(null);
      track.GetArmyCountLimit(supply: 6, size: 2).Should().Be(3);
      track.GetArmyCountLimit(supply: 6, size: 3).Should().Be(1);
      track.GetArmyCountLimit(supply: 6, size: 4).Should().Be(1);
    }
  }
}
