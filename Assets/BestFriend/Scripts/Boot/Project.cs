using Cysharp.Threading.Tasks;
using UnityEngine;

public class Project : MonoBehaviour {
	public ScenesLoader scenesLoader;
	public SessionCache sessionCache;

	public readonly StartScenario scenario = new();
	private readonly DatabaseApi _firebase = new();

	public async UniTask Initialize() {
		await _firebase.Initialize();
		scenesLoader.Initialize();
		sessionCache.Initialize();
		await scenario.Initialize();
	}
}