using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Proyecto26;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthController : MonoBehaviour
{
    public InputField emailLogin, passwordLogin, emailReg, passwordReg, confirmPasswordReg;
    public GameObject SuccessDialogue, ErrorDialogue;
    public static string ID;
    const string AuthKey = "AIzaSyAGYUKR0KqH1JnVG0xkiyxTNhVfLHWZw5o";
    bool isDone;
    GameObject ErrorToast;
    public static bool TimeisRun = false;

    IEnumerator LoginbyEmailandPass(string email, string pass)
    {
        isDone = false;

        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + pass + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + AuthKey, userData).Then(
            response =>
            {
                ID = response.localId;
                isDone = true;
            }).Catch(error =>
        {
            Debug.Log(error.Message);
            loadErrorToast("This account is not Registered or Wrong email and password!");
        });
        yield return new WaitUntil(() => isDone == true);
        SceneManager.LoadScene("MainPage");
    }

    IEnumerator RegisterbyEmailandPass(string email, string pass)
    {
        isDone = false;
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + pass + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=" + AuthKey, userData).Then(
            response =>
            {

                string emailVerification = "{\"requestType\":\"VERIFY_EMAIL\",\"idToken\":\"" + response.idToken + "\"}";
                RestClient.Post("https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=" + AuthKey, emailVerification);

                ID = response.localId;
                isDone = true;
            }).Catch(error =>
        {
            Debug.Log(error);
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
                loadErrorToast("Email cannot be empty! Please input!");
                return;
            }
            if (pass.Length == 0)
            {
                loadErrorToast("Password cannot be empty! Please input!");
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


        if (Validation.checkNullData(new string[] { email, password, confirmPassword }))
        {
            if (email.Length == 0)
            {
                loadErrorToast("Email cannot be empty! Please input!");
                return;
            }
            if (password.Length == 0)
            {
                loadErrorToast("Password cannot be empty! Please input!");
                return;
            }
            else if (password.Length < 8)
            {
                loadErrorToast("Password must have 8 characters! Please input more!");
                return;
            }

            if (confirmPassword.Length == 0)
            {
                loadErrorToast("Confirm password cannot be empty! Please input!");
                return;
            }
        }
        else if (!Validation.checkRegisterFormat(email, password, confirmPassword))
        {

            if (!Validation.checkEmailFormat(email))
            {
                loadErrorToast("Email must be in correct format. (Ex: thanh@gmail.com)");
                return;
            }

            if (!Validation.checkStrongPassword(password))
            {
                loadErrorToast("Your password must contain at least 1 uppercase, 1 lowercase letter, 1 digit, 1 special character.");
                return;
            }

            if (!Validation.checkConfirmPassword(password, confirmPassword))
            {
                loadErrorToast("The confirm password did not match.");
                return;
            }

        }
        else StartCoroutine(changeSnece(email, password));
    }

    IEnumerator changeSnece(string email, string password)
    {
        StartCoroutine(RegisterbyEmailandPass(email, password));

        yield return new WaitUntil(() => isDone == true);
        SceneManager.LoadScene("Create Character");
        yield return null;
    }

    private void loadErrorToast(string message)
    {
        ErrorToast = (GameObject)Instantiate(ErrorDialogue, transform);
        ErrorToast.transform.Find("Message").gameObject.GetComponent<Text>().text = message;
        Destroy(ErrorToast, 2);
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
