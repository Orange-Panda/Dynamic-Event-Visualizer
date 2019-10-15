using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
	public TMP_InputField title;
	public TMP_InputField subtitle;
	public TMP_InputField dayClockFormat;
	public TMP_InputField nightClockFormat;
	public TMP_InputField iconOverride;
	public TMP_InputField qrOverride;
	public TMP_InputField searchTerm;

	public ColorControl dayBG, nightBG, dayBorder, nightBorder, dayText, nightText, dayImage, nightImage;
	public CountdownSetting primary, secondary;

	private void Awake()
	{
		SavedData.LoadGame();
		SetComponentValues();
	}

	private void SetComponentValues()
	{
		title.SetTextWithoutNotify(SavedData.data.title);
		subtitle.SetTextWithoutNotify(SavedData.data.subtitle);
		dayClockFormat.SetTextWithoutNotify(SavedData.data.dayClockFormat);
		nightClockFormat.SetTextWithoutNotify(SavedData.data.nightClockFormat);
		iconOverride.SetTextWithoutNotify(SavedData.data.iconOverride);
		qrOverride.SetTextWithoutNotify(SavedData.data.qrOverride);
		searchTerm.SetTextWithoutNotify(SavedData.data.playingSearchTerm);

		dayBG.SetColor(SavedData.data.dayBackground.ToColor());
		nightBG.SetColor(SavedData.data.nightBackground.ToColor());
		dayBorder.SetColor(SavedData.data.dayBorder.ToColor());
		nightBorder.SetColor(SavedData.data.nightBorder.ToColor());
		dayText.SetColor(SavedData.data.dayText.ToColor());
		nightText.SetColor(SavedData.data.nightText.ToColor());
		dayImage.SetColor(SavedData.data.dayImage.ToColor());
		nightImage.SetColor(SavedData.data.nightImage.ToColor());

		primary.SetComponentValue();
		secondary.SetComponentValue();
	}

	private void WriteToData()
	{
		SavedData.data.title = title.text;
		SavedData.data.subtitle = subtitle.text;
		SavedData.data.dayClockFormat = dayClockFormat.text;
		SavedData.data.nightClockFormat = nightClockFormat.text;
		SavedData.data.iconOverride = iconOverride.text;
		SavedData.data.qrOverride = qrOverride.text;
		SavedData.data.playingSearchTerm = searchTerm.text;

		SavedData.data.dayBackground = new SavedColor(dayBG.GetColor());
		SavedData.data.nightBackground = new SavedColor(nightBG.GetColor());
		SavedData.data.dayBorder = new SavedColor(dayBorder.GetColor());
		SavedData.data.nightBorder = new SavedColor(nightBorder.GetColor());
		SavedData.data.dayText = new SavedColor(dayText.GetColor());
		SavedData.data.nightText = new SavedColor(nightText.GetColor());
		SavedData.data.dayImage = new SavedColor(dayImage.GetColor());
		SavedData.data.nightImage = new SavedColor(nightImage.GetColor());

		SavedData.data.countdowns[CountdownTimer.Primary] = primary.GetSettings();
		SavedData.data.countdowns[CountdownTimer.Secondary] = secondary.GetSettings();
	}

	public void SaveReturn()
	{
		WriteToData();
		SavedData.SaveGame();
		SceneManager.LoadScene("Main");
	}

	public void CloseApplication()
	{
		WriteToData();
		SavedData.SaveGame();
		Application.Quit();
	}

	public void ResetData()
	{
		SavedData.data = new SaveFile();
		SetComponentValues();
		SavedData.SaveGame();
	}
}
