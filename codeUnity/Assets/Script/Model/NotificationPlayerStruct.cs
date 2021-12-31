using Firebase.Firestore;

[FirestoreData]
public class NotificationPlayerStruct
{
    [FirestoreProperty]
    public string content_Notification{get; set;}
    [FirestoreProperty]
    public string sentID_Notification{get; set;}
    [FirestoreProperty]
    public bool status_Notification{get; set;}
    [FirestoreProperty]
    public string title_Notification{get; set;}
    [FirestoreProperty]
    public int type_Notification{get; set;}
}
