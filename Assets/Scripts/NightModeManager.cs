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
	private Color dayText, nightText, dayImage, nightImage, dayBG, nightBG, dayBorder, nightBorder;

	private void Start()
	{
		InvokeRepeating("NightTimeCheck", 0, 1f);
		dayText = SavedData.data.GetColor("dayText", new SavedColor()).ToColor();
		nightText = SavedData.data.GetColor("nightText", new SavedColor()).ToColor();
		dayImage = SavedData.data.GetColor("dayImage", new SavedColor()).ToColor();
		nightImage = SavedData.data.GetColor("nightImage", new SavedColor()).ToColor();
		dayBG = SavedData.data.GetColor("dayBG", new SavedColor()).ToColor();
		nightBG = SavedData.data.GetColor("nightBG", new SavedColor()).ToColor();
		dayBorder = SavedData.data.GetColor("dayBorder", new SavedColor()).ToColor();
		nightBorder = SavedData.data.GetColor("nightBorder", new SavedColor()).ToColor();
	}

	public void NightTimeCheck()
	{
		nightTime = SavedData.data.GetBool("useDayNightCycle", true) ? DateTime.Now.Hour < 8 || DateTime.Now.Hour >= 20 : false;
	}

	private void Update()
	{
		foreach (TextMeshProUGUI textMesh in text)
		{
			textMesh.color = nightTime ? nightText : dayText;
		}
		foreach (Image image in images)
		{
			image.color = nightTime ? nightImage : dayImage;
		}

		statusIcon.sprite = Resources.Load<Sprite>(nightTime ? "moon" : "sun");
		background.color = nightTime ? nightBG : dayBG;
		border.color = nightTime ? nightBorder : dayBorder;
		nightSky.enabled = nightTime;

		ParticleSystem.MainModule newMain = system.main;
		newMain.simulationSpeed = nightTime ? 0.1f : 0.25f;
	}
}
