using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyManager : MonoBehaviour
{
    public UILobbyUpdater uiLobbyUpdater;

    private void Start()
    {
        string username = PlayerPrefs.GetString("username");
        if (string.IsNullOrEmpty(username))
        {
            Debug.LogError("Username is not set in PlayerPrefs");
            return;
        }
        UserData();
    }

   
    public void UserData()
    {
        StartCoroutine(GetUserData());
    }
    IEnumerator GetUserData()
    {
        string username = PlayerPrefs.GetString("username");
        if (string.IsNullOrEmpty(username))
        {
            Debug.LogError("Username is not set in PlayerPrefs");
            yield break;
        }

        string url = "http://localhost:8080/get_user_data.php?username=" + PlayerPrefs.GetString("username");
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            Debug.Log("Received JSON: " + json);
            PlayerData player = JsonUtility.FromJson<PlayerData>(json);

            Debug.Log("Diamond" + player.diamond);
            Debug.Log("Heart" + player.diamond);

            if (player != null)
            {
                uiLobbyUpdater.UpdateLobbyUI(player);
            }
            else
            {
                Debug.Log("Failed to parse PlayerData from JSON");
            }

        }
        else
        {
            Debug.LogError("Error retrieving data: " + www.error);
        }
    }
}


