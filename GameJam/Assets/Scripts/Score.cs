using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public int score_value;
    public TextMeshProUGUI text;
    public SpaceMan miniship;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        score_value = miniship.score;
        text.text = score_value.ToString();
    }
}
