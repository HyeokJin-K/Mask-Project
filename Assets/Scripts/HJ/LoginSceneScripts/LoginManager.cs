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
            warningText.text = "아이디를 입력해주세요.";
    }

    public void RegisterID()
    {
        if(registerID.text != "")
        {                        
            StartCoroutine(LoadRegisterDB(registerID.text));            
        }
        else
        {
            warningText.text = "생성할 아이디를 입력해주세요.";
            print("생성할 아이디를 입력해주세요.");
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

    //  유저 데이터를 읽어와서 유저가 가입되어 있는지 확인
    IEnumerator LoadRegisterDB(string inputText)
    {
        searchComplete = false;

        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri(SaveValues.db_URL);
        DatabaseReference database = FirebaseDatabase.DefaultInstance.GetReference("Users");
        
        database.GetValueAsync().ContinueWith((task) =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                print("실패");
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

        //  대기
        while (!searchComplete)
        {
            yield return null;
        }

        //  DB에 이미 아이디가 있는지 확인
        if (registerCheck)
        {
            //  qr코드 생성
            Texture2D myQR = QRCodeCreate.generateQR(registerID.text);

            //  저장할 루트 이름
            UserData data = new UserData(registerID.text, null);

            //  저장할 값을 Json형식으로 변환
            string userNameData = JsonUtility.ToJson(data);

            //  DB 루트 찾기
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;            

            //  루트 하위에 값을 추가, 변경
            
            reference.Child("Users").Child(registerID.text).SetRawJsonValueAsync(userNameData);
            print("가입완료!");
            
            ChangeUI();
        }
        else
        {
            warningText.text = "존재하는 아이디 입니다.";
            print("존재하는 아이디 입니다.");
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
                print("실패");
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
            print("로그인 성공");
            print("현재 접속 중인 아이디: " + SaveValues.currentLoginID);
            SceneManager.LoadScene("DrawScene");
        }
        else
        {
            warningText.text = "존재하지 않는 아이디입니다.";
            print("존재하지 않는 아이디입니다.");
        }

    }
}
