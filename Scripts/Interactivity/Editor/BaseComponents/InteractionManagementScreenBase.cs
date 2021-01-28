using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEditor.IMGUI.Controls;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEditor.UIElements;
using System.Management.Instrumentation;
using UnityEditor.UI;
using System.Reflection;

using UnityEditor.Rendering;
using UnityEditor.Graphs;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

class InteractionManagementScreenBase<T, R> : EditorWindow
    where T : IGUIllaume
    where R : IGUIllaume
{
    [SerializeField]
    TreeViewState viewStateL, viewStateR;
    InteractionTreeView<T> interactionTreeList;
    InteractionTreeView<R> interactionTreeListR;
    public GuillaumeInspector inspector;
    public ComponentReference selected;
    protected Color colour = new Color(0.25f, 0.22f, 0.23f);
    protected bool oneToMany;   //todo: find relation attribute and check 1:n || 1:1 trough that
    protected ConnetionLinkDictionary<Type, Component> linktionary ;

    protected virtual void OnEnable()
    {
        if (viewStateL == null)
        {
            viewStateL = new TreeViewState();
        }
        if (viewStateR == null)
        {
            viewStateR = new TreeViewState();
        }
        //todo
        var dic = new Dictionary<Type, bool>();
        if (oneToMany)
            dic.Add(typeof(R), true);

        linktionary = new ConnetionLinkDictionary<Type, Component>(typeof(T), typeof(R), dic);
        selected = new ComponentReference();
        inspector = new GuillaumeInspector() { selected = selected };
        interactionTreeList = new InteractionTreeView<T>(inspector, viewStateL, false, ref linktionary);
        interactionTreeListR = new InteractionTreeView<R>(inspector, viewStateR, true, ref linktionary);

    }


protected virtual void OnGUI()
    {
        var style = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).button;
        style.margin = new RectOffset(2,2,1,2);
        style.border.left = 3;
        style.border.right = 3;
        style.border.bottom = 2;
        style.border.top = 1;
        interactionTreeList.OnGUI(new Rect(0, 32, position.width / 3, position.height-10));
        interactionTreeListR.OnGUI(new Rect(position.width * 0.6667f, 32, position.width / 3, position.height));
        EditorGUIUtility.DrawColorSwatch(new Rect(position.width / 3, 32, position.width * 0.333333f, position.height), colour);
        switch (GUILayout.Toolbar(-1, new GUIContent[]
            {
                        EditorGUIUtility.TrTextContent("<=", "Und davos kanker af"),
                        new GUIContent("Cancer", "Und davos kanker bij"),
                        new GUIContent(RowDataHelpers<R>.RowIcon(typeof(IConsequence)),"krijg kanker"),
                        GUIContent.none,
                        new GUIContent("AddComponent", "Und davos kanker ee"),
                        new GUIContent("Couple", "Und davos kanker weer"),
                        EditorGUIUtility.TrTextContent("=>", "Und davos kanker af"),



                }, new bool[] {false,true,true,false,false,true,false },
                style,
                new GUILayoutOption[]{GUILayout.Height(30), GUILayout.ExpandHeight(false)  }))
        {
            case 1:
                OnEnable();
                break;
            case 5:
                linktionary.Link<T,R>();
                break;
            default:
                //nothing
                break;
        }
        inspector.OnGUI(new Rect(12 + position.width / 3,44, -24 + position.width / 3, -56 + position.height),selected);
        /* if (GUILayout.Button(new GUIContent("Cancer", "Und davos kanker bij"),GUI.skin.button))
         {
             OnEnable();
         }*/
    }
}
