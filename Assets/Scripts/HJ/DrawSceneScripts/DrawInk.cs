using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Android;


public class DrawInk : MonoBehaviour
{
    public Image palleteImage;
    public RawImage selectedColor;
    public Texture Knob;// sim..
    public GameObject backGround;// sim..

    public Text debug;

    [Range(0.1f, 1.00f)]
    public float range = 0.5f;

    Color[] colors;

    public GameObject target;
    Mesh mesh;
    Vector3[] vertices;

    int count;
    Color drawColor = Color.red;

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
        //  ?????? ???? ???? ????        
        if (Input.GetMouseButton(0))
        {
            
            Texture2D tex = palleteImage.sprite.texture;
            Rect r = palleteImage.rectTransform.rect;
            Vector2 localPoint = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(palleteImage.rectTransform, Input.mousePosition,
                Camera.main, out localPoint);            

            

            if ((localPoint.x >= r.width / -2.0f && localPoint.x <= r.width / 2.0f) &&
                (localPoint.y >= r.height / -2.0f && localPoint.y <= r.height / 2.0f))
            {

                int px = Mathf.Clamp(0, (int)((localPoint.x - r.x) * tex.width / r.width), tex.width);
                int py = Mathf.Clamp(0, (int)((localPoint.y - r.y) * tex.height / r.height), tex.height);

                Color col = (Color)tex.GetPixel(px, py);
                //print(col);
                
                if(col.a!=0)
                drawColor = (Color)tex.GetPixel(px, py);                
                selectedColor.GetComponent<RawImage>().texture = Knob; // sim
                selectedColor.color = drawColor;
            }

            if (EventSystem.current.IsPointerOverGameObject() == false)
            {

                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.farClipPlane;
                Vector3 dir = Camera.main.ScreenToWorldPoint(mousePos);
                Vector3 palletePos = palleteImage.transform.position;

                RaycastHit hit;
                if (Physics.Raycast(transform.position, dir, out hit, mousePos.z))
                {
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

    }

    public void Range1()// sim..
    {
        range = 0.1f;
    }

    public void Range2()
    {
        range = 0.3f;
    }

    public void Range3()
    {
        range = 0.05f;
    }

    public void Eraser()
    {

        drawColor = Color.white;

    }
}
