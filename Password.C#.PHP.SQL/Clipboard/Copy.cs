using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Copy : MonoBehaviour
{
   public TMP_InputField Passwords;

   public void CopyToClipBoard(){
        TextEditor TextED = new TextEditor();
        TextED.text = Passwords.text;
        TextED.SelectAll();
        TextED.Copy();
   }

   public void PasteFromClipBoard(){
        TextEditor TextED = new TextEditor();
        TextED.multiline = true;
        TextED.Paste();
        Passwords.text = TextED.text;
   }
}
