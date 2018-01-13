using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Level Editor/Level", order = 1)]
[System.Serializable]
public class BaseLevelObject : ScriptableObject
{
    [SerializeField]
    public string levelName;

    [SerializeField]
    public int numToCheck = 42;

    [SerializeField]
    public int numRows = 10;
    [SerializeField]
    public int numColumns = 10;
    [SerializeField]
    public char[] LevelData;
    //public char[,] LevelData;

    BaseLevelObject()
    {
        GetLevelData();
    }

    public char[] GetLevelData()
    {
        if (LevelData == null)
        {
            // Initialise the walls so that I don't have to keep creating them myself
            char[] initData = new char[numRows * numColumns];
            for(int i = 0; i < numRows; i++)
            {
                for(int j = 0; j < numColumns; j++)
                {
                    if ((i == 0 || i == (numRows - 1)) || (j == 0 || j == (numColumns - 1)))
                    {
                        // Set outer edges to be walls
                        initData[(j * numColumns) + i] = '#';
                    }
                    else
                    {
                        // Set all other tiles to be floors
                        initData[(j * numColumns) + i] = ' ';
                    }
                }
            }

            // Set LevelData to the initialised wall data
            LevelData = initData;
        }
        return LevelData;
    }
}
