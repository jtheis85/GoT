using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoT.Geography
{
  /// <summary>
  /// Encapsulates the concept of map region adjacency.
  /// </summary>
  public class RegionAdjacency
  {
    public int FirstRegion { get; set; }
    public int SecondRegion { get; set; }
    public AdjacencyType AdjacencyType { get; set; }

    /// <summary>
    /// Compares two RegionAdjacencies for equality, including reversing the 
    /// adjacency order.
    /// </summary>
    /// <param name="adjacency">A RegionAdjacency object to compare to</param>
    /// <returns>Whether they are equal</returns>
    public override bool Equals(object adjacency)
    {
      // Don't want to fail in this case, as Equals may be used for validation.
      // TODO: Check type for adjacency equals override
      if (adjacency == null)
      {
        return false;
      }
      // Want to fail "noisily" in this case as something is definitely wrong.
      else if (!(adjacency is RegionAdjacency))
      {
        throw new ArgumentException("Unable to compare equality with non-RegionAdjacency parameter.");
      }
      else
      {
        return ((RegionAdjacency)adjacency).AdjacencyType == AdjacencyType &&
               (FirstRegion == ((RegionAdjacency)adjacency).FirstRegion && SecondRegion == ((RegionAdjacency)adjacency).SecondRegion ||
                FirstRegion == ((RegionAdjacency)adjacency).SecondRegion && SecondRegion == ((RegionAdjacency)adjacency).FirstRegion);
      }
    }
  }

  /// <summary>
  /// The type of adjacency between two regions
  /// </summary>
  public enum AdjacencyType
  {
    /// The default adjacency behavior between regions
    /// <summary>
    /// <para>The default adjacency behavior between regions</para>
    /// <para>-</para>
    /// <para>Note: This value is automatically used if no value is supplied</para>
    /// </summary>
    Default,
    /// Separates two land areas with an impassable barrier
    /// <summary>
    /// Separates two land areas with an impassable barrier
    /// </summary>
    River,
    /// Connects two land areas over a river
    /// <summary>
    /// <para>Connects two land areas over a river</para>
    /// <para>-</para>
    /// <para>Note that this is identical to a default AdjacencyType for all </para>
    /// <para>mechanical purposes in the base game. However, it may be used for </para>
    /// <para>graphical purposes, or for modifications (for instance, destroying or </para>
    /// <para>creating bridges).</para>
    /// </summary>
    Bridge
  }
}
