using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSideTrigger : MonoBehaviour
{
    public int sideNumber;
    GameController gameController;
    void Start()
    {
        gameController = GameController.instance;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("DiceNumberDetector"))
        {
            gameController.currentDiceNumber = sideNumber;
        }
    }
}
