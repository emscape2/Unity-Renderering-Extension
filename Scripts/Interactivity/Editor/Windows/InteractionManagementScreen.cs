using UnityEditor;
using UnityEngine;
using Assets.Scripts.Interactivity.Core;

namespace Assets.Scripts.Interactivity.Engine.Windows
{
    class InteractionManagementScreen : InteractionManagementScreenBase<IInteraction, ActivationReciever>
    {

        
        [MenuItem("GUIllaume/1. Interaction Management", priority = 1)]
        public static void ShowWindow()
        {
           var window = EditorWindow.GetWindow(typeof(InteractionManagementScreen)); 
            window.titleContent = new GUIContent("Interaction Management");
        }

        
        
    }
} 