using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    [SerializeField] Button buttonToMainGame;
    [SerializeField] Button buttonToPractice;
    //[SerializeField] Button buttonQuit;

    // Start is called before the first frame update
    void Start()
    {
        buttonToMainGame.onClick.AddListener(ToMainGame);
        buttonToPractice.onClick.AddListener(ToPractice);
        //buttonQuit.onClick.AddListener(GameEnd);

        //Music
        MusicManager.Play(BgmCode.CupTitle);

        //Initialize Director
        MainGame.Director.phase = 1;
        MainGame.Director.dificulity = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToMainGame()
    {
        //はじめる
        SceneManager.LoadScene("StageSelect");
    }

    public void ToPractice()
    {
        //練習ステージ
        SceneManager.LoadScene("MainGameScene");
    }

    public void GameEnd()
    {
        //ゲーム終了
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
