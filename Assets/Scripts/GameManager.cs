using ChatGPTWrapper;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string prompt;
    
    private Button _sendButton;
    private ChatGPTConversation _chatGptRequest;

    private void Start()
    {
        _chatGptRequest = FindObjectOfType<ChatGPTConversation>();


        _sendButton = FindObjectOfType<Button>();
        _sendButton.onClick.AddListener(OnSendButtonClick);
    }

    private void OnSendButtonClick()
    {
        _chatGptRequest.SendToChatGPT(prompt);
    }
}