using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FacebookManager : MonoBehaviour
{
    void Awake()
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
                Debug.Log(aToken.UserId);
                AccessToken token = AccessToken.CurrentAccessToken;
                Firebase.Auth.Credential credential = Firebase.Auth.FacebookAuthProvider.GetCredential(token.TokenString);
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
        // else
        
            auth.SignInWithCredentialAsync(firebaseResult).ContinueWith(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                    // TODO: Show error message to player
                    return;
                }
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("Credentials successfully created Firebase user: {0} ({1})", newUser.DisplayName, newUser.UserId);
                    SceneManager.LoadScene(0);
            });
        
    }
}
