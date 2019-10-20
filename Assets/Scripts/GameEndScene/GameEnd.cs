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
    [SerializeField] GameObject background;

    public static bool Cleared = false;

    Image backgroundImage;
    int count = 0;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        buttonToTitle.onClick.AddListener(ToTitleScene);
        buttonRetry.onClick.AddListener(Retry);
        buttonToNext.onClick.AddListener(ToNextStage);
        backgroundImage = background.GetComponent<Image>();
        Color c = backgroundImage.color;
        backgroundImage.color = new Color(c.r, c.g, c.b, 0.0f);
        count = 0;
        score = MainGame.Director.phase - 1;

        if (!Cleared)
        {
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(count < 30)
        {
            Color c = backgroundImage.color;
            backgroundImage.color = new Color(c.r, c.g, c.b, (float)count / (30 * 2));
        }
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

        count++;
    }

    //クリア（true）orゲームオーバー（false）をセット
    public static void SetClearFlag(bool f)
    {
        Cleared = f;
    }

    void ToTitleScene()
    {
        //タイトルへ
        FadeManager.FadeOut(1.0f, "TitleScene");
    }

    void Retry()
    {
        //もういちど
        Debug.Log("Retry");
    }

    void ToNextStage()
    {
        //つぎのステージへ
        FadeManager.FadeOut(1.0f, "StageSelect");
    }

}
