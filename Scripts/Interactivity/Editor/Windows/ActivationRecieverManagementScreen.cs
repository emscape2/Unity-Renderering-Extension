using UnityEditor;
using UnityEngine;

    class ActivationRecieverManagementScreen : InteractionManagementScreenBase<ActivationReciever, IActivationPattern>
    {


        [MenuItem("GUIllaume/2. Activation Reciever Management", priority = 2)]
        public static void ShowWindow()
        {
            var window = EditorWindow.GetWindow( typeof(ActivationRecieverManagementScreen));
            window.titleContent = new GUIContent("Activation Management");
        }
        protected override void OnGUI()
        {
            colour = new Color(0.15f, 0.22f, 0.23f);
            base.OnGUI();
        }

    }
