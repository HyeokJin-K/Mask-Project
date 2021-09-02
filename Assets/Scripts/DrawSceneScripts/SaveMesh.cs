using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
    

public class SaveMesh : MonoBehaviour
{
    public GameObject saveObject;

    string localPath;
    public Text inputText;

    public InputField field;
    
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.K) && EventSystem.current.currentSelectedGameObject == null)
        {
            saveObject = GameObject.Find("MainMesh");
            localPath = "Assets/Mesh/" + inputText.text + ".mesh";

            AssetDatabase.Refresh();
            Mesh saveMesh = (Mesh)Instantiate(saveObject.GetComponent<MeshFilter>().mesh);
            AssetDatabase.CreateAsset(saveMesh, AssetDatabase.GenerateUniqueAssetPath(localPath));

            Debug.Log("Mesh saved: " + localPath);
            AssetDatabase.SaveAssets();

        }
    }

    
}
