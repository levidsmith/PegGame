//2022 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour {
    public GameObject panelGameOver;
    public Text TextPegsLeft;
    void Start() {
        
    }

    void Update() {
        
    }

    public void showGameOver(int in_iPegsLeft) {
        panelGameOver.SetActive(true);
        if (in_iPegsLeft == 1) {
            TextPegsLeft.text = "YOU WIN " + in_iPegsLeft + " Peg Left";
        } else {
            TextPegsLeft.text = in_iPegsLeft + " Pegs Left";
        }


    }

    public void hideGameOver() {
        panelGameOver.SetActive(false);
    }
}