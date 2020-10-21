using System;
using System.Collections.Generic;
using UnityEngine;

internal class ConnetionLinkDictionary<Key, Value> : Dictionary<Key,Value>
     where Key : Type
        where Value : Component
    {
        public Key leftType,rightType;
        
        public new void Add(Key key, Value value)
        {
            if(ContainsKey(key))
            {
                base[key] = value;
            }
            else
            {
                base.Add(key, value);
            }

            if (ContainsKey(leftType) && ContainsKey(rightType))
            {
                var connect = new InteractionConnecter<Value, Value> ();
                    
                    connect.Connect(this[leftType], this[rightType]);
            }
        }

        public ConnetionLinkDictionary(Key left, Key right)  : base()
        {
            leftType = left;
            rightType = right;
        }

        
    }
