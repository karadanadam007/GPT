using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Button _sendButton;
    private ChatGPTRequest _chatGptRequest;

    private void Start()
    {
        _chatGptRequest = FindObjectOfType<ChatGPTRequest>();


        _sendButton = FindObjectOfType<Button>();
        _sendButton.onClick.AddListener(OnSendButtonClick);
    }

    private void OnSendButtonClick()
    {
        _chatGptRequest.StartChatRequest();
    }
}