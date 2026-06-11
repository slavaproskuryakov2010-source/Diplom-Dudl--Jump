using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public Transform player;
    public float heihghtInterval = 1f;

    private float nextScoreHeight;
    private int score;
    private void Start()
    {
        nextScoreHeight = player.position.y + heihghtInterval;
        score = 0;
        scoreText.text =  score.ToString();
            

    }

    public void ScorePlus(int count)
    {
     score += count;
        scoreText.text = score.ToString();
    }
}
