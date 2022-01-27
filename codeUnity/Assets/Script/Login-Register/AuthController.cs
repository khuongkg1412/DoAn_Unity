using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthController : MonoBehaviour
{
    public InputField emailLogin, passwordLogin, emailReg, passwordReg, confirmPasswordReg;
    public GameObject SuccessDialogue, ErrorDialogue;
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
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            if (task.IsCompleted)
            {
                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
                isDone = true;
                ID = auth.CurrentUser.UserId;
            }
        });
        yield return new WaitUntil(() => isDone == true);
        SceneManager.LoadScene("MainPage");
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

    GameObject ErrorToast;
    public void Login()
    {
        string email = emailLogin.text.Trim();
        string pass = passwordLogin.text.Trim();


        if (Validation.checkNullData(new string[] { email, pass }))
        {
            ErrorToast = (GameObject)Instantiate(ErrorDialogue, transform); Debug.Log("Ko the qua day");
            if (email.Length == 0)
            {
                string Erorr = "Email cannot be empty! Please input!";
                ErrorToast.transform.Find("Message").gameObject.GetComponent<Text>().text = Erorr;
                Destroy(ErrorToast, 2);
                Debug.Log("Ko the qua day 1");
                return;
            }
            if (pass.Length == 0)
            {
                string Erorr = "Password cannot be empty! Please input!";
                ErrorToast.transform.Find("Message").gameObject.GetComponent<Text>().text = Erorr;
                Destroy(ErrorToast, 2);
                Debug.Log("Ko the qua day 2");
                return;
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
        ErrorToast = (GameObject)Instantiate(ErrorDialogue, transform);

        if (Validation.checkNullData(new string[] { email, password, confirmPassword }))
        {
            if (email.Length == 0)
            {
                string Erorr = "Email cannot be empty! Please input!";
                ErrorToast.transform.Find("Message").gameObject.GetComponent<Text>().text = Erorr;
                Destroy(ErrorToast, 2);

                return;
            }
            if (password.Length == 0)
            {
                string Erorr = "Password cannot be empty! Please input!";
                ErrorToast.transform.Find("Message").gameObject.GetComponent<Text>().text = Erorr;
                Destroy(ErrorToast, 2);

                return;
            }

            if (confirmPassword.Length == 0)
            {
                string Erorr = "Confirm password cannot be empty! Please input!";
                ErrorToast.transform.Find("Message").gameObject.GetComponent<Text>().text = Erorr;
                Destroy(ErrorToast, 2);

                return;
            }
        }
        else if (!Validation.checkRegisterFormat(email, password, confirmPassword))
        {

            if (!Validation.checkEmailFormat(email))
            {
                string Erorr = "Email must be in correct format. (Ex: thanh@gmail.com)";
                ErrorToast.transform.Find("Message").gameObject.GetComponent<Text>().text = Erorr;
                Destroy(ErrorToast, 2);

                return;
            }

            if (!Validation.checkStrongPassword(password))
            {
                string Erorr = "Your password is not strong enough. It must contain at least 1 uppercase, 1 lowercase letter, 1 digit, and 1 special character.";
                ErrorToast.transform.Find("Message").gameObject.GetComponent<Text>().text = Erorr;
                Destroy(ErrorToast, 2);

                return;
            }

            if (!Validation.checkConfirmPassword(password, confirmPassword))
            {

                string Erorr = "The confirm password did not match.";
                ErrorToast.transform.Find("Message").gameObject.GetComponent<Text>().text = Erorr;
                Destroy(ErrorToast, 2);

                return;
            }

        }
        else StartCoroutine(changeSnece(email, password));
    }

    IEnumerator changeSnece(string email, string password)
    {
        StartCoroutine(RegisterbyEmailandPass(email, password));
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
        string Erorr = msg;
        print(msg);
    }
}
