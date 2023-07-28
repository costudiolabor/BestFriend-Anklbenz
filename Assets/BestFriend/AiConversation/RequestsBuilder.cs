using System.Collections.Generic;

public class RequestsBuilder {
	private readonly Queue<AiMessage> _messages = new ();
	private UserData _userData;
	private AiMessage _legend;
	private AiRequest _requestTemplate;
	private int _historyCount;

	public void Initialize(ChatSettings chatSettings, UserData userData) {
		_userData = userData;
		_legend = BuildLegend(chatSettings.conversationSettings.legend);
		_requestTemplate = chatSettings.modelSettings;
		_historyCount = chatSettings.conversationSettings.messagesInMemory;
	}

	private AiMessage BuildLegend(string rawLegend) {
		rawLegend = rawLegend
				.Replace("\n","\n")
				.Replace("[aiGender]", "female")
				.Replace("[aiName]", _userData.avatar.name)
				.Replace("[playerName]", _userData.user.name)
				.Replace("[playerGender]", _userData.user.gender);

		return CreateMessage(MessageRole.system, rawLegend); 
	}

	public AiMessage CreateUserMessage(string content) =>
			CreateMessage(MessageRole.user, content);

	private AiMessage CreateMessage(MessageRole role, string content) =>
			new() {role = role.ToString(), content = content};
	
	public AiRequest CreateAiRequest(AiMessage aiMessage) {
		var request = (AiRequest)_requestTemplate.Clone(); 
		request.messages = GetMessages(aiMessage);
		return request;
	}

	private AiMessage[] GetMessages(AiMessage message) {
		var list = new List<AiMessage>();
		list.Add(_legend);
		list.AddRange(_messages);
		list.Add(message);
		return list.ToArray();
	}

	public void AddHistory(AiMessage message) {
		_messages.Enqueue(message);
		
		if (_messages.Count > _historyCount)
			_messages.Dequeue();
	}
}