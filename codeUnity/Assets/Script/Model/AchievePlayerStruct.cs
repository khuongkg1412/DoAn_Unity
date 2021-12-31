using Firebase.Firestore;

[FirestoreData]
public class AchievePlayerStruct
{
    [FirestoreProperty]
    public bool achived_Player { get; set; }
    [FirestoreProperty]
    public int progress_Player { get; set; }
}
