using System.Collections.Generic;
using UnityEngine;

class ActivationPatternTimedEnabled : MonoBehaviour, IActivationPattern
    {
        [SerializeField]
        public List<MonoBehaviour> consequence; //IConsequence
        [SerializeField]

        public int enablyat;//Actually a boolean but hey Unity doesnt understand lists of booleans
        public bool counting;
        public float timer;
        public bool enableDisengage;
        public List<MonoBehaviour> Consequences { get => consequence; set => consequence = value; }


        void Start()
        {
            foreach (var c in consequence)
            {
                if ((IConsequence)(c) == null)
                {
                    try
                    {
                        c.Invoke("CanEngage", 0);//try if implemements members anyways
                    }
                    catch
                    {
                        Debug.LogError($"Invalid consequence {c.name} in {this.name}");
                    }
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
                    consequence.ForEach(c => c.Invoke("Engage", 0));
                }
            }
        }
        void IActivationPattern.Disengage(int i)
        {
            if (enableDisengage)
            {
                consequence.ForEach(c => c.Invoke("Disengage", 0));
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
