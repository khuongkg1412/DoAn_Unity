using UnityEngine;
using Firebase.Firestore;


[FirestoreData]
public class playerStruct : MonoBehaviour
{
    [FirestoreProperty]
    public string avatar_Player { get; set; }
    [FirestoreProperty]
    public int coin_Player { get; set; }
    [FirestoreProperty]
    public int diamond_Player { get; set; }
    [FirestoreProperty]
    public int energy_Player { get; set; }
    [FirestoreProperty]
    public int gender_Player { get; set; }
    [FirestoreProperty]
    public int level_Player { get; set; }
    [FirestoreProperty]
    public string name_Player { get; set; }
    [FirestoreProperty]
    public int stage_Player { get; set; }
    [FirestoreProperty]
    public int xp_Player { get; set; }
}
