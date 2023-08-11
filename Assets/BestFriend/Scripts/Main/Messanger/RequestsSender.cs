using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class RequestsSender {
	private readonly Queue<AiMessage> _messages = new();
	private AiMessage _legend;
	private AiRequest _requestTemplate;
	private int _historyCount;

	public void Initialize(ChatSettings chatSettings, UserData userData) {
		_legend = BuildLegend(userData, chatSettings.conversationSettings.legend);
		_requestTemplate = chatSettings.modelSettings;
		_historyCount = chatSettings.conversationSettings.messagesInMemory;
	}

	public async UniTask<string> SendRequestAsync(string messageText) {
		var userMessage = CreateMessage(MessageRole.user, messageText);
		var request = CreateAiRequest(userMessage);

		var aiResponseRequest = await DatabaseApi.instance.SendAiRequest(request);
		if (!aiResponseRequest.isSuccess) return string.Empty;

		var aiResponseMessage = aiResponseRequest.data.choices[0].message;

		AddHistory(userMessage);
		AddHistory(aiResponseMessage);

		return aiResponseMessage.content;
	}

	private AiMessage CreateMessage(MessageRole role, string content) =>
			new() {role = role.ToString(), content = content};

	private AiRequest CreateAiRequest(AiMessage message) {
		var request = (AiRequest)_requestTemplate.Clone();
		request.messages = CreateMessageHistory(message);
		return request;
	}

	private AiMessage BuildLegend(UserData userData, string rawLegend) {
		rawLegend = rawLegend
				.Replace("[aiGender]", "female")
				.Replace("[aiName]", userData.avatar.name)
				.Replace("[playerName]", userData.user.name)
				.Replace("[playerGender]", userData.user.gender);

		return CreateMessage(MessageRole.system, rawLegend);
	}

	private AiMessage[] CreateMessageHistory(AiMessage message) {
		var list = new List<AiMessage>();
		list.Add(_legend);
		list.AddRange(_messages);
		list.Add(message);
		return list.ToArray();
	}

	private void AddHistory(AiMessage message) {
		_messages.Enqueue(message);

		if (_messages.Count > _historyCount)
			_messages.Dequeue();
	}
}