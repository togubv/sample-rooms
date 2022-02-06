using UnityEngine;

public class UI_buff_cell : MonoBehaviour
{
    public UI_canvas ui;
    public string _name, _description;

    public void SetComponents(UI_canvas ui_, Buff_card card)
    {
        ui = ui_;
        _description = card.buff_description;
        _name = card.buff_name;
    }

    private void OnMouseEnter()
    {
        ui.ShowBuffDescription(transform, _name, _description);
    }

    private void OnMouseExit()
    {
        ui.HideBuffDescription();
    }
}
