using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        
    public void Link(T left, R right)
    {
        if (leftInteraction.Connection == typeof(R))
        {
            WrapUp(leftInteraction, left, right);
        }
        else if (rightInteraction.Connection == typeof(T))
        {
            WrapUp(rightInteraction, right, left);
        }
    }

    public void WrapUp(InteractionAttribute sourceInteraction, IGUIllaume source, IGUIllaume target)
    {
        krijgkanker ghehehe nu zit je met een probleem
    }
    public void WrapUp(InteractionAttribute sourceInteraction, IGUIllaume source, IEnumerable<IGUIllaume> target)
    {

    }



}

