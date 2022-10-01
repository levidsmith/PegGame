//2022 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject PegPrefab;
    public GameObject HolePrefab;
    public GameObject FillInvPrefab;
    public GameObject Board;

    public DisplayManager displaymanager;


    public const float fHoleWidth = 2f;
    public const float fHoleHeight = 0.866025f * 2f;

    void Start() {
        displaymanager.hideGameOver();
        setupBoard(); 
    }


    void Update() {
        Peg[] listPegs = Board.GetComponentsInChildren<Peg>();

        foreach (Peg p in listPegs) {
            p.isHovered = false;
        }

        Hole[] listHoles = Board.GetComponentsInChildren<Hole>();
        foreach (Hole h in listHoles) {
            h.isHovered = false;
        }


        handleInput();

    }

    private void setupBoard() {
        int i, j;


        for (i = 0; i < 5; i++) {
            for (j = 0; j <= i; j++) {
                Vector3 pos = new Vector3((i * -1f) + j * fHoleWidth, 0f, (-i * fHoleHeight));
                
                Hole h = Instantiate(HolePrefab, pos, Quaternion.identity).GetComponent<Hole>();
                h.transform.SetParent(Board.transform);
                h.iRow = i;
                h.iCol = j;

                if (i < 4) {
                    GameObject obj = Instantiate(FillInvPrefab, pos + new Vector3(0f, 0f, -fHoleHeight), Quaternion.identity);
                    obj.transform.SetParent(Board.transform);
                }


                if (!(i == 0 && j == 0)) {
                    Peg p = Instantiate(PegPrefab, pos + new Vector3(Hole.posPegX, 0f, Hole.posPegZ), Quaternion.identity).GetComponent<Peg>();
                    p.transform.SetParent(Board.transform);
                    p.iRow = i;
                    p.iCol = j;
                    p.hole = h;
                    h.peg = p;
                }

                

            }
        }
    }

    private void handleInput() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hit);

        if (hit.collider != null) {
            Peg p = hit.collider.GetComponent<Peg>();

            //if (hit.collider.transform.parent != null) {
//                p = hit.collider.transform.parent.GetComponent<Peg>();
//            }
            
            if (p != null) {
                //Debug.Log("Hovered over Peg");
                p.isHovered = true;

                if (Input.GetMouseButtonDown(0)) {
                    p.isDragged = true;

                }


            }

            Hole h = null;
            if (hit.collider.transform.parent != null) {
                h = hit.collider.transform.parent.GetComponent<Hole>();
            }
            
            if (h != null) {
//                Debug.Log("Hovered over hole");
                h.isHovered = true;

            }
        }


        //move any dragged peg
        Peg[] listPegs = Board.GetComponentsInChildren<Peg>();
        foreach(Peg p in listPegs) {
            if (p.isDragged) {
                //Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2f));
                Vector3 pos = hit.point;
                p.transform.position = pos;
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            foreach (Peg p in listPegs) {
                if (p.isDragged) {
                    p.isDragged = false;

                    bool isSet = false;
                    //check for hole
                    Hole[] listHoles = Board.GetComponentsInChildren<Hole>();

                    foreach (Hole h in listHoles) {
                        if (h.isHovered) {

                            Peg pegHopped = null;
                            if (p.iRow - 2 == h.iRow && p.iCol == h.iCol) {
                                Hole holeHopped = getHoleAt(p.iRow - 1, p.iCol);
                                if (holeHopped != null) {
                                    pegHopped = holeHopped.peg;
                                }

                            } else if (p.iRow - 2 == h.iRow && p.iCol - 2 == h.iCol) {
                                Hole holeHopped = getHoleAt(p.iRow - 1, p.iCol - 1);
                                if (holeHopped != null) {
                                    pegHopped = holeHopped.peg;
                                }


                            } else if (p.iRow == h.iRow && p.iCol - 2 == h.iCol) {
                                Hole holeHopped = getHoleAt(p.iRow, p.iCol - 1);
                                if (holeHopped != null) {
                                    pegHopped = holeHopped.peg;
                                }

                            } else if (p.iRow == h.iRow && p.iCol + 2 == h.iCol) {
                                Hole holeHopped = getHoleAt(p.iRow, p.iCol + 1);
                                if (holeHopped != null) {
                                    pegHopped = holeHopped.peg;
                                }

                            } else if (p.iRow + 2 == h.iRow && p.iCol == h.iCol) {
                                Hole holeHopped = getHoleAt(p.iRow + 1, p.iCol);
                                if (holeHopped != null) {
                                    pegHopped = holeHopped.peg;
                                }

                            } else if (p.iRow + 2 == h.iRow && p.iCol + 2 == h.iCol) {
                                Hole holeHopped = getHoleAt(p.iRow + 1, p.iCol + 1);
                                if (holeHopped != null) {
                                    pegHopped = holeHopped.peg;
                                }




                            }

                            if (pegHopped != null) {
                                DestroyImmediate(pegHopped.gameObject);
                                //Destroy(pegHopped.gameObject);
                                h.setPeg(p);
                                isSet = true;
                                checkGameOver();
                            }
                        }
                    }

                    if (!isSet) {
                        p.transform.position = p.hole.transform.position + new Vector3(Hole.posPegX, 0f, Hole.posPegZ);
                    }

                    //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //p.transform.position = pos;
                }
            }

        }
    }

    public Hole getHoleAt(int in_iRow, int in_iCol) {
        Hole holeReturn = null;
        Hole[] listHoles = Board.GetComponentsInChildren<Hole>();
        foreach (Hole h in listHoles) {
            if (h.iRow == in_iRow && h.iCol == in_iCol) {
                holeReturn = h;
            }
        }

        return holeReturn;
    }

    public Peg getPegAt(int in_iRow, int in_iCol) {
        Peg pegReturn = null;
        Peg[] listPegs = Board.GetComponentsInChildren<Peg>();
        foreach (Peg p in listPegs) {
            if (p.iRow == in_iRow && p.iCol == in_iCol) {
                pegReturn = p;
            }
        }

        return pegReturn;
    }


    public void checkGameOver() {
        Debug.Log("Check game over");
        bool isValidMovesRemaining;
        isValidMovesRemaining = false;

        Peg[] listPegs = Board.GetComponentsInChildren<Peg>();
        
        int[,] checks = new int[6,2]{
            {-1, 0},
            {-1, -1 },
            {0, -1},
            {1, 0 },
            {1, 1 },
            {0, 1 }
        };
        

        foreach (Peg p in listPegs) {
            int i;
            for (i = 0; i < 6; i++) {
                Peg pegHopped = getPegAt(p.iRow + checks[i, 0], p.iCol + checks[i, 1]);
                if (pegHopped != null) {
                    
                    Hole h = getHoleAt(p.iRow + checks[i, 0] * 2, p.iCol + checks[i, 1] * 2);
                    if (h != null) {
                        if (h.peg == null) {
                            isValidMovesRemaining = true;
//                            Debug.Log("Direction: " + checks[i, 0] + ", " + checks[i, 1]);
//                            Debug.Log("Valid move: peg " + p.iRow + ", " + p.iCol + " hole: " + h.iRow + ", " + h.iCol);
                        }

                    }
                }
            }

        }


        if (!isValidMovesRemaining) {
            Debug.Log("GAME OVER");
            Debug.Log(listPegs.Length + " Pegs remaining");
            displaymanager.showGameOver(listPegs.Length);
        }



    }

    public void doRestart() {
        //foreach(GameObject obj in Board.transform.)
        int i;
        for (i = 0; i < Board.transform.childCount; i++) {
            Destroy(Board.transform.GetChild(i).gameObject);
        }
        displaymanager.hideGameOver();
        setupBoard();

    }

    public void doQuit() {
        Application.Quit();
    }

}