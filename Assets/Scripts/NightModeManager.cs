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
			textMesh.color = NightTime ? SavedData.data.nightText.ToColor() : SavedData.data.dayText.ToColor();
		}
		foreach (Image image in images)
		{
			image.color = NightTime ? SavedData.data.nightImage.ToColor() : SavedData.data.dayImage.ToColor();
		}

		statusIcon.sprite = Resources.Load<Sprite>(NightTime ? "moon" : "sun");
		background.color = NightTime ? SavedData.data.nightBackground.ToColor() : SavedData.data.dayBackground.ToColor();
		border.color = NightTime ? SavedData.data.nightBorder.ToColor() : SavedData.data.dayBorder.ToColor();
		nightSky.enabled = NightTime;

		ParticleSystem.MainModule newMain = system.main;
		newMain.simulationSpeed = NightTime ? 0.1f : 0.25f;
	}
}
