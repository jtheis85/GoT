using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoT
{
  public static class Combat
  {
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>>
    /// TODO: Convert DetermineVictor to use a MaxBy extension method.
    /// TODO: Error checking of DetermineVictor
    /// 
    /// http://stackoverflow.com/questions/1101841/linq-how-to-perform-max-on-a-property-of-all-objects-in-a-collection-and-ret
    /// </remarks>
    /// <param name="armies"></param>
    /// <returns></returns>
    public static Army DetermineVictor(params Army[] armies)
    {
      var sortedArmies = armies.OrderByDescending(a => a.Strength);
      return sortedArmies.Count(s => s.Strength == armies.Max(a => a.Strength)) > 1 ? null : sortedArmies.First();
    }
  }
}
