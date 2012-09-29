using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoT
{
  public class Army
  {
    public int Strength { get; private set; }

    public void Add(UnitType unit, int count = 1)
    {
      Strength+= unit.Strength * count;
    }
  }
}
