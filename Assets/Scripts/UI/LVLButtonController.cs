using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LVLButtonController : MonoBehaviour
{

    private Button button;

    [SerializeField] private Scenes scene;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        if (!PlayerPrefs.HasKey(GamePrefs.LvlPlayed.ToString() + ((int)scene).ToString()))
        {
            button.interactable = false;
            return;
        }

        button.onClick.AddListener(ChangeLvl);
        GetComponentInChildren<TMP_Text>().text = ((int)scene).ToString();
    }


    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeLvl()
    {
        GameManager.Instanse.ChangeLevel((int)scene);
    }
}
