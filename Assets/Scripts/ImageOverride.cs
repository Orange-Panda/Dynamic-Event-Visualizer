using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageOverride : MonoBehaviour
{
	private RawImage rawImage;

	void Start()
    {
		rawImage = GetComponent<RawImage>();
		StartCoroutine(LoadImage());
    }

	IEnumerator LoadImage()
	{
		using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(SavedData.data.iconOverride))
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
}
