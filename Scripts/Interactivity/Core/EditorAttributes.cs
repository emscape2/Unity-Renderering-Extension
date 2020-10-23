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
    public Type Connection;
    public InteractionAttribute(Type c)
    {
        Connection = c;
    }


    public static G getAttribute<G> (Type T)
        where G: InteractionAttribute
    {
        var properties = T.GetProperties();
        G lefInteraction = null;
        foreach (var prop in properties)
        {
            var attr = prop.GetCustomAttributes(typeof(G), true);
            lefInteraction = (G)attr.FirstOrDefault();
        }
        return lefInteraction;
    }
}

public class LeftInteractionAttribute : InteractionAttribute
{
    public LeftInteractionAttribute(Type c) : base(c)
    {

    }
}

public class RightInteractionAttribute : InteractionAttribute
{
    public RightInteractionAttribute(Type c) : base(c)
    {

    }
}
