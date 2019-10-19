using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    [SerializeField]
    private GameObject openEff;
    [SerializeField]
    private GameObject openEff2;
    [SerializeField]
    Transform itemPos;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isOpen)
            {
                animator.Play("close");
            }
            else
            {
                animator.Play("open");
                Instantiate(openEff, itemPos.position, Quaternion.identity);
                Instantiate(openEff2, itemPos.position, Quaternion.identity);
            }
            isOpen = !isOpen;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(testCoroutine());
        }
    }

    private IEnumerator testCoroutine()
    {
        animator.Play("open");
        Instantiate(openEff, itemPos.position, Quaternion.identity);
        Instantiate(openEff2, itemPos.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        animator.Play("close");
    }
}
