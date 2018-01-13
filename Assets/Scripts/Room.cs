using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector2 RoomPosition;

    public int RoomTileWidth;

    public int RoomTileHeight;

	public void CreateRoom(GameObject Grass, GameObject Dirt, float CellWidth, float CellHeight)
    {
        for (int i = 0; i < RoomTileWidth; i++)
        {
            for (int j = 0; j < RoomTileHeight; j++)
            {
                GameObject tile;
                if ((i == 0 || i == RoomTileWidth - 1) || (j == 0 || j == RoomTileHeight - 1))
                {
                    tile = GameObject.Instantiate<GameObject>(Grass);
                }
                else
                {
                    tile = GameObject.Instantiate<GameObject>(Dirt);
                    //tile = GameObject.Instantiate<GameObject>((Random.Range(0, 2) == 0) ? Grass : Dirt);
                }
                tile.transform.SetParent(gameObject.transform);
                tile.name = "(" + i + ", " + j + ")";
                tile.transform.position = new Vector3(RoomPosition.x + (i * CellWidth), RoomPosition.y + (j * CellHeight), 0);
            }
        }
    }

    public void SetRoomSprite(float xIndex, float yIndex, GameObject prefab)
    {
        GameObject tile = GameObject.Instantiate<GameObject>(prefab);
        tile.transform.SetParent(gameObject.transform);
        tile.name = "(" + xIndex + ", " + yIndex + ")";
        tile.transform.position = new Vector3(RoomPosition.x + (xIndex * 1.28f), RoomPosition.y - (yIndex * 1.28f), 0);
    }
}
