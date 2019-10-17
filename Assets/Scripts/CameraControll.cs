using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public GameObject LookAtPoint;
    public float rotateSpeed = 3.0f;
    public bool ReverseVertical = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 angle = new Vector3(Input.GetAxis("Horizontal") * rotateSpeed, Input.GetAxis("Vertical") * rotateSpeed, 0);
        Vector3 pos = LookAtPoint.transform.position;
        transform.RotateAround(pos, Vector3.up, angle.x);
        if (!ReverseVertical)
        {
            transform.RotateAround(pos, transform.right, angle.y);
        }
        else
        {
            transform.RotateAround(pos, transform.right, angle.y * -1);
        }
    }
}
