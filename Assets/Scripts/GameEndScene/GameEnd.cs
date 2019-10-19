using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameEnd : MonoBehaviour
{
    [SerializeField] Button buttonToTitle;
    [SerializeField] Button buttonRetry;
    [SerializeField] Button buttonToNext;
    [SerializeField] TextMeshProUGUI ResultText;

    public static bool Cleared = false;

    // Start is called before the first frame update
    void Start()
    {
        buttonToTitle.onClick.AddListener(ToTitleScene);
        buttonRetry.onClick.AddListener(Retry);
        buttonToNext.onClick.AddListener(ToNextStage);
    }

    // Update is called once per frame
    void Update()
    {
        if (Cleared)
        {
            buttonToNext.interactable = true;
            buttonToTitle.interactable = true;
            buttonRetry.interactable = true;
            ResultText.text = "CLEAR";
        }
        else
        {
            buttonToNext.interactable = false;
            buttonToTitle.interactable = true;
            buttonRetry.interactable = true;
            ResultText.text = "GAME OVER";
        }
    }

    //クリア（true）orゲームオーバー（false）をセット
    public static void SetClearFlag(bool f)
    {
        Cleared = f;
    }

    void ToTitleScene()
    {
        //タイトルへ
        SceneManager.LoadScene("TitleScene");
    }

    void Retry()
    {
        //もういちど
        Debug.Log("Retry");
    }

    void ToNextStage()
    {
        //つぎのステージへ
        SceneManager.LoadScene("MainGameScene");
    }

}
