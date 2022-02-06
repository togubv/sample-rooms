using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_world_collider : MonoBehaviour
{
    [SerializeField] private Room_mover room_mover;
    [SerializeField] private Player_data player_data;
    [SerializeField] private LevelManager level_manager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door_up"))
        {
            room_mover.SetSideChecker(0);
        }
        else if (collision.gameObject.CompareTag("Door_down"))
        {
            room_mover.SetSideChecker(1);
        }
        else if (collision.gameObject.CompareTag("Door_left"))
        {
            room_mover.SetSideChecker(2);
        }
        else if (collision.gameObject.CompareTag("Door_right"))
        {
            room_mover.SetSideChecker(3);
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            if (player_data.TakeItem(collision.gameObject.GetComponent<Item_data>().item_card)) Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Portal"))
        {
            Destroy(collision.gameObject);
            level_manager.EndLevel();
        }
    }
}
