using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_data : MonoBehaviour, IDamageable
{
    [SerializeField] private Unit_card _unit_card;
    private Room room;
    private bool isAlive;
    private Unit unit;
    public Unit_card unit_card => _unit_card;

    public void EnemyDataInitialization(Unit_card card, Room room_, Transform player, int level)
    {
        unit = new Unit(card);
        room = room_;
        _unit_card = card;
        isAlive = true;
        CurrentHP = unit.max_hp;

        GetComponentInChildren<SpriteRenderer>().sprite = card.sprite;
        SetController(player);
    }

    private void SetController(Transform player)
    {
        Enemy_controller controller = GetComponent<Enemy_controller>();
        controller.SetData(player, unit.move_speed, unit.attack_cooldown, unit.shell_speed, unit.damage);
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
            if (isAlive == true) room.ReduceEnemysCount();
            isAlive = false;
            Destroy(gameObject);
        }
    }
}
