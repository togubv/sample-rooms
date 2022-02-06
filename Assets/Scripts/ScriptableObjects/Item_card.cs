using UnityEngine;

public enum ItemType
{
    Default,
    Expendable,
    Weapon,
    Buff
}

[CreateAssetMenu(fileName = "Item_card", menuName = "New Item")]
public class Item_card : ScriptableObject
{
    [SerializeField]
    private Sprite _item_sprite;
    [SerializeField]
    private ItemType _item_type;
    [SerializeField]
    private int _item_id;
    [SerializeField]
    private string _item_name;
    [SerializeField]
    private float _item_increase_health;
    [SerializeField]
    private ShootType _item_shoot_type;
    [SerializeField]
    private Buff_card _item_buff_card;

    public Sprite item_sprite => _item_sprite;
    public ItemType item_type => _item_type;
    public int item_id => _item_id;
    public string item_name => _item_name;
    public float item_increase_health => _item_increase_health;
    public ShootType item_shoot_type => _item_shoot_type;
    public Buff_card item_buff_card => _item_buff_card;

}
