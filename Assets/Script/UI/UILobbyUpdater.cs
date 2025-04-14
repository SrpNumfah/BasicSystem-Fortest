using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UILobbyUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text diamond_Text;
    [SerializeField] private Slider heart_Slider;
    

    public void UpdateLobbyUI(PlayerData data)
    {
        Debug.Log("Updating UI: Diamond = " + data.diamond + ", Heart = " + data.heart);
        diamond_Text.text = data.diamond.ToString();
        heart_Slider.value = data.heart;

    }
}
