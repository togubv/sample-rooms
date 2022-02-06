using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_canvas : MonoBehaviour
{
    private Boss_data boss_data;
    [SerializeField] private Player_data player_data;

    [SerializeField] private Slider  boss_hp_bar;
    [SerializeField] private Text text_boss_name;
    [SerializeField] private GameObject canvas_stats, canvas_game, canvas_gameover, canvas_minimap, canvas_nextlevel, canvas_game_menu;
    [SerializeField] private Text text_shoot_type, text_movespeed, text_damage, text_shell_speed, text_buff_description, text_buff_name, text_complete;
    [SerializeField] private Transform buffs_grid;
    [SerializeField] private GameObject buff_cell, boss_ui, buff_description;
    [SerializeField] private Image[] hp_icon;
    [SerializeField] private Sprite[] hp_sprites;

    private bool isGame;

    private void Start()
    {
        isGame = true;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && isGame)
        {
            if (canvas_stats.activeInHierarchy)
            {
                HideBuffDescription();
                canvas_stats.SetActive(false);
                Cursor.visible = false;
            }
            else
            {
                canvas_stats.SetActive(true);
                UpdateStatsUI();
                Cursor.visible = true;
            }
        }

        if (boss_ui.activeInHierarchy)
        {
            BossHPBarUpdate(boss_data.CurrentHP);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canvas_game_menu.activeInHierarchy)
            {
                GamePause(true);
                canvas_game_menu.SetActive(false);
            }
            else
            {
                GamePause(false);
                canvas_game_menu.SetActive(true);
            }
        }
    }

    public void GamePause(bool toggle)
    {
        if (toggle)
        {
            Time.timeScale = 1;
            isGame = true;
            Cursor.visible = false;
        }
        else
        {
            Time.timeScale = 0;
            isGame = false;
            Cursor.visible = true;
        }
    }

    public void UpdatePlayerHPBar(float hp, float max_hp)
    {

        for (int i = 0; i < max_hp; i++)
        {
            if (i < hp && i + 1 > hp) hp_icon[i].sprite = hp_sprites[1];
            else if (i + 1 <= hp) hp_icon[i].sprite = hp_sprites[0];
            else if (i + 1 > hp) hp_icon[i].sprite = hp_sprites[2];
        }
        if (hp <= 0) ShowCanvasGameOver();
    }

    private void UpdateStatsUI()
    {
        if (text_shoot_type.text == "") text_shoot_type.text = "DEFAULT";
        text_movespeed.text = player_data.move_speed.ToString();
        text_damage.text = player_data.damage.ToString();
        text_shell_speed.text = player_data.shell_speed.ToString();
    }

    public void UpdateBuffsGrid(Buff_card buff_card)
    {
        GameObject new_buff = Instantiate(buff_cell, buffs_grid);
        new_buff.GetComponent<Image>().sprite = buff_card.buff_sprite;
        new_buff.GetComponent<UI_buff_cell>().SetComponents(this, buff_card);
    }

    public void ShowBuffDescription(Transform position, string name, string description)
    {
        text_buff_name.text = name;
        text_buff_description.text = description;
        buff_description.transform.position = position.position;
        buff_description.SetActive(true);
    }

    public void HideBuffDescription()
    {
        buff_description.SetActive(false);
    }

    public void UpdateShootType(Item_card card)
    {
        text_shoot_type.text = card.item_name;
    }

    public void ShowBossUI(Boss_data boss_data_)
    {
        boss_data = boss_data_;
        text_boss_name.text = boss_data_.unit_card.unit_name;
        boss_hp_bar.maxValue = boss_data_.CurrentHP;
        boss_ui.SetActive(true);
    }

    public void BossHPBarUpdate(float hp)
    {
        boss_hp_bar.value = hp;
        if (hp <= 0) boss_ui.SetActive(false);
    }

    void ShowCanvasGameOver()
    {
        canvas_stats.SetActive(false);
        canvas_game.SetActive(false);
        canvas_gameover.SetActive(true);
        Cursor.visible = true;
    }
    public void ShowCanvasGame()
    {
        canvas_game.SetActive(true);
    }

    public void CloseAllCanvas()
    {
        canvas_stats.SetActive(false);
        canvas_game.SetActive(false);
        canvas_gameover.SetActive(false);
        canvas_minimap.SetActive(false);
        canvas_nextlevel.SetActive(false);
    }

    public void EndCurrentLevel(int level)
    {
        GamePause(false);
        CloseAllCanvas();
        text_complete.text = "C O M P L E T E  L E V E L  -  " + level;
        canvas_nextlevel.SetActive(true);
        Cursor.visible = true;
    }

    public void ShowGameCanvas()
    {
        canvas_game.SetActive(true);
        canvas_minimap.SetActive(true);
    }
}
