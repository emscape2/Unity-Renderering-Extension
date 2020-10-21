using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEditor.IMGUI.Controls;

    class InteractionManagementScreenBase<T,R> : EditorWindow
        where T : IGUIllaume
        where R : IGUIllaume
    {
        [SerializeField]
        TreeViewState viewStateL, viewStateR;
        InteractionTreeView<T> interactionTreeList;
        InteractionTreeView<R> interactionTreeListR;
        protected Color colour = new Color(0.25f, 0.22f, 0.23f);
        /*
        [MenuItem("GUIllaume/Interaction Management")]
        public static void ShowWindow()
        {
            var window = EditorWindow.GetWindow(typeof(InteractionManagementScreen));
            window.titleContent = new GUIContent("Interaction Management");
        }*/

        protected void OnEnable()
        {
            if (viewStateL == null)
            {
                viewStateL = new TreeViewState();
            }
            if (viewStateR == null)
            {
                viewStateR = new TreeViewState();
            }


            InteractionTreeLinks.links = new ConnetionLinkDictionary<Type, Component>(typeof(T), typeof(R));
            interactionTreeList = new InteractionTreeView<T>(viewStateL,false);
            interactionTreeListR = new InteractionTreeView<R>(viewStateR,true);

        }
        protected virtual void OnGUI()
        {
            interactionTreeList.OnGUI(new Rect(0, 0, position.width/3, position.height));
            interactionTreeListR.OnGUI(new Rect(position.width *0.6667f, 0, position.width/3, position.height));
            EditorGUIUtility.DrawColorSwatch(new Rect(position.width / 3, 0, position.width * 0.333333f, position.height), colour);
        }
    }
