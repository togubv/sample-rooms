using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_game_menu : MonoBehaviour
{
    [SerializeField] private Fader fader;
    [SerializeField] private GameObject game_menu, panel_settings, canvas_confirm;

    private delegate void Confirm();
    private Confirm confirm;

    public void Continue()
    {
        if (Time.timeScale == 0) Time.timeScale = 1;
        game_menu.SetActive(false);
    }

    public void Settings()
    {
        panel_settings.SetActive(true);
    }

    private void ToMainMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(StartToMainMenu());
    }

    public void DelegateToMainMenu()
    {
        confirm = ToMainMenu;
        canvas_confirm.SetActive(true);
    }

    public void ClosePanelSettings()
    {
        panel_settings.SetActive(false);
    }

    IEnumerator StartToMainMenu()
    {
        fader.FadeIn();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }

    public void AcceptConfirmWindow()
    {
        confirm();
    }

    public void CancelConfirmWindow()
    {
        canvas_confirm.SetActive(false);
    }
}
