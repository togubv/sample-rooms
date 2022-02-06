using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_mover : MonoBehaviour
{
    #region COMPONENTS

    [SerializeField] private Minimap minimap;
    [SerializeField] private Player_shooter player_shooter;
    [SerializeField] private Enemys_generator enemys_generator;
    [SerializeField] private Transform transform_player;
    [SerializeField] private UI_canvas ui;
    [SerializeField] private Room_generator room_generator;

    private Room current_room;

    #endregion COMPONENTS

    private void Start()
    {
        SetStartSettings();
    }

    public void SetStartSettings()
    {
        current_room = GetStartRoom();
        transform_player.position = current_room.transform.position;
        Camera.main.transform.position = current_room.transform.position;
        minimap.MinimapSetPlayerPosition(current_room.coordinate);
        minimap.MinimapAddRoom(current_room.coordinate.x, current_room.coordinate.y);
        DisableOtherRooms();
    }

    Room GetStartRoom()
    {
        for (int i = 0; i < room_generator.done_rooms.Length; i++)
        {
            if (room_generator.done_rooms[i].room_type == RoomType.Start)
            {
                return room_generator.done_rooms[i];
            }
        }
        return null;
    }

    private void TeleportToRoom(int direction)
    {
        switch (direction)
        {
            case 0:
                if (current_room.neighbour_up != null)
                {
                    SetNewCurrentRoom(current_room.neighbour_up);
                    MoveToNeighbourRoom(0, -2.8f);
                    ChangeOtherRoomSettings();
                }
                break;
            case 1:
                if (current_room.neighbour_down != null)
                {
                    SetNewCurrentRoom(current_room.neighbour_down);
                    MoveToNeighbourRoom(0, 2.8f);
                    ChangeOtherRoomSettings();
                }
                break;
            case 2:
                if (current_room.neighbour_left != null)
                {
                    SetNewCurrentRoom(current_room.neighbour_left);
                    MoveToNeighbourRoom(4.8f, 0);
                    ChangeOtherRoomSettings();
                }
                break;
            case 3:
                if (current_room.neighbour_right != null)
                {
                    SetNewCurrentRoom(current_room.neighbour_right);
                    MoveToNeighbourRoom(-4.8f, 0);
                    ChangeOtherRoomSettings();
                }
                break;
        }
        void SetNewCurrentRoom(Room new_room)
        {
            current_room = new_room;
        }

        void MoveToNeighbourRoom(float offset_x, float offset_y)
        {
            transform_player.position = new Vector2(current_room.transform.position.x + offset_x, current_room.transform.position.y + offset_y);
        }

        void ChangeOtherRoomSettings()
        {
            minimap.MinimapSetPlayerPosition(current_room.coordinate);
            DisableOtherRooms();
            EnterToRoom(current_room);
            ClearPlayerShells();
        }
    }

    public void ClearPlayerShells()
    {
        foreach (GameObject go in player_shooter.shell_list)
        {
            Destroy(go);
        }
        player_shooter.shell_list.Clear();
    }

    private void EnterToRoom(Room room)
    {
        if (!room.wasUsed && room.room_type == RoomType.Default)
        {
            minimap.MinimapAddRoom(room.coordinate.x, room.coordinate.y);
            room.CloseRoom();
            int count_enemys = Random.Range(4, 7);
            for (int i = 0; i < count_enemys; i++)
            {
                enemys_generator.InstantiateEnemy(enemys_generator.Enemy_prefab, room);
            }
        }
        else if (!room.wasUsed && room.room_type == RoomType.Boss)
        {
            minimap.MinimapAddRoom(room.coordinate.x, room.coordinate.y);
            room.CloseRoom();
            GameObject boss = enemys_generator.InstantiateEnemy(enemys_generator.Boss_prefab, room);
            ui.ShowBossUI(boss.GetComponent<Boss_data>());
        }
    }

    private void DisableOtherRooms()
    {
        current_room.gameObject.SetActive(true);
        for (int i = 0; i < room_generator.done_rooms.Length; i++)
        {
            if (room_generator.done_rooms[i] != current_room)
            {
                room_generator.done_rooms[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetSideChecker(int direction)
    {
        switch(direction)
        {
            case 0:
                TeleportToRoom(0);      // Door to up
                break;
            case 1:
                TeleportToRoom(1);      // Door to down
                break;
            case 2:
                TeleportToRoom(2);      // Door to left
                break;
            case 3:
                TeleportToRoom(3);      // Door to right
                break;
        }
    }
}
