using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class DiamondManager : MonoBehaviour
{
    [SerializeField] private Button plusDiamond_Button;
    [SerializeField] private int diamond;

    private void Awake()
    {
        plusDiamond_Button.onClick.AddListener(() => OnClickDiamond());
    }

    #region Private
    private void OnClickDiamond()
    {
        StartCoroutine(AddDiamond());
    }

    IEnumerator AddDiamond()
    {

        WWWForm form = new WWWForm();
        form.AddField("username", PlayerPrefs.GetString("username"));
        form.AddField("amount", 100);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/add_diamond.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Diamond updated"+ diamond);
            FindObjectOfType<LobbyManager>().UserData(); 
        }

        diamond++;
    }
    #endregion
}
