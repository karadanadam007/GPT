using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SimpleJSON;

public class ChatGPTRequest : MonoBehaviour
{
    public string apiKey;
    public string prompt;

    private string chatGPTUrl = "https://api.openai.com/v1/engines/davinci/completions";
    private string responseText;

    public void StartChatRequest()
    {
        StartCoroutine(ChatRequest());
    }

    IEnumerator ChatRequest()
    {
        string requestData = "{\"prompt\": \"" + prompt + "\", \"max_tokens\": 150, \"temperature\": 0.5\"}";
        byte[] requestDataBytes = Encoding.UTF8.GetBytes(requestData);

        UnityWebRequest request = UnityWebRequest.PostWwwForm(chatGPTUrl, "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);
        request.uploadHandler = new UploadHandlerRaw(requestDataBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            responseText = request.downloadHandler.text;
            Debug.Log(responseText);
            ParseResponse(responseText);
        }
    }

    void ParseResponse(string response)
    {
        JSONNode json = JSON.Parse(response);

        if (json != null)
        {
            JSONNode choices = json["choices"][0];
            string text = choices["text"];
            Debug.Log(text);
        }
        else
        {
            Debug.Log("Error parsing response");
        }
    }
}