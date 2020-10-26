using System.Collections.Generic;
using UnityEngine;
[InteractionFlow(SideOption.BothSides)]
public class InteractableBehavior : MonoBehaviour, IConsequence, IActivationPattern
{
    [SerializeField]
    [LeftInteraction(typeof(List<IConsequence>), "consequences")]
    public List<MonoBehaviour> consequences; //IConsequence
    [SerializeField]
    bool engaged;

    public List<MonoBehaviour> Consequences { get => consequences; set => consequences=value; }

    // Start is called before the first frame update
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
                    Debug.LogError($"Invalid consequence {consequence.name} in {this.name}");
                }
            }
        }
    }
    public void Engage()
    {
        foreach (var consequence in consequences)
        {
            (consequence as IConsequence).Engage();
        }
    }

    public void Disengage()
    {
        foreach (var consequence in consequences)
        {
            (consequence as IConsequence).Disengage();
        }
    }

    public bool CanEngage()
    {
        return true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        /*//debug masks 
        if (Input.GetKeyDown(KeyCode.U))
        {
            MouseBehavior.InstantiateDrawRect(gameObject);
        }
        if (interaction == null)
            return;
        switch (interaction.TryInteract(gameObject))
        {
            case (true):
                engaged = true;
                Engage();
                break;
            case (false):
                engaged = false;
                Disengage();
                break;
            case (null):
                break;
        }*/
    }

    public void Engage(int i)
    {
       Engage();
    }

    public void Disengage(int i)
    {
        Disengage();
    }
}

