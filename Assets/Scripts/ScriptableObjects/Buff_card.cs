using UnityEngine;

[CreateAssetMenu(fileName = "Buff_card", menuName = "New Buff")]
public class Buff_card : ScriptableObject
{
    [SerializeField]
    private int _buff_id;
    [SerializeField]
    private string _buff_name;
    [SerializeField]
    private Sprite _buff_sprite;
    [SerializeField]
    private Sprite _buff_model;
    [SerializeField]
    private string _buff_description;
    [SerializeField]
    private float _move_speed;

    public int buff_id => _buff_id;
    public string buff_name => _buff_name;
    public Sprite buff_sprite => _buff_sprite;
    public Sprite buff_model => _buff_model;
    public string buff_description => _buff_description;
    public float move_speed => _move_speed;
}
