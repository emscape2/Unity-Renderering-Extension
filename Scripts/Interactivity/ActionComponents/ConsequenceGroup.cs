using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[InteractionFlow(SideOption.Left)]
public class ConsequenceGroup : MonoBehaviour, IConsequence
{
    [SerializeField]
    [LeftInteraction(typeof(IEnumerable<ConsequenceGroup>), "consequences")]
    public List<MonoBehaviour> consequences; //IConsequence
    void Start()
    {

        foreach (var consequence in consequences)
        {
            if ((IConsequence)(consequence) == null)
            {
                try
                {
                    consequence.Invoke("CanEngage", 0);//try if implemements members anyways
                }
                catch
                {
                    Debug.LogError($"Invalid consequence {consequence?.name} in {this?.name}");
                }
            }
        }
    }
    public virtual void Engage()
    {
        foreach (IConsequence con in consequences)
        {
            con.Engage();
        }
    }
    public virtual void Disengage()
    {
        foreach (IConsequence con in consequences)
        {
            con.Disengage();
        }
    }
    public virtual bool CanEngage()
    {
        return true;

    }

}
