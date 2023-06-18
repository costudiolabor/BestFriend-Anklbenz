using TMPro;
using UnityEngine;

public class Chat : MonoBehaviour {
	[SerializeField] private TMP_InputField input;
	[SerializeField] private GptConversationBehaviour behaviour;
	[SerializeField] private GptConversation gptConversation;
	[SerializeField] private MessageBoard messageBoard;
	[SerializeField] private string botName;
	[SerializeField] private string userName;

	private void Awake() {
		gptConversation.StartConversation(behaviour);
		input.onSubmit.AddListener(OnSubmit);
	}
	private async void OnSubmit(string text) {
		input.interactable = false;
		messageBoard.CreateUserMessage(userName, text);
		
		var f = await gptConversation.SendUser(text);

		input.interactable = true;
		messageBoard.CreateCompanionMessage(botName, f);

		//text.text = string.Join("\n", f);
		input.text = string.Empty;
	}
}
