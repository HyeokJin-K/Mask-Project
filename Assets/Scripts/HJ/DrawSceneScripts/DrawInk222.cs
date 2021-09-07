using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class DrawInk222 : MonoBehaviour
{
    public Image palleteImage;
    public RawImage selectedColor;

    [Range(0.1f, 1.00f)]
    public float range = 0.5f;

    Color[] colors;

    public GameObject target;
    Mesh mesh;
    Vector3[] vertices;

    int count;
    Color drawColor = Color.white;

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
        //  마우스 왼쪽 버튼 입력
        if (Input.GetMouseButton(0))
        {
            Texture2D tex = palleteImage.sprite.texture;
            Rect r = palleteImage.rectTransform.rect;
            Vector2 localPoint = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(palleteImage.rectTransform, Input.mousePosition,
                Camera.main, out localPoint);
            //print(localPoint);

            if ((localPoint.x >= r.width / -2.0f && localPoint.x <= r.width / 2.0f) &&
                (localPoint.y >= r.height / -2.0f && localPoint.y <= r.height / 2.0f))
            {
                int px = Mathf.Clamp(0, (int)((localPoint.x - r.x) * tex.width / r.width), tex.width);
                int py = Mathf.Clamp(0, (int)((localPoint.y - r.y) * tex.height / r.height), tex.height);

                Color32 col = (Color32)tex.GetPixel(px, py);
                //print(col);
                
                if(col.a!=0)
                drawColor = (Color32)tex.GetPixel(px, py);
                selectedColor.color = drawColor;
            }

            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.farClipPlane;
                Vector3 dir = Camera.main.ScreenToWorldPoint(mousePos);

                RaycastHit hit;
                if (Physics.Raycast(transform.position, dir, out hit, mousePos.z))
                {
                    //print(hit.transform.gameObject.name);

                    //Instantiate(testOb, hit.point, Quaternion.identity);
                    //  충돌한 오브젝트의 메쉬를 가져온다.

                    //  해당 메쉬의 정점을 모두 구한다.


                    //  모든 정점의 좌표가 클릭한 좌표로 부터 해당 범위 안에 있는지 확인한다.                                

                    //if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Palette"))
                    //{
                    //    tex = (Texture2D)hit.transform.GetComponent<MeshRenderer>().material.mainTexture;
                    //    drawColor = tex.GetPixel((int)(hit.textureCoord.x * tex.width), (int)(hit.textureCoord.y * tex.height));
                    //    print(drawColor);
                    //}
                    Vector3 localHitPoint = hit.transform.InverseTransformPoint(hit.point);

                    for (int i = 0; i < vertices.Length; i++)
                    {
                        if (Vector3.Distance(vertices[i], localHitPoint) < range*0.1f)
                        {
                            colors[i] = drawColor;
                        }
                    }
                    mesh.colors = colors;
                    
                }
            }
        }

        //if (Input.GetKey(KeyCode.P))
        //{
        //    for (int i = 0; i < mesh.colors.Length; i++)
        //    {
        //        print(mesh.colors[i]);
        //    }
        //    print(mesh.colors.Length);
        //}
        #region 키보드 C키 색칠
        //if (Input.GetKey(KeyCode.C))
        //{
        //    Vector3 mousePos = Input.mousePosition;
        //    mousePos.z = Camera.main.farClipPlane;
        //    Vector3 dir = Camera.main.ScreenToWorldPoint(mousePos);

        //    RaycastHit hit;
        //    if (Physics.Raycast(transform.position, dir, out hit, mousePos.z))
        //    {
        //        //print(hit.transform.gameObject.name);

        //        //Instantiate(testOb, hit.point, Quaternion.identity);
        //        //  충돌한 오브젝트의 메쉬를 가져온다.

        //        //  해당 메쉬의 정점을 모두 구한다.


        //        //  모든 정점의 좌표가 클릭한 좌표로 부터 해당 범위 안에 있는지 확인한다.                                

        //        //if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Palette"))
        //        //{
        //        //    tex = (Texture2D)hit.transform.GetComponent<MeshRenderer>().material.mainTexture;
        //        //    drawColor = tex.GetPixel((int)(hit.textureCoord.x * tex.width), (int)(hit.textureCoord.y * tex.height));
        //        //    print(drawColor);
        //        //}
        //        Vector3 localHitPoint = hit.transform.InverseTransformPoint(hit.point);

        //        for (int i = 0; i < vertices.Length; i++)
        //        {
        //            if (Vector3.Distance(vertices[i], localHitPoint) < range)
        //            {
        //                colors[i] = drawColor;
        //            }
        //        }
        //        mesh.colors = colors;

        //    }
        //}
        #endregion
    }
}
