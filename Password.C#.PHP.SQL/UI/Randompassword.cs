using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text;

public class Raondompassword : MonoBehaviour
{
    public TMP_InputField Lenght;
    public TMP_InputField password;
    public TMP_Text warn;
   private const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&()";

    // Generate a random password
    public string GenerateRandomPassword(int length)
    {
        StringBuilder password = new StringBuilder();
        System.Random random = new System.Random();

        for (int i = 0; i < length; i++)
        {
            int index = random.Next(0, allowedChars.Length);
            password.Append(allowedChars[index]);
        }

        return password.ToString();
        
        
    }
    public void Randoms(){
        if(Lenght.text != ""){
            password.text = "";
            int Langht = Convert.ToInt16(Lenght.text);
            string randomPassword = GenerateRandomPassword(Langht);
            password.text = randomPassword;
        }else{
            warn.text = "Input Lenght";
        }
    }
    public void CopyToClipBoard(){
        TextEditor TextED = new TextEditor();
        TextED.text = password.text;
        TextED.SelectAll();
        TextED.Copy();
   }
}
