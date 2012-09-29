using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoT
{
  public class Player
  {
    public int CurrentPower   { get; private set; }
    public int CurrentSupply  { get; private set; }
    public int MinPlayers     { get; private set; }
    public HouseName House    { get; private set; }

    public Player(HouseName house)
    {
      House = house;
    }

    public Player()
    {
      // TODO: Complete member initialization
    }

    public void AddPower(int power)
    {
      CurrentPower += power;
    }

    public void AddSupply(int supply)
    {
      CurrentSupply += supply;
    }
  }

  public enum HouseName
  {
    Baratheon,
    Stark,
    Lannister,
    Greyjoy,
    Tyrell,
    Martell
  }
}


