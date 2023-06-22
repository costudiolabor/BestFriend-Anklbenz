using Cysharp.Threading.Tasks;
using UnityEngine;

public class Project : MonoBehaviour {
	public OpenAIApi openOpenAIApi;
	public ScenesLoader scenesLoader;
	public DatabaseApi firebase;
	public GameScenario scenario;
//	public SessionCache sessionCache;

	public async UniTask Initialize() {
		await firebase.Initialize();
		scenesLoader.Initialize();
		openOpenAIApi.Initialize();
		
		scenario = new GameScenario();
		scenario.Initialize();
	}
}