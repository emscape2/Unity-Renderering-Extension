using System;
using System.Collections.Generic;
using UnityEngine;

internal class ConnetionLinkDictionary<Key, Value> : Dictionary<Key, Value>
     where Key : Type
        where Value : Component
{
    public Key leftType;
    public Key rightType;
    public List<Value> rightList;
    public new void Add(Key key, Value value)//todo: needs better coding, unoptimal data structure
    {
        if (ContainsKey(key))
        {
            if (key == leftType && rightList != null)
            {
                rightList = new List<Value>();
            }
            
            if (base[key] == value)
            {
                if (key == rightType && rightList != null)
                    rightList.Remove(value);
                base.Remove(key);
            }
            else
            {

                if (base.ContainsKey(rightType))
                {
                    base.Remove(rightType);
                }

                base[key] = value;
            }
        }
        else
        {
            base.Add(key, value);
        }
        if(key == rightType && rightList != null)
        {
            rightList.Add(value);
        }
        if (ContainsKey(leftType) && ContainsKey(rightType))
        {
            var connect = new InteractionConnecter<Value, Value>();
            if (rightList != null)
                connect.Connect(
                    this[leftType],
                    rightList);
            else
                connect.Connect(
                    this[leftType],
                    this[rightType]);
        }
    }



    public ConnetionLinkDictionary(Key left, Key right, bool rList) : base()
    {
        leftType = left;
        rightType = right;
        if (rList)
            rightList = new List<Value>();
        else
            rightList = null;
    }


}
