using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InputFieldSetting : MonoBehaviour
{
	public string key;
	private TMP_InputField input;

	private void Start()
	{
		input = GetComponentInChildren<TMP_InputField>();
		input.SetTextWithoutNotify(SavedData.data.GetString(key));
		input.onValueChanged.AddListener(new UnityAction<string>(ValueChanged));
	}

	private void ValueChanged(string value)
	{
		SavedData.data.SetString(key, value);
	}
}