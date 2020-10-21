using UnityEditor;
using UnityEngine;

    class ConsequenceGroupOrderingScreen : InteractionManagementScreenBase<ConsequenceGroup, IConsequence>
    {

        
        [MenuItem("GUIllaume/4. Consequence Group Management", priority = 4)]
        public static void ShowWindow()
        {
           var window = EditorWindow.GetWindow(typeof(ConsequenceGroupOrderingScreen)); 
            window.titleContent = new GUIContent("Consequence Grouping");
        }

        protected override void OnGUI()
        {
            colour = new Color(0.22f, 0.2f, 0.2f);
            base.OnGUI();

        }


    }
