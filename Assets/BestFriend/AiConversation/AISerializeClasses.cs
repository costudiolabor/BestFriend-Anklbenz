using System;
using UnityEngine;

public enum MessageRole {
	system = 0,
	user = 1,
	assistant = 2,
	function = 3
}


[Serializable]
public class AiResponse {
	public string id;
	public string @object;
	public int created;
	public string model;
	public TokensUsage usage;
	public ResponseChoice[] choices;
}

[Serializable]
public class ResponseChoice {
	public AiMessage message;
	public string finish_reason;
	public int index;
}

[Serializable]
public class AiMessage {
	public string role;
	public string content;
}

[Serializable]
public class TokensUsage {
	public int prompt_tokens;
	public int completion_tokens;
	public int total_tokens;
}

[SerializeField]
public class ServerTime {
	public long now;
}

[Serializable]
public class AiRequest : ICloneable {
	public AiMessage[] messages;
	public string model; // ID of the model to use
	public int max_tokens; // The maximum number of tokens allowed for the generated answer. By default, the number of tokens the model can return will be (4096 - prompt tokens).
	public float temperature; // What sampling temperature to use, between 0 and 2. Higher values like 0.8 will make the output more random, while lower values like 0.2 will make it more focused and deterministic.
	public float top_p; // An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered.
	public float presence_penalty; //Number between -2.0 and 2.0. Positive values penalize new tokens based on whether they appear in the text so far, increasing the model's likelihood to talk about new topics.
	public float frequency_penalty; // Number between -2.0 and 2.0. Positive values penalize new tokens based on their existing frequency in the text so far, decreasing the model's likelihood to repeat the same line verbatim.
	public int n; // How many chat completion choices to generate for each input message.
	public bool stream = false;

	public object Clone() {
		return (AiRequest)this.MemberwiseClone();
	}
}

[Serializable]
public class CloudRequest {
	public string uid;
	public AiRequest data;
}


