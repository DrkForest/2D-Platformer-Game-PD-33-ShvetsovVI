using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : BaseMenuController
{

    [SerializeField] private Button restart;
    
    [SerializeField] private Button backToMenu;
    

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        play.onClick.AddListener(ChangeMenuStatus);
        restart.onClick.AddListener(gameManager.Restart);
        backToMenu.onClick.AddListener(goToMainMenu);
    }
    protected override void OnDestroy()
    {
        play.onClick.RemoveListener(ChangeMenuStatus);
        restart.onClick.RemoveListener(gameManager.Restart);
        backToMenu.onClick.RemoveListener(goToMainMenu);
 
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void ChangeMenuStatus()
    {
        base.ChangeMenuStatus();
        Time.timeScale = menu.activeInHierarchy ? 0 : 1;
    }

    public void goToMainMenu()
    {
        GameManager.Instanse.ChangeLevel((int)Scenes.MainMenu);
    }
}
