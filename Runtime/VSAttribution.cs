using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace AlturaNFT
{
    using Internal;

    public class VSAttributaion : MonoBehaviour
    {
        // URL for the API
        string url = "https://api.alturanft.com/api/sdk/unity/";

        public void CallAction()
        {
            Debug.Log("lol");

            // Start a coroutine to make the API call
            StartCoroutine(PostData());
        }

        IEnumerator PostData()
        {
            // Create a new form to send data to the API
            WWWForm form = new WWWForm();

            // Add any data you need to send to the API here
            // For example, form.AddField("key", "value");
            // Create a UnityWebRequest and set its url and method
            UnityWebRequest www = UnityWebRequest.Post(url + "lol", form);

            // Send the request and wait for a response
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
