using UnityEngine;
using UnityEditor;
public class FindMissingScriptsRecursively : EditorWindow
{
    static int go_count = 0, components_count = 0, missing_count = 0;

    [MenuItem("GUIllaume/FindMissingScriptsRecursively")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(FindMissingScriptsRecursively));
    }

    public void OnGUI()
    {
        if (GUILayout.Button("Find Missing Scripts in selected GameObjects"))
        {
            FindInSelected();
        }
    }
    private static void FindInSelected()
    {
        GameObject[] go = Selection.gameObjects;
        go_count = 0;
        components_count = 0;
        missing_count = 0;
        foreach (GameObject g in go)
        {
            FindInGO(g);
        }
        Debug.Log(string.Format("Searched {0} GameObjects, {1} components, found {2} missing", go_count, components_count, missing_count));
    }

    private static void FindInGO(GameObject g)
    {
        go_count++;
        var serializedObject = new SerializedObject(g);
        Component[] components =  g.GetComponents<Component>();
                    var prop = serializedObject.FindProperty("m_Component");
        for (int i = 0; i < components.Length; i++)
        {
            
                    // Create a serialized object so that we can edit the component list
                    // Find the component list property

                    // Track how many components we've removed
                    int r = 0;

                    // Iterate over all components
                    
                        // Check if the ref is null
                        if (components[i] == null)
                        {
                // If so, remove from the serialized component array
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(g);
                
                missing_count++;
                            // Increment removed count
                            r++;
                        }
                    

                    Debug.Log(g.name + " has an empty script attached in position: " + i, g);
                    
            
        }

        // Apply our changes to the game object
        serializedObject.ApplyModifiedProperties();
        // Now recurse through each child GO (if there are any):
        foreach (Transform childT in g.transform)
        {
            //Debug.Log("Searching " + childT.name  + " " );
            FindInGO(childT.gameObject);
        }
    }
}