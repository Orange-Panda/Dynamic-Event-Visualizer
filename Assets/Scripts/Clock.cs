using System;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
	private TextMeshProUGUI textMesh;

	string TimeSuffix => DateTime.Now.Hour >= 12 ? " PM" : " AM";
	public int HourTwelve => DateTime.Now.Hour == 0 ? 12 : (DateTime.Now.Hour - 1) % 12 + 1;

	private void Start()
	{
		textMesh = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		textMesh.SetText(string.Format(NightModeManager.NightTime ? SavedData.data.nightClockFormat : SavedData.data.dayClockFormat, DateTime.Now.Hour, HourTwelve, DateTime.Now.Minute.ToString("D2"), DateTime.Now.Second.ToString("D2"), DateTime.Now.Millisecond.ToString("D3"), TimeSuffix));
	}
}
