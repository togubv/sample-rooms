using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    Default,
    Start,
    Boss,
    Shop,
    Secret
}

public class Room : MonoBehaviour
{ 
    public RoomType room_type;

    [SerializeField] private GameObject portal_prefab;
    [SerializeField] private GameObject item_prefab;
    private Item_card[] item_card;
    private Room _neighbour_up, _neighbour_down, _neighbour_left, _neighbour_right;
    public Room neighbour_up 
    {
        get { return _neighbour_up; }
        set { _neighbour_up = (Room)value; }
    }
    public Room neighbour_down
    {
        get { return _neighbour_down; }
        set { _neighbour_down = (Room)value; }
    }
    public Room neighbour_left
    {
        get { return _neighbour_left; }
        set { _neighbour_left = (Room)value; }
    }
    public Room neighbour_right
    {
        get { return _neighbour_right; }
        set { _neighbour_right = (Room)value; }
    }

    private int _neighbours_count;
    public int neighbours_count { get { return _neighbours_count; } }

    public bool wasUsed;
    public Vector2Int coordinate;
    public Sprite[] sprite_pack;
    [SerializeField] private GameObject[] go_walls = new GameObject[28];
    [SerializeField] private SpriteRenderer[] sprite_r_grounds = new SpriteRenderer[60];
    [SerializeField] private GameObject prefab_vase;
    [SerializeField] private Transform transform_room_objects;

    [SerializeField] private GameObject[] go_door = new GameObject[4];
    [SerializeField] private GameObject[] active_doors = new GameObject[4];
    [SerializeField] private SpriteRenderer[] sprite_r_doorway = new SpriteRenderer[8];
    private SpriteRenderer[] sprite_r_doors = new SpriteRenderer[4];
    private int enemys_count;

    public void SetDoors()
    {
        if (_neighbour_up != null)
        {
            SetDoorsData(3);
        }
        if (_neighbour_down != null)
        {
            SetDoorsData(1);
        }
        if (_neighbour_left != null)
        {
            SetDoorsData(0);
        }
        if (_neighbour_right != null)
        {
            SetDoorsData(2);
        }

        void SetDoorsData(int i)
        {
            active_doors[i] = go_door[i];
            sprite_r_doors[i] = active_doors[i].GetComponent<SpriteRenderer>();
            active_doors[i].GetComponent<BoxCollider2D>().isTrigger = true;
            _neighbours_count++;
        }
    }
    
    public void SetRoomType(RoomType room_type_)
    {
        room_type = room_type_;
    }

    public void ChangeSpritePack(string path)
    {
        sprite_pack = Resources.LoadAll<Sprite>(path);
        if (sprite_pack.Length < 1)
        {
            Debug.Log("Cant load sprite pack...");
        }
        InitializateRoom();
    }

    public void InitializateRoom()
    {
        AddRoomObjectsVaseVariation();
        ChangeSprites();
        item_card = Resources.LoadAll<Item_card>("Cards/Items/");
    }

    public void ChangeSprites()
    {
        SetGroundSprites();
        SetWallsSprites();
    }

    void SetWallsSprites()
    {
        for (int i = 0; i < go_walls.Length; i++)
        {
            if (i < 4)
                go_walls[i].GetComponent<SpriteRenderer>().sprite = sprite_pack[0];
            else
                go_walls[i].GetComponent<SpriteRenderer>().sprite = sprite_pack[1];
        }

        for (int i = 0; i < 4; i++)
        {
            if (active_doors[i] != null)
            {
                if (sprite_r_doorway[i * 2].sprite == null) sprite_r_doorway[i * 2].sprite = sprite_pack[3];
                if (sprite_r_doorway[i * 2 + 1].sprite == null) sprite_r_doorway[i * 2 + 1].sprite = sprite_pack[2];
            }
            else
            {
                sprite_r_doorway[i * 2].sprite = sprite_pack[1];
                sprite_r_doorway[i * 2 + 1].sprite = sprite_pack[1];
            }
        }

        if (room_type == RoomType.Boss) SetDoorsToBossRoom();
    }

    void SetGroundSprites()
    {
        for (int i = 0; i < sprite_r_grounds.Length; i++)
        {
            int random_sprite = Random.Range(12, 16);
            sprite_r_grounds[i].sprite = sprite_pack[random_sprite];
        }
    }

