using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ScenesLoader {
    [SerializeField] private int loginSceneIndex;
    [SerializeField] private int introSceneIndex;
    [SerializeField] private int conversationSceneIndex;
    public static ScenesLoader instance;

    public void Initialize() =>
        instance ??= this;

    public void GoToIntroScene() =>
        SceneManager.LoadScene(loginSceneIndex, LoadSceneMode.Single);

    public void GoToUserScene() =>
        SceneManager.LoadScene(introSceneIndex, LoadSceneMode.Single);

    public void GoToWorkArScene() =>
        SceneManager.LoadScene(conversationSceneIndex, LoadSceneMode.Single);

    public void UnloadWorkArScene() =>
		    SceneManager.UnloadSceneAsync(conversationSceneIndex);
}
