using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Mainmenu
{
    public class UI_mainmenu : MonoBehaviour
    {
        [SerializeField] private Fader fader;
        [SerializeField] private GameObject panel_settings, canvas_confirm;
        [SerializeField] private Dropdown dropdown_resolution;

        private delegate void Confirm();
        private Confirm confirm;

        public void NewGame()
        {
            StartCoroutine(StartNewGame());
        }

        public void Settings()
        {
            panel_settings.SetActive(true);
        }        

        private void Exit()
        {
            Application.Quit();
        }

        public void DelegateExit()
        {
            confirm = Exit;
            canvas_confirm.SetActive(true);
        }

        public void AcceptConfirmWindow()
        {
            confirm();
        }

        public void CancelConfirmWindow()
        {
            canvas_confirm.SetActive(false);
        }

        public void ClosePanelSettings()
        {
            panel_settings.SetActive(false);
        }

        IEnumerator StartNewGame()
        {
            fader.FadeIn();
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(1);
        }

        public void ChangeResolution()
        {
            switch (dropdown_resolution.value)
            {
                case 0:
                    Screen.SetResolution(1366, 768, true);
                    break;
                case 1:
                    Screen.SetResolution(1920, 1080, true);
                    break;
            }
        }
    }
}
