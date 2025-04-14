using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [Header("Display Text Login")]
    [SerializeField] private TMP_Text loging_usernameText;
    [SerializeField] private TMP_Text loging_passwordText;
    [SerializeField] private TMP_Text loging_SignUpButton_Text;
    [SerializeField] private TMP_Text loging_LoginButton_Text;

    [Header("Display Text SignUp")]
    [SerializeField] private TMP_Text singUp_user_Text;
    [SerializeField] private TMP_Text singUp_password_Text;
    [SerializeField] private TMP_Text singUp_confirmPassword_Text;
    [SerializeField] private TMP_Text singUp_signUpButton_Text;

    



    private void Start()
    {
        LoginPanelDisplayText();
        SingUPDisplayText();
        
    }

    #region private
    private void LoginPanelDisplayText()
    {
        loging_usernameText.text = "Use<size=80%>R</size>name";
        loging_passwordText.text = "Passwo<size=80%>R</size><size=80%>D</size>";
        loging_SignUpButton_Text.text = "Sin<size=80%>G</size>" + " " + "Up";
        loging_LoginButton_Text.text = "Lo<size=80%>G</size>" + " " + "In";
    }

    private void SingUPDisplayText()
    {
        singUp_user_Text.text = "Use<size=80%>R</size>name";
        singUp_password_Text.text = "Passwo<size=80%>R</size><size=80%>D</size>";
        singUp_confirmPassword_Text.text = "Con<size=80%>FIRM</size>" + " " + "Pass<size=80%>WORD</size>";
        singUp_signUpButton_Text.text = "Sin<size=80%>G</size>" + " " + "Up";
    }

   
    #endregion
}
