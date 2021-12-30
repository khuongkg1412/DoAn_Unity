using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FacebookManager : MonoBehaviour
{

    bool IsOld = true;
    bool isDone = false;
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (isGameShown)
        {
            FB.ActivateApp();
        }
    }

    public void LoginFB()
    {
        FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email" }, AuthCallback);
    }
    private void AuthCallback(ILoginResult result)
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        if (result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                // AccessToken class will have session details
                var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
                // Print current access token's User ID
                Debug.Log("I am here");
                Debug.Log(aToken.UserId);

                var credential = Firebase.Auth.FacebookAuthProvider.GetCredential(aToken.TokenString);
                Debug.Log("Taoj credential thanh cong");
                accessToken(credential);

            }
            else
            {
                Debug.Log("Sign in false");
            }
        }
    }

    public void accessToken(Credential firebaseResult)
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        Debug.Log("Auth CurrentUser: " + FirebaseAuth.DefaultInstance.CurrentUser);
        if (!FB.IsLoggedIn)
        {
            FB.ActivateApp();
            return;
        }

        // if (auth.CurrentUser != null && !string.IsNullOrEmpty(auth.CurrentUser.UserId))
        // {
        //     Debug.Log("CurrentUser ID: " + auth.CurrentUser.UserId);

        //     auth.CurrentUser.UnlinkAsync(Firebase.Auth.FacebookAuthProvider.ProviderId).ContinueWith(task => {
        //         if (task.IsCanceled || task.IsFaulted)
        //         {
        //             Debug.LogError("UnLinkWithCredentialAsync encountered an error: " + task.Exception);
        //             // TODO: Show error message to player
        //             return;
        //         }
        //     });

        //     auth.CurrentUser.LinkWithCredentialAsync(firebaseResult).ContinueWith(task =>
        //     {
        //         if (task.IsCanceled || task.IsFaulted)
        //         {
        //             Debug.LogError("LinkWithCredentialAsync encountered an error: " + task.Exception);
        //             // TODO: Show error message to player
        //             SceneManager.LoadScene(0);
        //             return;
        //         }

        //         FirebaseUser newUser = task.Result;
        //         Debug.LogFormat("User Credentials successfully linked to Firebase user: {0} ({1})", newUser.DisplayName, newUser.UserId);
        //             SceneManager.LoadScene(0);
        //     });
        // }
        else
        {
            auth.SignInWithCredentialAsync(firebaseResult).ContinueWith(task =>
            {
                Debug.Log("da den day");
                if (task.IsCanceled || task.IsFaulted)
                {
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                    // TODO: Show error message to player
                    return;
                }
                else
                {
                    FirebaseUser newUser = task.Result;
                    Debug.LogFormat("Credentials successfully created Firebase user: {0} ({1})", newUser.DisplayName, newUser.UserId);

                    StartCoroutine(changeSnece(newUser.UserId));
                }
            });
        }
    }

    IEnumerator checkOldPlayer(string IDPlayer)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        Debug.Log("Da vo đay");
        DocumentReference docRef = db.Collection("Player").Document(IDPlayer);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Debug.Log("Dang check");
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                IsOld = true;isDone = true;
            }
            else
            {
                IsOld = false;isDone = true;
            }
            
        });
        yield return null;
    }
    IEnumerator changeSnece(string IDPlayer)
    {
        StartCoroutine(checkOldPlayer(IDPlayer));
        yield return new WaitUntil(() => isDone == true);
        if (!IsOld) SceneManager.LoadScene("Register by Email");
        else SceneManager.LoadScene("MainPage");
    }
}
