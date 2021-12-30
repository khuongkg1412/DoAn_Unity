using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthController : MonoBehaviour
{
    public InputField emailLogin, passwordLogin, emailReg, passwordReg, confirmPasswordReg;
    
    public Text debugMessage;
    public static string ID;
    bool isDone;

    IEnumerator LoginbyEmailandPass(string email, string pass)
    {
        isDone = false;
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignInWithEmailAndPasswordAsync(email, pass).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsFaulted)
            {
                Debug.Log("Login failed");
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsCompleted)
            {
                print("Login Completed!");
                isDone = true;
            }
        });
        yield return new WaitUntil(() => isDone == true);

        yield return null;
    }

    IEnumerator RegisterbyEmailandPass(string email, string pass)
    {
        isDone = false;
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsFaulted)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsCompleted)
            {
                print("Registration Completed!");
                ID = auth.CurrentUser.UserId;
                isDone = true;
            }
        });
        
        yield return null;
    }

    public void Login()
    {
        string email = emailLogin.text.Trim();
        string pass = passwordLogin.text.Trim();

        if (Validation.checkNullData(new string[] { email, pass }))
        {
            if (email.Length == 0)
            {
                debugMessage.text = "Email cannot be empty! Please input!";
            }
            if (pass.Length == 0)
            {
                debugMessage.text = "Password cannot be empty! Please input!";
            }
        }
        else
        {
            StartCoroutine(LoginbyEmailandPass(email, pass));
        }
    }

    public void Register()
    {
        string email = emailReg.text.Trim();
        string password = passwordReg.text.Trim();
        string confirmPassword = confirmPasswordReg.text.Trim();

        if (Validation.checkNullData(new string[] { email, password, confirmPassword }))
        {

            if (email.Length == 0)
            {
                Debug.Log("Email cannot be empty! Please input!");
            }

            if (password.Length == 0)
            {
                Debug.Log("Password cannot be empty! Please input!");
            }

            if (confirmPassword.Length == 0)
            {
                Debug.Log("Confirm password cannot be empty! Please input!");
            }
        }
        else if (!Validation.checkRegisterFormat(email, password, confirmPassword))
        {

            if (!Validation.checkEmailFormat(email))
            {
                Debug.Log("Email must be in correct format. (Ex: thanh@gmail.com)");
            }

            if (!Validation.checkStrongPassword(password))
            {
                Debug.Log("Your password is not strong enough. It must contain at least 1 uppercase, 1 lowercase letter, 1 digit, and 1 special character.");
            }

            if (!Validation.checkConfirmPassword(password, confirmPassword))
            {
                Debug.Log("The confirm password did not match.");
            }

        }
        else StartCoroutine(changeSnece(email,password));
    }

    IEnumerator changeSnece(string email, string password){
        StartCoroutine (RegisterbyEmailandPass(email, password));
        yield return new WaitUntil(() => isDone == true);
        SceneManager.LoadScene("Register by Email");
        yield return null;
    }


    void GetErrorMessage(AuthError errCode)
    {
        string msg = "";
        msg = errCode.ToString();

        switch (errCode)
        {
            case AuthError.MissingPassword:
                break;
            case AuthError.WrongPassword:
                break;
            case AuthError.InvalidEmail:
                break;
            case AuthError.UserDisabled:
                break;
            case AuthError.MissingEmail:
                break;
        }
        debugMessage.text = msg;
        print(msg);
    }
}
