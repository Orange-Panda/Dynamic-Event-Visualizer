using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
	public TMP_InputField title;
	public TMP_InputField subtitle;
	public TMP_InputField dayClockFormat;
	public TMP_InputField nightClockFormat;

	private void Awake()
	{
		SavedData.LoadGame();

		title.SetTextWithoutNotify(SavedData.data.title);
		subtitle.SetTextWithoutNotify(SavedData.data.subtitle);
		dayClockFormat.SetTextWithoutNotify(SavedData.data.dayClockFormat);
		nightClockFormat.SetTextWithoutNotify(SavedData.data.nightClockFormat);
	}

	private void SetContent()
	{
		SavedData.data.title = title.text;
		SavedData.data.subtitle = subtitle.text;
		SavedData.data.dayClockFormat = dayClockFormat.text;
		SavedData.data.nightClockFormat = nightClockFormat.text;
	}

	public void SaveReturn()
	{
		SetContent();
		SavedData.SaveGame();
		SceneManager.LoadScene("Main");
	}

	public void CloseApplication()
	{
		SetContent();
		SavedData.SaveGame();
		Application.Quit();
	}
}
