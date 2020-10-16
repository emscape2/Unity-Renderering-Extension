using Assets.Scripts.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactivity.Process
{
    class ActivationPatternTimedEnabled : MonoBehaviour, IActivationPattern
    {
        [SerializeField]
        public MonoBehaviour consequence; //IConsequence
        [SerializeField]

        public int enablyat;//Actually a boolean but hey Unity doesnt understand lists of booleans
        public bool counting;
        public float timer;
        public bool enableDisengage;

        void Start()
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
        void Update()
        {
            if (counting)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    counting = false;
                    consequence.Invoke("Engage", 0);
                }
            }
        }
        void IActivationPattern.Disengage(int i)
        {
            if (enableDisengage)
            {
                consequence.Invoke("Disengage", 0);//try if implemements members anyways();
                counting = false;
            }
        }

        void IActivationPattern.Engage(int i)
        {
            if (!enableDisengage)
            {
                if (timer < i)
                {
                    if (counting)
                        timer = i;
                    counting = false;
                }
            }
            else
            {
                if (!counting || i < timer)
                {
                    counting = true;
                }
            }
        }
    }
}
