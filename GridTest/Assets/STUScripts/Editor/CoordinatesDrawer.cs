using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(CellCoords))]
public class CoordinatesDrawer : PropertyDrawer
{

    public override void OnGUI(
        Rect position, SerializedProperty property, GUIContent label
    )
    {
        CellCoords coordinates = new CellCoords(
            property.FindPropertyRelative("x").intValue,
            property.FindPropertyRelative("z").intValue
        );

        position = EditorGUI.PrefixLabel(position, label);
        GUI.Label(position, coordinates.ToString());
    }
}
