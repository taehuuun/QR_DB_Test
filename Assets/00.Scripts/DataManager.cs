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

    /// <summary>
    /// QR�����Ͱ� DB�� �����Ϳ� �ִ��� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <param name="QRdata">QR��ĵ ������</param>
    private void ValidationData(string QRdata)
    {
        // QR�����Ͱ� null�̰ų� ���̰� 2���� ���� ��� ����
        if(QRdata == null || QRdata.Length < 2)
            return;

        string modelType = QRdata.Substring(0,2);
        string shaVal = QRdata.Substring(2,QRdata.Length);

        DocumentReference docRef = db.Collection(modelType).Document(shaVal);
    }
}
