using Assets.Scripts.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactivity.Process
{
    class ActivationPatternOneEnabled : MonoBehaviour, IActivationPattern
    {
        [SerializeField]
        public List<MonoBehaviour> consequences; //IConsequence
        [SerializeField]

        public int enablyat;//Actually a boolean but hey Unity doesnt understand lists of booleans
        public bool isDisengageSensitive, isDisengageEveryone;


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
                else
                {
                    consequences[i].Invoke("Disengage", 0);//try if implemements members anyways();
                }
            }
        }

        void IActivationPattern.Engage(int i)
        {
            if (!isDisengageSensitive)
                consequences[i].Invoke("Disengage", 0);//try if implemements members anyways();
            if (enablyat >=  0 &&  i != enablyat)
            {
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
