using UnityEditor;
using UnityEngine;
using Assets.Scripts.Interactivity.Core;

namespace Assets.Scripts.Interactivity.Engine.Windows
{
    class InteractionManagementScreen : InteractionManagementScreenBase<IInteraction, ActivationReciever>
    {

        
        [MenuItem("GUIllaume/Interaction Management")]
        public static void ShowWindow()
        {
           var window = EditorWindow.GetWindow(typeof(InteractionManagementScreen)); 
            window.titleContent = new GUIContent("KANKER Management");
        }

        
        
    }
} 