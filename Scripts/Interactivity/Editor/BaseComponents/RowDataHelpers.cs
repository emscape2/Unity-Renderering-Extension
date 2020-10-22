using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class RowDataHelpers<T> where T : IGUIllaume
{
    public static Texture2D RowIcon(Component component)
    {
        var typetje = component.GetType();
        return RowIcon(typetje);
    }

    public static Texture2D RowIcon(Type typetje)
    {
        var answer = AssetPreview.GetMiniTypeThumbnail(typetje);
        if (answer != null)
            return answer;

        if (typetje == typeof(ActivationReciever))
            typetje = typeof(EventTrigger);
        else if (typetje == typeof(IInteraction))
            typetje = typeof(Animator);
        else if (typetje == typeof(InteractableBehavior))
            typetje = typeof(Animation);
        else if (typetje == typeof(IActivationPattern))
            typetje = typeof(Pose);
        else if (typetje == typeof(IConsequence))
            typetje = typeof(CanvasRenderer);
        else
            typetje = typeof(LensFlare);
        answer = AssetPreview.GetMiniTypeThumbnail(typetje);
        if (answer != null)
            return answer;

        return (AssetPreview.GetMiniTypeThumbnail(typeof(T)));//EditorGUIUtility.IconContent("")
    }
}

