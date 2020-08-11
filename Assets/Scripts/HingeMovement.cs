using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HingeMovement : MonoBehaviour
{
	private bool _grabbing; //yapışık mı yakalıoz mu ya da

	private void Update()
	{
		LineRenderer _myLine = gameObject.GetComponentInChildren<LineRenderer>();
		//_myLine.startColor = Color.white;
		//_myLine.material = new Material(Shader.Find("Transparent / Diffuse"));
		_myLine.startWidth = 0.08f;
		_myLine.endWidth = 0.08f;



		if (Input.GetMouseButtonDown(0))
		{
			_grabbing = true;

		}
		if (Input.GetMouseButton(0))
		{
			_myLine.positionCount = 2;
			GameObject _closest = FindNearest();
			if(_grabbing)
			{
				_myLine.SetPosition(1, _closest.transform.position);
				_closest.GetComponentInChildren<HingeJoint2D>().connectedBody = gameObject.GetComponentInChildren<Rigidbody2D>();
				_grabbing = false;
			}
			_myLine.SetPosition(0, transform.position);

		}
		if (Input.GetMouseButtonUp(0))
		{
			GameObject[] _hinges;
			_hinges = GameObject.FindGameObjectsWithTag("Hinge");
			_myLine.positionCount=0;

			foreach (var _oneOfHinges in _hinges)
			{
				_oneOfHinges.GetComponentInChildren<HingeJoint2D>().connectedBody = null; // Elimizi mouse'dan çekince bir şeyler yapsın istemıoz
			}
		}

	}

	GameObject FindNearest()
	{
		GameObject[] hinges;
		hinges = GameObject.FindGameObjectsWithTag("Hinge");
		GameObject _closest = null;

		float _distance = Mathf.Infinity;
		Vector3 _position = transform.position; // Hangi objenin hook etmesini istiorsan onu alabilir.

		foreach (GameObject _myObject in hinges)
		{
			Vector3 _diff = _myObject.transform.position - _position;//her bir hinge için farkı buluyoz
			float _currentDistance = _diff.sqrMagnitude; //vectorun uzunluğunu bulmak için
			if(_currentDistance <= _distance)
			{
				_closest = _myObject;
				_distance = _currentDistance;
			}
		}

		return _closest;
	}


}
