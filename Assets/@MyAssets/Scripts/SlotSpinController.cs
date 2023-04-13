using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SlotSpinController : MonoBehaviour
{
    public static SlotSpinController instance;
    public List<ColumnController> allColumnControllers;
    public Image slotMachineUp, slotMachineDown;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void SpinAnimation()
    {
        StartCoroutine(StartSpinAnimation());
    }
    IEnumerator StartSpinAnimation()
    {
        slotMachineUp.gameObject.Hide();
        slotMachineDown.gameObject.Show();
        for (int i = 0; i < allColumnControllers.Count; i++)
        {
            yield return new WaitForEndOfFrame();
            allColumnControllers[i].Spin();
        }
    }

    public void StopSpin()
    {
        slotMachineDown.gameObject.Hide();
        slotMachineUp.gameObject.Show();
    }
}
