using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SavedData
{
	private const string FileName = "/preset.dat";
	public static bool loadedFromSave = false;

	public static SaveFile data;

	/// <summary>
	/// Save the serializable data to the disk.
	/// </summary>
	public static void SaveGame(bool forceSave = false)
	{
		if (!loadedFromSave && !forceSave)
		{
			Debug.LogError("There was an attempt to save the game but a load was never attempted. Avoiding save to prevent data loss.");
			return;
		}

		//Initialize saving process, save to disk, then close the saving process.
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + FileName);
		bf.Serialize(file, data);
		file.Close();
	}

	/// <summary>
	/// Finds the save data on the disk and loads it.
	/// </summary>
	public static void LoadGame()
	{
		if (File.Exists(Application.persistentDataPath + FileName))
		{
			//Open the existing file and load its properties into a SavedData insance.
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + FileName, FileMode.Open);
			data = (SaveFile)bf.Deserialize(file);
			file.Close();
		}
		else
		{
			data = new SaveFile();
		}

		loadedFromSave = true;
	}

	public static void ResetSettings()
	{
		data = new SaveFile();
		SaveGame(true);
	}
}

public enum CountdownTimer
{
	Primary, Secondary
}

public class CountdownSettings
{
	public bool useCountdown = true;
	public string dayTimeFormat = "{1}:{3}:{4} Remaining{6}";
	public string nightTimeFormat = "{1}:{3} Remaining{6}";
	public string expiredText = "Countdown Over!";
	public DateTime objective = new DateTime(2019, 9, 15);
}

[Serializable]
public class SaveFile
{
	//Globals
	public string title = "Event Name";
	public string subtitle = "Have a great time!";
	public bool useNightBG = true;

	//Clock 
	// {0} 24hr, {1} 12hr, {2} Minutes, {3} Seconds, {4} Milliseconds, {5} AM/PM
	public string dayClockFormat = "{1}:{2}:{3}{5}";
	public string nightClockFormat = "{1}:{2}{5}";

	//Colors
	public Color dayBackground = new Color(1f, 1f, 1f, 0.7f);
	public Color nightBackground = new Color(0f, 0f, 0f, 0.5f);
	public Color dayBorder = Color.black;
	public Color nightBorder = Color.white;
	public Color dayText = Color.black;
	public Color nightText = Color.white;
	public Color dayImage = Color.black;
	public Color nightImage = Color.white;

	//Change background and text colors between night and day
	public bool useDayNightCycle = true;

	//Playing panel
	public bool usePlayingPanel = true;
	public string playingSearchTerm = " - YouTube";

	//Countdowns
	public Dictionary<CountdownTimer, CountdownSettings> countdowns = new Dictionary<CountdownTimer, CountdownSettings>
	{
		{ CountdownTimer.Primary, new CountdownSettings() },
		{ CountdownTimer.Secondary, new CountdownSettings() }
	};
}
