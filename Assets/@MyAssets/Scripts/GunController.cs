using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunController : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] ParticleSystem bullet, demage;
    public void Shoot()
    {
        bullet.transform.position = startPoint.position;
        bullet.gameObject.Show();
        bullet.transform.LookAt(endPoint.position);
        bullet.transform.DOMove(endPoint.position, 0.7f).SetEase(Ease.Linear).OnComplete(() =>
        {
            bullet.gameObject.Hide();
        });
    }
}
