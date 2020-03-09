using System;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
	private TextMeshProUGUI textMesh;
	DateTime now;
	string TimeSuffix => now.Hour >= 12 ? " PM" : " AM";
	public int HourTwelve => now.Hour == 0 ? 12 : (now.Hour - 1) % 12 + 1;
	private string nightFormat, dayFormat;

	private void Start()
	{
		textMesh = GetComponent<TextMeshProUGUI>();
		InvokeRepeating("UpdateText", 0f, 0.1f);
		dayFormat = SavedData.data.GetString("clockDayFormat");
		nightFormat = SavedData.data.GetString("clockNightFormat");
	}

	private void UpdateText()
	{
		now = DateTime.Now;
		string format = NightModeManager.nightTime ? nightFormat : dayFormat;
		textMesh.SetText(string.Format(format, now.Hour, HourTwelve, now.Minute.ToString("D2"), now.Second.ToString("D2"), now.Millisecond.ToString("D3"), TimeSuffix));
	}
}
