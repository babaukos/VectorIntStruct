using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Vector3Int))]
public class Vector3IntDrawer : PropertyDrawer 
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
            SerializedProperty prop_z = property.FindPropertyRelative("m_Z");

            // Calculate rects
            Rect vectorRect = new Rect(position.x, position.y, position.width, position.height);

            // Draw Vector3Field
            EditorGUI.BeginChangeCheck();
            Vector3 newValFloat = EditorGUI.Vector3Field(vectorRect, label, new Vector3(prop_x.intValue, prop_y.intValue, prop_z.intValue));
            Vector3Int newValInt = Vector3Int.FloorToInt(newValFloat);
            if (EditorGUI.EndChangeCheck())
            {
                prop_x.intValue = newValInt.x;
                prop_y.intValue = newValInt.y;
                prop_z.intValue = newValInt.z;
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
