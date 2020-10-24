using UnityEngine;
[InteractionFlow(SideOption.BothSides)]
public class ActivationReciever : MonoBehaviour, IConsequence
    {
        
        public int interactionNumber;
        [SerializeField]
        [RightInteractionAttribute(typeof(IActivationPattern), "activationPattern")]
        public MonoBehaviour activationPattern; 
        [SerializeField]
        [LeftInteraction(typeof(IInteraction), "interaction")]
        public Interaction interaction;
        public bool engaged;
        public void Start()
        {
                if ((IActivationPattern)(activationPattern) == null)
                {
                        Debug.LogError($"Missing activation pattern {activationPattern} in {this.name}, generating empty.");
                        activationPattern = gameObject.AddComponent<DummyActivationPattern>();
                }
        }
        public bool CanEngage()
        {
            return true;
        }

        public void Disengage()
        {
            ((IActivationPattern)activationPattern).Disengage(interactionNumber);
        }

        public void Engage()
        {
            ((IActivationPattern)activationPattern).Engage(interactionNumber);
        }
        
        // Update is called once per frame
        protected virtual void Update()
        {
            //debug masks 
            if (Input.GetKeyDown(KeyCode.U))
            {
                MouseBehavior.InstantiateDrawRect(gameObject);
            }
            if (interaction == null)
                return;

            switch (interaction.TryInteract(gameObject))
            {
                case (true):
                    if (!engaged)
                    {
                        engaged = true;
                        Engage();
                    }
                    break;
                case (false):
                        engaged = false;
                        Disengage();
                       
                    break;

                case (null):
                    {
                        break;
                    }
            }

        }
    }
