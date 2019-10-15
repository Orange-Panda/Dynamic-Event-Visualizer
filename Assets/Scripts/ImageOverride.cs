using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageOverride : MonoBehaviour
{
	private RawImage rawImage;
	public OverrideTarget overrideTarget;
	private Dictionary<OverrideTarget, string> textureLocation = new Dictionary<OverrideTarget, string>()
	{
		{ OverrideTarget.Icon, SavedData.data.iconOverride },
		{ OverrideTarget.QR, SavedData.data.qrOverride }
	};

	void Start()
	{
		rawImage = GetComponent<RawImage>();
		StartCoroutine(LoadImage());
	}

	IEnumerator LoadImage()
	{
		using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(textureLocation[overrideTarget]))
		{
			yield return uwr.SendWebRequest();

			if (uwr.isNetworkError || uwr.isHttpError)
			{
				Debug.Log(uwr.error);
			}
			else
			{
				// Get downloaded asset bundle
				rawImage.texture = DownloadHandlerTexture.GetContent(uwr);
			}
		}
	}

	public enum OverrideTarget { Icon, QR }
}
