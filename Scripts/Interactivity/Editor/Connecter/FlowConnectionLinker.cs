using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class FlowConnectionLinker<T, R>
where T : IGUIllaume
    where R : IGUIllaume
{
    LeftInteractionAttribute leftInteraction;
    RightInteractionAttribute rightInteraction;

    public FlowConnectionLinker()
    {
        leftInteraction = InteractionAttribute.getAttribute<LeftInteractionAttribute>(typeof(T));
        rightInteraction = InteractionAttribute.getAttribute<RightInteractionAttribute>(typeof(R));
    }
        
    public void Link(IEnumerable<T> left, IEnumerable<R> right)
    {
        if (leftInteraction?.ConnectionType == typeof(R))
        {
            WrapUp(leftInteraction, left.First(), right.First());
        }
        else if (rightInteraction?.ConnectionType == typeof(T))
        {
            WrapUp(rightInteraction, right.First(), left.First());
        }
        else if (leftInteraction?.ConnectionType == typeof(IEnumerable<R>))
        {
            WrapUp(leftInteraction, left.First(), right);
        }
        else if (rightInteraction?.ConnectionType == typeof(IEnumerable<T>))
        {
            WrapUp(rightInteraction, right.First(), left);
        }
    }

    protected void WrapUp(InteractionAttribute sourceInteraction, IGUIllaume source, object target)
    {
        sourceInteraction.setTargetRef(source, target);
    }



}

