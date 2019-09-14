using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.Text;
using TMPro;

// Get the title of the Google Chrome tab playing YouTube
public class WindowTitle : MonoBehaviour
{
	private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);
	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	private static extern int GetWindowTextLength(IntPtr hWnd);
	[DllImport("user32.dll")]
	private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

	private TextMeshProUGUI _text;

	public GameObject[] musicObjects;

	string[] lookups = { "- YouTube - Mozilla Firefox", "- YouTube Music - Mozilla Firefox", "- YouTube Music - Google Chrome" , "- YouTube - Google Chrome", "- YouTube Music", "- YouTube" };

	private void Start()
	{
		_text = GetComponent<TextMeshProUGUI>();
		StartCoroutine(UpdateText());
	}

	private IEnumerator UpdateText()
	{
		while (true)
		{
			string windowName = " ";
			string succesfulLookup = " ";
			// Look for a window with this string as its name
			foreach (string lookup in lookups)
			{
				if ((windowName = GetWindowText(FindWindowsWithText(lookup).FirstOrDefault())) != string.Empty)
				{
					succesfulLookup = lookup;
					break;
				}
			}
			
			if(windowName == string.Empty)
			{
				foreach(GameObject go in musicObjects)
				{
					go.SetActive(false);
				}
			}
			else
			{
				foreach (GameObject go in musicObjects)
				{
					go.SetActive(true);
				}
			}
			
			// Format the window title
			_text.text = windowName.Replace(succesfulLookup, "");

			// Wait a bit so we call this code less frequently
			yield return new WaitForSeconds(0.5f);
		}
	}

	// Converts a handler to the window's title
	private static string GetWindowText(IntPtr hWnd)
	{
		int size = GetWindowTextLength(hWnd);

		if (size <= 0)
		{
			return string.Empty;
		}

		var builder = new StringBuilder(size + 1);
		GetWindowText(hWnd, builder, builder.Capacity);
		return builder.ToString();
	}

	// Finds all windows with the given text in the title
	private static IEnumerable<IntPtr> FindWindowsWithText(string titleText)
	{
		return FindWindows((wnd, param) => GetWindowText(wnd).Contains(titleText));
	}

	// Finds all windows with the given proc.
	// DO NOT call this directly
	private static IEnumerable<IntPtr> FindWindows(EnumWindowsProc filter)
	{
		List<IntPtr> windows = new List<IntPtr>();

		EnumWindows(delegate (IntPtr wnd, IntPtr param)
		{
			// only add the windows that pass the filter
			if (filter(wnd, param))
				windows.Add(wnd);

			// but return true here so that we iterate all windows
			return true;
		}, IntPtr.Zero);

		return windows;
	}
}