using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameStartText : MonoBehaviour
{
    TextMeshProUGUI text;
    RectTransform rect;
    [SerializeField] int StartTime = 180;

    int count = 0;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        rect = gameObject.GetComponent<RectTransform>();
        pos = rect.transform.position;
        text.text = "";
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (count == 0)
        {
            DOTween.ToAlpha(() => text.color, color => text.color = color, 0f, 0f);
            DOTween.ToAlpha(() => text.color, color => text.color = color, 1.0f, 0.5f);
            rect.DOMoveY(pos.y + 50.0f, 1.0f);
            text.text = "3";
        }
        if (count == StartTime / 3)
        {
            DOTween.ToAlpha(() => text.color, color => text.color = color, 0f, 0f);
            DOTween.ToAlpha(() => text.color, color => text.color = color, 1.0f, 0.5f);
            rect.DOMoveY(pos.y, 0f);
            rect.DOMoveY(pos.y + 50.0f, 1.0f);
            text.text = "2";
        }
        if (count == 2 * StartTime / 3)
        {
            DOTween.ToAlpha(() => text.color, color => text.color = color, 0f, 0f);
            DOTween.ToAlpha(() => text.color, color => text.color = color, 1.0f, 0.5f);
            rect.DOMoveY(pos.y, 0f);
            rect.DOMoveY(pos.y + 50.0f, 1.0f);
            text.text = "1";
        }
        if(count == StartTime)
        {
            DOTween.ToAlpha(() => text.color, color => text.color = color, 0f, 0f);
            DOTween.ToAlpha(() => text.color, color => text.color = color, 1.0f, 0.5f);
            rect.DOMoveY(pos.y, 0f);
            rect.DOMoveY(pos.y + 50.0f, 1.0f);
            text.text = "START!";
            rect.DOShakePosition(1.0f,5.0f);
        }
        if(count == StartTime + 30)
        {
            DOTween.ToAlpha(() => text.color, color => text.color = color, 0f, 1f);
        }

        count++;
    }
}
