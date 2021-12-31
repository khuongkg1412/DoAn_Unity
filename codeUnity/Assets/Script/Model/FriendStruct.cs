using Firebase.Firestore;

[FirestoreData]
public class friendStruct
{
    [FirestoreProperty]
    public bool accept_Friend { get; set; }
    [FirestoreProperty]
    public bool notificationID { get; set; }
}
