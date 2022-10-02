//2022 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour {
    public GameObject panelGameOver;
    public GameObject panelOutOfTime;
    public Text TextPegsLeft;

    public Text TextTimeCountdown;
    public GameManager gamemanager;

    void Start() {
        
    }

    void Update() {
        if (!gamemanager.isGameOver) {
            TextTimeCountdown.text = string.Format("{0:0}", Mathf.FloorToInt(gamemanager.fTimeCountdown) + 1);
        } else {
            TextTimeCountdown.text = "";
        }

        
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

    public void showOutOfTime() {
        panelOutOfTime.SetActive(true);


    }

    public void hideOutOfTime() {
        panelOutOfTime.SetActive(false);


    }

}