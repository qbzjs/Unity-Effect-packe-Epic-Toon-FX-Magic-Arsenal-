using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColumnController : MonoBehaviour
{
    public bool isFinalColumn;
    [SerializeField] RectTransform startPoint, endPoint, centerPoint;
    public List<SlotNumber> slotNumbers;
    int spineCount = 0;
    public void Spin()
    {
        spineCount = 0;
        StartSpining();
    }

    public void StartSpining()
    {
        var t = spineCount.Equals(7) ? 0.3f : 0.05f;
        slotNumbers[0].transform.position = startPoint.position;
        slotNumbers[0].number = Random.Range(1, 7);
        slotNumbers[1].transform.position = centerPoint.position;
        slotNumbers[1].transform.DOMove(endPoint.position, t);
        slotNumbers[0].transform.DOMove(centerPoint.position, t).OnComplete(() =>
        {
            slotNumbers[1].number = Random.Range(1, 7);
            slotNumbers[1].transform.DOMove(centerPoint.position, t).From(startPoint.position);
            slotNumbers[0].transform.DOMove(endPoint.position, t).OnComplete(() =>
            {
                if (spineCount.Equals(7))
                {
                    spineCount = 0;
                    StartCoroutine(Shoot());
                    if (isFinalColumn)
                    {
                        SlotSpinController.instance.StopSpin();
                    }
                    return;
                }
                spineCount++;
                StartSpining();
            });
        });
    }

    IEnumerator Shoot()
    {
        var gameController = GameController.instance;
        var slotSpinController = SlotSpinController.instance;
        gameController.allGunController[slotNumbers[1].number - 1].ShowHighlightImage();
        yield return new WaitForEndOfFrame();
        if (isFinalColumn)
        {
            for (int i = 0; i < 3; i++)
            {
                if (slotSpinController.allColumnControllers[i].slotNumbers[1].number != 0)
                {
                    gameController.allGunController[slotSpinController.allColumnControllers[i].slotNumbers[1].number - 1].Shoot(slotSpinController.allColumnControllers[i].slotNumbers[1].number);
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }
}
