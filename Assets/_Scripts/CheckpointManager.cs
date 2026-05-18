using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    public int totalRings;
    public int allowedMisses = 2;
    public List<int> passedRings = new List<int>();
    private int missedRings = 0;
    private int currentRingIndex = 0;
    public TextMeshProUGUI totalRingsText;
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        totalRingsText.text = totalRings.ToString();
    }
    public void PlayerPassedRing(int ringIndex)
    {
        if (ringIndex == currentRingIndex)
        {
            passedRings.Add(ringIndex);
            currentRingIndex++;
        }
        else if (ringIndex > currentRingIndex)
        {
            // Player skipped some rings
            missedRings += ringIndex - currentRingIndex;
            currentRingIndex = ringIndex + 1;
        }
        totalRingsText.text = currentRingIndex.ToString();
        CheckGameStatus();
    }

    void CheckGameStatus()
    {
        if (missedRings > allowedMisses)
        {
            Debug.Log("Game Over: Too many missed rings!");
            // Trigger Game Over
        }
        else if (passedRings.Count >= totalRings)
        {
            Debug.Log("Level Complete!");
            // Trigger Win
        }
    }
}
