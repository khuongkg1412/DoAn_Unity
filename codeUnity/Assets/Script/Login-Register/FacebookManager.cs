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
    public static string ID;
    private void Start()
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
                var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
                accessToken(aToken);

            }
            else
            {
                Debug.Log("Sign in false");
            }
        }
    }

    void accessToken(AccessToken aToken)
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
            var credential = Firebase.Auth.FacebookAuthProvider.GetCredential(aToken.TokenString);
            auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
            {
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

                    checkOldPlayer(newUser.UserId);
                }
            });
        }
    }

    void checkOldPlayer(string IDPlayer)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        DocumentReference docRef = db.Collection("Player").Document(IDPlayer);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                SceneManager.LoadScene("MainPage");
                ID = IDPlayer;
            }
            else
            {
                SceneManager.LoadScene("Register by Email");
                ID = IDPlayer;
            }

        });

    }
}
