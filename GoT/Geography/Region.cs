using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoT
{
  public class Region
  {
    public int ID { get; set; }

    public string Name { get; set; }

    public HouseName HomeRegion { get; set; }

    public int Supply { get; set; }

    public int Power { get; set; }

    public RegionType RegionType { get; set; }

    public string Shape { get; set; }

    /////          Left, Top, Right, Bottom
    ///// <summary>
    ///// Left, Top, Right, Bottom
    ///// </summary>
    //public Tuple<int,  int, int,   int> Margin { get; set; }

    public string Margin { get; set; }
  }

  public enum RegionType
  {
    Land,
    Sea,
    Port
  }
}
