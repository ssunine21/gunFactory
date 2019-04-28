using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Check : MonoBehaviour
{
	[Range(1, 100)]
	public int font_Size;
	[Range(0, 1)]
	public float Red, Green, Blue;

	float deltaTime = 0.0f;

	private void Start() {
		font_Size = font_Size == 0 ? 50 : font_Size;
	}

	private void Update() {
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}

	private void OnGUI() {
		int w = Screen.width, h = Screen.height;

		GUIStyle g_Style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		g_Style.alignment = TextAnchor.UpperLeft;
		g_Style.fontSize = h * 2 / font_Size;
		g_Style.normal.textColor = new Color(Red, Green, Blue, 1f);

		float msec = deltaTime * 1000f;
		float fps = 1f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, g_Style);
	}
}
