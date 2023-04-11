using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlotSpinController : MonoBehaviour
{
    public static SlotSpinController instance;
    public List<ColumnController> allColumnControllers;

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
        for (int i = 0; i < allColumnControllers.Count; i++)
        {
            yield return new WaitForEndOfFrame();
            allColumnControllers[i].Spin();
        }
    }
}
