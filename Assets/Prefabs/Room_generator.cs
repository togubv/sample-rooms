using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room_generator : MonoBehaviour
{
    private int _count_rooms;
    private int room_width = 12, room_height = 8;
    public int count_rooms { get { return _count_rooms; } }
    [SerializeField] private Transform go_rooms;
    [SerializeField] private Room[] prefab_room;
    private Room[,] _spawned_rooms;
    private int size_middle;
    private Room[] _done_rooms;
    public Room[] done_rooms 
    { 
        get { return _done_rooms; } 
    }

    public Room[,] spawned_rooms
    {
        get { return _spawned_rooms; }
    }

    [SerializeField] private string[] sprites_pack;

    public void GenerateLevel(int count)
    {
        _count_rooms = count;
        _spawned_rooms = new Room[count_rooms, count_rooms];
        _done_rooms = new Room[count_rooms];
        size_middle = (int)Mathf.Floor(count_rooms / 2);

        AddNewRooms();
        SetNeighbours();
        SetStartRoom();
        SetBossRoom();

        SetRoomSpritePack();
    }

    public void ClearLevel()
    {
        for (int i = 0; i < _count_rooms; i++)
        {
            for (int j = 0; j < _count_rooms; j++)
            {
                _spawned_rooms[i, j] = null;
            }

            if (_done_rooms[i] != null)
            {
                Destroy(_done_rooms[i].gameObject);
                _done_rooms[i] = null;
            }
        }
    }

    private void AddNewRooms()
    {
        if (_done_rooms[0] == null)
        {
            Room first_room = Instantiate(prefab_room[Random.Range(0, prefab_room.Length)], go_rooms);
            Vector2Int started_position = new Vector2Int(size_middle, size_middle);
            first_room.transform.position = new Vector2((started_position.x - size_middle) * room_width, (started_position.y - size_middle) * room_height);
            spawned_rooms[started_position.x, started_position.y] = first_room;
            _done_rooms[0] = first_room;
        }

        int x = size_middle;
        int y = size_middle;
        
        for (int i = 1; i < spawned_rooms.GetLength(0); i++)
        {
            int max_x = spawned_rooms.GetLength(0) - 2;
            int max_y = spawned_rooms.GetLength(1) - 2;

            HashSet<Vector2Int> vacant_places = new HashSet<Vector2Int>();
            if (x > 1 && spawned_rooms[x - 1, y] == null) vacant_places.Add(new Vector2Int(x - 1, y));
            if (y > 1 && spawned_rooms[x, y - 1] == null) vacant_places.Add(new Vector2Int(x, y - 1));
            if (x < max_x && spawned_rooms[x + 1, y] == null) vacant_places.Add(new Vector2Int(x + 1, y));
            if (y < max_y && spawned_rooms[x, y + 1] == null) vacant_places.Add(new Vector2Int(x, y + 1));

            Vector2Int position = vacant_places.ElementAt(Random.Range(0, vacant_places.Count));
            Vector2 new_pos = new Vector2((position.x - size_middle) * 12, (position.y - size_middle) * 8);
            Room new_room = Instantiate(prefab_room[Random.Range(0, prefab_room.Length)], new_pos, Quaternion.identity, go_rooms);
            _done_rooms[i] = new_room;
            x = position.x;
            y = position.y;
            spawned_rooms[x, y] = new_room;
        }
    }

    private void SetNeighbours()
    {
        for (int x = 0; x < spawned_rooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawned_rooms.GetLength(1); y++)
            {
                if (spawned_rooms[x, y] == null) continue;

                int max_x = spawned_rooms.GetLength(0) - 2;
                int max_y = spawned_rooms.GetLength(1) - 2;

                if (x > 1 && spawned_rooms[x - 1, y] != null) spawned_rooms[x, y].neighbour_left = spawned_rooms[x - 1, y];
                if (y > 1 && spawned_rooms[x, y - 1] != null) spawned_rooms[x, y].neighbour_down = spawned_rooms[x, y - 1];
                if (x < max_x && spawned_rooms[x + 1, y] != null) spawned_rooms[x, y].neighbour_right = spawned_rooms[x + 1, y];
                if (y < max_y && spawned_rooms[x, y + 1] != null) spawned_rooms[x, y].neighbour_up = spawned_rooms[x, y + 1];
                spawned_rooms[x, y].coordinate = new Vector2Int(x, y);
            }
        }

        for (int i = 0; i < _done_rooms.Length; i++)
        {
            _done_rooms[i].SetDoors();
        }
    }

    void SetStartRoom()
    {
        List<Room> ListMaxDoorRooms = GetMaxDoorsInRoom();
        Room start_room = ListMaxDoorRooms[Random.Range(0, ListMaxDoorRooms.Count)];
        start_room.SetRoomType(RoomType.Start);
        start_room.wasUsed = true;
    }

    void SetBossRoom()
    {
        List<Room> ListMinDoorsInRooms = GetMinDoorsInRoom();
        Room boss_room = ListMinDoorsInRooms[Random.Range(0, ListMinDoorsInRooms.Count)];
        boss_room.SetRoomType(RoomType.Boss);
    }

    void SetRoomSpritePack()
    {
        for (int i = 0; i < _done_rooms.Length; i++)
        {
            if (_done_rooms[i].room_type == RoomType.Boss) done_rooms[i].ChangeSpritePack(sprites_pack[1]);
            else _done_rooms[i].ChangeSpritePack(sprites_pack[0]);
        }
    }

    List<Room> GetMaxDoorsInRoom()
    {
        int max = 0;
        List<Room> max_doors_rooms = new List<Room>();
        for (int i = 0; i < count_rooms; i++)
        {
            if (_done_rooms[i].neighbours_count > max) max = _done_rooms[i].neighbours_count;
        }

        for (int i = 0; i < count_rooms; i++)
        {
            if (_done_rooms[i].neighbours_count == max) max_doors_rooms.Add(_done_rooms[i]);
        }
        return max_doors_rooms;
    }

    List<Room> GetMinDoorsInRoom()
    {
        int  min = 4;
        List<Room> min_doors_rooms = new List<Room>();
        for (int i = 0; i < _done_rooms.Length; i++)
        {
            if (_done_rooms[i].neighbours_count < min) min = _done_rooms[i].neighbours_count;
        }

        for (int i = 0; i < _done_rooms.Length; i++)
        {
            if (_done_rooms[i].neighbours_count == min) min_doors_rooms.Add(_done_rooms[i]);
        }
        return min_doors_rooms;
    }    
}

