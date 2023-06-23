using UnityEngine;

public class Boot : MonoBehaviour {
	[SerializeField] private Project project;

	private async void Awake() {
		await project.Initialize();
		
	DatabaseApi.instance.SingOut();
		project.scenario.SelectScenario();
		
		DontDestroyOnLoad(project);


		/*
		if(!DatabaseApi.instance.isSigned)
			 await DatabaseApi.instance.CreateAsAnonymousAsync();
			 */

//		await FirebaseApi.instance.RegistrationWithEmailAndPassword("aabb.cc", "111");
		/*if (DatabaseApi.instance.isSigned) {
			await DatabaseApi.instance.SetUserData(new UserData() {age = "1", gender = "female", name = "Grisha1"});
			await DatabaseApi.instance.SetAvatarData(new AvatarData() {gender = "male", name = " Petya"});
			await DatabaseApi.instance.SetAvatarMeshData(new AvatarMeshData() {hair = 2, head = 1, body = 3, eyes = 21, legs = 24});
			var avatar = await DatabaseApi.instance.GetAvatarData();
			var user = await DatabaseApi.instance.GetUserData();
			var mesh = await DatabaseApi.instance.GetAvatarMeshData();
			var openAi = await DatabaseApi.instance.GetAiKey();
			var google = await DatabaseApi.instance.GetSpeechKey();
		}*/

		//	await DatabaseApi.instance.SignInWithEmailAndPasswordAsync("aa@bb.cc", "111111");
		//await DatabaseApi.instance.Ddd();
		//"aabb@eee.cc", "password"
//		var r=await DatabaseApi.instance.LinkAnonymousToEmailCredential("aabb@eee.cc1","password");
	}
	/*private void SelectScene() {
		if (DatabaseApi.instance.isSigned)
			Debug.Log("Go to next");
		else
			ScenesLoader.instance.GoToLoginScene();
	}*/
}