using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_data : MonoBehaviour, IDamageable
{
    [SerializeField] private Unit_card _unit_card;
    private Unit unit;
    private Room room;
    public Unit_card unit_card => _unit_card;

    public void EnemyDataInitialization(Unit_card card, Room room_, Transform player, int level)
    {
        unit = new Unit(card);
        room = room_;
        _unit_card = card;
        CurrentHP = card.max_hp;

        SetController(player);
    }

    private void SetController(Transform player)
    {
        Boss_controller controller = GetComponent<Boss_controller>();
        controller.SetController(player, unit.move_speed, unit.shell_speed, unit.damage);
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
            room.ReduceEnemysCount();
            room.OpenPortal(gameObject.transform);
            Destroy(gameObject);
        }
    }
}
