using System;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

public class Typewriter : AnimatedView
{
    private const char PAUSE_CHAR = '~';

    [SerializeField] private int minDelayMilliseconds = 20, maxDelayMilliseconds = 200, pauseMilliseconds = 500;
    [SerializeField] private TMP_Text textField;
    [SerializeField] private bool printOnEnable;
    [SerializeField] private string serializedMessage;

    public event Action PrintFinishedEvent;
    
    private async void OnEnable() {
	    if(printOnEnable)
		    await PrintAsNew(serializedMessage);
    }

    public async UniTask PrintAsNew(string message) {
        textField.text = string.Empty;
        await Print(message);
    }
    public async UniTask Print(string message) {

        foreach (var character in message) {
            if (character == PAUSE_CHAR) {
                await UniTask.Delay(pauseMilliseconds);
                continue;
            }

            textField.text += character;
            await UniTask.Delay(Random.Range(minDelayMilliseconds, maxDelayMilliseconds));
        }
        
        PrintFinishedEvent?.Invoke();
    }
}
