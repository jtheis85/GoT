﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GoT.Data
{
  public class DesignGameData : IGameData
  {
    public int GetVictoryPointLimit()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Region> GetRegions()
    {
      var Regions = new ObservableCollection<Region>();

      Regions.Add(new Region()
      {
        Name = "The North",
        Margin = "50, 62, 148, 228",
        Shape = "M64,277.5 L65,62.5 974,67 858.5,253 878,361 990.5,470.5 1080.5,481 1116.5,542.5 1196,569.5 1280,506.5 1211,658 1257.5,904 1442,983.5 1511,956.5 C1511,956.5 1517,1162 1512.5,1162 1508,1162 1431.4996,1069 1431.4996,1069 L1368.4996,1253.5 1270.9997,1217.5 1341.4996,1552 1176.4997,1730.5 1045.9997,1742.5 946.99975,1619.5 936.49975,1720 664.99983,1718.5 517.99987,1684 324.49993,1723 C324.49993,1723 229.99995,1561 229.99995,1553.5 229.99995,1546 160.99997,1517.5 160.99997,1517.5 L210.49996,1336 321.49993,1365.5 321.49993,1029.5 443.49989,1175.5 715.49981,1045.5 811.49979,811.5 899.49976,697.5 731.49981,705.5 795.49979,627.5 623.49984,589.5 489.49988,715.5 213.49996,565.5 287.49994,393.5 121.49998,277.5 z"
      });

      Regions.Add(new Region()
      {
        Name = "The South",
        Margin = "148, 1746, 219, 143",
        Shape = "M148,1934L211.5,1837.5 335.5,1877.5 403.5,1793.5 899.5,1745.5 809,1921 884,2068 1268,1954 1248.5,2132.5 1463,2083 1311.5,2296C1311.5,2296 1577,2285.5 1572.5,2285.5 1568,2285.5 1302.5,2476 1302.5,2476L1061,2474.5 1154,2603.5 1410.5,2564.5 1227.5,2777.5 1069.5,2909.5 1385.5,2889.5 1453.5,3013.5 1357.5,3069.5 1387.5,3167.5 1263.5,3171.5 1297.5,3265.5 1535.5,3287.5 1415.5,3413.5 1139.5,3329.5 963.5,3433.5 891.5,3623.5 1337.5,3691.5 1481.5,3633.5 1529.5,3695.5 1389.5,3835.5 1017.5,3817.5 630.5,3878.5 597.5,3668.5 390.5,3797.5 303.5,3695.5 366.5,3566.5 237.5,3590.5 195.5,3545.5 318.5,3362.5 426.5,3359.5 441.5,3239.5 258.5,3104.5 324.5,2756.5 231.5,2735.5 357.5,2651.5 420.5,2429.5 729.5,2329.5 729.5,2181.5 573.5,2211.5 637.5,2099.5 455.5,1905.5 195.5,1967.5z"
      });

      return Regions;
    }

    public IList<Geography.RegionAdjacency> GetRegionAdjacencies()
    {
      throw new NotImplementedException();
    }

    public IList<UnitType> GetUnitTypes()
    {
      throw new NotImplementedException();
    }

    public Tuple<int, int> GetBackgroundDimensions()
    {
      throw new NotImplementedException();
    }
  }
}
