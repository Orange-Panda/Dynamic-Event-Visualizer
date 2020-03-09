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
	public static readonly SavedColor white = new SavedColor(Color.white);

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
	protected Dictionary<string, string> stringData = new Dictionary<string, string>
	{
		{ "title", "Welcome to Event Visualizer" },
		{ "subtitle", "Press the escape key to modify the properties." },
		{ "searchTerm", "- YouTube - Google Chrome" },
		{ "clockDayFormat",  "{1}:{2}:{3}{5}" },
		{ "clockNightFormat", "{1}:{2}{5}" },
	};
	protected Dictionary<string, SavedColor> colorData = new Dictionary<string, SavedColor>
	{
		{ "dayBG", new SavedColor(1f, 1f, 1f, 0.7f) },
		{ "dayBorder", new SavedColor(Color.black) },
		{ "dayText", new SavedColor(Color.black) },
		{ "dayImage", new SavedColor(Color.black) },
		{ "nightBG", new SavedColor(0f, 0f, 0f, 0.5f) },
		{ "nightBorder", new SavedColor(Color.white) },
		{ "nightText", new SavedColor(Color.white) },
		{ "nightImage", new SavedColor(Color.white) },
	};
	protected Dictionary<string, bool> boolData = new Dictionary<string, bool>
	{
		{ "useDayCycle", true },
		{ "usePlayingPanel", true },
		{ "useNightBG", true }
	};
	protected Dictionary<string, CountdownSettings> countdownData = new Dictionary<string, CountdownSettings>
	{
		{ "primary", new CountdownSettings() },
		{ "secondary", new CountdownSettings() { useCountdown = false } }
	};

	public string GetString(string key, string fallback = "")
	{
		if (stringData.ContainsKey(key))
		{
			return stringData[key];
		}
		else return fallback;
	}

	public SavedColor GetColor(string key, SavedColor fallback)
	{
		if (colorData.ContainsKey(key))
		{
			return colorData[key];
		}
		else return fallback;
	}

	public bool GetBool(string key, bool fallback)
	{
		if (boolData.ContainsKey(key))
		{
			return boolData[key];
		}
		else return fallback;
	}

	public CountdownSettings GetCountdownSettings(string key, CountdownSettings fallback)
	{
		if (countdownData.ContainsKey(key))
		{
			return countdownData[key];
		}
		else return fallback;
	}

	public void SetString(string key, string value)
	{
		if (stringData.ContainsKey(key))
		{
			stringData[key] = value;
		}
		else stringData.Add(key, value);
	}

	public void SetColor(string key, SavedColor value)
	{
		if (colorData.ContainsKey(key))
		{
			colorData[key] = value;
		}
		else colorData.Add(key, value);
	}

	public void SetBool(string key, bool value)
	{
		if (boolData.ContainsKey(key))
		{
			boolData[key] = value;
		}
		else boolData.Add(key, value);
	}

	public void SetCountdownSettings(string key, CountdownSettings value)
	{
		if (countdownData.ContainsKey(key))
		{
			countdownData[key] = value;
		}
		else countdownData.Add(key, value);
	}
}
