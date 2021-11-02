using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.UI;
//using Firebase.Unity.Editor;
public class DataConnect : MonoBehaviour
{
    private Players data;
    public GameObject playerName;
    private string dataURL = "https://ltd2k-fptk14-default-rtdb.asia-southeast1.firebasedatabase.app/";


    // Start is called before the first frame update
    void Start()
    {
        //FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(dataURL);
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        //LoadData();

    }

    // Update is called once per frame
    void Update()
    {
        LoadData();
    }

    public void LoadData()
    {
        FirebaseDatabase.DefaultInstance
      .GetReference("user")
      .GetValueAsync().ContinueWithOnMainThread(task =>
      {
          if (task.IsFaulted)
          {
              // Handle the error...

          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              // Do something with snapshot...
              string playerData = snapshot.GetRawJsonValue();
              foreach (var child in snapshot.Children)
              {
                  string printer = child.GetRawJsonValue();

                  Players extract = JsonUtility.FromJson<Players>(printer);
                  //playerName.GetComponent<UnityEngine.UI.Text>().text = printer.ToString();

                  playerName.GetComponent<UnityEngine.UI.Text>().text = extract.playerName;

                  // print("Extrac is: " + extract.playerName);


              }


          }
      });
    }
}
