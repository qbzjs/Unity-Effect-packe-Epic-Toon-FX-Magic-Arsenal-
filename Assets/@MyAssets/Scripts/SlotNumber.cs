using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotNumber : MonoBehaviour
{
    public int number;
    TextMeshProUGUI numberText;
    private void Start()
    {
        numberText = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        numberText.text = number.ToString();
    }
}
