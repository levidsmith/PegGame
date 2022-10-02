//2022 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public GameManager gamemanager;
    public Vector3 posDefault;

    float fOrbitTime;

    void Start() {
        posDefault = transform.position;
        
    }

    void Update() {
        Vector3 posCenter = new Vector3(0f, 0f, -5.2f);
        if (!gamemanager.isGameOver) {
            transform.position = posDefault;
            transform.LookAt(posCenter);
        } else {
            float fSpeed = 0.5f;
            float fOrbitDistance = 8f;
            float fOrbitHeight = 16f;
            fOrbitTime += Time.deltaTime * fSpeed;
            transform.position = new Vector3(posCenter.x + fOrbitDistance * Mathf.Cos(fOrbitTime), fOrbitHeight, posCenter.z + fOrbitDistance * Mathf.Sin(fOrbitTime));
            transform.LookAt(posCenter);

        }
        
    }
}