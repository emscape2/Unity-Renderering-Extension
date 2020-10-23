using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[AttributeUsage(
   AttributeTargets.Class|
   AttributeTargets.Interface)]

public class InteractionFlow : System.Attribute
{
    public SideOption sideOption;
    public InteractionFlow(SideOption side)
    {
        sideOption = side;
    }
}

public enum SideOption : int
{
    Left,
    Right,
    BothSides
} 



[AttributeUsage(
   AttributeTargets.Field |
   AttributeTargets.Property,
   AllowMultiple = true)]

public class InteractionAttribute : System.Attribute
{
    public IGUIllaume refComponent;
    public IEnumerable<IGUIllaume> refList;
    public InteractionAttribute()
    {
    }
}

public class LeftInteractionAttribute : InteractionAttribute
{
    public LeftInteractionAttribute() : base()
    {

    }
}

public class RightInteractionAttribute : InteractionAttribute
{
    public RightInteractionAttribute() : base()
    {

    }
}
