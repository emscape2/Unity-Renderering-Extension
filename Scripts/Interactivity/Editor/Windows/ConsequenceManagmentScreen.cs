using UnityEditor;
using UnityEngine;
using Assets.Scripts.Interactivity.Core;

namespace Assets.Scripts.Interactivity.Engine.Windows
{
    class ConsequenceManagmentScreen : InteractionManagementScreenBase<IActivationPattern, IConsequence>
    {

        
        [MenuItem("GUIllaume/3. Consequence Management", priority = 3)]
        public static void ShowWindow()
        {
           var window = EditorWindow.GetWindow(typeof(ConsequenceManagmentScreen)); 
            window.titleContent = new GUIContent("Consequence Management");
        }

        protected override void OnGUI()
        {
            colour = new Color(0.24f, 0.16f, 0.22f);
            base.OnGUI();

        }


    }
} 