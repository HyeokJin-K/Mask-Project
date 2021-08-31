using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawInk : MonoBehaviour
{

    //public GameObject testOb;
    
    Texture2D tex;

    [Range(0.1f, 1.0f)]
    public float range = 0.5f;

    Color[] colors;

    public GameObject target;
    Mesh mesh;
    Vector3[] vertices;

    int count;
    Color drawColor;

    private void Awake()
    {
        if (target == null) target = GameObject.Find("MainMesh");
    }
    private void Start()
    {        

        mesh = target.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        colors = new Color[vertices.Length];
        for(int i = 0; i < vertices.Length; i++)
        {
            colors[i] = Color.white;
        }
        mesh.colors = colors;


    }

    void Update()
    {
        //  ���콺 ���� ��ư �Է�
        if (Input.GetMouseButton(0))
        {

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.farClipPlane;           

            Vector3 dir = Camera.main.ScreenToWorldPoint(mousePos);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, mousePos.z))
            {
                //print(hit.transform.gameObject.name);

                //Instantiate(testOb, hit.point, Quaternion.identity);
                //  �浹�� ������Ʈ�� �޽��� �����´�.

                //  �ش� �޽��� ������ ��� ���Ѵ�.


                //  ��� ������ ��ǥ�� Ŭ���� ��ǥ�� ���� �ش� ���� �ȿ� �ִ��� Ȯ���Ѵ�.                                

                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Palette"))
                {
                    tex = (Texture2D)hit.transform.GetComponent<MeshRenderer>().material.mainTexture;
                    drawColor = tex.GetPixel((int)(hit.textureCoord.x * tex.width), (int)(hit.textureCoord.y * tex.height));
                    print(drawColor);
                }                                
                Vector3 localHitPoint =  hit.transform.InverseTransformPoint(hit.point);

                for (int i = 0; i < vertices.Length; i++)
                {                    
                    if (Vector3.Distance(vertices[i] , localHitPoint) < range)
                    {
                        colors[i] = drawColor;
                    }
                }
                mesh.colors = colors;

            }
        }
    }
}
