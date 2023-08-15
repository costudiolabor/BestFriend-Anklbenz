
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(UIColorToggle))]

public class UIColorToggleEditor : UnityEditor.UI.ToggleEditor {
	private SerializedProperty 
			_highlighted,
			_firstGraphics,
			_secondGraphics,
			_thirdGraphics,
			_activeFirst,
			_inactiveFirst,
			_activeSecond,
			_inactiveSecond,
			_activeThird,
			_inactiveThird;

	protected override void OnEnable() {
		base.OnEnable();
		_firstGraphics = serializedObject.FindProperty("firstGraphics");
		_secondGraphics = serializedObject.FindProperty("secondGraphics");
		_thirdGraphics = serializedObject.FindProperty("thirdGraphics");
		
		_activeFirst = serializedObject.FindProperty("activeFirst");
		_inactiveFirst = serializedObject.FindProperty("inactiveFirst");
		
		_activeSecond = serializedObject.FindProperty("activeSecond");
		_inactiveSecond = serializedObject.FindProperty("inactiveSecond");
		
		_activeThird = serializedObject.FindProperty("activeThird");
		_inactiveThird = serializedObject.FindProperty("inactiveThird");
		
	
	}

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
		GUILayout.Label("Is Active/Inactive color settings:");

		DrawUILine(Color.black);

		EditorGUILayout.PropertyField(_firstGraphics, true);
		EditorGUILayout.PropertyField(_activeFirst);
		EditorGUILayout.PropertyField(_inactiveFirst);

		DrawUILine(Color.black);

		EditorGUILayout.PropertyField(_secondGraphics, true);
		EditorGUILayout.PropertyField(_activeSecond);
		EditorGUILayout.PropertyField(_inactiveSecond);

		DrawUILine(Color.black);
		
		EditorGUILayout.PropertyField(_thirdGraphics, true);
		EditorGUILayout.PropertyField(_activeThird);
		EditorGUILayout.PropertyField(_inactiveThird);

		DrawUILine(Color.black);

		serializedObject.ApplyModifiedProperties();
	}

	private static void DrawUILine(Color color, int thickness = 1, int padding = 10) {
		Rect rect = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
		rect.height = thickness;
		rect.y += padding / 2;
		rect.x -= 2;
		rect.width += 6;
		EditorGUI.DrawRect(rect, color);
	}
}
