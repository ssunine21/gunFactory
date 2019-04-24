using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FlameGunFOV))]
public class FOVEditor : Editor
{
	private void OnSceneGUI() {
		FlameGunFOV fov = (FlameGunFOV)target;

		Vector3 fromAnglePos = fov.CirclePoint(-fov.viewAngle * 0.5f);

		Handles.color = Color.red;

		Handles.DrawWireDisc(fov.transform.position, Vector3.up, fov.viewRange);

		Handles.color = new Color(0, 0, 1, 0.3f);

		Handles.DrawSolidArc(fov.transform.position, Vector3.up, fromAnglePos, fov.viewAngle, fov.viewRange);
		Handles.Label(fov.transform.position + (fov.transform.forward * 2.0f), fov.viewAngle.ToString());
	}
}
