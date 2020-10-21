using UnityEditor;
using UnityEngine;

    class InteractionManagementScreen : InteractionManagementScreenBase<IInteraction, ActivationReciever>
    {

        
        [MenuItem("GUIllaume/1. Interaction Management", priority = 1)]
        public static void ShowWindow()
        {
           var window = EditorWindow.GetWindow(typeof(InteractionManagementScreen)); 
            window.titleContent = new GUIContent("Interaction Management");
        }


        protected override void OnGUI()
        {
            colour = new Color(0.23f, 0.12f, 0.14f);
            base.OnGUI();

        }

    }
