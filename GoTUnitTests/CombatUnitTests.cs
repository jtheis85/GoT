using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using GoT;
using Moq;
using GoT.Data;

namespace GoTUnitTests
{
  [TestClass]
  public class CombatUnitTests
  {
    Mock<IGameData> mockData;

    [TestMethod]
    public void TestSingleUnitArmyStrength()
    {
      mockData = new Mock<IGameData>(MockBehavior.Strict);
      mockData.Setup(d => d.GetUnitTypes()).Returns(new List<UnitType>() { new UnitType() { Name = "Footman", Strength = 1 }});

      var units = new UnitFactory(mockData.Object);
      var army = new Army();

      army.Add(units.GetUnit(0));
      army.Strength.Should().Be(1);
    }

    [TestMethod]
    public void TestMultiUnitArmyStrength()
    {
      mockData = new Mock<IGameData>(MockBehavior.Strict);
      mockData.Setup(d => d.GetUnitTypes()).Returns(new List<UnitType>() { new UnitType() { Name = "Footman", Strength = 1 } });

      var units = new UnitFactory(mockData.Object);
      var army = new Army();

      army.Add(units.GetUnit(0));
      army.Add(units.GetUnit(0));
      army.Strength.Should().Be(2);
    }

    [TestMethod]
    public void TestMultiTypeArmyStrength()
    {
      mockData = new Mock<IGameData>(MockBehavior.Strict);
      mockData.Setup(d => d.GetUnitTypes()).Returns(new List<UnitType>() 
      { 
        new UnitType() { Name = "Footman", Strength = 1 },
        new UnitType() { Name = "Knight", Strength = 2 }
      });

      var units = new UnitFactory(mockData.Object);
      var army = new Army();

      army.Add(units.GetUnit(0));
      army.Add(units.GetUnit(1));
      army.Strength.Should().Be(3);
    }

    [TestMethod]
    public void TestSimpleCombatCalculation()
    {
      mockData = new Mock<IGameData>(MockBehavior.Strict);
      mockData.Setup(d => d.GetUnitTypes()).Returns(new List<UnitType>() 
      { 
        new UnitType() { Name = "Footman", Strength = 1 },
        new UnitType() { Name = "Knight", Strength = 2 }
      });

      var units = new UnitFactory(mockData.Object);
      var weakArmy = new Army();
      var strongArmy = new Army();

      weakArmy.Add(units.GetUnit(0));
      strongArmy.Add(units.GetUnit(1));

      Combat.DetermineVictor(weakArmy, strongArmy).Should().Be(strongArmy);
    }

    [TestMethod]
    public void TestLargeCombatCalculation()
    {
      mockData = new Mock<IGameData>(MockBehavior.Strict);
      mockData.Setup(d => d.GetUnitTypes()).Returns(new List<UnitType>() 
      { 
        new UnitType() { Name = "Footman", Strength = 1 },
        new UnitType() { Name = "Knight", Strength = 2 },
        new UnitType() { Name = "Dragon", Strength = 10 }
      });

      var units = new UnitFactory(mockData.Object);
      var infantryArmy = new Army();
      var mountedArmy = new Army();
      var airCavArmy = new Army();

      infantryArmy.Add(units.GetUnit(ID: 0), count: 20);
      infantryArmy.Add(units.GetUnit(ID: 1), count: 5);
      infantryArmy.Add(units.GetUnit(ID: 2), count: 1);

      mountedArmy.Add(units.GetUnit(ID: 0), count: 5);
      mountedArmy.Add(units.GetUnit(ID: 1), count: 10);
      mountedArmy.Add(units.GetUnit(ID: 2), count: 1);

      airCavArmy.Add(units.GetUnit(ID: 0), count: 5);
      airCavArmy.Add(units.GetUnit(ID: 1), count: 2);
      airCavArmy.Add(units.GetUnit(ID: 2), count: 5);

      Combat.DetermineVictor(infantryArmy, mountedArmy, airCavArmy).Should().Be(airCavArmy);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// TODO: Determine what to do in the event of combat ties
    /// </remarks>
    [TestMethod]
    public void TestCombatTieCalculation()
    {
      mockData = new Mock<IGameData>(MockBehavior.Strict);
      mockData.Setup(d => d.GetUnitTypes()).Returns(new List<UnitType>() 
      { 
        new UnitType() { Name = "Footman", Strength = 1 },
        new UnitType() { Name = "Knight", Strength = 2 }
      });

      var units = new UnitFactory(mockData.Object);
      var weakArmy = new Army();
      var strongArmy = new Army();

      weakArmy.Add(units.GetUnit(1));
      strongArmy.Add(units.GetUnit(1));

      Combat.DetermineVictor(weakArmy, strongArmy).Should().Be(null);
    }
  }
}
