﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ControlerManager : MonoBehaviour
{
    public float MinSwipeInputLenght;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                GetSwipeDirection(touch);
            }
        }
    }

    private void GetSwipeDirection(Touch touch)
    {
        if (touch.deltaPosition.x > MinSwipeInputLenght)
            Debug.Log("user is swiping to the right" + touch.deltaPosition.x);

        if (touch.deltaPosition.x < -MinSwipeInputLenght)
            Debug.Log("user is swiping to the left" + touch.deltaPosition.x);

        if (touch.deltaPosition.y < -MinSwipeInputLenght)
            Debug.Log("user is swiping to down" + touch.deltaPosition.y);

        if (touch.deltaPosition.y > MinSwipeInputLenght)
            Debug.Log("user is swiping to up" + touch.deltaPosition.y);
    }
}
