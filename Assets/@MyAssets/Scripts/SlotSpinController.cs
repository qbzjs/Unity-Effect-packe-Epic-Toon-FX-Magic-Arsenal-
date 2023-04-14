using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class SlotSpinController : MonoBehaviour
{
    public static SlotSpinController instance;
    public List<ColumnController> allColumnControllers;
    public Image slotMachineUp, slotMachineDown;
    [SerializeField] GameObject spinButton;

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
        spinButton.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            spinButton.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.Linear);
            StartCoroutine(StartSpinAnimation());
        });
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
