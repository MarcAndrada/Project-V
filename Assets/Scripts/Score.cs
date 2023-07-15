using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreCounter;

    private int score;

    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        scoreCounter.text = "Score: " + score.ToString();
    }

    public void SetScore(int objectScore)
    {
        score += objectScore; 
    }
}
