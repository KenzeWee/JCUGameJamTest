using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Scoreboard : MonoBehaviour {
    public List<PlayerVariable> allPlayers = new List<PlayerVariable> ();

    public GameObject scoreboard;
    public TMPro.TextMeshProUGUI scoreText;

    public void ShowScoreBoard () {
        scoreboard.SetActive (true);

        allPlayers = allPlayers.QuickSortList ();

        scoreText.text = "GAME SET\n";

        int pos = 0;

        foreach (PlayerVariable player in allPlayers) {
            if (player.controlType != StaticFunctions.CONTROLLERTYPE.NULL) {
                //POS
                switch (pos) {
                    case 0:
                        scoreText.text += (pos + 1) + "st" + " - " + player.currentName + " (" + player.CurrentScore + " Death)\n";
                        break;
                    case 1:

                        scoreText.text += (pos + 1) + "nd" + " - " + player.currentName + " (" + player.CurrentScore + " Death)\n";
                        break;
                    case 2:

                        scoreText.text += (pos + 1) + "rd" + " - " + player.currentName + " (" + player.CurrentScore + " Death)\n";
                        break;
                    case 3:

                        scoreText.text += (pos + 1) + "th" + " - " + player.currentName + " (" + player.CurrentScore + " Death)\n";
                        break;
                }
                pos++;
            }

        }
    }
}