using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase.Firestore;
using Firebase.Extensions;

public class DataManager : MonoBehaviour
{
    private static DataManager ins;

    private string testQRData = "AAsad123sadDa123";

    private FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

    public static DataManager Ins
    {
        get
        {
            if(ins == null)
            {
                return null;
            }

            return ins;
        }
    }

    private void Awake()
    {
        #region 싱글톤 
        if(ins == null)
        {
            ins = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion
    
    }

    /// <summary>
    /// QR데이터가 DB내 데이터에 있는지 확인하는 함수
    /// </summary>
    /// <param name="QRdata">QR스캔 데이터</param>
    private void ValidationData(string QRdata)
    {
        // QR데이터가 null이거나 길이가 2보다 작은 경우 리턴
        if(QRdata == null || QRdata.Length < 2)
            return;

        string modelType = QRdata.Substring(0,2);
        string shaVal = QRdata.Substring(2,QRdata.Length);

        DocumentReference docRef = db.Collection(modelType).Document(shaVal);
    }
}
