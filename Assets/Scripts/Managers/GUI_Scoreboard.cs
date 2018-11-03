using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Scoreboard : MonoBehaviour {
    public List<PlayerVariable> allPlayers = new List<PlayerVariable>();

    public GameObject scoreboard;
    public TMPro.TextMeshProUGUI scoreText;

    public void ShowScoreBoard()
    {
        scoreboard.SetActive(true);

        allPlayers = allPlayers.QuickSortList();

        scoreText.text = "GAME SET\n";

        int pos = 0;

        foreach(PlayerVariable player in allPlayers)
        {
            //POS
            switch (pos)
            {
                case 0:
                    scoreText.text +=  "1st" + " - " + player.currentName + " (" + player.CurrentScore + " Death)\n" ;
                    break;
                case 1:

                    scoreText.text += "2nd" + " - " + player.currentName + " (" + player.CurrentScore + " Death)\n";
                    break;
                case 2:

                    scoreText.text += "3rd" + " - " + player.currentName + " (" + player.CurrentScore + " Death)\n";
                    break;
                case 3:

                    scoreText.text += "4th" + " - " + player.currentName + " (" + player.CurrentScore + " Death)\n";
                    break;
            }

            pos++;
        }
    }
}
