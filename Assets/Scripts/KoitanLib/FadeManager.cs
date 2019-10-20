using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeManager : MonoBehaviour
{    
    static FadeManager instance;
    public static FadeManager Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private Canvas fadeCanvas;

    public static RawImage image;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(instance);
            DontDestroyOnLoad(fadeCanvas);
        }
        else Destroy(gameObject);

        image = GetComponent<RawImage>();       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void FadeIn(float duration)
    {
        image.DOFade(1, duration);
    }

    public static void FadeOut(float duration)
    {
        image.DOFade(0, duration);
    }
}
