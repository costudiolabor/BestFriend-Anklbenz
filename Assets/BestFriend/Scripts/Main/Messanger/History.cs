using Enums;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class History {
	private const string HISTORY_FILE_NAME = "messagesHistory";
	private string path => $"{Application.persistentDataPath}/{HISTORY_FILE_NAME}";
	private long unixUtcNow => ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();

	public IEnumerable<HistoryMessage> Get() {
		var fileLines = File.ReadAllLines(path);
		var historyList = new List<HistoryMessage>();
		
		foreach (var line in fileLines) 
			if (!string.IsNullOrWhiteSpace(line))
				historyList.Add(JsonConvert.DeserializeObject<HistoryMessage>(line));
		
		return historyList;
	}
	
	public void SaveUserMessage(string message) =>
			SaveMessage(message, HistoryMessageType.User);

	public void SaveCompanionMessage(string message) =>
			SaveMessage(message, HistoryMessageType.Companion);
	
	private void SaveMessage(string message, HistoryMessageType type) {
		var historyMessage = new HistoryMessage() {message = message, role = type, unixTime = unixUtcNow};
		string jsonString = JsonConvert.SerializeObject(historyMessage, Formatting.None);

		using StreamWriter writer = File.AppendText(path);
		writer.WriteLine(jsonString);
	}
}
