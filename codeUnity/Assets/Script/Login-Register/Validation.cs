using System.Text.RegularExpressions;
using UnityEngine;

public class Validation : MonoBehaviour
{
    public static bool checkNullData(string[] strings)
    {
        foreach (string s in strings)
        {
            if (s.Length == 0)
            {
                return true;
            }
        }
        return false;
    }


    public static bool checkFullNameFormat(string fullName)
    {
        string specialCharacters = "~`!@#$%^&*()-_=+[{]}\\|;:'\"<>,./?*";
        for (int i = 0; i < fullName.Length; i++)
        {
            if (specialCharacters.IndexOf(fullName[i]) != -1 || (fullName[i] >= 48 && fullName[i] <= 57))
            {
                return false;
            }
        }
        return true;
    }

    public static bool checkEmailFormat(string email)
    {
        string regexEmail = ("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
        return Regex.IsMatch(email, regexEmail);
    }

    public static bool checkStrongPassword(string password)
    {
        bool checkDigit = false;
        bool checkLowercase = false;
        bool checkUppercase = false;
        bool checkSpecialCharacter = false;

        if (password.Length < 8)
            return false;

        for (int i = 0; i < password.Length; i++)
        {
            char c = password[i];
            if (char.IsDigit(c))
                checkDigit = true;
            else if (char.IsLower(c))
                checkLowercase = true;
            else if (char.IsUpper(c))
                checkUppercase = true;
            else if (!char.IsLetterOrDigit(c) && c != 32)
                checkSpecialCharacter = true;
        }
        return checkDigit && checkLowercase && checkUppercase && checkSpecialCharacter;
    }

    public static bool checkConfirmPassword(string password, string confirmPassword)
    {
        return password.Equals(confirmPassword);
    }


    public static bool checkRegisterFormat(string email, string password, string confirmPassword)
    {
        return checkEmailFormat(email) && checkStrongPassword(password) && checkConfirmPassword(password, confirmPassword);
    }
}
