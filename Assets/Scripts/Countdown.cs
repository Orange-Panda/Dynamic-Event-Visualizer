using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
	public string key;
	private TextMeshProUGUI textMesh;
	private CountdownSettings Settings => SavedData.data.GetCountdownSettings(key, new CountdownSettings());
	private TimeSpan TimeLeft => Settings.objective - DateTime.Now;

	int numberOfDots;

	private void Start()
	{
		textMesh = GetComponent<TextMeshProUGUI>();
		StartCoroutine(DotIncrement());
		if (!Settings.useCountdown) gameObject.SetActive(false);
		InvokeRepeating("UpdateText", 0f, 0.1f);
	}

	private IEnumerator DotIncrement()
	{
		while (true)
		{
			numberOfDots = numberOfDots + 1 > 3 ? 1 : numberOfDots + 1;
			yield return new WaitForSecondsRealtime(NightModeManager.nightTime ? 4f : 1f);
		}
	}

	void UpdateText()
	{
		TimeSpan timeLeft = TimeLeft;
		if (timeLeft.TotalSeconds > 0)
		{
			textMesh.SetText(string.Format(NightModeManager.nightTime ? Settings.nightTimeFormat : Settings.dayTimeFormat, 
				(int)timeLeft.TotalDays,				//{0}
				(int)timeLeft.TotalHours,               //{1}
				timeLeft.Hours,                         //{2}
				timeLeft.Minutes.ToString("D2"),        //{3}
				timeLeft.Seconds.ToString("D2"),        //{4}
				timeLeft.Milliseconds.ToString("D3"),	//{5}
				new string('.', numberOfDots)));		//{6}
		}
		else
		{
			textMesh.SetText(Settings.expiredText);
		}
	}
}