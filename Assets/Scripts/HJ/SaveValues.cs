using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;


//  어플리케이션이 실행되는 동안 유지할 값들
public static class SaveValues
{
    // 현재 컬러 값
    public static Color[] myColors;
    //  DB저장주소
    public static string db_URL= "https://mask-project-31d0b-default-rtdb.asia-southeast1.firebasedatabase.app/";
    //  현재 로그인 중인 ID
    public static string currentLoginID;
}
