using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroying_object : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject item_prefab;
    [SerializeField] private Item_card[] item_card;

    void Awake()
    {
        CurrentHP = 1;
    }

    public float CurrentHP
    {
        get;
        private set;
    }

    public void ApplyDamage(float damage)
    {
        CurrentHP -= damage;
        if (CurrentHP <= 0)
        {
            if (RandomizerBool(10))
            {
                Room room = GetComponentInParent<Room>();
                GameObject item = Instantiate(item_prefab, transform.position, Quaternion.identity, room.transform);
                item.GetComponent<Item_data>().SetCard(item_card[0]);
            }
            Destroy(gameObject);
        }
    }

    bool RandomizerBool(int chance)
    {
        int rand = Random.Range(1, 100);
        if (chance > rand) return true;
        else return false;
    }
}
