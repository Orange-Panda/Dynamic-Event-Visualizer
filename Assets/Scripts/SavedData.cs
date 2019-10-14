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
	public static void SaveGame()
	{
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
		SaveGame();
	}
}

public enum CountdownTimer
{
	Primary, Secondary
}

[Serializable]
public class CountdownSettings
{
	public bool useCountdown = true;
	public string dayTimeFormat = "{1}:{3}:{4} Remaining{6}";
	public string nightTimeFormat = "{1}:{3} Remaining{6}";
	public string expiredText = "Countdown Over!";
	public DateTime objective = new DateTime(2019, 9, 15);
}

[Serializable]
public struct SavedColor
{
	public float r;
	public float g;
	public float b;
	public float a;

	public SavedColor(float r, float g, float b, float a = 1f)
	{
		this.r = r;
		this.g = g;
		this.b = b;
		this.a = a;
	}

	public SavedColor(Color color)
	{
		r = color.r;
		g = color.g;
		b = color.b;
		a = color.a;
	}

	public Color ToColor()
	{
		return new Color(r, g, b, a);
	}
}


[Serializable]
public class SaveFile
{
	//Globals
	public string title = "Welcome to Event Visualizer";
	public string subtitle = "Press the escape key to modify the properties.";
	public string playingSearchTerm = "- YouTube - Google Chrome";
	public string iconOverride = "";

	//Clock 
	// {0} 24hr, {1} 12hr, {2} Minutes, {3} Seconds, {4} Milliseconds, {5} AM/PM
	public string dayClockFormat = "{1}:{2}:{3}{5}";
	public string nightClockFormat = "{1}:{2}{5}";

	//Colors
	public SavedColor dayBackground = new SavedColor(1f, 1f, 1f, 0.7f);
	public SavedColor nightBackground = new SavedColor(0f, 0f, 0f, 0.5f);
	public SavedColor dayBorder = new SavedColor(Color.black);
	public SavedColor nightBorder = new SavedColor(Color.white);
	public SavedColor dayText = new SavedColor(Color.black);
	public SavedColor nightText = new SavedColor(Color.white);
	public SavedColor dayImage = new SavedColor(Color.black);
	public SavedColor nightImage = new SavedColor(Color.white);

	//Change background and text colors between night and day
	public bool useDayNightCycle = true;
	public bool usePlayingPanel = true;
	public bool useNightBG = true;

	//Countdowns
	public Dictionary<CountdownTimer, CountdownSettings> countdowns = new Dictionary<CountdownTimer, CountdownSettings>
	{
		{ CountdownTimer.Primary, new CountdownSettings() },
		{ CountdownTimer.Secondary, new CountdownSettings() { useCountdown = false } }
	};
}
