using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session_Checking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Setting up Firebase Auth" + SystemInfo.deviceUniqueIdentifier);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Setting up Firebase Auth" + SystemInfo.deviceUniqueIdentifier);
    }
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth" + SystemInfo.deviceUniqueIdentifier);
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
        Debug.Log("Infomration " + user.UserId);
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        Debug.Log("Run this");
        if (auth.CurrentUser != user)
        {
            Debug.Log("Run this2");
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            Debug.Log("Run this3");
            if (!signedIn && user != null)
            {
                Debug.Log("Run this4");
                Debug.Log("Signed out " + user.UserId);
            }
            Debug.Log("Run this5");
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Run this6");
                Debug.Log("Signed in " + user.UserId);
            }
            Debug.Log("Run this7");
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;

    }
}
