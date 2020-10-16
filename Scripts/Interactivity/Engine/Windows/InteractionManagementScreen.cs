using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEditor.IMGUI.Controls;
using Assets.Scripts.Interactivity.Engine.BaseComponents;

namespace Assets.Scripts.Interactivity.Engine.Windows
{
    class InteractionManagementScreen : EditorWindow

    {
        [SerializeField]
        TreeViewState viewState;
        InteractionTreeView interactionTreeList;
        [MenuItem("GUIllaume/Interaction Management")]
        public static void ShowWindow()
        {
            var window = EditorWindow.GetWindow(typeof(InteractionManagementScreen));
            window.titleContent = new GUIContent("Interaction Management");
        }
        void OnEnable()
        {
            if (viewState == null)
            {
                viewState = new TreeViewState();
            }
            interactionTreeList = new InteractionTreeView(viewState);
            
        }
        void OnGUI()
        {
            interactionTreeList.OnGUI(new Rect(0, 0, position.width/5, position.height));
            EditorGUIUtility.DrawColorSwatch(new Rect(position.width / 5, 0, position.width * 0.6f, position.height), new Color(0.25f, 0.22f, 0.23f));
        }
    }
}