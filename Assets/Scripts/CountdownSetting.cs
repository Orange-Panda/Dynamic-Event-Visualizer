using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownSetting : MonoBehaviour
{
	public string key;

	public Toggle use;
	public TMP_InputField dayFormat;
	public TMP_InputField nightFormat;
	public TMP_InputField expiredText;

	public TMP_InputField year;
	public TMP_InputField month;
	public TMP_InputField day;
	public TMP_InputField hour;
	public TMP_InputField minute;

	private void Start()
	{
		SetComponentValue();
	}

	public void SetComponentValue()
	{
		CountdownSettings settings = SavedData.data.GetCountdownSettings(key, new CountdownSettings());
		use.isOn = settings.useCountdown;
		dayFormat.SetTextWithoutNotify(settings.dayTimeFormat);
		nightFormat.SetTextWithoutNotify(settings.nightTimeFormat);
		expiredText.SetTextWithoutNotify(settings.expiredText);

		year.SetTextWithoutNotify(settings.objective.Year.ToString());
		month.SetTextWithoutNotify(settings.objective.Month.ToString());
		day.SetTextWithoutNotify(settings.objective.Day.ToString());
		hour.SetTextWithoutNotify(settings.objective.Hour.ToString());
		minute.SetTextWithoutNotify(settings.objective.Minute.ToString());
	}

	public DateTime GetDateTime()
	{
		return new DateTime(int.Parse(year.text), int.Parse(month.text), int.Parse(day.text), int.Parse(hour.text), int.Parse(minute.text), 0);
	}

	public CountdownSettings GetSettings()
	{
		return new CountdownSettings()
		{
			useCountdown = use.isOn,
			dayTimeFormat = dayFormat.text,
			nightTimeFormat = nightFormat.text,
			expiredText = expiredText.text,
			objective = GetDateTime()
		};
	}
}