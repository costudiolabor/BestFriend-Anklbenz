using UnityEngine;

public class Boot : MonoBehaviour {
	[SerializeField] private Project project;

	private async void Awake() {
		await project.Initialize();
		//DatabaseApi.instance.SignOut();
		project.scenario.SelectScenario();
		DontDestroyOnLoad(project);
	}
}