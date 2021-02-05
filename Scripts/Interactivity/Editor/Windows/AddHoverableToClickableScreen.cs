using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;

public class AddHoverableToClickableScreen : EditorWindow
{
    private ColorField unlit, lit, hold;
    private Button button;
    [MenuItem("GUIllaume/Helpers/AddHoverableToClickableScreen")]
    public static void ShowExample()
    {
        AddHoverableToClickableScreen wnd = GetWindow<AddHoverableToClickableScreen>();
        wnd.titleContent = new GUIContent("AddHoverableToClickableScreen");
        
    }

    public void ExecuteFunctionality()
    {
        Debug.Log("Starting Hoverable Invasion");
        Debug.Log("Searching for Clickables in Scene");

        var text = FindObjectsOfType<SpriteRenderer>();
        foreach (var spr in text)
        {
            if (spr.GetComponents<Clickable>().Length == 0)
            {
                spr.gameObject.AddComponent<Clickable>();
            }
        }

        var text2 = FindObjectsOfType<TextMesh>();
        foreach (var spr in text2)
        {
            if (spr.GetComponents<Clickable>().Length == 0)
            {
                spr.gameObject.AddComponent<Clickable>();
            }
        }

        var clickables = FindObjectsOfType<Clickable>();
        Debug.Log($"Found {clickables.Length} Clickables in Scene");
        foreach (var clickable in clickables)
        {
            var hoverable = clickable.gameObject.GetComponent<Hoverable>() ?? clickable.gameObject.AddComponent<Hoverable>();
            var holdable = clickable.gameObject.GetComponent<Holdable>() ?? clickable.gameObject.AddComponent<Holdable>();
            var Events = clickable.gameObject.GetComponents<MakeMaterialLookActivatedConsequence>();

            var hoverEvent = Events.Length > 0 ? Events[0] : clickable.gameObject.AddComponent<MakeMaterialLookActivatedConsequence>();
            hoverEvent.Unlit = unlit.value;
            hoverEvent.Lit = lit.value;

            var holdEvent = Events.Length > 1 ? Events[1] : clickable.gameObject.AddComponent<MakeMaterialLookActivatedConsequence>();
            holdEvent.Unlit = lit.value;
            holdEvent.Lit = hold.value;

            //interactionreciever hover
            var interactionReciever = clickable.gameObject.GetComponents<InteractableBehavior>().Where(ic => ic.Consequences.Contains(hoverEvent)).FirstOrDefault()
                ?? clickable.gameObject.AddComponent<InteractableBehavior>();
            interactionReciever.Consequences = new List<MonoBehaviour>() { hoverEvent };
            
            //interactionreciever hold
            var interactionRecieverHold = clickable.gameObject.GetComponents<InteractableBehavior>().Where(ic => ic.Consequences.Contains(holdEvent)).FirstOrDefault()
                ?? clickable.gameObject.AddComponent<InteractableBehavior>();
            interactionRecieverHold.Consequences = new List<MonoBehaviour>() { holdEvent };

            var activationReciever = clickable.gameObject.GetComponents<ActivationReciever>().Where(ac => ac.activationPattern == interactionReciever && ac.interaction == hoverable).FirstOrDefault()
                ?? clickable.gameObject.AddComponent<ActivationReciever>();
            activationReciever.activationPattern = interactionReciever;
            activationReciever.interaction = hoverable;

            var activationRecieverHold = clickable.gameObject.GetComponents<ActivationReciever>().Where(ac => ac.activationPattern == interactionRecieverHold && ac.interaction == holdable).FirstOrDefault()
            ?? clickable.gameObject.AddComponent<ActivationReciever>();
            activationRecieverHold.activationPattern = interactionRecieverHold;
            activationRecieverHold.interaction = holdable;


        }
        Debug.Log("Done Hoverable Invasion");


    }


    public void OnEnable()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        root.style.backgroundColor = new Color(0.125f, 0.10f, 0.1126f);
        unlit = unlit ?? new ColorField("Unlit");
        lit = lit ?? new ColorField("Lit");
        hold = hold ?? new ColorField("Hold");

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Interactivity/Editor/Windows/AddHoverable.uss");
        root.styleSheets.Add(styleSheet);


        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Ыуат ряк, Select your colours.");
        button = new ToolbarButton() ;
        var buttonRect = new Box();
        button.Add(buttonRect);
        button.Add(new Label() { text = "GO" });
        button.clickable.clicked += () => ExecuteFunctionality();
        
        buttonRect.style.backgroundColor = new Color(0.14f, 0.13f, 0.12f, 0.95f);
        buttonRect.style.opacity = 0.25f;
        button.style.color = new Color(0.04f, 0.07f, 0.43f);

        root.Add(label);
        root.Add(unlit);
        root.Add(lit);
        root.Add(hold);
        root.Add(button);

 
    }
}