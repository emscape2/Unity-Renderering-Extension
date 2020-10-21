using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.SceneManagement;


class InteractionTreeLinks
    {
        public static ConnetionLinkDictionary<Type, Component> links;

    }

    class InteractionTreeView<T> : TreeView
        where T : IGUIllaume
    {
        public InteractionTreeView(TreeViewState state, bool right) : base(state)
        {
            if (InteractionTreeLinks.links == null)
            {

                InteractionTreeLinks.links = new ConnetionLinkDictionary<Type, Component>(typeof(Component), typeof(Component));
            }
                Reload();
        }

        internal static Texture texIcon;
        protected override TreeViewItem BuildRoot()
            
        {
            texIcon = AssetPreview.GetMiniTypeThumbnail(typeof(Flare)); 
            var iconType = typeof(T) == typeof(IInteraction) ? typeof(Animator) : typeof(CanvasRenderer);
            var root = new TreeViewItem { id = InteractionList<T>.currentId, depth = -1, displayName = "Root" };
            var firstItem = new InteractionList<T>(root,
                SceneManager.GetActiveScene().GetRootGameObjects().Select(go => go.transform),
                InteractionList<T>.currentId, 0, typeof(T).ToString(), iconType);
            root.AddChild( firstItem);
            return root;
        }

        protected override void SingleClickedItem(int id)
        {
            if (id == 1)
                return;
            var guirow = FindRows(new List<int> () {id} );
            var fi = guirow.FirstOrDefault() as InteractionListLine<T>;
            var el = fi.GetComponent() as Component;
            if (el !=null )
                UnityEditor.Selection.SetActiveObjectWithContext(el.gameObject,el.gameObject.transform);
        }
        protected override  void RowGUI(RowGUIArgs rowGUIArgs)
        {
            base.RowGUI(rowGUIArgs);
            bool isactive = InteractionTreeLinks.links.ContainsKey(typeof(T)) && InteractionTreeLinks.links[typeof(T)] != null && InteractionTreeLinks.links[typeof(T)].GetInstanceID() == (rowGUIArgs.item as InteractionListLine<T>).unitID;
            if ( isactive || this.IsSelected(rowGUIArgs.item.id))
            {
                var rekt = rowGUIArgs.rowRect;
                var newRekt = new Rect(rekt.x, rekt.y, rekt.width * 0.1f, rekt.height);
                //GUILayout.BeginArea(newRekt);
                var scope = new GUILayout.AreaScope(newRekt);
                if ((rowGUIArgs.item as InteractionListLine<T>)._special)
                {
                    if (isactive)
                    {

                       GUILayout.Box(new GUIContent(texIcon, "Selected"), EditorStyles.miniLabel);
                    }
                    else
                    {
                        if (GUILayout.Button(new GUIContent(texIcon, "Start Linking"), EditorStyles.miniButton))
                        {
                            InteractionTreeLinks.links.Add(typeof(T), (rowGUIArgs.item as InteractionListLine<T>).GetComponent() as Component);

                        }
                    }
                }
                GUILayout.EndArea();
            }
        }

    }

    class InteractionListLine<T> : TreeViewItem
        where T : IGUIllaume
    {
        
        Transform _transform;
        internal int unitID;
        internal bool _special;
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
            _special = special;
        }
        public T GetComponent()
        {
            var target = _transform.GetComponents<T>().Where(c => (c as Component).GetInstanceID() == unitID).ToList();
            return target.FirstOrDefault();
        }


        public override Texture2D icon { get => base.icon; set => base.icon = value; }

        

    }

    class InteractionList<T> : InteractionListLine<T>
        where T: IGUIllaume
    {
        //alternatieve zoekmethode met 
        //Editor.FindObjectsOfType

        private static int _curid;
        public static int currentId { get { return _curid++; } }

        public InteractionList(TreeViewItem rootItem , IEnumerable<Transform> root, int id, int depth, string displayName, Type icon) : base(root.FirstOrDefault(),root.FirstOrDefault().GetInstanceID(),false,id, depth, displayName)
           
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
