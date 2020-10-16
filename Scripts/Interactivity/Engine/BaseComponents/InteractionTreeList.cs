using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

namespace Assets.Scripts.Interactivity.Engine.BaseComponents
{

    class InteractionTreeView : TreeView
    {
        public InteractionTreeView(TreeViewState state) : base(state)
        {
            Reload();
        }


        protected override TreeViewItem BuildRoot()
        {
            
            var root = new TreeViewItem { id = InteractionList.currentId, depth = -1, displayName = "Root" };
            var firstItem = new InteractionList(root,
                SceneManager.GetActiveScene().GetRootGameObjects().Select(go=>go.transform),
                InteractionList.currentId, 0, "Scene");
            root.AddChild( firstItem);
            return root;
        }
    }

    class InteractionListLine : TreeViewItem
    {
        bool _special;
        Transform _transform;
        public InteractionListLine(Transform transform, bool special, int id, int depth, string displayName) : base(id, depth, displayName)
        {
            _transform = transform;
            _special = special;
            if (_special)
            {
            }
        }
        public void onGUI()
        {
        }

        public override Texture2D icon { get => base.icon; set => base.icon = value; }

    }

    class InteractionList : TreeViewItem
    {

        private static int _curid;
        public static int currentId { get { return _curid++; } }

        public InteractionList(TreeViewItem rootItem , IEnumerable<Transform> root, int id, int depth, string displayName) : base(id, depth, displayName)
        {
            var SceneRoot = root;

            var children = AddChildData(this, depth+1, SceneRoot);

            foreach (var child in children)
            {
                AddChild(child);
            }
        }

        private List<TreeViewItem> AddChildData(TreeViewItem current, int depth, IEnumerable<Transform> SceneRoot)
        {
            List<TreeViewItem> answer = new List<TreeViewItem>();
            foreach (var obj in SceneRoot)
            {
                var children = obj.GetComponentsInChildren<Transform>().Where(child=> child.gameObject != obj.gameObject).ToList();
                IInteraction[] Interactions = obj.GetComponents<IInteraction>();
                IInteraction interact = Interactions.FirstOrDefault();
                TreeViewItem curData = null;
                if (interact != null)
                {
                    curData = new InteractionListLine(obj,true,currentId, depth + 1, "[I]" + interact.Name);
                    
                }

                var childData = AddChildData(current, depth + 1,children);
                if (childData.Any())
                {
                    if (curData == null)
                    {
                        curData = new InteractionListLine(obj,false,currentId, depth + 1, obj.name);
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
