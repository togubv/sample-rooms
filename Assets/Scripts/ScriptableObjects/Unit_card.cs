using UnityEngine;

[CreateAssetMenu(fileName = "Unit_card", menuName = "New Unit")]
public class Unit_card : ScriptableObject
{
    [SerializeField]
    private int _id;
    [SerializeField]
    private string _unit_name;
    [SerializeField]
    private float _max_hp;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _attack_cooldown;
    [SerializeField]
    private float _move_speed;
    [SerializeField]
    private float _shell_speed;
    [SerializeField]
    private Sprite _sprite;

    public int id => _id;
    public string unit_name => _unit_name;
    public float max_hp => _max_hp;
    public float damage => _damage;
    public float attack_cooldown => _attack_cooldown;
    public float move_speed => _move_speed;
    public float shell_speed => _shell_speed;
    public Sprite sprite => _sprite;
}
