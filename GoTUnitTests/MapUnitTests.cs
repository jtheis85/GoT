using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoT;
using Moq;
using FluentAssertions;
using GoT.Data;
using GoT.Geography;

namespace GoTUnitTests
{
  [TestClass]
  public class MapUnitTests
  {
    Mock<IGameData> mockData;

    /// <summary>
    /// Ensures that if a number of regions are added to a map, the map
    /// has that number of regions.
    /// </summary>
    [TestMethod]
    public void BasicRegions()
    {
      mockData = new Mock<IGameData>(MockBehavior.Loose);
      mockData.Setup(d => d.GetRegions()).Returns(new List<Region>()
      {
        new Region() { ID = 0, Name = "Dragonstone", HomeRegion = HouseName.Baratheon, Supply = 1, Power = 1},
        new Region() { ID = 1, Name = "Lannisport",  HomeRegion = HouseName.Lannister, Supply = 1, Power = 1},
        new Region() { ID = 2, Name = "Winterfell",  HomeRegion = HouseName.Stark,     Supply = 1, Power = 1}
      });

      var map = new Map(mockData.Object);
      map.RegionCount.Should().Be(3);
    }

    /// <summary>
    /// Ensures that regions set to be adjacent are adjacent on the map, and 
    /// that those that are not set to be adjacent are not adjacent on the map.
    /// </summary>
    [TestMethod]
    public void BasicAdjacency()
    {
      mockData = new Mock<IGameData>(MockBehavior.Loose);
      mockData.Setup(d => d.GetRegions()).Returns(new List<Region>()
      {
        new Region() { ID = 0, Name = "Dragonstone", HomeRegion = HouseName.Baratheon, Supply = 1, Power = 1},
        new Region() { ID = 1, Name = "Lannisport",  HomeRegion = HouseName.Lannister, Supply = 1, Power = 1},
        new Region() { ID = 2, Name = "Winterfell",  HomeRegion = HouseName.Stark,     Supply = 1, Power = 1},
        new Region() { ID = 3, Name = "Highgarden",  HomeRegion = HouseName.Tyrell,    Supply = 1, Power = 1}
      });
      mockData.Setup(d => d.GetRegionAdjacencies()).Returns(new List<RegionAdjacency>()
      {
        new RegionAdjacency() { FirstRegion = 0,  SecondRegion = 1 },
        new RegionAdjacency() { FirstRegion = 1,  SecondRegion = 2 },
        new RegionAdjacency() { FirstRegion = 2,  SecondRegion = 0 }
      });

      var map = new Map(mockData.Object);

      map.AreAdjacent(firstRegionID: 0, secondRegionID: 1).Should().Be(true);
      map.AreAdjacent(firstRegionID: 1, secondRegionID: 2).Should().Be(true);
      map.AreAdjacent(firstRegionID: 2, secondRegionID: 0).Should().Be(true);

      map.AreAdjacent(firstRegionID: 3, secondRegionID: 2).Should().Be(false);
      map.AreAdjacent(firstRegionID: 3, secondRegionID: 1).Should().Be(false);
      map.AreAdjacent(firstRegionID: 3, secondRegionID: 0).Should().Be(false);
    }

