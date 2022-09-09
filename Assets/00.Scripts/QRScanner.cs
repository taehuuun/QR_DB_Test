using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class QRScanner : MonoBehaviour
{
    public RawImage preview;
    private Rect screenRect;
    private WebCamTexture camTexture;

    private string readQRData;


    private void Start()
    {
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = Screen.height;
        camTexture.requestedWidth = Screen.width;

        if(camTexture != null)
        {
            camTexture.Play();
        }

        preview.texture = camTexture;
    }

    private void Update()
    {
        if(camTexture.isPlaying)
        {
            try
            {
                IBarcodeReader barcodeReader = new BarcodeReader();
                var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);

                if(result != null && result.Text != readQRData)
                {   
                    readQRData = result.Text;
                    DataManager.Ins.QRDataValidation(readQRData);
                    return;
                }
            }
            catch (System.Exception ex) 
            {
                Debug.LogWarning(ex.Message);;
            }
        }
    }
}
