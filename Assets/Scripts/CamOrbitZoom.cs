//
//From Filename: maxCamera.cs
// original: http://www.unifycommunity.com/wiki/index.php?title=MouseOrbitZoom
//
// --01-18-2010 - create temporary target, if none supplied at start

using UnityEngine;
using System.Collections;


[AddComponentMenu("Camera-Control/3dsMax Camera Style")]
public class CamOrbitZoom: MonoBehaviour
{
	public Transform target;
	public float distance = 10.0f;
	public float maxDistance = 10f;
	public float minDistance = 2f;
	public float xSpeed = 150.0f;
	public float ySpeed = 150.0f;
	public int yMinLimit = 10;
	public int yMaxLimit = 85;
	public int zoomRate = 30;
	public float panSpeed = 0.3f;
	public float zoomDampening = 14f;
	
	public float currentDistance = 8f;
	public float desiredDistance = 8f;
	public bool isInputable = true ;
	private float xDeg = 0.0f;
	private float yDeg = 0.0f;
	private Quaternion currentRotation;
	private Quaternion desiredRotation;
	private Quaternion rotation;
	private Vector3 calcPosition;

	
	void Start() { Init(); }
	void OnEnable() { Init(); }
	
	public void Init()
	{
		//		Debug.Log (Screen.dpi);
		
		//If there is no target, create a temporary target at 'distance' from the cameras current viewpoint
		if (!target)
		{
			GameObject go = new GameObject("Cam Target");
			go.transform.position = transform.position + (transform.forward * distance);
			target = go.transform;
		}
		
		distance = Vector3.Distance(transform.position, target.position);
		
		//be sure to grab the current rotations as starting points.
		calcPosition = transform.position;
		rotation = transform.rotation;
		currentRotation = transform.rotation;
		desiredRotation = transform.rotation;
		
		xDeg = transform.rotation.eulerAngles.y;
		yDeg = transform.rotation.eulerAngles.x;
	}
	
	/*
     * Camera logic on LateUpdate to only update after all character movement logic has been handled. 
     */
	void LateUpdate()
	{
		// ORBIT MOUSE
		if (Input.GetMouseButton(0) && isInputable)
		{
		
			xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
			yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
			
			
			//Clamp the vertical axis for the orbit
			yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);
			// set camera rotation 
			desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
			currentRotation = transform.rotation;
			
			rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
			transform.rotation = rotation;
		}
		// otherwise if middle mouse is selected, we pan by way of transforming the target in screenspace
		else if (Input.GetMouseButton(2) && isInputable)
		{
		
			//grab the rotation of the camera so we can move in a psuedo local XY space
			target.rotation = transform.rotation;
			target.Translate(Vector3.right * -Input.GetAxis("Mouse X") * panSpeed);
			target.Translate(transform.up * -Input.GetAxis("Mouse Y") * panSpeed, Space.World);
			
		}
		
		if (isInputable) // ZOOM with scrollWheel
		{
			desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
			//clamp the zoom min/max
			desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
			// calculate new currentDistance
			currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);
			
		}
		else // NOT INPUTABLE : HERE WE ARE IN A TWEEN CONTROLLED TRANSITION
		{
			currentDistance = distance = Vector3.Distance(transform.position, target.position);
			//COMMENTED FOR SERFIGROUP PROJECT
//			this.transform.LookAt(target.transform);
			//We update rotation variables for consistency for when the script is back to orbit mode
			rotation = currentRotation = desiredRotation = transform.rotation;
			xDeg = rotation.eulerAngles.y ;
			yDeg = rotation.eulerAngles.x ;
		}
		
		//calculate new position based on the target and on the new currentDistance 
		calcPosition = target.position - (transform.rotation * Vector3.forward * currentDistance);
		//APPLY CAMERA POSITION
		transform.position =  calcPosition;
	}
	
	
	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp(angle, min, max);
	}
}
