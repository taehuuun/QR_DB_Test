using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase.Firestore;
using Firebase.Extensions;

public class DataManager : MonoBehaviour
{
    [SerializeField] private UI ui;
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

    private void Start()
    {
        QRDataValidation(testQRData);
    }

    /// <summary>
    /// QR데이터가 DB내에 있는 데이터인지 검사
    /// </summary>
    /// <param name="QRData">QR코드의 데이터</param>
    private bool QRDataValidation(string QRData)
    {
        // 데이터가 null이거나 길이가 2보다 작은경우 함수 종료
        if(QRData == null || QRData.Length < 2)
            return false;

        string modelType = QRData.Substring(0,2);
        string cryptoData = QRData.Substring(2,QRData.Length);

        DocumentReference docRef = db.Collection(modelType).Document(cryptoData);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;

            if(snapshot.Exists)
            {
                ui.statusText.text = $"QRData | type : {modelType} crptoData : {cryptoData} Data is valid";
                return true;
            }
            else
            {
                ui.statusText.text = $"QRData | type : {modelType} crptoData : {cryptoData} Data is Not valid";
                return false;
            }
        });

        return false;
    }
}
