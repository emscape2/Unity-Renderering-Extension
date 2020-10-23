using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.MemoryProfiler;
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
   AllowMultiple = true,
    Inherited =true)]

public class InteractionAttribute : System.Attribute
{
    public Type ConnectionType;
    public string Name;
    public InteractionAttribute(Type c, string name)
    {
        ConnectionType = c;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourceObject"></param>
    /// <returns></returns>
    public void setTargetRef(IGUIllaume sourceObject, object Target)
    {
        SetPropertyValue(sourceObject, Name,Target);
       
             
    }

    public static void SetPropertyValue(object obj, string propName, object value) 
    { 
        obj.GetType().GetProperty(propName)?.SetValue(obj,value);
       
    }


    public static G getAttribute<G> (Type T)
        where G: InteractionAttribute
    {
        System.Reflection.MemberInfo[] properties = T.GetProperties();//T.GetMembers( System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.SetField | System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.SetProperty);
        System.Reflection.FieldInfo[] fields = T.GetFields();
        var info = properties.Union(fields);
        G Interaction = null;
        foreach (var prop in info)
        {
            var attr = prop.GetCustomAttributes(typeof(G), true);
            Interaction = (G)attr.FirstOrDefault();
        }
        
        return Interaction;
    }

    
}

public class LeftInteractionAttribute : InteractionAttribute
{
    public LeftInteractionAttribute(Type c, string name) : base( c,name)
    {

    }
}

public class RightInteractionAttribute : InteractionAttribute
{
    public RightInteractionAttribute(Type c, string name) : base( c,name)
    {

    }
}
