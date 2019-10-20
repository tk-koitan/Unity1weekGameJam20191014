using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveTest : MonoBehaviour
{
    public Vector3[] paths;
    public float duration = 1;
    public PathType pathType;
    public int roundNum = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.DOPath(paths, duration, pathType).SetRelative().SetEase(Ease.InOutSine);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int l = paths.Length * roundNum;
            Vector3[] p = new Vector3[l];
            for (int i = 0; i < l; i++)
            {
                p[i] = paths[i % paths.Length];
            }
            transform.DOPath(p, duration, pathType).SetRelative().SetEase(Ease.InOutSine);
        }
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < paths.Length - 1; i++)
        {
            Gizmos.DrawLine(transform.position + paths[i], transform.position + paths[i + 1]);
        }
        Gizmos.DrawLine(transform.position + paths[paths.Length - 1], transform.position + paths[0]);
    }
}
