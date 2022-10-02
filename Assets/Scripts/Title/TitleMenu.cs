//2022 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour {
    public Dropdown dropdownColor;

    void Start() {
        
    }

    void Update() {
        
    }

    public void doStart() {
        Options.iPegColor = dropdownColor.value;
        Debug.Log("Peg color: " + Options.iPegColor);
        SceneManager.LoadScene("game");

    }

    public void doQuit() {
        Application.Quit();
    }
}