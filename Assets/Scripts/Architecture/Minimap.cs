using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] private GameObject go_minimap_room;
    [SerializeField] private Transform transform_minimap_player;
    [SerializeField] private Transform canvas_minimap;
    public List<GameObject> rooms;

    public void MinimapAddRoom(int x, int y)
    {
        GameObject new_room = Instantiate(go_minimap_room, canvas_minimap);
        new_room.transform.localPosition = new Vector2(x * 100, y * 55);
        rooms.Add(new_room);
    }

    public void MinimapSetPlayerPosition(Vector2Int position)
    {
        transform_minimap_player.localPosition = new Vector2(position.x * 100, position.y * 55);
    }

    public void ClearMinimap()
    {
        foreach(GameObject go in rooms)
        {
            Destroy(go);
        }
        rooms.Clear();
    }
}
