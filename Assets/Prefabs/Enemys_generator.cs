using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys_generator : MonoBehaviour
{
    [SerializeField] private LevelManager level;
    [SerializeField] private Transform transform_player;
    [SerializeField] private GameObject enemy_prefab, boss_prefab;
    private Unit_card[] enemy_list;
    private Unit_card[] boss_list;

    public GameObject Enemy_prefab { get { return enemy_prefab; } }
    public GameObject Boss_prefab { get { return boss_prefab; } }

    private void Awake()
    {
        enemy_list = Resources.LoadAll<Unit_card>("Cards/Units/");
        boss_list = Resources.LoadAll<Unit_card>("Cards/Bosses/");
    }

    public GameObject InstantiateEnemy(GameObject enemy_type, Room room)
    {
        Vector2 room_vector = new Vector2(room.transform.position.x + (Random.Range(-4, 4)), room.transform.position.y + (Random.Range(-2, 2)));
        GameObject new_enemy = Instantiate(enemy_type, room_vector, Quaternion.identity);
        room.IncreaseEnemysCount();
        Unit_card rand_card;
        if (enemy_type == enemy_prefab)
        {
            rand_card = enemy_list[Random.Range(0, enemy_list.Length)];
            new_enemy.GetComponent<Enemy_data>().EnemyDataInitialization(rand_card, room, transform_player, level.current_level);
        }
        else if (enemy_type == boss_prefab)
        {
            rand_card = boss_list[Random.Range(0, boss_list.Length)];
            new_enemy.GetComponent<Boss_data>().EnemyDataInitialization(rand_card, room, transform_player, level.current_level);
        }
        return new_enemy;
    }
}
