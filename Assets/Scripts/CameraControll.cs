using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public GameObject LookAtPoint;
    public float rotateSpeed = 3.0f;
    public float distance = 5.0f;
    public bool ReverseVertical = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //入力から回転させる角度を決める
        Vector3 angle = new Vector3(Input.GetAxis("Horizontal") * rotateSpeed, Input.GetAxis("Vertical") * rotateSpeed, 0);
        //注目する対象の位置を取得
        Vector3 pos = LookAtPoint.transform.position;

        //対象の位置を中心に回転させる
        transform.RotateAround(pos, Vector3.up, angle.x);
        if (!ReverseVertical)
        {
            transform.RotateAround(pos, transform.right, angle.y);
        }
        else
        {
            transform.RotateAround(pos, transform.right, angle.y * -1);
        }

        //対象の方を向く
        transform.LookAt(LookAtPoint.transform);

        //対象との距離を保つ
        transform.position = (transform.position - pos).normalized * distance + pos;
    }
}
