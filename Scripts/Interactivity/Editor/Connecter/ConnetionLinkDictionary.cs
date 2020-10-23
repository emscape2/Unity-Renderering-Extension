using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ConnetionLinkDictionary<Key, Value>
     where Key : Type
        where Value : Component
{
    public Key leftType;
    public Key rightType;
    public Dictionary<Key, bool> lists;
    public Dictionary<Key, IList<Value>> dictionary;



    public IEnumerable<Value> this[Key key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Add(Key key, Value value)//todo: needs better coding, unoptimal data structure
    {
        if (!lists.ContainsKey(key)|| !lists[key])
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);
                return;
            }
        }
        if (!dictionary.ContainsKey(key))
        {
            dictionary[key] = new List<Value>();
        }
            if (dictionary[key].Contains(value))
            dictionary[key].Add(value);
            else
            dictionary[key].Add(value);

        
        /*
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
        }*/
    }

    public bool ContainsKey(Key key)
    {
        throw new NotImplementedException();
    }

    public bool Remove(Key key)
    {
        throw new NotImplementedException();
    }



    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(KeyValuePair<Key, Value> item)
    {
        throw new NotImplementedException();
    }



    public ConnetionLinkDictionary(Key left, Key right, Dictionary<Key,bool> ListTypes) 
    {
        dictionary = new Dictionary<Key, IList<Value>>();
        lists = ListTypes;
        leftType = left;
        rightType = right;
    }


}
