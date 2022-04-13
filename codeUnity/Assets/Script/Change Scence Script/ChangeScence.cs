using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScence : MonoBehaviour
{
    //Wait for reoading excutes
    private float waitToLoad;

    public void reloadScence()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void storeOpening()
    {
        SceneManager.LoadScene("Store");
    }

    public void achiveOpening()
    {
        SceneManager.LoadScene("ACHIEVEMENT");
    }

    public void howToPlayOpening() //khuong
    {
        SceneManager.LoadScene("how to play");
    }

    public void leaderGlobalOpening() //khuong
    {
        SceneManager.LoadScene("Leaderboard Citizen");
    }

    public void leaderLocalOpening() //khuong
    {
        SceneManager.LoadScene("Leaderboard Level");
    }

    public void notificationOpening() //khuong
    {
        SceneManager.LoadScene("Notification");
    }

    public void backtoMainPage()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MainPage");
    }

    public void openProfile()
    {
        SceneManager.LoadScene("Main screen");
    }
    public void openLogin()
    {
        SceneManager.LoadScene("Login - Main theme");
    }

    public void gameplayOpening()
    {
        //PlayerStruct player = SaveSystem.LoadDataPlayer();
        if (Player_DataManager.Instance.Player.level.stage == 0)
        {
            Screen.orientation = ScreenOrientation.Landscape;
            foreach (var i in Item_DataManager.Instance.loadItemBuff())
            {
                if (i.name_Item.Equals("Heal"))
                {
                    Player_DataManager.Instance.playerCharacter.setBuff(i);
                }
            }
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
            SceneManager.LoadScene("StageList");
        }
    }
}

