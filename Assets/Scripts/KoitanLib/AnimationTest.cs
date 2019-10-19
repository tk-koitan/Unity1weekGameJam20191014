using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    [SerializeField]
    private GameObject[] openEff;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isOpen)
            {
                animator.Play("close");
            }
            else
            {
                animator.Play("open");
                for (int i = 0; i < openEff.Length; i++)
                {
                    EmitParticle(openEff[i]);
                }
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
        for (int i = 0; i < openEff.Length; i++)
        {
            EmitParticle(openEff[i]);
        }
        yield return new WaitForSeconds(1);
        animator.Play("close");
    }

    private void EmitParticle(GameObject par)
    {
        GameObject obj = Instantiate(par, itemPos.position, Quaternion.identity);
        Destroy(obj, obj.GetComponent<ParticleSystem>().duration);
    }
}
