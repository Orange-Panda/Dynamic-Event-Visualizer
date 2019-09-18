using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
	public CountdownTimer timer;
	private TextMeshProUGUI textMesh;
	private CountdownSettings Settings => SavedData.data.countdowns[timer];
	private TimeSpan TimeLeft => Settings.objective - DateTime.Now;

	int numberOfDots;
	public int NumberOfDots
	{
		get
		{
			return numberOfDots;
		}
		set
		{
			if (value > 3)
			{
				numberOfDots = 1;
			}
			else
			{
				numberOfDots = value;
			}
		}
	}

	private void Start()
	{
		textMesh = GetComponent<TextMeshProUGUI>();
		StartCoroutine(DotIncrement());
		if (!Settings.useCountdown) gameObject.SetActive(false);
	}

	private IEnumerator DotIncrement()
	{
		while (true)
		{
			NumberOfDots++;
			yield return new WaitForSeconds(NightModeManager.NightTime ? 3.5f : 0.75f);
		}
	}

	void Update()
	{
		if (TimeLeft.TotalSeconds > 0)
		{
			textMesh.SetText(string.Format(NightModeManager.NightTime ? Settings.nightTimeFormat : Settings.dayTimeFormat, 
				(int)TimeLeft.TotalDays,        // {0}
				(int)TimeLeft.TotalHours,	// {1}
				TimeLeft.Hours,			// {2}
				TimeLeft.Minutes.ToString("D2"),		//{3}
				TimeLeft.Seconds.ToString("D2"),		//{4}
				TimeLeft.Milliseconds.ToString("D3"),	//{5}
				new string('.', NumberOfDots)));		//{6}
		}
		else
		{
			textMesh.SetText(Settings.expiredText);
		}
	}
}