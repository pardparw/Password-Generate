using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Pin : MonoBehaviour
{
    
    public TMP_InputField PIN_In;
    public TMP_Text Wran;

    public void ChekPin(){
        
        if(PIN_In.text.Length == 6){
            //SceneManager.LoadScene("main");
            StartCoroutine(Register(password:PIN_In.text));
        }else{
            Wran.text = "";
        }
    }
    IEnumerator Register(string password){
        WWWForm form = new WWWForm();
            
            form.AddField("loginPassword", password);

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/passman/login.php", form)) {
                yield return www.SendWebRequest();

                if(www.isNetworkError || www.isHttpError) {
                    Debug.Log(www.error);
                    
                    
                }
                else{
                    Debug.Log(www.downloadHandler.text);
                    if(www.downloadHandler.text == "1"){
                        Wran.text = "OK";
                        SceneManager.LoadScene("main");
                    }else{
                        Wran.text = "Wrong Password";
                    }
                }
            }
        }
}
