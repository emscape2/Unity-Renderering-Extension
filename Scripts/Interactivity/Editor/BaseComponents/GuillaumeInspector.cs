using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GuillaumeInspector
{

    public ComponentReference selected;
    
    Component ref1test;
    int ref2test;
    float ref3test;
    public void OnGUI(Rect rect, ComponentReference select)
    {
        if (selected?.serialized != null)
        {
            EditorGUIUtility.DrawColorSwatch(rect, new Color(0.1f,0.1f,0.1f,0.5f));
            DrawProperties(selected.serialized, rect);
        }
    }

    public void DrawProperties(SerializedObject toDraw, Rect rect)
    {
        Rect drawRect = new Rect(rect.position + new Vector2(2,4), new Vector2(rect.width - 4,0));
        EditorGUI.LabelField(new Rect(drawRect.position, new Vector2(drawRect.width, 24)), toDraw.targetObject.name + ": " + toDraw.targetObject.GetType());
        drawRect = new Rect(drawRect.position + new Vector2(0, 32), drawRect.size);
        toDraw.Update();
        var obj = toDraw.GetIterator();
        if (obj.Next(true)) 
        {
            do
            {
                if (obj.propertyPath != "m_ObjectHideFlags")
                {
                    if (obj.propertyType == SerializedPropertyType.Integer)
                    {
                        EditorGUI.LabelField(new Rect(drawRect.position, new Vector2(drawRect.width, 24)), obj.displayName + ":");
                        obj.intValue = EditorGUI.DelayedIntField(new Rect(drawRect.position + new Vector2(0, 24), new Vector2(drawRect.width, 19)), obj.intValue);
                        drawRect = new Rect(drawRect.position + new Vector2(0, 48), drawRect.size);
                    }
                    else if (obj.propertyType == SerializedPropertyType.Float)
                    {
                        EditorGUI.LabelField(new Rect(drawRect.position, new Vector2(drawRect.width, 24)), obj.displayName +  ":");
                        obj.floatValue = EditorGUI.DelayedFloatField(new Rect(drawRect.position + new Vector2(0, 24), new Vector2(drawRect.width, 19)), obj.floatValue);
                        drawRect = new Rect(drawRect.position + new Vector2(0, 48), drawRect.size);
                    }
                    else if (obj.propertyType == SerializedPropertyType.Boolean)
                    {
                        obj.boolValue = EditorGUI.Toggle(new Rect(drawRect.position, new Vector2(32, 24)), obj.boolValue);
                        EditorGUI.LabelField(new Rect(drawRect.position+ new Vector2(24,0), new Vector2(drawRect.width-32,24)), obj.displayName);
                        drawRect = new Rect(drawRect.position + new Vector2(0, 48), drawRect.size);
                    }
                    else if (obj.propertyType == SerializedPropertyType.Color)
                    {
                        EditorGUI.LabelField(new Rect(drawRect.position, new Vector2(drawRect.width, 24)), obj.displayName +  ":");
                        obj.colorValue = EditorGUI.ColorField(new Rect(drawRect.position + new Vector2(0, 24), new Vector2(drawRect.width, 24)), obj.colorValue);
                        drawRect = new Rect(drawRect.position + new Vector2(0, 48), drawRect.size);
                    }
                    else if (obj.propertyType == SerializedPropertyType.Vector3)
                    {
                        obj.vector3Value = EditorGUI.Vector3Field(new Rect(drawRect.position, new Vector2(drawRect.width, 28)), obj.displayName, obj.vector3Value);
                        drawRect = new Rect(drawRect.position + new Vector2(0, 36), drawRect.size);
                    }
                    else
                    {

                    }
                }
                //EditorGUI.ObjectField(new Rect(rect.position, new Vector2(rect.width, 64)), ref1test, typeof(Component));
                //EditorGUI.FloatField(new Rect(rect.position + new Vector2(0, 128), new Vector2(rect.width, 64)), ref3test);
            } while (obj.NextVisible(false));




        }

        toDraw.ApplyModifiedProperties();
    }

}
