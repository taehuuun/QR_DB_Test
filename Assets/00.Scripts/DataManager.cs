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
        #region �̱��� 
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
    /// QR�����Ͱ� DB���� �ִ� ���������� �˻�
    /// </summary>
    /// <param name="QRData">QR�ڵ��� ������</param>
    private bool QRDataValidation(string QRData)
    {
        // �����Ͱ� null�̰ų� ���̰� 2���� ������� �Լ� ����
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
                return true;
            }
            else
            {
                Debug.Log($"QRData | type : {modelType} crptoData : {cryptoData} ��ȿ�� �����Ͱ� �ƴմϴ�");
                return false;
            }
        });

        return false;
    }
}
