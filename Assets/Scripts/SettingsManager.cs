using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
	public CountdownSetting[] countdownSettings;

	private void Awake()
	{
		SavedData.LoadGame();
	}

	public void SaveReturn()
	{
		SetCountdownSettings();
		SavedData.SaveGame();
		SceneManager.LoadScene("Main");
	}

	private void SetCountdownSettings()
	{
		foreach (CountdownSetting setting in countdownSettings)
		{
			SavedData.data.SetCountdownSettings(setting.key, setting.GetSettings());
		}
	}

	public void CloseApplication()
	{
		SavedData.SaveGame();
		Application.Quit();
	}

	public void ResetData()
	{
		SavedData.data = new SaveFile();
		SavedData.SaveGame();
	}
}
