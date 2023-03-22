using ChatGPTWrapper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [TextArea] public string prompt;
    
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _resultText;
        

    private Button _sendButton;
    private ChatGPTConversation _chatGptConversation;
    private ChatGPTRequest _chatGptRequest;

    private void Start()
    {
        _chatGptConversation = FindObjectOfType<ChatGPTConversation>();
        _chatGptRequest = FindObjectOfType<ChatGPTRequest>();

        _chatGptConversation.chatGPTResponse.AddListener(OnChatGPTResponse);


        _sendButton = FindObjectOfType<Button>();
        _sendButton.onClick.AddListener(OnSendButtonClick);
    }

    private void OnChatGPTResponse(string response)
    {
        Debug.Log(response);
        _resultText.text = response;
        _sendButton.interactable = true;
    }

    private void OnSendButtonClick()
    {
        // _chatGptRequest.prompt = prompt;
        // _chatGptRequest.StartChatRequest();
        // var result = _chatGptRequest.GetResponse(prompt);
        // Debug.Log(result.Result + " " + result.Id);
        _chatGptConversation.SendToChatGPT(prompt);
        _resultText.text = "Sending...";
        
        _sendButton.interactable = false;
    }
}