    private void AddRoomObjectsVaseVariation()
    {
        #region VASES

        Vector2 vase_variation_1_1 = new Vector2(-4.5f, 2.5f);      // 1
        Vector2 vase_variation_1_2 = new Vector2(-3.5f, 2.5f);
        Vector2 vase_variation_1_3 = new Vector2(-4.5f, 1.5f);

        Vector2 vase_variation_2_1 = new Vector2(-4.5f, -2.5f);     // 2
        Vector2 vase_variation_2_2 = new Vector2(-4.5f, -1.5f);
        Vector2 vase_variation_2_3 = new Vector2(-3.5f, -2.5f);

        Vector2 vase_variation_3_1 = new Vector2(4.5f, 2.5f);       // 3
        Vector2 vase_variation_3_2 = new Vector2(4.5f, 1.5f);
        Vector2 vase_variation_3_3 = new Vector2(3.5f, 2.5f);

        Vector2 vase_variation_4_1 = new Vector2(4.5f, -2.5f);      // 4
        Vector2 vase_variation_4_2 = new Vector2(3.5f, -2.5f);
        Vector2 vase_variation_4_3 = new Vector2(4.5f, -1.5f);

        Vector2 vase_variation_5_1 = new Vector2(0, 1);      // 5
        Vector2 vase_variation_5_2 = new Vector2(0, -1);
        Vector2 vase_variation_5_3 = new Vector2(-1, 0);
        Vector2 vase_variation_5_4 = new Vector2(1, 0);

        int i = Random.Range(0, 7);
        switch(i)
        {
            case 0:
                break;
            case 1:
                AddVase(vase_variation_1_1); 
                AddVase(vase_variation_1_2);
                AddVase(vase_variation_1_3);
                AddVase(vase_variation_4_1);
                AddVase(vase_variation_4_2);
                AddVase(vase_variation_4_3);
                break;
            case 2:
                AddVase(vase_variation_2_1);
                AddVase(vase_variation_2_2);
                AddVase(vase_variation_2_3);
                AddVase(vase_variation_3_1);
                AddVase(vase_variation_3_2);
                AddVase(vase_variation_3_3);
                break;
            case 3:
                AddVase(vase_variation_5_1);
                AddVase(vase_variation_5_2);
                AddVase(vase_variation_5_3);
                AddVase(vase_variation_5_4);
                break;
            case 4:
                AddVase(vase_variation_1_1);
                AddVase(vase_variation_1_2);
                AddVase(vase_variation_1_3);
                AddVase(vase_variation_2_1);
                AddVase(vase_variation_2_2);
                AddVase(vase_variation_2_3);
                AddVase(vase_variation_3_1);
                AddVase(vase_variation_3_2);
                AddVase(vase_variation_3_3);
                AddVase(vase_variation_4_1);
                AddVase(vase_variation_4_2);
                AddVase(vase_variation_4_3);
                break;
            case 5:
                AddVase(vase_variation_1_1);
                AddVase(vase_variation_1_2);
                AddVase(vase_variation_1_3);
                AddVase(vase_variation_4_1);
                AddVase(vase_variation_4_2);
                AddVase(vase_variation_4_3);
                break;
            case 6:
                AddVase(vase_variation_2_1);
                AddVase(vase_variation_2_2);
                AddVase(vase_variation_2_3);
                AddVase(vase_variation_3_1);
                AddVase(vase_variation_3_2);
                AddVase(vase_variation_3_3);
                break;
        }

        #endregion

        void AddVase(Vector2 position)
        {
            GameObject new_object = Instantiate(prefab_vase, transform_room_objects);
            new_object.transform.localPosition = new Vector2(new_object.transform.localPosition.x + position.x, new_object.transform.localPosition.x + position.y);
        }
    }

    public void SetDoorsToBossRoom()
    {
        if (neighbour_up != null)
        {
            neighbour_up.sprite_r_doorway[2].sprite = neighbour_up.sprite_pack[5];
            neighbour_up.sprite_r_doorway[3].sprite = neighbour_up.sprite_pack[4];
        }

        if (neighbour_down != null)
        {
            neighbour_down.sprite_r_doorway[6].sprite = neighbour_down.sprite_pack[5];
            neighbour_down.sprite_r_doorway[7].sprite = neighbour_down.sprite_pack[4];
        }

        if (neighbour_left != null)
        {
            neighbour_left.sprite_r_doorway[4].sprite = neighbour_left.sprite_pack[5];
            neighbour_left.sprite_r_doorway[5].sprite = neighbour_left.sprite_pack[4];
        }

        if (neighbour_right != null)
        {
            neighbour_right.sprite_r_doorway[0].sprite = neighbour_right.sprite_pack[5];
            neighbour_right.sprite_r_doorway[1].sprite = neighbour_right.sprite_pack[4];
        }
    }

    public void CloseRoom()
    {
        wasUsed = true;
        StartCoroutine(CloseAllDoors());
    }

    public void OpenRoom()
    {
        StartCoroutine(OpenAllDoors());
    }

    private IEnumerator CloseAllDoors()
    {
        for (int i = 0; i < 4; i++)
        {
            if (active_doors[i] != null)
            {
                active_doors[i].GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }

        for (int j = 6; j <= 11; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                if (sprite_r_doors[i] != null)
                {
                    sprite_r_doors[i].sprite = sprite_pack[j];
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator OpenAllDoors()
    {
        for (int j = 11; j >= 6; j--)
        {
            for (int i = 0; i < 4; i++)
            {
                if (sprite_r_doors[i] != null)
                {
                    sprite_r_doors[i].sprite = sprite_pack[j];
                }
            }
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < 4; i++)
        {
            if (active_doors[i] != null)
            {
                active_doors[i].GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }

        for (int i = 0; i < 4; i++)       // REFRESH DOOR COLLIDER...
        {
            if (active_doors[i] != null)
            {
                active_doors[i].SetActive(false);
                active_doors[i].SetActive(true);
            }
        }
    }

    public void IncreaseEnemysCount()
    {
        enemys_count++;
    }

    public void ReduceEnemysCount()
    {
        enemys_count--;
        if (enemys_count <= 0)
        {
            OpenRoom();
        }
    }

    public void OpenPortal(Transform position)
    {
        Instantiate(portal_prefab, position.position, Quaternion.identity);
        CreateItem(this);
    }

    private void CreateItem(Room room)
    {
        GameObject item = Instantiate(item_prefab, transform.position, Quaternion.identity, room.transform);
        int random_item = Random.Range(1, 4);
        item.GetComponent<Item_data>().SetCard(item_card[random_item]);
    }
}
