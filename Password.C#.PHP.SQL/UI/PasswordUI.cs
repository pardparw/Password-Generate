using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PasswordUI : MonoBehaviour
{

    private bool Showpass = false;
    public TMP_InputField PassIn;
    public RawImage EyeUI;
    public Texture EyeOff;
    public Texture EyeOn;

    public void ViewPass(){
        if(Showpass){
            Showpass = false;
            PassIn.contentType = TMP_InputField.ContentType.Password;
            EyeUI.texture = EyeOff;
        }else{
            Showpass = true;
            PassIn.contentType = TMP_InputField.ContentType.Standard;
            EyeUI.texture = EyeOn;
        }
    }
        


}
