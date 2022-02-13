using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Vector2Int))]
public class Vector2IntDrawer : PropertyDrawer 
{
    
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        EditorGUI.BeginProperty(position, label, property);
        {
            // Don't make child fields be indented
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Get Property Field
            SerializedProperty prop_x = property.FindPropertyRelative("m_X");
            SerializedProperty prop_y = property.FindPropertyRelative("m_Y");

            // Calculate rects
            Rect vectorRect = new Rect(position.x, position.y, position.width, position.height);

            // Draw Vector3Field
            EditorGUI.BeginChangeCheck();
            Vector2 newValFloat = EditorGUI.Vector2Field(vectorRect, label, new Vector2(prop_x.intValue, prop_y.intValue));
            Vector2Int newValInt = Vector2Int.FloorToInt(newValFloat);
            if (EditorGUI.EndChangeCheck())
            {
                prop_x.intValue = newValInt.x;
                prop_y.intValue = newValInt.y;
            }          
            
            // Set indent back to what it was
            EditorGUI.indentLevel = indent;
        }
        EditorGUI.EndProperty();
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return Screen.width < 333 ? (16f + 18f) : 16f;
	}
}
