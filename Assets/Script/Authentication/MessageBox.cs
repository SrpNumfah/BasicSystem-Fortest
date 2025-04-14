using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    public static MessageBox Instace;

    [SerializeField] private GameObject messageBox_Panel;
    [SerializeField] private TMP_Text message_Text;
    [SerializeField] private Button okButton;

    private Action OnclickOk;

    private void Awake()
    {
        if (Instace != null && Instace != this)
        {
            Destroy(gameObject);
            return;
        }

        Instace = this;
        DontDestroyOnLoad(gameObject);

       
    }
    #region public
    public void ShowMessage(string message, System.Action onOk = null)
    {
        messageBox_Panel.SetActive(true);
        message_Text.text = message;
        OnclickOk = onOk;

        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(() =>
        {
            HideMessage();
            OnclickOk?.Invoke();
            OnclickOk = null;
        });
      
    }
    #endregion

    #region Private
    private void  HideMessage()
    {
        messageBox_Panel.SetActive(false);
    }
    #endregion
}
