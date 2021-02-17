using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.Interactivity.Interactions;
using Assets.Scripts.Interactivity.ActionComponents;

public class AddHoverableToClickableScreen : EditorWindow
{
    private TextField objectName, emissionName, teintName;
    private TagField tagName;
    private UnityEngine.UIElements.Toggle hoverChange, holdChange, addReshapeLogicChange;
    private ColorField unlit, lit, hold;
    private Button button;
    private ObjectField baseObject;

    [MenuItem("GUIllaume/Helpers/AddHoverableToClickableScreen")]
    public static void ShowExample()
    {
        AddHoverableToClickableScreen wnd = GetWindow<AddHoverableToClickableScreen>();
        wnd.titleContent = new GUIContent("AddHoverableToClickableScreen");

    }
    /// <summary>
    /// filters the list of objects to change
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public T[] FilterQuery<T>(IEnumerable<T> list) where T : Component
    {
        var answer = list;
        if (!new string[2] { "", "untagged" }.Contains(tagName?.value ?? ""))
        {
            answer = answer.Where(a => a.gameObject.tag == tagName.value);
        }
        if ((objectName?.value?.Trim() ?? "") != "")
        {
            answer = answer.Where(a => a.gameObject.name.Contains(objectName.value));
        }
        return answer.ToArray();
    }

    public void ExecuteFunctionality()
    {
        Debug.Log("Starting Hoverable Invasion");
        Debug.Log("Searching for Clickables in Scene");



        var text = FindObjectsOfType<SpriteRenderer>();
        text = FilterQuery(text);

        foreach (var spr in text)
        {
            if (spr.GetComponents<Clickable>().Length == 0)
            {
                spr.gameObject.AddComponent<Clickable>();
            }
        }



        var text2 = FindObjectsOfType<TextMesh>();
        text2 = FilterQuery(text2);
        foreach (var spr in text2)
        {
            if (spr.GetComponents<Clickable>().Length == 0)
            {
                spr.gameObject.AddComponent<Clickable>();
            }
        }

        var clickables = FindObjectsOfType<Clickable>();
        clickables = FilterQuery(clickables);
        Debug.Log($"Found {clickables.Length} Clickables in Scene");
        foreach (var clickable in clickables)
        {
            var hoverable = clickable.gameObject.GetComponent<Hoverable>() ?? clickable.gameObject.AddComponent<Hoverable>();
            var holdable = clickable.gameObject.GetComponent<Holdable>() ?? clickable.gameObject.AddComponent<Holdable>();
            var Events = clickable.gameObject.GetComponents<MakeMaterialLookActivatedConsequence>();

            if (hoverChange.value)
            {
                var hoverEvent = Events.Length > 0 ? Events[0] : clickable.gameObject.AddComponent<MakeMaterialLookActivatedConsequence>();
                hoverEvent.Unlit = unlit.value;
                hoverEvent.Lit = lit.value;

                if ((emissionName.value ?? "") != "")
                    hoverEvent.ColorName = emissionName.value;
                //interactionreciever hover
                var interactionReciever = clickable.gameObject.GetComponents<InteractableBehavior>().Where(ic => ic.Consequences.Contains(hoverEvent)).FirstOrDefault()
                    ?? clickable.gameObject.AddComponent<InteractableBehavior>();
                interactionReciever.Consequences = new List<MonoBehaviour>() { hoverEvent };


                var activationReciever = clickable.gameObject.GetComponents<ActivationReciever>().Where(ac => ac.activationPattern == interactionReciever && ac.interaction == hoverable).FirstOrDefault()
                    ?? clickable.gameObject.AddComponent<ActivationReciever>();
                activationReciever.activationPattern = interactionReciever;
                activationReciever.interaction = hoverable;
            }

            if (holdChange.value)
            {
                var holdEvent = Events.Length > 1 ? Events[1] : clickable.gameObject.AddComponent<MakeMaterialLookActivatedConsequence>();
                holdEvent.Unlit = lit.value;
                holdEvent.Lit = hold.value;
                if ((teintName.value ?? "") != "")
                    holdEvent.ColorName = teintName.value;
                //interactionreciever hold
                var interactionRecieverHold = clickable.gameObject.GetComponents<InteractableBehavior>().Where(ic => ic.Consequences.Contains(holdEvent)).FirstOrDefault()
                    ?? clickable.gameObject.AddComponent<InteractableBehavior>();
                interactionRecieverHold.Consequences = new List<MonoBehaviour>() { holdEvent };

                var activationRecieverHold = clickable.gameObject.GetComponents<ActivationReciever>().Where(ac => ac.activationPattern == interactionRecieverHold && ac.interaction == holdable).FirstOrDefault()
                ?? clickable.gameObject.AddComponent<ActivationReciever>();
                activationRecieverHold.activationPattern = interactionRecieverHold;
                activationRecieverHold.interaction = holdable;

            }

        }
        Debug.Log("Done Hoverable Invasion");

        if (addReshapeLogicChange.value)
        {
            Debug.Log("Add Reshape Logic Started");
            var objectsToSchrink = FindObjectsOfType<Transform>();
            var filteredObjects = FilterQuery<Transform>(objectsToSchrink);

            var transformOfBase = baseObject.value as Transform;
            if (transformOfBase.GetComponents<CompareGlobalVarInteraction>().Length == 0)
            {
                transformOfBase.gameObject.AddComponent<CompareGlobalVarInteraction>();
            }
            var CompareGlobalVarInteractions = transformOfBase.GetComponents<CompareGlobalVarInteraction>();//todo: alle interactions in deze list nagaan
            var ActivationRecievers = transformOfBase.GetComponents<ActivationReciever>().Where(ac => ac.interaction == CompareGlobalVarInteractions.FirstOrDefault()).ToList();
            ActivationReciever toEdit;
            if (ActivationRecievers.Count == 0)
            {
                toEdit = transformOfBase.gameObject.AddComponent<ActivationReciever>();
                toEdit.interaction = CompareGlobalVarInteractions.FirstOrDefault();

            }
            else
            {
                toEdit = ActivationRecievers.FirstOrDefault();
            }


            foreach (var transform in filteredObjects)
            {

                var Rescalabiliata = transform.GetComponent<RescaleConsequence>() ?? transform.gameObject.AddComponent<RescaleConsequence>();
                

                InteractableBehavior actipatterno;
                if (toEdit.activationPattern == null)
                     toEdit.activationPattern = toEdit.gameObject.AddComponent<InteractableBehavior>() as InteractableBehavior ;

                actipatterno = (InteractableBehavior)toEdit.activationPattern;
                
                if (actipatterno.consequences == null)
                    actipatterno.consequences = new List<MonoBehaviour>();
                
                actipatterno.consequences.Add(Rescalabiliata);

            }






        }
    }


