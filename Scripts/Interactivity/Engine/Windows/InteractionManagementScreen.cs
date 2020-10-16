﻿using System;
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
        TreeViewState viewStateL, viewStateR;
        InteractionTreeView<IInteraction> interactionTreeList;
        InteractionTreeView<IConsequence> interactionTreeListR;
        [MenuItem("GUIllaume/Interaction Management")]
        public static void ShowWindow()
        {
            var window = EditorWindow.GetWindow(typeof(InteractionManagementScreen));
            window.titleContent = new GUIContent("Interaction Management");
        }
        void OnEnable()
        {
            if (viewStateL == null)
            {
                viewStateL = new TreeViewState();
            }
            if (viewStateR == null)
            {
                viewStateR = new TreeViewState();
            }
            interactionTreeList = new InteractionTreeView<IInteraction>(viewStateL);
            interactionTreeListR = new InteractionTreeView<IConsequence>(viewStateR);

        }
        void OnGUI()
        {
            interactionTreeList.OnGUI(new Rect(0, 0, position.width/3, position.height));
            interactionTreeListR.OnGUI(new Rect(position.width *0.6667f, 0, position.width/3, position.height));
            EditorGUIUtility.DrawColorSwatch(new Rect(position.width / 3, 0, position.width * 0.333333f, position.height), new Color(0.25f, 0.22f, 0.23f));
        }
    }
}