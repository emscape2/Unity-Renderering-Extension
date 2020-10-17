using Assets.Scripts.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Interactivity.Engine.Connecter
{
    class InteractionConnecter<T, R>
        where T: Component
        where R: Component
    {
        public InteractionConnecter()
        {
        }

        public  void Connect(T source, R target)
        {
            if((source as Interaction )!= null && (target as ActivationReciever )!= null)
                Connect(source as Interaction, target as ActivationReciever);
        }

        public  void Connect(Interaction source, ActivationReciever target)
        {
            SerializedObject serializedObject = new SerializedObject(target as UnityEngine.Object);
            target.interaction = source;
        }
        
        
    }
}
