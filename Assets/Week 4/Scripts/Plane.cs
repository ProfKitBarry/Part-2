using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
   public List<Vector2> points;
   public float newPointThreshold = 0.2f;
   Vector2 lastPosition;
   LineRenderer lineRenderer;

   void Start()
   {
	   lineRenderer = GetComponent<LineRenderer>();
	   lineRenderer.positionCount = 1;
	   lineRenderer.SetPosition(0,transform.position);
   }

   void OnMouseDown() 
   {
	   points = new List<Vector2>();
	   Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	   points.Add(newPosition);
		lineRenderer.positionCount = 1;
		lineRenderer.SetPosition(0,transform.position);
   }

   void OnMouseDrag()
   {
	   Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	   if (Vector2.Distance(lastPosition, newPosition) > newPointThreshold)
	   {
	    points.Add(newPosition);
		lineRenderer.positionCount++;
		lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPosition);
	   lastPosition = newPosition;
	   }
   }
}
