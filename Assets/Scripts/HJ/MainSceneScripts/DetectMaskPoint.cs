using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;
using Unity.Collections;
using UnityEngine.UI;

public class DetectMaskPoint : MonoBehaviour
{
    public Text logtext;
    public GameObject maskPrefab;
    GameObject mask;

    ARFaceManager faceManager;
    ARCoreFaceSubsystem faceSubsystem;

    int count;
    
    void Start()
    {
        mask = Instantiate(maskPrefab);
        mask.SetActive(false);

        faceManager = GetComponent<ARFaceManager>();
        faceSubsystem = (ARCoreFaceSubsystem)faceManager.subsystem;

        faceManager.facesChanged += DetectNose;
    }

    void DetectNose(ARFacesChangedEventArgs args)
    {
        //if(args.updated.Count > 0)
        //{
        //    NativeArray<ARCoreFaceRegionData> nose = new NativeArray<ARCoreFaceRegionData>();

        //    faceSubsystem.GetRegionPoses(args.updated[0].trackableId, Allocator.Persistent, ref nose);

        //    mask.transform.position = nose[0].pose.position;
        //    mask.transform.rotation = nose[0].pose.rotation;
        //    mask.SetActive(true);
        //}
        //else
        //{
        //    mask.SetActive(false);
        //}

        if (args.updated.Count > 0)
        {
            Quaternion rotate = args.updated[0].transform.rotation;
            Vector3 point = args.updated[0].vertices[count];
            mask.transform.position = args.updated[0].transform.TransformPoint(point);
            mask.transform.forward = args.updated[0].normals[0];
            //mask.transform.forward = args.updated[0].normals[0];
            
            //mask.transform.rotation = new Quaternion(-rotate.x, -rotate.y + 180.0f, -rotate.z, rotate.w);
            mask.SetActive(true);

            NativeArray<Vector3> verticeArray = args.updated[0].vertices;
        }
        else if(args.removed.Count > 0)
        {
            mask.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                //count++;
            }
        }

        if (count > 4)
        {
            count = 0;
        }
    }

    public void GetMaskMeshColor(Color[] maskColors)
    {
        mask.GetComponent<MeshFilter>().mesh.colors = maskColors;
    }
}
