using Assets.Scripts.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.IMGUI.Controls;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

namespace Assets.Scripts.Interactivity.Engine.BaseComponents
{

    class InteractionTreeView<T> : TreeView
        where T : IGUIllaume
    {
        public InteractionTreeView(TreeViewState state) : base(state)
        {
            Reload();
        }


        protected override TreeViewItem BuildRoot()
            
        {
            var iconType = typeof(T) == typeof(IInteraction) ? typeof(Animator) : typeof(CanvasRenderer);
            var root = new TreeViewItem { id = InteractionList<T>.currentId, depth = -1, displayName = "Root" };
            var firstItem = new InteractionList<T>(root,
                SceneManager.GetActiveScene().GetRootGameObjects().Select(go => go.transform),
                InteractionList<T>.currentId, 0, typeof(T).ToString(), iconType);
            {
                
            };

            root.AddChild( firstItem);
            return root;
        }
        protected  void SelectionClick(UnityEditor.IMGUI.Controls.TreeViewItem item, bool keepMultiSelection)
        {
            //base.SelectionClick(item, keepMultiSelection);
            //InspectorElement elem = new InspectorElement((item as InteractionListLine<T>).GetComponent() as Component);

        }

        protected override void SingleClickedItem(int id)
        {
            var guirow = FindRows(new List<int> () {id} );
            var fi = guirow.FirstOrDefault() as InteractionListLine<T>;
            var el = (fi.GetComponent() as Component);
            UnityEditor.Selection.SetActiveObjectWithContext(el.gameObject,el.gameObject.transform.parent);
        }
        
    }

    class InteractionListLine<T> : TreeViewItem
    {
        
        Transform _transform;
        int unitID;
        public InteractionListLine(Transform transform,int unityId, bool special,  int id, int depth, string displayName) : base(id, depth, displayName)
        {
            _transform = transform;
            var typetje = typeof(T);
            if (typeof(T) == typeof(IInteraction))
                typetje = typeof(Animator);

            else if (typeof(T) == typeof(IConsequence))
                typetje = typeof(CanvasRenderer);
            if (special)
                icon = (AssetPreview.GetMiniTypeThumbnail(typetje));//EditorGUIUtility.IconContent("")
            unitID = unityId;
        }
        public T GetComponent()
        {
            var target = _transform.GetComponents<T>().Where(c => (c as Component).GetInstanceID() == unitID).ToList();
            return target.FirstOrDefault();
        }


        public override Texture2D icon { get => base.icon; set => base.icon = value; }

    }

    class InteractionList<T> : TreeViewItem
        where T: IGUIllaume
    {
        //alternatieve zoekmethode met 
        //Editor.FindObjectsOfType

        private static int _curid;
        public static int currentId { get { return _curid++; } }

        public InteractionList(TreeViewItem rootItem , IEnumerable<Transform> root, int id, int depth, string displayName, Type icon) : base(id, depth, displayName)
           
        {
            var SceneRoot = root;

            var children = AddChildData<T>(this, depth+1, SceneRoot);

            foreach (var child in children)
            {
                AddChild(child);
            }
        }

        private List<TreeViewItem> AddChildData<T>(TreeViewItem current, int depth, IEnumerable<Transform> SceneRoot)
            where T :  IGUIllaume
        {
            List<TreeViewItem> answer = new List<TreeViewItem>();
            foreach (var obj in SceneRoot)
            {
                var children = obj.GetComponentsInChildren<Transform>().Where(child=> child.gameObject != obj.gameObject).ToList();
                T[] Interactions = obj.GetComponents<T>();
                TreeViewItem curData = null;
                if (Interactions.Any())
                {
                    foreach (var interact in Interactions)
                    {
                        if (curData != null)
                        {
                            answer.Add(curData);
                        }
                        curData = new InteractionListLine<T>(obj,(interact as Component)?.GetInstanceID()??0,true,currentId, depth + 1, "[I]" +interact.GetType()+ " " + (interact as UnityEngine.Component)?.name);

                    }
                    
                }

                var childData = AddChildData<T>(current, depth + 1,children);
                if (childData.Any())
                {
                    if (curData == null)
                    {
                        curData = new InteractionListLine<T>(obj,0,false,currentId, depth + 1, obj.name);
                    }
                    foreach (var childat in childData)
                    {
                        curData.AddChild(childat);
                    }
                }
                if (curData != null)
                    answer.Add(curData);

            }
            return answer;
        }

        

    }
}
