﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; //60FPSに設定
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
