using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIAuthController : MonoBehaviour
{
    public AuthManager authManager;

    [Header("Login")]
    [SerializeField] private TMP_InputField login_User;
    [SerializeField] private TMP_InputField login_Password;
    [SerializeField] private Button login_Button;
    [SerializeField] private Button login_register_Button;
    [SerializeField] private GameObject registerPanel;
    [SerializeField] private GameObject loginPanel;

    [Header("Register")]
    [SerializeField] private TMP_InputField register_User;
    [SerializeField] private TMP_InputField register_Password;
    [SerializeField] private TMP_InputField register_ConfirmPassword;
    [SerializeField] private Button register_Button;



    private void Awake()
    {
        if (authManager == null)
        {
            Debug.Log("AuthManager not found in the scene!");
        }
        login_Button.onClick.AddListener(() => OnLoginClick());
        register_Button.onClick.AddListener(() => OnRegisterClick());
        login_register_Button.onClick.AddListener(() => ChangeToRegister());
    }

    #region Private
    private void OnLoginClick()
    {
        string user = login_User.text;
        string password = login_Password.text;

        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
        {
            MessageBox.Instace.ShowMessage("Please enter both username and password.");
            return;
        }

        Debug.Log($"Attempting login: {user} / {password}");

        StartCoroutine(authManager.Login(user, password, success =>
        {
            Debug.Log($"Login success: {success}");
            if (success)
            {
              
                UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
            }
            else
            {
                MessageBox.Instace.ShowMessage("Login failed. Please check your username or password.");
            }
        }));
    }


    private void OnRegisterClick()
    {
        string user = register_User.text;
        string password = register_Password.text;
        string confirmPassword = register_ConfirmPassword.text;


        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            MessageBox.Instace.ShowMessage("Please fill in all fields.");
            return;
        }


        if (password != confirmPassword)
        {
            MessageBox.Instace.ShowMessage("Passwords do not match. Please try again.");
            return;
        }


        StartCoroutine(authManager.Register(user, password, confirmPassword, success =>
        {
            if (success)
            {
                MessageBox.Instace.ShowMessage("Registration successful! Please log in.", () =>
                {
                    registerPanel.SetActive(false);
                    loginPanel.SetActive(true);
                });
            }
            else
            {
                MessageBox.Instace.ShowMessage("Registration failed. Username might already exist. Please try again.");
            }
        }));
    }

    private void ChangeToRegister()
    {
        registerPanel.SetActive(true);
        loginPanel.SetActive(false);
    }
    #endregion
}
