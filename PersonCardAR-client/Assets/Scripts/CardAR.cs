using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Class;

public class CardAR : MonoBehaviour
{
    public int id;
    public new Text name;
    public Text country;
    public Text company;
    public Text position;
    public Text mail;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine( GetUser(id) );
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    void canvasInfo( User user ) {
        name.text = user.name;
        country.text = user.country;
        company.text = user.company;
        position.text = user.position;
        mail.text = user.mail;
    }

    IEnumerator GetUser( int id )
    {
        UnityWebRequest www = UnityWebRequest.Get("http://myhouselan.ddns.net:3000/id/" + id);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
            yield break;
        }

        Debug.Log(www.downloadHandler.text);    // Show results as text
        User user = new User().fromJSON( www.downloadHandler.text );
        canvasInfo( user );
    }

}
