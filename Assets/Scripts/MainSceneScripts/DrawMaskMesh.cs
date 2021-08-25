using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMaskMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshFilter maskMF = gameObject.AddComponent<MeshFilter>();
        MeshRenderer maskMR = gameObject.AddComponent<MeshRenderer>();

        Mesh maskMesh = new Mesh();
        maskMesh.name = "MaskMesh";

        // ���� ��ǥ �߰�
        Vector3[] verticeArray = new Vector3[6]
        {
            new Vector3(-1.5f, 1.5f, 2.0f),
            new Vector3(-0.5f, 0.5f, 0),
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(0.5f, 0.5f, 0),
            new Vector3(0.5f, -0.5f, 0),
            new Vector3(1.5f, 1.5f, 2.0f)

        };
        maskMesh.vertices = verticeArray;

        // Ʈ���̾ޱ� ���� ���� ����
        int[] triangleArray = new int[12]
        {
            0, 1, 2,
            1, 3, 2,
            2, 3, 4,
            3, 5, 4
        };
        maskMesh.triangles = triangleArray;

        // ���, ź��Ʈ, �ٿ�� ���
        maskMesh.RecalculateNormals();
        maskMesh.RecalculateTangents();
        maskMesh.RecalculateBounds();

        // uv ���� ����
        //Vector2[] uvArray = new Vector2[4]
        //{
        //    new Vector2(0, 0),
        //    new Vector2(0, 1),
        //    new Vector2(1, 1),
        //    new Vector2(1, 0)
        //};
        //maskMesh.uv = uvArray;
        maskMesh.RecalculateUVDistributionMetrics();

        // �޽� ���Ϳ� ����ũ �޽� ����
        maskMF.mesh = maskMesh;

        // ���̴�, ���͸��� ����        
        Shader maskShader = Shader.Find("Mobile/Particles/Additive");
        Material maskMat = new Material(maskShader);

        // ���͸��� �÷� ����
        maskMat.color = Color.white;

        // �޽� �������� ���͸��� ����
        maskMR.material = maskMat;

    }

}
