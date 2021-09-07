using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using System.IO;

public class QRCodeCreate : MonoBehaviour
{
    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        //���ڵ� �۾�    //QRcode Make..
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,

            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }

        };

        return writer.Write(textForEncoding);

        //QRcode�� ���ܼ� ������ ���Ͽ� �ν��� �ϰ� �Ǵµ�, �ν��� �ϰ� �Ǹ� �ش� QRcode�� ����� �ؽ�Ʈ�� ���� ���� �� ������ Ȯ�� �ϴ� ���̴�. �̿� ���� QR �ڵ带 ���� ������ QRcode �ȿ� ������ �ؽ�Ʈ�� �Բ� ������Ų��.

    }

    public static Texture2D generateQR(string text)

    {

        //���ڵ� �۾��� ���� Encode �Լ� ȣ��

        var encoded = new Texture2D(256, 256);

        var color32 = Encode(text, encoded.width, encoded.height);

        encoded.SetPixels32(color32);

        encoded.Apply();



        //���ڵ带 �Ϸ��� PNG ���Ϸ� ����� ���� File �ý���.

        byte[] bytes = encoded.EncodeToPNG();

        File.WriteAllBytes(Application.persistentDataPath + "/" + text + ".png", bytes);

        //Application.persistentDataPath �� ������ �����Ͽ����� C:\Users\��������\AppData\LocalLow\�� ����Ƽ ���۴� ȸ�� �̸��� ���� �� ���̴�.

        return encoded;

    }


        ////10���� qr�ڵ带 ����� ���ؼ� for���� �̿��� ���� ���۵Ǹ� ���� �ǵ��� �Ͽ���.
        //for (int i = 1; i <= 10; i++)
        //{
        //    //generateQR �Լ��� ���� �̸��� ������ ����

        //    Texture2D myQR = generateQR("Unit_" + i);
        //}
    
}
