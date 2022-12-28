using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCircle : MonoBehaviour
{
    public float rotateSpeed = 20.0f;
    public Canvas childCanvas;

    void Start()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            rotateSpeed = 20 + Mathf.Pow(2*PlayerPrefs.GetInt("level"),0.5f);
        }
        else
        {
            rotateSpeed = 20.0f;
        }
    }

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        childCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
