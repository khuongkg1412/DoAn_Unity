using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using UnityEngine.SceneManagement;
public class AuthController : MonoBehaviour
{
    public Text emailInput, passwordInput;


    public void goToLoginPage(){
        SceneManager.LoadScene(1);
    }
    public void goToRegisterPage(){
        SceneManager.LoadScene(2);
    }
    public void Login()
    {
        Debug.Log("Logining. Email: " + emailInput.text + ", Password: " + passwordInput.text);
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync
        (emailInput.text, passwordInput.text).ContinueWith((task =>
        {
            Debug.Log("Start Login: Email: " + emailInput.text + ", Password: " + passwordInput.text);
            if (task.IsCanceled)
            {
                return;
            }

            if (task.IsFaulted)
            {
                Debug.Log("Login failed");
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0]
                as Firebase.FirebaseException;

                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsCompleted)
            {

            }


        }));
    }

    public void Login_Anonymous()
    {

    }
    public void Register()
    {
        Debug.Log("Registering: Email: " + emailInput.text + ", Password: " + passwordInput.text);
        if (emailInput.text.Equals("") && passwordInput.Equals(""))
        {
            print("Please enter a valid email and password!");
            return;
        }
        
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailInput.text, passwordInput.text)
        .ContinueWith((task =>
        {
            Debug.Log("Start Register: Email: " + emailInput.text + ", Password: " + passwordInput.text);
            if (task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0]
                 as Firebase.FirebaseException;

                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsFaulted)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0]
                as Firebase.FirebaseException;

                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsCompleted)
            {
                print("Registration Completed!");
            }

        }));
    }
    public void Logout()
    {

    }
    void GetErrorMessage(AuthError errCode)
    {
        string msg = "";
        msg = errCode.ToString();


        print(msg);

    }

}
