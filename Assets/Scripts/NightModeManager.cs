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

	public static bool nightTime;

	private void Start()
	{
		InvokeRepeating("NightTimeCheck", 0, 1f);
	}

	public void NightTimeCheck()
	{
		nightTime = SavedData.data.useDayNightCycle ? DateTime.Now.Hour < 8 || DateTime.Now.Hour >= 20 : false;
	}

	private void Update()
	{
		foreach (TextMeshProUGUI textMesh in text)
		{
			textMesh.color = nightTime ? SavedData.data.nightText.ToColor() : SavedData.data.dayText.ToColor();
		}
		foreach (Image image in images)
		{
			image.color = nightTime ? SavedData.data.nightImage.ToColor() : SavedData.data.dayImage.ToColor();
		}

		statusIcon.sprite = Resources.Load<Sprite>(nightTime ? "moon" : "sun");
		background.color = nightTime ? SavedData.data.nightBackground.ToColor() : SavedData.data.dayBackground.ToColor();
		border.color = nightTime ? SavedData.data.nightBorder.ToColor() : SavedData.data.dayBorder.ToColor();
		nightSky.enabled = nightTime;

		ParticleSystem.MainModule newMain = system.main;
		newMain.simulationSpeed = nightTime ? 0.1f : 0.25f;
	}
}
