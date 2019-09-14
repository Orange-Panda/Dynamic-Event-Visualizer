using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NightModeManager : MonoBehaviour
{
	public TextMeshProUGUI[] text;
	public Image[] images;
	public Image background;
	public Image border;
	public Image statusIcon;
	public SpriteRenderer nightSky;
	public ParticleSystem system;

	public static bool NightTime => SavedData.data.useDayNightCycle ? DateTime.Now.Hour < 8 || DateTime.Now.Hour >= 20 : false;

	private void Update()
	{
		foreach (TextMeshProUGUI textMesh in text)
		{
			textMesh.color = NightTime ? SavedData.data.nightText : SavedData.data.dayText;
		}
		foreach (Image image in images)
		{
			image.color = NightTime ? SavedData.data.nightImage : SavedData.data.dayImage;
		}

		statusIcon.sprite = Resources.Load<Sprite>(NightTime ? "moon" : "sun");
		background.color = NightTime ? SavedData.data.nightBackground : SavedData.data.dayBackground;
		border.color = NightTime ? SavedData.data.nightBorder : SavedData.data.dayBorder;
		nightSky.enabled = NightTime;

		ParticleSystem.MainModule newMain = system.main;
		newMain.simulationSpeed = NightTime ? 0.1f : 0.25f;
	}
}
