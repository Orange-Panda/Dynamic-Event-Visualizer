using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsLoader : MonoBehaviour
{
	public TextMeshProUGUI title;
	public TextMeshProUGUI subtitle;

	private void Awake()
	{
		SavedData.LoadGame();
		title.SetText(SavedData.data.GetString("title"));
		subtitle.SetText(SavedData.data.GetString("subtitle"));
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("Main");
		}
	}
}
