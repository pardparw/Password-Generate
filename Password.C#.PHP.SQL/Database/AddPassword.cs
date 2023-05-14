using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;

public class AddPassword : MonoBehaviour
{
    public TMP_InputField site;
    public TMP_InputField username;
    public TMP_InputField password; 
    public TMP_InputField Lenght;
    public TMP_Text warn;
    
    Net _net = new Net();

    void Start(){
        StartCoroutine(_net.NetGetFunc("view.php"));
        
    }

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
    public void Random(){
        if(Lenght.text != ""){
            password.text = "";
            int Langht = Convert.ToInt16(Lenght.text);
            string randomPassword = GenerateRandomPassword(Langht);
            password.text = randomPassword;
        }else{
            warn.text = "Input Lenght";
        }
    }
    public void Genarate(){
        if(site.text != "" && username.text != "" && password.text != ""){
            StartCoroutine(Register(site:site.text, username:username.text, password:password.text));
            site.text = "";
            username.text = "";
            password.text = "";
            Lenght.text = "";
        }else{
            warn.text = "Input all box";
        }    
    }
     IEnumerator Register(string site, string username, string password){
        WWWForm form = new WWWForm();
            form.AddField("loginSite", site);
            form.AddField("loginUser", username);
            form.AddField("loginPassword", password);

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/passman/Add.php", form)) {
                yield return www.SendWebRequest();

                if(www.isNetworkError || www.isHttpError) {
                    Debug.Log(www.error);
                    
                    
                }
                else{
                    Debug.Log(www.downloadHandler.text);
                    if(www.downloadHandler.text == "User is already taken"){
                        warn.text = "User is already taken";
                    }
                }
            }
        }
}
