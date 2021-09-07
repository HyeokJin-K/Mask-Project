using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;


public class DetectQR : MonoBehaviour
{
    WebCamTexture webcamTexture;
    public string QrCode = string.Empty;

    public AudioSource beepSound;
    public Text QRCodeText;

    MeshRenderer mr;

    public Text debug;

    bool searchComplete = false;

    Color[] colors;

    GameObject arSession;

    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri(SaveValues.db_URL);

        mr = GetComponent<MeshRenderer>();
        arSession = GameObject.Find("AR Session Origin");
        var renderer = GetComponent<RawImage>();
        webcamTexture = new WebCamTexture(512, 512);
        renderer.material.mainTexture = webcamTexture;        
    }

    public void OnQRCode()
    {
        mr.enabled = true;
        StartCoroutine(GetQRCode());
    }

    IEnumerator GetQRCode()
    {       
        IBarcodeReader barCodeReader = new BarcodeReader();
        webcamTexture.Play();
        var snap = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.ARGB32, false);
        while (string.IsNullOrEmpty(QrCode))
        {
            try
            {
                snap.SetPixels32(webcamTexture.GetPixels32());
                var Result = barCodeReader.Decode(snap.GetRawTextureData(), webcamTexture.width, webcamTexture.height, RGBLuminanceSource.BitmapFormat.ARGB32);
                if (Result != null)
                {
                    QrCode = Result.Text;
                    if (!string.IsNullOrEmpty(QrCode))
                    {
                        debug.text += QrCode.ToString();
                        Debug.Log("DECODED TEXT FROM QR: " + QrCode);
                        QRCodeText.text = QrCode;                                               
                        break;
                    }
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
            yield return null;
        }
        if (!QrCode.Equals(""))
        {
            StartCoroutine(GetUserData());
            debug.text += '\n' + "22";
        }

        mr.enabled = false;
        webcamTexture.Stop();
    }

    IEnumerator GetUserData()
    {
        DatabaseReference database = FirebaseDatabase.DefaultInstance.GetReference("Users");
        searchComplete = false;
        database.GetValueAsync().ContinueWith((task) =>
        {
            debug.text += '\n' + "qq";
            if (task.IsCanceled || task.IsFaulted)
            {
                debug.text += '\n' + "33failed";
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                debug.text += '\n' + "33";
                foreach (DataSnapshot data in snapshot.Children)
                {
                    UserData user = JsonUtility.FromJson<UserData>(data.GetRawJsonValue());

                    if (user.userName.Equals(QrCode.ToString()))
                    {
                        debug.text += '\n' + "44";
                        debug.text = user.userName.ToString() + " " + user.userColors[0];
                        arSession.GetComponent<DetectMaskPoint>().GetMaskMeshColor(user.userColors);
                        searchComplete = true;
                        break;
                    }
                    else
                    {
                        print("없는 데이터 입니다.");
                    }
                }
            }            

            debug.text += '\n' + "kk";
        });

        while (!searchComplete)
        {
            yield return null;
        }
    }
}
