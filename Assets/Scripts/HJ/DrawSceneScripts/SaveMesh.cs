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

        #region ������ ���� ���� 
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

        #region ���ø����̼� ���� ����
        //  ����Ű �Է½�
        if (Input.GetKey(KeyCode.Return))
        {
            if (inputText.text != null)
                SaveColors();            
        }
        #endregion
    }

    //  �÷� �迭 �� ���� �Լ�
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

            print("���� ����");
        }
        else
        {
            print("�̸��� �Է��ϼ���.");
        }
    }
    
}
