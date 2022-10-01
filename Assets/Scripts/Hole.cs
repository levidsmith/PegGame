//2022 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour {
    public GameObject model;
    public int iRow, iCol;
    public bool isHovered;
    public Material matHole;
    public Material matHoleHighlight;

    public const float posPegX = 0f;
    public const float posPegZ = -0.2f;

    public Peg peg;

    void Start() {
        
    }

    void Update() {
        if (isHovered) {
            model.GetComponent<Renderer>().material = matHoleHighlight;
        } else {
            model.GetComponent<Renderer>().material = matHole;
        }

    }

    public bool setPeg(Peg in_p) {
        bool isValid = false;
        if (peg == null) {
            in_p.hole.peg = null;

            peg = in_p;
            peg.hole = this;
            peg.iRow = iRow;
            peg.iCol = iCol;
            peg.transform.localPosition = transform.position + new Vector3(posPegX, 0f, posPegZ);
            
            isValid = true;
        }

        return isValid;

    }
}