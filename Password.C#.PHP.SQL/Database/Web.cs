using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using TMPro;
using System.Text;
public class Web : MonoBehaviour
{
    public GameObject UserInfo;
    public GameObject UserTemplate;
    public GameObject Confrimobj;
    public RectTransform Content;
    public void Load(){
        DeleatItem();
        StartCoroutine(GetData());  
    }
    void Start(){
        
       // StartCoroutine(Register("googles", "pardprews", "12s34"));
        //StartCoroutine(Deleat(2));
        //StartCoroutine(Edit(5, "gossa", "poppula", "123455pass"));
    }
    IEnumerator GetData(){//Load Data from list
            using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/passman/view.php")) {
                yield return www.SendWebRequest();

                if(www.isNetworkError || www.isHttpError) {
                    Debug.Log(www.error);
                    warn.text = www.error;
                }
                else{
                   /* //Show Result text
                    Debug.Log(www.downloadHandler.text);

                    //Or retrieve result as binary data
                    byte[] result = www.downloadHandler.data;*/
                    string rawresponse = www.downloadHandler.text;
                    string[] user = rawresponse.Split('*');
                    for (int i = 0; i < user.Length; i++) {
                        
                        if(user[i] != ""){
                            string[] userinfo = user[i].Split(',');
                        Debug.Log("ID: " + userinfo[0] + " site: " + userinfo[1] + " username: " + userinfo[2] + " passowrd: " + userinfo[3]);
                        

                        GameObject gobj = (GameObject)Instantiate(UserTemplate);
                        gobj.transform.SetParent(UserInfo.transform);
                        gobj.GetComponent<ListUI>().id.text = userinfo[0];
                        gobj.GetComponent<ListUI>().site.text = userinfo[1];
                        gobj.GetComponent<ListUI>().username.text = userinfo[2];
                        gobj.GetComponent<ListUI>().password.text = userinfo[3];
                        gobj.GetComponent<ListUI>().Deleat.onClick.AddListener(()=> Confirm(id:Convert.ToInt16(userinfo[0]), name:userinfo[1]));
                        gobj.GetComponent<ListUI>().Edit.onClick.AddListener(()=> StartCoroutine(Edituser(id:Convert.ToInt16(userinfo[0]))));
                        
                        }
                    }
                }
            }
        }
    void Confirm(int id, string name){
        //Debug.Log(id);
        GameObject gobj = (GameObject)Instantiate(Confrimobj);
        Debug.Log(name);
        Confrimobj.SetActive(true);
        //gobj.GetComponent<NameUI>().sitename.text = "sss";
        Confrimobj.transform.Find("name").GetComponent<TMP_Text>().text = name;
        Confrimobj.transform.Find("Confirm").GetComponent<Button>().onClick.RemoveAllListeners();
        Confrimobj.transform.Find("Cancle").GetComponent<Button>().onClick.RemoveAllListeners();
        Confrimobj.transform.Find("Confirm").GetComponent<Button>().onClick.AddListener(() => StartCoroutine(Deleat(id:id)));
        Confrimobj.transform.Find("Cancle").GetComponent<Button>().onClick.AddListener(() => Confrimobj.SetActive(false));
    }
   
   public void DeleatItem()
    {
        for (int i = 0; i < Content.childCount; i++)
        {
            Debug.Log("Delllllllllllllllllllet");
            Destroy(Content.transform.GetChild(i).gameObject);
        }
    }

  

     IEnumerator Deleat(int id){
        WWWForm form = new WWWForm();
            form.AddField("IDS", id);
            

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/passman/Deleat.php", form)) {
                yield return www.SendWebRequest();

                if(www.isNetworkError || www.isHttpError) {
                    Debug.Log(www.error);
                    warn.text = www.error;
                }
                else{
                    Debug.Log(www.downloadHandler.text);
                    Confrimobj.SetActive(false);
                    DeleatItem();
                    StartCoroutine(GetData());
                }
            }
        }

        //Edit Data
    public TMP_InputField site;
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_InputField Lenght;
    public GameObject ListUI;
    public GameObject EditUi;
    public TMP_Text warn; 
  
    string result;
    
       
        
    int IDS;
    //Random password
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
    public void Save(){
        StartCoroutine(Edit(id:IDS, site:site.text, user:username.text, password:password.text));
    }
    IEnumerator Edituser(int id){
    Debug.Log(id);
        StartCoroutine(View(id:id));
    yield return new WaitForSeconds(1);
        ListUI.SetActive(false);
        EditUi.SetActive(true);
        string rawresponse = result;
        string[] user = rawresponse.Split(',');
        site.text = user[0];
        username.text = user[1];
        password.text = user[2];
        IDS = id;
    }
   
    IEnumerator View(int id){
        WWWForm form = new WWWForm();
            form.AddField("IDS", id);

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/passman/viewid.php", form)) {
                yield return www.SendWebRequest();

                if(www.isNetworkError || www.isHttpError) {
                    Debug.Log(www.error);
                    warn.text = www.error;
                }
                else{
                    result = www.downloadHandler.text;
                    Debug.Log(www.downloadHandler.text);
                }
            }
        }
    IEnumerator Edit(int id, string site, string user, string password){
        WWWForm form = new WWWForm();
            form.AddField("SiteName", site);
            form.AddField("UserName", user);
            form.AddField("Password", password);
            form.AddField("IDS", id);

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/passman/Edit.php", form)) {
                yield return www.SendWebRequest();

                if(www.isNetworkError || www.isHttpError) {
                    Debug.Log(www.error);
                    warn.text = www.error;
                }
                else{
                    Debug.Log(www.downloadHandler.text);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////
      
        

}
