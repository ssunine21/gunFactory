using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{
	public enum Type { NORMAL, SPAWNENEMY }
	public Type type = Type.NORMAL;

	public Color _color = Color.yellow;
	public float _radius = 0.5f;

	private void OnDrawGizmos() {
		if(type == Type.SPAWNENEMY ) {
			_color = Color.red;
			Gizmos.color = _color;
			Gizmos.DrawSphere(transform.position, _radius);
		}

		else {
			Gizmos.color = _color;
			Gizmos.DrawSphere(transform.position, _radius);
		}
	}
}
