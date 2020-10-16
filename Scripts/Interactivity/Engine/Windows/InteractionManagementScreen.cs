using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Interactivity.Engine.Windows
{
    class InteractionManagementScreen : EditorWindow

    {
        [MenuItem("GUIllaume/InteractionManagement")]

        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(InteractionManagementScreen));
        }

        void OnGUI()
        {
            // The actual window code goes here
        }
    }
}