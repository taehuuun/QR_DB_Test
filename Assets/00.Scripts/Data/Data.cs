using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

[System.Serializable]
[FirestoreData]
public class Data
{
    [FirestoreProperty] public int effectVal {get; set;}
    [FirestoreProperty] public int modelProdOrder {get; set;}
    [FirestoreProperty] public string birth {get; set;}
    [FirestoreProperty] public string clean {get; set;}
    [FirestoreProperty] public string health {get; set;}
    [FirestoreProperty] public string modelID {get; set;}
    [FirestoreProperty] public string nickName {get; set;}
    [FirestoreProperty] public string size {get; set;}
}
