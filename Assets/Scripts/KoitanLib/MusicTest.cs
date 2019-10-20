using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            MusicManager.Play(BgmCode.CupBattle);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MusicManager.Play(BgmCode.CupTitle);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MusicManager.Play(BgmCode.CupClear);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            MusicManager.Play(BgmCode.CupResult);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            MusicManager.FadeIn(2);
            FadeManager.FadeIn(2);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            MusicManager.FadeOut(2);
            FadeManager.FadeOut(2);
        }
    }
}
