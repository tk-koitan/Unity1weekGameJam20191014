using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameStartText : MonoBehaviour
{
    TextMeshProUGUI text;
    RectTransform rect;
    [SerializeField] float StartTime = 2.5f;

    float time = 0f;
    Vector3 pos;

    int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        rect = gameObject.GetComponent<RectTransform>();
        pos = rect.transform.position;
        text.text = "";
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (state == 0)
        {
            DOTween.ToAlpha(() => text.color, color => text.color = color, 0f, 0f);
            DOTween.ToAlpha(() => text.color, color => text.color = color, 1.0f, 0.5f);
            rect.DOMoveY(pos.y + 50.0f, 1.0f);
            text.text = "3";
            ++state;
        }
        if (state == 1 && time >= StartTime / 3)
        {
            DOTween.ToAlpha(() => text.color, color => text.color = color, 0f, 0f);
            DOTween.ToAlpha(() => text.color, color => text.color = color, 1.0f, 0.5f);
            rect.DOMoveY(pos.y, 0f);
            rect.DOMoveY(pos.y + 50.0f, 1.0f);
            text.text = "2";
            ++state;

        }
        if (state == 2 && time >= 2 * StartTime / 3)
        {
            DOTween.ToAlpha(() => text.color, color => text.color = color, 0f, 0f);
            DOTween.ToAlpha(() => text.color, color => text.color = color, 1.0f, 0.5f);
            rect.DOMoveY(pos.y, 0f);
            rect.DOMoveY(pos.y + 50.0f, 1.0f);
            text.text = "1";

            ++state;
        }
        if (state == 3 && time >= StartTime)
        {
            DOTween.ToAlpha(() => text.color, color => text.color = color, 0f, 0f);
            DOTween.ToAlpha(() => text.color, color => text.color = color, 1.0f, 0.5f);
            rect.DOMoveY(pos.y, 0f);
            rect.DOMoveY(pos.y + 50.0f, 1.0f);
            text.text = "START!";
            rect.DOShakePosition(1.0f,5.0f);

            ++state;
        }
        if (state == 4 && time >= StartTime + 0.5f)
        {
            DOTween.ToAlpha(() => text.color, color => text.color = color, 0f, 1f);

            ++state;
        }
    }
}