    public void OnEnable()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        root.style.backgroundColor = new Color(0.125f, 0.10f, 0.1126f);
        unlit = unlit ?? new ColorField("Unlit");
        lit = lit ?? new ColorField("Lit");
        hold = hold ?? new ColorField("Hold");
        teintName = new TextField("Material Teint name (Hold)");
        emissionName = new TextField("Material Emission name (Hover)");

        hoverChange = hoverChange ?? new Toggle("Write Hover");
        holdChange = holdChange ?? new Toggle("Write Hold");
        addReshapeLogicChange = addReshapeLogicChange ?? new Toggle("Write Reshape Logic");

        tagName = tagName ?? new TagField("Tag");
        objectName = new TextField("Object name filter");

        baseObject = new ObjectField("Basis Object: Pas op, niet voeren!");
        baseObject.objectType = typeof(Transform);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Interactivity/Editor/Windows/AddHoverable.uss");
        root.styleSheets.Add(styleSheet);


        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Ыуат ряк, Select your colours.");
        button = new ToolbarButton();
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
        root.Add(teintName);
        root.Add(emissionName);
        root.Add(hoverChange);
        root.Add(holdChange);
        root.Add(tagName);
        root.Add(objectName);


        root.Add(addReshapeLogicChange);
        root.Add(baseObject);
        root.Add(button);


    }
}