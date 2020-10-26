using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class ComponentReference
{
    public UnityEngine.Object component
    {
        get => serialized.targetObject; set
        {
            serialized = new SerializedObject(value as UnityEngine.Object);
        }
    }
    public SerializedObject serialized;
    
}

