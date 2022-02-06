using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Room_generator room_generator;
    [SerializeField] private UI_canvas ui;
    [SerializeField] private Room_mover room_mover;
    [SerializeField] private Minimap minimap;
    [SerializeField] private Player_data player_data;
    private int rooms_count;
    public int current_level
    {
        get;
        private set;
    }

    private void Start()
    {
        rooms_count = 8;
        GenerateNewLevel();
        ui.ShowCanvasGame();
    }

    public void ClearWorld()
    {
        minimap.ClearMinimap();
        player_data.gameObject.SetActive(false);
        room_generator.ClearLevel();
        room_mover.ClearPlayerShells();
    }

    public void ShowLevelResults()
    {
        ui.EndCurrentLevel(current_level);
    }

    public void GenerateNewLevel()
    {
        current_level++;
        room_generator.GenerateLevel(rooms_count + current_level);
        room_mover.SetStartSettings();
        ui.CloseAllCanvas();
        ui.ShowGameCanvas();
        ui.GamePause(true);
        player_data.gameObject.SetActive(true);
    }   

    public void EndLevel()
    {
        ClearWorld();
        ShowLevelResults();
    }
}
