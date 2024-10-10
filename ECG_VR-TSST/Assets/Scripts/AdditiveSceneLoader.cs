using UnityEngine;
using UnityEngine.SceneManagement;


public class AdditiveSceneLoader : MonoBehaviour {
	
	
	[SerializeField]
	private string _sceneName;
	
	private void Start() {
		LoadScene();
	}
	
	[ContextMenu("LoadScene")]
	private void LoadScene() {
		SceneManager.LoadSceneAsync(_sceneName,LoadSceneMode.Additive);		
	}
	
}
