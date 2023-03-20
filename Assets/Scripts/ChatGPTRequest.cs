using System;
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

    private IEnumerator ChatRequest()
    {
        Debug.Log("prompt " + prompt);

      
        
        var requestData = "{\"prompt\": \"" + prompt + "\", \"max_tokens\": 550, \"temperature\": 0.5}";
        var requestDataBytes = Encoding.UTF8.GetBytes(requestData);

        var request = UnityWebRequest.PostWwwForm(chatGPTUrl, "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);
        request.uploadHandler = new UploadHandlerRaw(requestDataBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();


        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
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

    private void ParseResponse(string response)
    {
        var json = JSON.Parse(response);

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(prompt);
            StartChatRequest();
        }
    }
}