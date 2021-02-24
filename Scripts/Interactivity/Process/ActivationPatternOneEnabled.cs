using System.Collections.Generic;
using UnityEngine;

class ActivationPatternOneEnabled : MonoBehaviour, IActivationPattern
{
    [SerializeField]
    public List<MonoBehaviour> consequences; //IConsequence
    [SerializeField]

    public int enablyat;//Actually a boolean but hey Unity doesnt understand lists of booleans
    public int enablyat2;//Actually a boolean but hey Unity doesnt understand lists of booleans
    public bool isDisengageSensitive, isDisengageEveryone;

    public List<MonoBehaviour> Consequences { get => consequences; set => consequences = value; }

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

    void IActivationPattern.Disengage(int i)
    {
        if (isDisengageSensitive)
        {
            if (isDisengageEveryone)
            {
                for (int j = 0; j < consequences.Count; j++)
                {
                    var consequence = consequences[j];

                    consequence.Invoke("Disengage", 0);//try if implemements members anyways();
                }
            }
            else if (i >= 0)
            {
                consequences[i].Invoke("Disengage", 0);//try if implemements members anyways();
                if (enablyat == i && enablyat2 != 1)
                {
                    enablyat2 = 1;
                }
                else if (enablyat == i)
                {
                    enablyat2 = 0;
                    enablyat = -1;
                }
            }
            else if (enablyat >= 0)
            {
                consequences[enablyat].Invoke("Disengage", 0);//try if implemements members anyways();

                enablyat2 = 0;
                enablyat = -1;
            }

        }
    }

    void IActivationPattern.Engage(int i)
    {
        if (!isDisengageSensitive && i >= 0)
        {
            consequences[i].Invoke("Disengage", 0);//try if implemements members anyways();

        }
        if (isDisengageSensitive && enablyat > -1 && enablyat != i && enablyat2 == 1)
        {
            consequences[enablyat].Invoke("Engage", 0);//try if implemements members anyways();
            consequences[enablyat].Invoke("Disengage", 0);//try if implemements members anyways();
            enablyat = -1;
            enablyat2 = 0;
        }

        if (enablyat >= -1)
        {
            if (i != enablyat)
            {
                enablyat = i;
                consequences[i].Invoke("Engage", 0);//try if implemements members anyways();
            }
            if (i == enablyat)
            {
                enablyat = i;
                if (enablyat >= 0)
                {
                    consequences[i].Invoke("Engage", 0);//try if implemements members anyways
                }
            }
        }
    }
}
