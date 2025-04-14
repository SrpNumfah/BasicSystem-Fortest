using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TMP_Text loadingText;

    private void Start()
    {
        StartCoroutine(LoadDataAndNavigate());
    }

    IEnumerator LoadDataAndNavigate()
    {

        loadingSlider.value = 0;
        loadingText.text = "Loading...";

        string username = PlayerPrefs.GetString("username");
        if (string.IsNullOrEmpty(username))
        {
            Debug.LogError("Username is not set in PlayerPrefs");
            yield break;
        }

        string url = "http://localhost:8080/get_user_data.php?username=" + username;
        UnityWebRequest www = UnityWebRequest.Get(url);


        yield return www.SendWebRequest();

        
        float startTime = Time.time;  
        while (!www.isDone)
        {
           
            float progress = Mathf.Clamp01(www.downloadProgress); 
            loadingSlider.value = progress;
            yield return null;  
        }

        if (www.result == UnityWebRequest.Result.Success)
        {

            string json = www.downloadHandler.text;
            Debug.Log("Received JSON: " + json);


            PlayerData player = JsonUtility.FromJson<PlayerData>(json);

            

            loadingText.text = "Data loaded successfully!";
            loadingSlider.value = 1;

            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Lobby");



        }
        else
        {
            Debug.LogError("Error retrieving data: " + www.error);
            loadingText.text = "Failed to load data!";
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("Auth");
        }
    }



}
