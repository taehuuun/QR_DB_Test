using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase.Firestore;
using Firebase.Extensions;

public class DataManager : MonoBehaviour
{
    [SerializeField] private UI ui;
    private static DataManager ins;

    [SerializeField] private string testQRData = "AA.sad123sadDa123";
    [SerializeField] private string type = "";
    [SerializeField] private string cryptoData = "";
    [SerializeField] private const string DATE_STR = "yyyyMMddHHmmss";
    

    private FirebaseFirestore db;
    private Data data;

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
        db = FirebaseFirestore.DefaultInstance;

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
        

        string[] tmpData = QRData.Split('.');

        type = tmpData[0];
        cryptoData = tmpData[1];

        Debug.Log($"Type : {type}");
        Debug.Log($"cryptoData : {cryptoData}");

        DocumentReference docRef = db.Collection(type).Document(cryptoData);

        Debug.Log($"docRef : {docRef}");

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;

            if(snapshot.Exists)
            {
                Debug.Log($"QRData | type : {type} crptoData : {cryptoData} Data is valid");
                LoadData();
                return true;
            }
            else
            {
                Debug.Log($"QRData | type : {type} crptoData : {cryptoData} Data is Not valid");
                return false;
            }
        });

        return false;
    }

    private void LoadData()
    {
        Debug.Log("Start LoadData Func");
        DocumentReference docRef = db.Collection(type).Document(cryptoData);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>{
           DocumentSnapshot snapshot = task.Result; 

            if(snapshot.Exists)
            {
                Dictionary<string, object> loadData = snapshot.ToDictionary();

                data.birth = loadData["Birth"].ToString();
                data.modelID = loadData["ModelID"].ToString();
                data.nickName = loadData["NickName"].ToString();
                data.clean = float.Parse(loadData["Clean"].ToString());
                data.health = float.Parse(loadData["Health"].ToString());
                data.size = float.Parse(loadData["Size"].ToString());
                data.effectVal = int.Parse(loadData["EffectVal"].ToString());
                data.modelProdOrder = int.Parse(loadData["ModelPordOrder"].ToString());

                Debug.Log($"Data: ");
                Debug.Log($"birth : {data.birth} ");
                Debug.Log($"modelID : {data.modelID} ");
                Debug.Log($"nickName : {data.nickName} ");
                Debug.Log($"clean : {data.clean} ");
                Debug.Log($"health : {data.health} ");
                Debug.Log($"size : {data.size} ");
                Debug.Log($"effectVal : {data.effectVal} ");
                Debug.Log($"modelProdOrder : {data.modelProdOrder} ");
            }
            else
            {
                Debug.Log("해당하는 데이터를 로드하지 못했습니다"); 
            }
        });
    }
}
