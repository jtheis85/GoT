using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoT;
using FluentAssertions;
using Moq;
using GoT.Data;

namespace GoTUnitTests
{
  [TestClass]
  public class VictoryTrackUnitTests
  {
    Mock<IGameData> mockData;

    private TestContext m_testContext;
    public TestContext TestContext
    {
      get { return m_testContext; }
      set { m_testContext = value; }
    }

    
    #region Deployment Items
    [DeploymentItem("GoTUnitTests\\VictoryTrackTestData.xml")]
    [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\VictoryTrackTestData.xml", "TestCase", DataAccessMethod.Sequential)]
    #endregion
    [TestMethod]
    public void BasicVictory()
    {
      var baratheon = new Player();
      var lannister = new Player();
      var stark = new Player();

      var track = new VictoryTrack(new Mock<IGameData>().Object);

      track.Add(baratheon, Convert.ToInt32(TestContext.DataRow["baratheonInput"]));
      track.Add(lannister, Convert.ToInt32(TestContext.DataRow["lannisterInput"]));
      track.Add(stark,     Convert.ToInt32(TestContext.DataRow["starkInput"]));

      track.GetVictoryPoints(baratheon).Should().Be(Convert.ToInt32(TestContext.DataRow["baratheonOutput"]));
      track.GetVictoryPoints(lannister).Should().Be(Convert.ToInt32(TestContext.DataRow["lannisterOutput"]));
      track.GetVictoryPoints(stark).Should()    .Be(Convert.ToInt32(TestContext.DataRow["starkOutput"]));
    }

    [TestMethod]
    public void VictoryTrackLimit()
    {
      mockData = new Mock<IGameData>(MockBehavior.Strict);
      mockData.Setup(d => d.GetVictoryPointLimit()).Returns(10);
      var track = new VictoryTrack(mockData.Object);

      track.VictoryPointLimit.Should().Be(10);
    }
  }
}
