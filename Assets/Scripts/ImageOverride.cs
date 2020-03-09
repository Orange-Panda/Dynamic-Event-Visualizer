using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageOverride : MonoBehaviour
{
	private RawImage rawImage;
	public OverrideTarget overrideTarget;

	void Start()
	{
		rawImage = GetComponent<RawImage>();
		StartCoroutine(LoadImage());
	}

	private static string GetLocation(OverrideTarget target)
	{
		switch (target)
		{
			case OverrideTarget.Icon:
				return SavedData.data.GetString("iconOverride");
			case OverrideTarget.QR:
				return SavedData.data.GetString("qrOverride");
			default:
				return "";
		}
	}

	IEnumerator LoadImage()
	{
		using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(GetLocation(overrideTarget)))
		{
			yield return uwr.SendWebRequest();

			if (uwr.isNetworkError || uwr.isHttpError)
			{
				Debug.Log(uwr.error);
				rawImage.enabled = false;
			}
			else
			{
				// Get downloaded asset bundle
				rawImage.enabled = true;
				rawImage.texture = DownloadHandlerTexture.GetContent(uwr);
			}
		}
	}

	public enum OverrideTarget { Icon, QR }
}
