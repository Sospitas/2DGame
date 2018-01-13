using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestEditor : EditorWindow
{
    [SerializeField]
    private TestScriptableObject _testSO;

    [MenuItem("Window/TestEditor")]
    static void Init()
    {
        TestEditor testEditor = (TestEditor)EditorWindow.GetWindow(typeof(TestEditor));
        testEditor.Show();
    }

    public TestScriptableObject GetTest()
    {
        if (_testSO == null)
            _testSO = ScriptableObject.CreateInstance<TestScriptableObject>();

        return _testSO;
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        if(GUILayout.Button("Load Test", GUILayout.Height(50), GUILayout.Width(200)))
        {
            string assetPath = "Assets/Resources/Test.asset";
            _testSO = AssetDatabase.LoadMainAssetAtPath(assetPath) as TestScriptableObject;

            Debug.Log(_testSO.GetInt());
            Debug.Log(_testSO.GetFloat());
            Debug.Log(_testSO.GetString());
        }

        if (GUILayout.Button("Save Test", GUILayout.Height(50), GUILayout.Width(200)))
        {
            EditorUtility.SetDirty(_testSO);
            AssetDatabase.SaveAssets();
        }

        if (_testSO == null)
            return;

        _testSO.SetInt(EditorGUILayout.IntField(_testSO.GetInt(), GUILayout.Width(200)));
        _testSO.SetFloat(EditorGUILayout.FloatField(_testSO.GetFloat(), GUILayout.Width(200)));
        _testSO.SetString(EditorGUILayout.TextField(_testSO.GetString(), GUILayout.Width(200)));
        GUILayout.EndVertical();
    }
}
