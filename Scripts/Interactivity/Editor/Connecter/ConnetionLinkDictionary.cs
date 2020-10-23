using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

internal class ConnetionLinkDictionary<Key, Value>
     where Key : Type
        where Value : Component
{
    public Key leftType;
    public Key rightType;
    public Dictionary<Key, bool> lists;
    public Dictionary<Key, IList<Value>> dictionary;



    public IEnumerable<Value> this[Key key]
    {
        get => dictionary[key];
        set  {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, null);
            dictionary[key] = value.ToList();
        } 
    }

    public void Link<T,R>()
        where T: IGUIllaume
        where R: IGUIllaume
    {
        throw new NotImplementedException("CAST TROUBLES");
        throw new NotImplementedException("CAST TROUBLES");
        var LeftList = dictionary[leftType].ToList()as List<T>;
        var RightList = dictionary[rightType].ToList() as List<R>;
        var interactionFlowLinker = new FlowConnectionLinker<T,R>();
        interactionFlowLinker.Link(LeftList, RightList);
    }
    public void Add(Key key, Value value)//todo: needs better coding, unoptimal data structure
    {
        if (!lists.ContainsKey(key)|| !lists[key])
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);
                
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
        return dictionary.ContainsKey(key);
    }

    public bool Remove(Key key)
    {
        return dictionary.Remove(key);
    }



    public void Clear()
    {
        dictionary = new Dictionary<Key, IList<Value>>();
    }

    public bool Contains(KeyValuePair<Key, Value> item)
    {
        return dictionary[item.Key].Contains(item.Value);
    }
    public bool Contains<K>( Value item)
        where K : Key
    {
        var type = typeof(K);
        return dictionary[(Key)type].Contains(item);
    }



    public ConnetionLinkDictionary(Key left, Key right, Dictionary<Key,bool> ListTypes) 
    {
        dictionary = new Dictionary<Key, IList<Value>>();
        lists = ListTypes;
        leftType = left;
        rightType = right;
    }


}
