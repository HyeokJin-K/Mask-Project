using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ZXing;
using ZXing.QrCode;


//  사용자A는 메시를 여러개 디자인 할 수 있고 해당 메시들을 로컬 메모리에 저장한다.

//  사용자A는 저장한 메시들 중에서 자신의 가상 마스크에 적용할 메시 하나를 DB에 보낸다.

//  사용자A는 자신의 정보가 담긴 이미지(마커) or QR코드를 DB에 보낸다.

//  스캔한 QR코드를 데이터 값으로 변환하는 스크립트를 

//  사용자A는 자신의 정보가 저장돼 있는 실제 이미지(마커), QR코드를 자신의 마스크에 붙인다. 

//  사용자B가 찍은 마커로 부터 해당 메시는 사용자A의 정보로 참조를 가능하게 해서 DB에서 해당 메시를 찾을 수 있게 한다. << 애매함

/*  사용자B는 다른 사용자들의 실제 이미지(마커), QR코드를 인식하기 위해
  사용자 정보가 담겨져 있는 모든 이미지(마커), QR코드들을 자신의 로컬 메모리에 저장한다.*/

/*  사용자B는 사용자A의 마스크에 붙어있는 이미지,QR을 카메라로 촬영하면, 로컬 메모리에 있는 이미지들과 비교해서 찾으면 
  해당 이미지의 사용자 정보를 DB에 보내고 해당 이미지(메시)를 받아온다*/

//  실제 마스크 이미지 위치 or 얼굴 인식한 위치에 가상 마스크 이미지(메시)를 띄운다

public class CreateQR : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
