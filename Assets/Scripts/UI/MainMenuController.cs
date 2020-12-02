using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : BaseMenuController
{

    [Header("MainButton")]
    //[SerializeField] protected Button play;

    [SerializeField] protected Button chooseLVL;
    [SerializeField] protected Button reset;

    [SerializeField] private GameObject lvlMenu;
    [SerializeField] private Button closeLVLMenu;
    //private TMP_Text playButtonText;

    private int lvl = 1;
    protected override void Start()
    {
        base.Start();
        //PlayerPrefs.DeleteAll();
        chooseLVL.onClick.AddListener(OnLvlMenuClicked);
        closeLVLMenu.onClick.AddListener(OnLvlMenuClicked);

        //playButtonText = play.GetComponentInChildren<TMP_Text>();
        if (PlayerPrefs.HasKey(GamePrefs.LastPlayedLvl.ToString()))
        {
            play.GetComponentInChildren<TMP_Text>().text = "Resume";
            lvl = PlayerPrefs.GetInt(GamePrefs.LastPlayedLvl.ToString());
        }
        play.onClick.AddListener(Play);
        reset.onClick.AddListener(OnResetClick);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        chooseLVL.onClick.RemoveListener(OnLvlMenuClicked);
        closeLVLMenu.onClick.RemoveListener(OnLvlMenuClicked);
        play.onClick.RemoveListener(Play);
        reset.onClick.RemoveListener(OnResetClick);
    }

    private void OnLvlMenuClicked()
    {
        lvlMenu.SetActive(!lvlMenu.activeInHierarchy);
        ChangeMenuStatus();
        audioManager.Play(UIClipNames.ChooseMenu);
    }

    private void Play()
    {
        gameManager.ChangeLevel(lvl);
        audioManager.Play(UIClipNames.Play);
    }

    private void OnResetClick()
    {
        play.GetComponentInChildren<TMP_Text>().text = "Play";
        gameManager.ResetProgress();
        lvl = 1;
        audioManager.Play(UIClipNames.Reset);
    }
}
