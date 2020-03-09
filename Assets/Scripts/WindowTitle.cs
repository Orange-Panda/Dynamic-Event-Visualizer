// https://github.com/ssvegaraju/GDC_Title_Screen/blob/master/Assets/Scripts/GetWindowTitle.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// Gets the name of a window playing 
/// </summary>
public class WindowTitle : MonoBehaviour
{
	private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);
	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	private static extern int GetWindowTextLength(IntPtr hWnd);
	[DllImport("user32.dll")]
	private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

	private const float UpdateTextRate = 1f;
	private TextMeshProUGUI textMesh;
	public GameObject[] musicObjects;
	private string searchTerm;

	private void Start()
	{
		textMesh = GetComponent<TextMeshProUGUI>();
		searchTerm = SavedData.data.GetString("searchTerm", "YouTube");
		InvokeRepeating("UpdateText", 0f, UpdateTextRate);
	}

	private void UpdateText()
	{
		string windowName = GetWindowText(FindWindowsWithText(searchTerm).FirstOrDefault());

		if (!string.IsNullOrEmpty(windowName))
		{
			textMesh.SetText(windowName.Replace(searchTerm, ""));
		}
		else
		{
			textMesh.SetText("");
		}

		foreach (GameObject musicObject in musicObjects)
		{
			musicObject.SetActive(!string.IsNullOrEmpty(windowName));
		}
	}

	/// <summary>
	/// Gets the window title string from the pointer.
	/// </summary>
	/// <param name="hWnd">Pointer to the window</param>
	/// <returns>String for the title of the window.</returns>
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