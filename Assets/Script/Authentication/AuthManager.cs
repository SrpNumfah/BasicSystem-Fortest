using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;





public class AuthManager : MonoBehaviour
{
    [Header("URL")]
    public string login_URL = "http://localhost:8080/login.php";
    public string register_URL = "http://localhost:8080/register.php";

    public IEnumerator Login(string username, string password, System.Action<bool> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        Debug.Log($"Sending Login Request: {username} / {password}");

        UnityWebRequest www = UnityWebRequest.Post(login_URL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string result = www.downloadHandler.text;
            Debug.Log("Received JSON: " + result);

            LoginResponse response = JsonUtility.FromJson<LoginResponse>(result);
            response.message = System.Text.RegularExpressions.Regex.Unescape(response.message);
            Debug.Log("Decoded message: " + response.message);
            if (response.success)
            {
               
                Debug.Log("Login success, changing scene...");
                callback(true);
            }
            else
            {
                Debug.LogError($"Login Failed: {response.message}");
                callback(false);
            }
        }
        else
        {
            Debug.LogError($"Error: {www.error}");
            callback(false);
        }

    }

    public IEnumerator Register(string username, string password, string confirmPassword, System.Action<bool> callback)
    {
        if (password != confirmPassword)
        {
            MessageBox.Instace.ShowMessage("Passwords do not match");
            callback(false);
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post(register_URL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string result = www.downloadHandler.text;
            Debug.Log("Register response: " + result);

            LoginResponse response = JsonUtility.FromJson<LoginResponse>(result);
            if (response.success)
            {
                PlayerPrefs.SetString("username", username);
                PlayerPrefs.Save();
                MessageBox.Instace.ShowMessage("Register success, please login");
                callback(true);
                
            }
            else
            {
                MessageBox.Instace.ShowMessage("Register failed: " + response.message);
                callback(false);
            }
        }
        else
        {
            MessageBox.Instace.ShowMessage("Connection error: " + www.error);
            callback(false);
        }


    }
}
[System.Serializable]
public class LoginResponse
{
    public bool success;
    public string message;
}