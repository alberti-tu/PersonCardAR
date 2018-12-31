using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using Class;
using System.Text;

public class MainMenu : MonoBehaviour
{
    public RectTransform panel1;
    public RectTransform panel2;

    public InputField username;
    public InputField password;

    public InputField userName;
    public InputField pass;
    public new InputField name;
    public InputField country;
    public InputField company;
    public InputField position;
    public InputField mail;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quit();
        }
    }

    public void login()
    {
        //Check if the Inputs Fields are empty
        if (username.text == "" || password.text == "")
        {
            SSTools.ShowMessage("Empty fields", SSTools.Position.bottom, SSTools.Time.twoSecond);
            return;
        }

        StartCoroutine(GetUser(username.text));
    }

    public void goToRegister()
    {
        panel1.gameObject.SetActive(false);
        panel2.gameObject.SetActive(true);
    }

    public void goToMain()
    {
        panel1.gameObject.SetActive(true);
        panel2.gameObject.SetActive(false);
    }

    public void accept()
    {
        //Check if the Inputs Fields are empty
        if (userName.text == "" || pass.text == "" || name.text == "" || country.text == "" || company.text == "" || position.text == "" || mail.text == "")
        {
            SSTools.ShowMessage("Empty fields", SSTools.Position.bottom, SSTools.Time.twoSecond);
            return;
        }

        string register = new User(userName.text, pass.text, name.text, country.text, company.text, position.text, mail.text).toJSON();
        StartCoroutine(PostUser(register));
    }

    //Close the App
    public void quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
		    Application.Quit();
        #endif
    }

    IEnumerator GetUser(string user)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://myhouselan.ddns.net:3000/user/" + user);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
            yield break;
        }

        Debug.Log(www.downloadHandler.text);    // Show results as text

        if(www.downloadHandler.text == "")
        {
            SSTools.ShowMessage("User not found", SSTools.Position.bottom, SSTools.Time.twoSecond);
            Debug.LogError("User not found");
        }

        else if (password.text == new User().fromJSON(www.downloadHandler.text).password)
        {
            SceneManager.LoadScene("CameraScene");
        }
    }

    IEnumerator PostUser(string body)
    {
        UnityWebRequest www = new UnityWebRequest("http://myhouselan.ddns.net:3000/", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        Debug.Log("Status Code: " + www.responseCode);
        if (www.responseCode == 201)
        {
            SSTools.ShowMessage("Open your mailbox", SSTools.Position.bottom, SSTools.Time.twoSecond);
            goToMain();
        }
        else
        {
            SSTools.ShowMessage("Error: Username exists", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
    }
}