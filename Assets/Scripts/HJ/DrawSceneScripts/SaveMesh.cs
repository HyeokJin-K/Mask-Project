using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Firebase;
using Firebase.Database;
    

public class SaveMesh : MonoBehaviour
{
    public GameObject saveObject;

    string localPath;
    public Text inputText;

    void Update()
    {

        #region 에디터 전용 저장 
        //if (Input.GetKeyDown(KeyCode.K) && EventSystem.current.currentSelectedGameObject == null)
        //{
        //    saveObject = GameObject.Find("MainMesh");
        //    localPath = "Assets/Mesh/" + inputText.text + ".mesh";

        //    AssetDatabase.Refresh();
        //    Mesh saveMesh = (Mesh)Instantiate(saveObject.GetComponent<MeshFilter>().mesh);
        //    AssetDatabase.CreateAsset(saveMesh, AssetDatabase.GenerateUniqueAssetPath(localPath));

        //    Debug.Log("Mesh saved: " + localPath);
        //    AssetDatabase.SaveAssets();
        //}
        #endregion

        #region 어플리케이션 전용 저장
        //  엔터키 입력시
        if (Input.GetKey(KeyCode.Return))
        {
            if (inputText.text != null)
                SaveColors();            
        }
        #endregion
    }

    //  컬러 배열 값 저장 함수
    public void SaveColors()
    {
        if (!inputText.text.Equals(""))
        {
            Mesh targetMesh = saveObject.GetComponent<MeshFilter>().mesh;
            Color[] colors = new Color[targetMesh.colors.Length];

            for (int i = 0; i < targetMesh.colors.Length; i++)
            {
                colors[i] = targetMesh.colors[i];
            }
            SaveValues.myColors = colors;

            UserData mydata = new UserData(SaveValues.currentLoginID, colors);
            print(mydata.userColors.Length);
            string myDataJson = JsonUtility.ToJson(mydata);

            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

            reference.Child("Users").Child(SaveValues.currentLoginID).SetRawJsonValueAsync(myDataJson);

            print("저장 성공");
        }
        else
        {
            print("이름을 입력하세요.");
        }
    }
    
}
