using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootType
{
    Default,
    None,
    Mirror,
    Triple,
}

public class Player_data : MonoBehaviour, IDamageable
{
    #region COMPONENTS

    [SerializeField] private UI_canvas ui_canvas;
    [SerializeField] private Unit_card unit_card;
    public List<Buff_card> player_buff_list;
    private bool isInvulnerable;
    private ShootType _shoot_type;
    public ShootType shoot_type => _shoot_type;

    public float shell_speed
    {
        get;
        private set;
    }

    public float damage
    {
        get;
        private set;
    }
    public float hp_max
    {
        get;
        private set;
    }
    public float move_speed
    {
        get;
        private set;
    }
    public float attack_speed
    {
        get;
        private set;
    }

    public float CurrentHP
    {
        get;
        private set;
    }

    #endregion

    private void Awake()
    {
        shell_speed = unit_card.shell_speed;
        damage = unit_card.damage;
        move_speed = unit_card.move_speed;
        attack_speed = unit_card.attack_cooldown;

        CurrentHP = unit_card.max_hp;
    }

    public void ApplyDamage(float damage)
    {
        if (!isInvulnerable)
        {
            CurrentHP -= damage;
            ui_canvas.UpdatePlayerHPBar(CurrentHP, unit_card.max_hp);
            if (CurrentHP <= 0)
            {
                gameObject.SetActive(false);
            }
            if (gameObject.activeInHierarchy) StartCoroutine(MakeInvulnerable(2.0f));
        }
    }

    public void IncreaseHealth(float health)
    {
        CurrentHP += health;
        if (CurrentHP > unit_card.max_hp) CurrentHP = unit_card.max_hp;
        ui_canvas.UpdatePlayerHPBar(CurrentHP, unit_card.max_hp);
    }

    public bool TakeItem(Item_card card)
    {
        if (card.item_type == ItemType.Expendable)
        {
            if (card.item_increase_health > 0 && CurrentHP < unit_card.max_hp)
            {
                IncreaseHealth(card.item_increase_health);
                return true;
            }
            else return false;
        }
        else if (card.item_type == ItemType.Weapon)
        {
            if (card.item_shoot_type != ShootType.None) _shoot_type = card.item_shoot_type;
            ui_canvas.UpdateShootType(card);
            return true;
        }
        else if (card.item_type == ItemType.Buff)
        {
            PlayerBuffsUpdate(card.item_buff_card);
            return true;
        }
        else return false;
    }

    public void PlayerBuffsUpdate(Buff_card buff_card)
    {
        player_buff_list.Add(buff_card);
        ui_canvas.UpdateBuffsGrid(buff_card);
        if (buff_card.move_speed != 0) move_speed += buff_card.move_speed;
    }

    private IEnumerator MakeInvulnerable(float duration)
    {
        Color old = GetComponentInChildren<Renderer>().material.color;
        Color trans = new Color(old.r, old.g, old.b, 0.5f);
        isInvulnerable = true;
        for (int i = 0; i < duration / 0.2f; i++)
        {
            GetComponentInChildren<Renderer>().material.color = trans;
            yield return new WaitForSeconds(0.10f);
            GetComponentInChildren<Renderer>().material.color = old;
            yield return new WaitForSeconds(0.10f);
        }
        GetComponentInChildren<Renderer>().material.color = old;
        isInvulnerable = false;
    }
}
