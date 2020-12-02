using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMenuController : MonoBehaviour
{
    protected GameManager gameManager;
    protected UIAudioManager audioManager;

    [SerializeField] protected GameObject menu;
    

    [Header("MainButton")]
    [SerializeField] protected Button play;
    [SerializeField] protected Button setting;
    [SerializeField] protected Button exit;

    [Header("Settings")]
    [SerializeField] protected GameObject settingsMenu;
    [SerializeField] protected Button closeSettings;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameManager = GameManager.Instanse;

        audioManager = UIAudioManager.Instance;
        exit.onClick.AddListener(OnQuitClicked);
        setting.onClick.AddListener(OnSettingsClicked);
        closeSettings.onClick.AddListener(OnSettingsClicked);
    }

    protected virtual void OnDestroy()
    {
        
        exit.onClick.RemoveListener(OnQuitClicked);
        setting.onClick.RemoveListener(OnSettingsClicked);
        closeSettings.onClick.RemoveListener(OnSettingsClicked);
    }

    protected virtual void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ChangeMenuStatus();
        }
    }

    protected virtual void ChangeMenuStatus()
    {
        menu.SetActive(!menu.activeInHierarchy);
       // Time.timeScale = menu.activeInHierarchy ? 0 : 1;
    }

    // Update is called once per frame
   private void OnQuitClicked()
    {
        gameManager.Quit();
        audioManager.Play(UIClipNames.Quit);
    }
    private void OnSettingsClicked()
    {

        audioManager.Play(UIClipNames.Settings);
        settingsMenu.SetActive(!settingsMenu.activeInHierarchy);
        ChangeMenuStatus();
    }
}
