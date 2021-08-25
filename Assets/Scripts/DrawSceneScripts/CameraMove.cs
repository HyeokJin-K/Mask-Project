using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject targetMesh;
    public float distacne = -3.0f;

    float xR;
    float yR;
    
    void Start()
    {
        if(targetMesh == null)
        {
            targetMesh = GameObject.Find("MainMesh");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            //  마우스 입력 
            float xRotation = Input.GetAxisRaw("Mouse X");
            float yRotation = Input.GetAxisRaw("Mouse Y");

            //  마우스 회전 적용
            xR += xRotation;
            yR += yRotation;                   
        }    

        distacne += Input.GetAxisRaw("Mouse ScrollWheel");
        distacne = Mathf.Clamp(distacne, -10.0f, -1.0f);
    }

    void LateUpdate()
    {                            
        transform.rotation = Quaternion.Euler(-yR, xR, 0);     
        transform.position = targetMesh.transform.position + transform.rotation * new Vector3(0,0,distacne);

    }
}
