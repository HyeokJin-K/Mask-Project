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

        // 정점 좌표 추가
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

        // 트라이앵글 연결 순서 지정
        int[] triangleArray = new int[12]
        {
            0, 1, 2,
            1, 3, 2,
            2, 3, 4,
            3, 5, 4
        };
        maskMesh.triangles = triangleArray;

        // 노멀, 탄젠트, 바운드 계산
        maskMesh.RecalculateNormals();
        maskMesh.RecalculateTangents();
        maskMesh.RecalculateBounds();

        // uv 순서 지정
        //Vector2[] uvArray = new Vector2[4]
        //{
        //    new Vector2(0, 0),
        //    new Vector2(0, 1),
        //    new Vector2(1, 1),
        //    new Vector2(1, 0)
        //};
        //maskMesh.uv = uvArray;
        maskMesh.RecalculateUVDistributionMetrics();

        // 메시 필터에 마스크 메시 적용
        maskMF.mesh = maskMesh;

        // 쉐이더, 머터리얼 생성        
        Shader maskShader = Shader.Find("Mobile/Particles/Additive");
        Material maskMat = new Material(maskShader);

        // 머터리얼 컬러 지정
        maskMat.color = Color.white;

        // 메시 렌더러에 머터리얼 적용
        maskMR.material = maskMat;

    }

}
