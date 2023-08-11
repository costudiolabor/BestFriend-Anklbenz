using TMPro;
using UnityEngine;

public class LimitsView : MonoBehaviour
{
	[SerializeField] private TMP_Text timerToNextText, requestsCountText;
	public string timerText { set => timerToNextText.text = value; }
	public string requestsText { set => requestsCountText.text = value; }
	public bool timerVisible {
		set => timerToNextText.gameObject.SetActive(value);
	}
}
