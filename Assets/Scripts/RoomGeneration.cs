using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class RoomGeneration : MonoBehaviour
{
    public char[] TileKeys;
    public GameObject[] TileValues;
    private Dictionary<char, GameObject> TileMappings;

    public GameObject PlayerPrefab;

    private Vector2 RoomStartPos = new Vector2(0, 0);

    [SerializeField]
    private GameObject GrassPrefab;

    [SerializeField]
    private GameObject DirtPrefab;

    [SerializeField]
    private GameObject RoomPrefab;

    [SerializeField]
    private float CellWidth = 1.28f;

    [SerializeField]
    private float CellHeight = 1.28f;

    [SerializeField]
    private int RoomsGenerationAttempts = 9;

	// Use this for initialization
	void Start ()
    {
        if (TileKeys.Length != TileValues.Length)
        {
            Debug.LogError("TileKeys Length != TileValues Length! Please set up the GenerationManager correctly");
        }

        foreach (GameObject go in TileValues)
        {
            if (go == null)
            {
                Debug.LogError("Not all TileValues are set. Please set up the GenerationManager correctly");
            }
        }

        // Set up the mappings for generation
        TileMappings = new Dictionary<char, GameObject>();
        for (int i = 0; i < TileKeys.Length; i++)
        {
            TileMappings.Add(TileKeys[i], TileValues[i]);
        }

        //GenerateRooms();

        string path = "Assets/Resources/ScriptableLevels/Chris.asset";
        GenerateRoomFromFile(path);
	}

    void GenerateRooms()
    {
        Random.InitState((int)Time.time);

        for (int i = 0; i < RoomsGenerationAttempts; i++)
        {
            Room room = Instantiate<GameObject>(RoomPrefab).GetComponent<Room>();
            room.transform.parent = this.transform;

            //room.RoomTileWidth = 10;
            //room.RoomTileHeight = 5;

            int xIndex = i % 3;
            int yIndex = i / 3;

            RoomStartPos = new Vector2(xIndex * ((room.RoomTileWidth * CellWidth) + CellWidth * 3), yIndex * ((room.RoomTileHeight * CellHeight) + CellHeight * 3));

            float PosX = RoomStartPos.x;
            float PosY = RoomStartPos.y;

            room.RoomPosition = new Vector2(PosX, PosY);
            room.CreateRoom(GrassPrefab, DirtPrefab, CellWidth, CellHeight);
        }

        GameObject player = Instantiate<GameObject>(PlayerPrefab);
        player.transform.position = new Vector3(CellWidth * 4, CellHeight * 2, 0);
    }

    void GenerateRoomFromFile(string path)
    {
        Room room = Instantiate<GameObject>(RoomPrefab).GetComponent<Room>();
        room.transform.parent = this.transform;

        BaseLevelObject Level = UnityEditor.AssetDatabase.LoadMainAssetAtPath(path) as BaseLevelObject;
        char[] currentLevelData = Level.GetLevelData();
        int rowCount = Level.numRows;

        RoomStartPos = new Vector2(0, CellHeight * rowCount);
        room.RoomPosition = new Vector2(RoomStartPos.x, RoomStartPos.y);

        for (int i = 0; i < currentLevelData.Length; i++)
        {
            int x = i / rowCount;
            int y = i % rowCount;
            char currentChar = currentLevelData[i];
            if (TileMappings.ContainsKey(currentChar))
                room.SetRoomSprite(x, y, TileMappings[currentChar]);
        }

        GameObject player = Instantiate<GameObject>(PlayerPrefab);
        player.transform.position = new Vector3(CellWidth * 2, CellHeight * 2, 0);
    }
}
