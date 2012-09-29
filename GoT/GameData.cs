using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace GoT
{
  public class GameData
  {
    public static IEnumerable<HouseName> GetValidHouses(int playerCount)
    {
      var xml = XDocument.Load(@"Data\Houses.xml");
      return xml.Descendants("house")
        .Where(x => (int) x.Attribute("minPlayers") <= playerCount)
        .Select(x => GameData.ParseHouse(x));
    }

    public static int GetStartingWildlingThreat()
    {
      var xml = XDocument.Load(@"Data\Game.xml");

      return xml.Descendants("gameParameters").Select(x => (int) x.Attribute("startingWildlingThreat")).First();
    }

    /// <summary>
    /// TODO: Write a test covering GetStartingInfluence
    /// </summary>
    /// <param name="house"></param>
    /// <param name="track"></param>
    /// <returns></returns>
    public static int GetStartingInfluence(HouseName house, InfluenceTrackType track)
    {
      var xml = XDocument.Load(@"Data\Houses.xml");
      return (int) xml.Descendants("house")
        .First(x => house == GameData.ParseHouse(x)).Attribute("starting" + track.ToString());
    }

    public static int GetStartingSupply(HouseName house)
    {
      var xml = XDocument.Load(@"Data\Houses.xml");
      return (int) xml.Descendants("house")
        .First(x => house == GameData.ParseHouse(x)).Attribute("startingSupply");
    }

    public static int GetStartingPower()
    {
      var xml = XDocument.Load(@"Data\Game.xml");

      return xml.Descendants("gameParameters").Select(x => (int)x.Attribute("startingPowerPerPlayer")).First();
    }

    public static IEnumerable<ArmySizeSupplyLimit> GetSupplyLimits()
    {
      var xml = XDocument.Load(@"Data\Game.xml");

      return xml.Descendants("supplyLimit").Select(x => new ArmySizeSupplyLimit() {
                                                    Supply = int.Parse((string)x.Attribute("supply")),
                                                    Size = int.Parse((string)x.Attribute("size")),
                                                    ArmyCountLimit = (string)x.Attribute("limit") == "null" ? null : (int?)int.Parse((string)x.Attribute("limit"))});
    }

    private static HouseName ParseHouse(XElement element)
    {
      return (HouseName)Enum.Parse(typeof(HouseName), (string)element.Attribute("name"));
    }
  }
}