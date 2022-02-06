using UnityEngine;

public class Item_data : MonoBehaviour
{
    private Item_card _item_card;
    public Item_card item_card { get { return _item_card; } }

    public void SetCard(Item_card card)
    {
        _item_card = card;
        GetComponent<SpriteRenderer>().sprite = card.item_sprite;
    }
}
