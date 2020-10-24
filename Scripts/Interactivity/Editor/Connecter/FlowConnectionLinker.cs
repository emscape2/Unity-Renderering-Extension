using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

class FlowConnectionLinker<T, R>
where T : IGUIllaume
    where R : IGUIllaume
{
    LeftInteractionAttribute leftInteraction;
    RightInteractionAttribute rightInteraction;

    public FlowConnectionLinker()
    {

        var type = typeof(T);
        var typed = typeof(R);
        leftInteraction = InteractionAttribute.getAttribute<LeftInteractionAttribute>(typeof(R));
        rightInteraction = InteractionAttribute.getAttribute<RightInteractionAttribute>(typeof(T));
    }
        
    public void Link(List<IGUIllaume> left, List<IGUIllaume> right)
    {
        var type = typeof(T);
        var typed = typeof(R);
        var typel = typeof(List<R>);
        if (leftInteraction != null && leftInteraction.ConnectionType == typeof(T))
        {
            WrapUp(leftInteraction, left.First(), right.First());
        }
        else if (rightInteraction != null && rightInteraction.ConnectionType == typeof(R))
        {
            WrapUp(rightInteraction, right.First(), left.First());
        }
        else if (leftInteraction != null && leftInteraction.ConnectionType == typeof(List<T>))
        {
            WrapUpList(leftInteraction, right.First(), left);
        }
        else if (rightInteraction != null &&  rightInteraction.ConnectionType == typeof(List<R>))
        {
            WrapUpList(rightInteraction, left.First(), right);
        }
    }

    protected void WrapUp(InteractionAttribute sourceInteraction, IGUIllaume source, object target)
    {
        SerializedObject serializedObject = new SerializedObject(source as UnityEngine.Object);
        sourceInteraction.setTargetRef(source,target );
    }

    protected void WrapUpList(InteractionAttribute sourceInteraction, IGUIllaume source, IEnumerable<IGUIllaume> target)
    {
        SerializedObject serializedObject = new SerializedObject(source as UnityEngine.Object);
        sourceInteraction.setTargetListRef(source, target);
    }



}

