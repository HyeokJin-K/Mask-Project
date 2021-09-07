using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.SceneManagement;
using UnityEngine.Android;

public class LoginManager : MonoBehaviour
{
    public GameObject loginUI;
    public GameObject registerUI;

    public InputField loginID;
    public InputField registerID;

    public Text warningText;

    //  True: LoginUI   False: RegisterUI
    bool checkUI = true;

    bool searchComplete;
    bool loginCheck = true;
    bool registerCheck = true;

    private void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
    }

    public void LoginID()
    {
        if (loginID.text != "")
            StartCoroutine(LoadLoginDB(loginID.text));
        else
            warningText.text = "���̵� �Է����ּ���.";
    }

    public void RegisterID()
    {
        if(registerID.text != "")
        {                        
            StartCoroutine(LoadRegisterDB(registerID.text));            
        }
        else
        {
            warningText.text = "������ ���̵� �Է����ּ���.";
            print("������ ���̵� �Է����ּ���.");
        }
    }

    public void ChangeUI()
    {
        warningText.text = "";
        switch (checkUI)
        {
            case true:
                loginID.text = "";
                registerID.text = "";
                loginUI.SetActive(false);
                registerUI.SetActive(true);
                checkUI = false;                
                return;
            case false:
                loginID.text = "";
                registerID.text = "";
                loginUI.SetActive(true);
                registerUI.SetActive(false);
                checkUI = true;
                return;
        }        
    }

    //  ���� �����͸� �о�ͼ� ������ ���ԵǾ� �ִ��� Ȯ��
    IEnumerator LoadRegisterDB(string inputText)
    {
        searchComplete = false;

        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri(SaveValues.db_URL);
        DatabaseReference database = FirebaseDatabase.DefaultInstance.GetReference("Users");
        
        database.GetValueAsync().ContinueWith((task) =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                print("����");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                
                foreach (DataSnapshot data in snapshot.Children)
                {
                    UserData user = JsonUtility.FromJson<UserData>(data.GetRawJsonValue());
                    
                    if (user.userName.Equals(inputText))
                    {
                        registerCheck = false;
                        break;
                    }
                    else
                    {
                        registerCheck = true;
                    }
                }
                searchComplete = true;
            }
        });

        //  ���
        while (!searchComplete)
        {
            yield return null;
        }

        //  DB�� �̹� ���̵� �ִ��� Ȯ��
        if (registerCheck)
        {
            //  qr�ڵ� ����
            Texture2D myQR = QRCodeCreate.generateQR(registerID.text);

            //  ������ ��Ʈ �̸�
            UserData data = new UserData(registerID.text, null);

            //  ������ ���� Json�������� ��ȯ
            string userNameData = JsonUtility.ToJson(data);

            //  DB ��Ʈ ã��
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;            

            //  ��Ʈ ������ ���� �߰�, ����
            
            reference.Child("Users").Child(registerID.text).SetRawJsonValueAsync(userNameData);
            print("���ԿϷ�!");
            
            ChangeUI();
        }
        else
        {
            warningText.text = "�����ϴ� ���̵� �Դϴ�.";
            print("�����ϴ� ���̵� �Դϴ�.");
        }
    }

    IEnumerator LoadLoginDB(string inputText)
    {
        searchComplete = false;

        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri(SaveValues.db_URL);

        DatabaseReference database = FirebaseDatabase.DefaultInstance.GetReference("Users");

        yield return null;
        database.GetValueAsync().ContinueWith((task) =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                print("����");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                foreach (DataSnapshot data in snapshot.Children)
                {
                    //UserData user = JsonUtility.FromJson<UserData>(data);
                    //if (user.userName.Equals(inputText))
                    //{
                    //    loginCheck = true;

                    //    break;
                    //}
                    //else
                    //{
                    //    loginCheck = false;
                    //}
                    if ((string)data.Child("userName").Value == inputText)
                    {
                        loginCheck = true;
                        break;
                    }
                    else
                    {
                        loginCheck = false;
                    }
                }
                searchComplete = true;
            }
        });

        while (!searchComplete)
        {
            yield return null;
        }

        if (loginCheck)
        {
            SaveValues.currentLoginID = loginID.text;
            print("�α��� ����");
            print("���� ���� ���� ���̵�: " + SaveValues.currentLoginID);
            SceneManager.LoadScene("DrawScene");
        }
        else
        {
            warningText.text = "�������� �ʴ� ���̵��Դϴ�.";
            print("�������� �ʴ� ���̵��Դϴ�.");
        }

    }
}
