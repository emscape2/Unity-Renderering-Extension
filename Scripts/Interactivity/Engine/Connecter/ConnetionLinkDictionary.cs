using Assets.Scripts.Interactivity.Core;
using Assets.Scripts.Interactivity.Engine.BaseComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Interactivity.Engine.Connecter
{
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
}
