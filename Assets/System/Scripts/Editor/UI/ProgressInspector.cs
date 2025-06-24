﻿using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Ballance2.UI.Core.Controls.Progress), true)]
class ProgressInspector : Editor
{
    private Ballance2.UI.Core.Controls.Progress myScript;

    private SerializedProperty pValue;
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        myScript = (Ballance2.UI.Core.Controls.Progress)target;

        EditorGUI.BeginChangeCheck();

        float val = EditorGUILayout.Slider(new GUIContent("Value", pValue.tooltip), pValue.floatValue, 0, 1);
        if (val != pValue.floatValue)
            pValue.floatValue = val;

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            myScript.UpdateVal();
        }

        base.OnInspectorGUI();
    }
    private void OnEnable()
    {
        pValue = serializedObject.FindProperty("val");
    }
    private void OnDisable()
    {
    }
}

