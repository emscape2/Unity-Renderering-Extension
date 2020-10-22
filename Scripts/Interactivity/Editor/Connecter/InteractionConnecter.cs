using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

class InteractionConnecter<T, R>
    where T : Component
    where R : Component
{
    public InteractionConnecter()
    {
    }

    public void Connect(T source, R target)
    {
        if ((source as Interaction) != null && (target as ActivationReciever) != null)
            Connect(source as Interaction, target as ActivationReciever);
        if ((source as ActivationReciever) != null && (target as IActivationPattern) != null)
            Connect(source as ActivationReciever, target as IActivationPattern);
    }

    public void Connect(T source, IEnumerable<R> target)
    {

    }
    /// <summary>
    /// Interaction Management
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public void Connect(Interaction source, ActivationReciever target)
    {
        SerializedObject serializedObject = new SerializedObject(target as UnityEngine.Object);
        target.interaction = source;
    }
    /// <summary>
    /// Activation Reciever Management
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public void Connect(ActivationReciever source, IActivationPattern target)
    {
        SerializedObject serializedObject = new SerializedObject(source as UnityEngine.Object);
        source.activationPattern = target as MonoBehaviour;
    }


}
