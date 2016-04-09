﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.

    public GameObject MoveArrow;
    Vector3 offset;                     // The initial offset from the target.

    void Start()
    {
        // Calculate the initial offset.
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        SpriteRenderer sRenderer = MoveArrow.GetComponent<SpriteRenderer>();

        if (Spawner.SpawnedEnemies.Count <= 0)
        {
            FlickerColor(sRenderer);
            if (target.position.x > transform.position.x)
            {
                // Create a postion the camera is aiming for based on the offset from the target.
                Vector3 targetCamPos = target.position + offset;

                // Smoothly interpolate between the camera's current position and it's target position.
                Vector3 newXPos = Vector3.Lerp(transform.position, targetCamPos, smoothing*Time.deltaTime);
                transform.position = new Vector3(newXPos.x, transform.position.y, transform.position.z);
            }
        }
        else
        {
            sRenderer.color = new Color(0,0,0, 0);
        }
    }

    void FlickerColor(SpriteRenderer sRenderer)
    {
        //int i = 0;
        //while (i < 20)
        {
            float colorValue = Mathf.Sin(Time.time * 5) ;//Mathf.PingPong(Time.time, 255);
            sRenderer.color = new Color(255, 0, 0,colorValue);
            //yield return new WaitForSeconds(0.1f);
            //i++;
        }
        //sRenderer.color = Color.white;
    }
}
