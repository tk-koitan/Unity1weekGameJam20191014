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
            MusicManager.Play(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MusicManager.Play(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MusicManager.Play(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            MusicManager.Play(3);
        }
    }
}
