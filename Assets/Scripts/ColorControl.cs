using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorControl : MonoBehaviour
{
	public string key;
	public Slider red, green, blue, alpha;
	public Image image;

	private void Start()
	{
		LoadColor(SavedData.data.GetColor(key, new SavedColor(Color.black)).ToColor());
		image.color = GetColor();
		red.onValueChanged.AddListener(new UnityAction<float>(ColorChanged));
		green.onValueChanged.AddListener(new UnityAction<float>(ColorChanged));
		blue.onValueChanged.AddListener(new UnityAction<float>(ColorChanged));
		alpha.onValueChanged.AddListener(new UnityAction<float>(ColorChanged));
	}

	public void ColorChanged(float value)
	{
		SavedData.data.SetColor(key, new SavedColor(GetColor()));
		image.color = GetColor();
	}

	public Color GetColor()
	{
		return new Color(red.value, green.value, blue.value, alpha.value);
	}

	public void LoadColor(Color color)
	{
		red.SetValueWithoutNotify(color.r);
		green.SetValueWithoutNotify(color.g);
		blue.SetValueWithoutNotify(color.b);
		alpha.SetValueWithoutNotify(color.a);
	}
}
