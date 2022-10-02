//2022 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peg : MonoBehaviour {

    public GameObject model;
    public int iRow, iCol;
    public bool isHovered;
    public bool isDragged;
    public Material matPeg;
    public Material matPegHighlight;

    public Hole hole;

    public List<Color> listPegColors;
    public int iPegColor;

    void Start() {
        //iPegColor = Random.Range(0, 8);
        


    }

    void Update() {
        if (isHovered) {
            model.GetComponent<Renderer>().material = matPegHighlight;
        } else {
            model.GetComponent<Renderer>().material = matPeg;
            model.GetComponent<Renderer>().material.color = listPegColors[iPegColor];
        }

        if (isDragged) {
//            Debug.Log("peg dragged");
            GetComponent<Collider>().enabled = false;
        } else {
            GetComponent<Collider>().enabled = true;
        }
        
        
    }

    public override string ToString() {
        return "(" + iRow + ", " + iCol + ")";
    }

}