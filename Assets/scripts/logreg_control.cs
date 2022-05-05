using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class logreg_control : MonoBehaviour
{
    public GameObject realmenu;
    public GameObject logreg_panel;
    public GameObject reg_panel;
    public GameObject log_panel;

    bool is_logreg = true;
    bool is_reg = false;
    bool is_log = true;

    public InputField reg_mail;
    public InputField reg_username;
    public InputField reg_password;

    public InputField log_username;
    public InputField log_password;

    public Text info_text;
    public Text username_text;

    // Start is called before the first frame update
    void Start()
    {
        logreg_panel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(is_logreg && is_log)
        {
            log_panel.SetActive(true);
        }
        if (is_logreg && is_reg)
        {
            reg_panel.SetActive(true);
        }
    }



    public void btn_log()
    {
        StartCoroutine(log_event());
    }
    IEnumerator log_event()
    {
        List<IMultipartFormSection> formdata = new List<IMultipartFormSection>();
        formdata.Add(new MultipartFormDataSection("log_username", log_username.text));
        formdata.Add(new MultipartFormDataSection("log_password", log_password.text));
        UnityWebRequest www = UnityWebRequest.Post("https://www.zenrales.net/log.php", formdata);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            if(www.downloadHandler.text==log_username.text)
            {
                reg_panel.SetActive(false);
                log_panel.SetActive(false);
                logreg_panel.SetActive(false);
                realmenu.SetActive(true);
                is_reg = false;
                is_log = false;
                is_logreg = false;
                username_text.text = www.downloadHandler.text;
            }
            else
            {
                info_text.text = www.downloadHandler.text;
                yield return new WaitForSeconds(1);

                info_text.text = "";
            }
            
            
        }
    }
    public void btn_reg()
    {
        StartCoroutine(reg_event());
       
    }
    IEnumerator reg_event()
    {
        List<IMultipartFormSection> formdata = new List<IMultipartFormSection>();
        formdata.Add(new MultipartFormDataSection("reg_mail", reg_mail.text));
        formdata.Add(new MultipartFormDataSection("reg_username", reg_username.text));
        formdata.Add(new MultipartFormDataSection("reg_password", reg_password.text));
        UnityWebRequest www = UnityWebRequest.Post("https://www.zenrales.net/reg.php", formdata);

        yield return www.SendWebRequest();
        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            info_text.text = www.downloadHandler.text;
            yield return new WaitForSeconds(1);
            reg_panel.SetActive(false);
            log_panel.SetActive(true);
            is_reg = false;
            is_log = true;
            info_text.text = "";
        }
    }

    public void btn_gotoreg()
    {
        reg_panel.SetActive(true);
        log_panel.SetActive(false);
        is_reg = true;
        is_log = false;
    }
    public void btn_gotolog()
    {
        reg_panel.SetActive(false);
        log_panel.SetActive(true);
        is_reg = false;
        is_log = true;
    }



}
