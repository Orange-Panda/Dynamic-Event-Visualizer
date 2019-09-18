using UnityEngine;
using UnityEngine.UI;

public class ColorControl : MonoBehaviour
{
	public Slider red, green, blue, alpha;
	public Image image;

	private void Update()
	{
		image.color = GetColor();
	}

	public Color GetColor()
	{
		return new Color(red.value, green.value, blue.value, alpha.value);
	}

	public void SetColor(Color color)
	{
		red.value = color.r;
		green.value = color.g;
		blue.value = color.b;
		alpha.value = color.a;
	}
}
