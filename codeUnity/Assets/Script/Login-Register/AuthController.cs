using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthController : MonoBehaviour
{
    public Text

            emailInput,
            passwordInput,
            debugMessage;

    bool isDone = false;

    public void goToLoginPage()
    {
        SceneManager.LoadScene(10);
    }

    public void goToRegisterPage()
    {
        SceneManager.LoadScene(11);
    }

    public void goToMainPage()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator LoginEmail()
    {
        Debug
            .Log("Logining. Email: " +
            emailInput.text +
            ", Password: " +
            passwordInput.text);
        FirebaseAuth
            .DefaultInstance
            .SignInWithEmailAndPasswordAsync(emailInput.text,
            passwordInput.text)
            .ContinueWith((
            task =>
            {
                Debug
                    .Log("Start Login: Email: " +
                    emailInput.text +
                    ", Password: " +
                    passwordInput.text);
                if (task.IsCanceled)
                {
                    Firebase.FirebaseException e =
                        task.Exception.Flatten().InnerExceptions[0] as
                        Firebase.FirebaseException;

                    GetErrorMessage((AuthError) e.ErrorCode);
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.Log("Login failed");
                    Firebase.FirebaseException e =
                        task.Exception.Flatten().InnerExceptions[0] as
                        Firebase.FirebaseException;
                    GetErrorMessage((AuthError) e.ErrorCode);
                    return;
                }

                if (task.IsCompleted)
                {
                    print("Login Completed!");
                    isDone = true;
                }
            }
            ));
        yield return new WaitUntil(() => isDone == true);
        goToMainPage();
        yield return null;
    }

    public void Login()
    {
        StartCoroutine("LoginEmail");
    }

    public void Login_Anonymous()
    {
    }

    public void Register()
    {
        Debug
            .Log("Registering: Email: " +
            emailInput.text +
            ", Password: " +
            passwordInput.text);
        if (emailInput.text.Equals("") && passwordInput.Equals(""))
        {
            print("Please enter a valid email and password!");
            return;
        }

        FirebaseAuth
            .DefaultInstance
            .CreateUserWithEmailAndPasswordAsync(emailInput.text,
            passwordInput.text)
            .ContinueWith((
            task =>
            {
                Debug
                    .Log("Start Register: Email: " +
                    emailInput.text +
                    ", Password: " +
                    passwordInput.text);
                if (task.IsCanceled)
                {
                    Firebase.FirebaseException e =
                        task.Exception.Flatten().InnerExceptions[0] as
                        Firebase.FirebaseException;

                    GetErrorMessage((AuthError) e.ErrorCode);
                    return;
                }

                if (task.IsFaulted)
                {
                    Firebase.FirebaseException e =
                        task.Exception.Flatten().InnerExceptions[0] as
                        Firebase.FirebaseException;
                    GetErrorMessage((AuthError) e.ErrorCode);
                    return;
                }

                if (task.IsCompleted)
                {
                    print("Registration Completed!");
                }
            }
            ));
        goToMainPage();
    }

    public void Logout()
    {
    }

    void GetErrorMessage(AuthError errCode)
    {
        string msg = "";
        msg = errCode.ToString();

        // switch(errCode){
        //     case AuthError.AccountExistsWithDifferentCredentials:

        //     break;
        //     case AuthError.MissingPassword:
        //     break;
        //     case AuthError.WrongPassword:
        //     break;
        //     case AuthError.InvalidEmail:
        //     break;
        //     case AuthError.UserDisabled:
        //     break;
        //     case AuthError.MissingEmail:
        //     break;
        // }
        debugMessage.text = msg;
        print (msg);
    }
}
