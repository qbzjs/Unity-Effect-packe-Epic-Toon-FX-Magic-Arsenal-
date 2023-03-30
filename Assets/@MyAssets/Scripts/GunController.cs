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
        var pos = endPoint.position;
        var enemyController = EnemyController.instance;
        Enemy enemy = null;
        if (enemyController.allEnemy.Count != 0)
        {
            enemy = enemyController.allEnemy.Find(x => x.isTargeted == false);
            if (enemy)
            {
                pos = enemy.damagePoint.position;
            }

        }
        bullet.transform.LookAt(pos);
        bullet.transform.DOMove(pos, 0.7f).SetEase(Ease.Linear).OnComplete(() =>
        {
            bullet.gameObject.Hide();
            demage.transform.position = pos;
            demage.Play();
            if (enemy)
            {
                enemy.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
                {
                    enemyController.allEnemy.Remove(enemy);
                    Destroy(enemy.gameObject);
                });
            }

        });
    }
}
