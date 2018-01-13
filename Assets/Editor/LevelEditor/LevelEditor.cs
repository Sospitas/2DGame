using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public enum TileTypes
{
    Dirt = 0,
    Grass = 1,
    Hole = 2
}

public class LevelEditor : EditorWindow
{
    [SerializeField]
    private BaseLevelObject source;

    static List<string> choices = new List<string>();

    BaseLevelObject selectedLevel = null;

    int selectedX = -1, selectedY = -1;
    Vector2 tileScrollPos;

    private void Awake()
    {
        selectedX = selectedY = -1;
    }

    // Add to the Window Menu
    [MenuItem("Window/Level Editor")]
    static void Init()
    {
        string[] files = Directory.GetFiles("Assets/Resources/ScriptableLevels");
        foreach (string file in files)
        {
            if(AssetDatabase.LoadAssetAtPath<BaseLevelObject>(file) != null)
            {
                choices.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        LevelEditor levelEditor = (LevelEditor)EditorWindow.GetWindow(typeof(LevelEditor));
        levelEditor.Show();
    }


    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Label("Level To Edit");
        source = EditorGUILayout.ObjectField(source, typeof(BaseLevelObject), true) as BaseLevelObject;
        GUILayout.Space(20);
        if (GUILayout.Button("Load Level Data", GUILayout.Height(50), GUILayout.Width(150)))
        {
            Debug.Log(source.name);
            LoadLevelData(source.name);
        }
        GUILayout.Space(50);

        if (selectedLevel == null)
        {
            GUILayout.EndVertical();
            return;
        }
        GUILayout.Label("Level Editing");

        if (selectedLevel.LevelData == null)
            return;
        if (selectedLevel.numRows > 0 && selectedLevel.numColumns > 0)
        {
            for (int i = 0; i < selectedLevel.numRows; i++)
            {
                GUILayout.BeginHorizontal();
                for (int j = 0; j < selectedLevel.numColumns; j++)
                {
                    if (GUILayout.Button(selectedLevel.LevelData[(j * selectedLevel.numColumns) + i].ToString(), GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
                    {
                        selectedX = i;
                        selectedY = j;
                    }
                }
                GUILayout.EndHorizontal();
            }
        }

        GUILayout.Space(20);
        GUILayout.Label("Selected Tile Value");
        tileScrollPos = EditorGUILayout.BeginScrollView(tileScrollPos, false, false, GUILayout.Height(200.0f));

        if(GUILayout.Button("Wall", GUILayout.Height(25.0f), GUILayout.Width(100.0f)))
        {
            //selectedLevel.LevelData.SetValue('#', (selectedY * selectedLevel.numColumns) + selectedX);
            selectedLevel.LevelData[(selectedY * selectedLevel.numColumns) + selectedX] = '#';
        }
        else if(GUILayout.Button("Floor", GUILayout.Height(25.0f), GUILayout.Width(100.0f)))
        {
            //selectedLevel.LevelData.SetValue(' ', (selectedY * selectedLevel.numColumns) + selectedX);
            selectedLevel.LevelData[(selectedY * selectedLevel.numColumns) + selectedX] = ' ';
        }
        else if(GUILayout.Button("Hole", GUILayout.Height(25.0f), GUILayout.Width(100.0f)))
        {
            //selectedLevel.LevelData.SetValue('x', (selectedY * selectedLevel.numColumns) + selectedX);
            selectedLevel.LevelData[(selectedY * selectedLevel.numColumns) + selectedX] = 'x';
        }
        EditorGUILayout.EndScrollView();
        GUILayout.Space(20);

        if (GUILayout.Button("Save Level Data", GUILayout.Height(50), GUILayout.Width(150), GUILayout.Height(100)))
        {
            SaveLevelData();
        }

        GUILayout.EndVertical();

    }

    private void Update()
    {
        Repaint();
    }

    private void LoadLevelData(string filename)
    {
        selectedX = -1;
        selectedY = -1;

        if (filename == string.Empty)
            return;

        string assetPath = "Assets/Resources/ScriptableLevels/" + filename + ".asset";

        selectedLevel = AssetDatabase.LoadMainAssetAtPath(assetPath) as BaseLevelObject;
    }

    private void SaveLevelData()
    {
        EditorUtility.SetDirty(selectedLevel);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private int GetTileSelectedIndex(int i, int j)
    {
        char selected = selectedLevel.GetLevelData()[(j * selectedLevel.numColumns) + i];
        int returnIdx = 0;
        switch(selected)
        {
            case ' ':
                returnIdx = 0;
                break;
            case '#':
                returnIdx = 1;
                break;
            case 'x':
                returnIdx = 2;
                break;
            default:
                break;
        }

        return returnIdx;
    }
}
