using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CallTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CallGameEndMenu();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(100);
        }
    }

    public void CallGameEndMenu()
    {
        if (!SceneManager.GetSceneByName("GameEndScene").isLoaded)
        {
            GameEnd.SetClearFlag(!GameEnd.Cleared);
            SceneManager.LoadScene("GameEndScene", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.UnloadSceneAsync("GameEndScene");
        }
    }
}
