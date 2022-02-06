using UnityEngine;

public interface IDamageable
{
    float CurrentHP { get; }
    void ApplyDamage(float damage);
}

public struct Unit
{
    private string _unit_name;
    private float _max_hp;
    private float _damage;
    private float _attack_cooldown;
    private float _move_speed;
    private float _shell_speed;

    public string unit_name => _unit_name;
    public float max_hp => _max_hp;
    public float damage => _damage;
    public float attack_cooldown => _attack_cooldown;
    public float move_speed => _move_speed;
    public float shell_speed => _shell_speed;

    public Unit(Unit_card unit)
    {
        _unit_name = unit.unit_name;
        _max_hp = unit.max_hp;
        _damage = unit.damage;
        _attack_cooldown = unit.attack_cooldown;
        _move_speed = unit.move_speed;
        _shell_speed = unit.shell_speed;
    }
}