    /// <summary>
    /// Ensures that adjacency works in both directions.
    /// </summary>
    /// <example>
    /// A Winterfell/Lannisport adjacency is the same as a Lannisport/Winterfell adjacency.
    /// </example>
    [TestMethod]
    public void TestReverseAdjacency()
    {
      mockData = new Mock<IGameData>(MockBehavior.Loose);
      mockData.Setup(d => d.GetRegions()).Returns(new List<Region>()
      {
        new Region() { ID = 0, Name = "Dragonstone", HomeRegion = HouseName.Baratheon, Supply = 1, Power = 1},
        new Region() { ID = 1, Name = "Lannisport",  HomeRegion = HouseName.Lannister, Supply = 1, Power = 1},
        new Region() { ID = 2, Name = "Winterfell",  HomeRegion = HouseName.Stark,     Supply = 1, Power = 1},
        new Region() { ID = 3, Name = "Highgarden",  HomeRegion = HouseName.Tyrell,    Supply = 1, Power = 1}
      });
      mockData.Setup(d => d.GetRegionAdjacencies()).Returns(new List<RegionAdjacency>()
      {
        new RegionAdjacency() { FirstRegion = 0,  SecondRegion = 1 },
        new RegionAdjacency() { FirstRegion = 1,  SecondRegion = 2 },
        new RegionAdjacency() { FirstRegion = 2,  SecondRegion = 0 }
      });

      var map = new Map(mockData.Object);

      map.AreAdjacent(firstRegionID: 0, secondRegionID: 2).Should().Be(true);
      map.AreAdjacent(firstRegionID: 2, secondRegionID: 1).Should().Be(true);
      map.AreAdjacent(firstRegionID: 1, secondRegionID: 0).Should().Be(true);

      map.AreAdjacent(firstRegionID: 0, secondRegionID: 3).Should().Be(false);
      map.AreAdjacent(firstRegionID: 1, secondRegionID: 3).Should().Be(false);
      map.AreAdjacent(firstRegionID: 2, secondRegionID: 3).Should().Be(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// TODO: Make test regions setting-agnostic
    /// 
    /// I started with regions named after GoT regions in my tests. This could 
    /// be confusing, especially if test cases don't match the map, or the map 
    /// system were to be used for alternate maps, settings, or even games. 
    /// Probably better to stick with test-specific region names, and names in 
    /// general, or no names at all if possible.
    /// </remarks>
    [TestMethod]
    public void TestAdjacencyType()
    {
      mockData = new Mock<IGameData>(MockBehavior.Loose);
      mockData.Setup(d => d.GetRegions()).Returns(new List<Region>()
      {
        new Region() { ID = 0}, 
        new Region() { ID = 1}, 
        new Region() { ID = 2}, 
        new Region() { ID = 3}
      });

      mockData.Setup(d => d.GetRegionAdjacencies()).Returns(new List<RegionAdjacency>()
      {
        new RegionAdjacency() { FirstRegion = 0,  SecondRegion = 1, AdjacencyType = AdjacencyType.Bridge},
        new RegionAdjacency() { FirstRegion = 1,  SecondRegion = 2, AdjacencyType = AdjacencyType.River},
        new RegionAdjacency() { FirstRegion = 2,  SecondRegion = 3}
      });

      var map = new Map(mockData.Object);


      // TODO: Allow adjacency tests to be called on region
      // This seems wrong. More natural: if (region1.IsAdjacentTo(Region2, by: AdjacencyType.Bridge))
      // Possibly have regions hold a reference to their map to call the base function?
      map.AreAdjacentBy(AdjacencyType.Bridge,  firstRegionID: 0, secondRegionID: 1).Should().Be(true);
      map.AreAdjacentBy(AdjacencyType.Bridge,  firstRegionID: 2, secondRegionID: 3).Should().Be(false);
      map.AreAdjacentBy(AdjacencyType.Default, firstRegionID: 2, secondRegionID: 3).Should().Be(true);
    }

    /// <summary>
    /// Tests that simple region shape and margin data is correctly stored and retrieved
    /// </summary>
    [TestMethod]
    public void TestSimpleRegionData()
    {
      mockData = new Mock<IGameData>(MockBehavior.Loose);

      mockData.Setup(d => d.GetRegions()).Returns(new List<Region>()
      {
        new Region() 
        { 
          ID = 0, 
          Shape = "M 5,5 L 10,5 L 10,10 L 5,10 Z",
          Margin = new Tuple<int, int, int, int>(15, 10, 5, 25)
        }, 
      });

      var map = new Map(mockData.Object);

      map.Region(0).Shape.Should().Be("M 5,5 L 10,5 L 10,10 L 5,10 Z");
      map.Region(0).Margin.Should().Be(new Tuple<int, int, int, int>(15, 10, 5, 25));
    }

    /// <summary>
    /// Tests that the map's art background size is correctly stored, in order
    /// to accurately line up overlaid regions
    /// </summary>
    [TestMethod]
    public void TestSimpleMapBackground()
    {
      mockData = new Mock<IGameData>(MockBehavior.Loose);

      mockData.Setup(d => d.GetBackgroundDimensions()).Returns(new Tuple<int, int>(1600, 900));

      var map = new Map(mockData.Object);

      map.BackgroundDimensions.Should().Be(new Tuple<int, int>(1600, 900));

    }
  }
}
