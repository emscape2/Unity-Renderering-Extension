using UnityEditor;
using UnityEngine;
using Assets.Scripts.Interactivity.Core;

namespace Assets.Scripts.Interactivity.Engine.Windows
{
    class ActivationRecieverManagementScreen : InteractionManagementScreenBase<ActivationReciever, IActivationPattern>
    {


        [MenuItem("GUIllaume/Activation Reciever Management")]
        public static void ShowWindow()
        {
            var window = EditorWindow.GetWindow( typeof(ActivationRecieverManagementScreen));
            window.titleContent = new GUIContent("Activation Management");
        } 


    }
}