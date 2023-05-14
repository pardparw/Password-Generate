using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Net
{

    private string WEB = "http://localhost/passman/";
    public string NetResult = string.Empty;
    

    public IEnumerator NetGetFunc(string target)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(WEB + target))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("NetPost error");
            }
            else
            {
                
                //Debug.Log($"NetPost success : {www.downloadHandler.text}");
                NetResult = www.downloadHandler.text;
            
                
            }
        }
    }

}
