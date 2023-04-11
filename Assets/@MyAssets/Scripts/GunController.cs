using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunController : MonoBehaviour
{
    [SerializeField] GameObject gun;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] ParticleSystem bullet, demage;

    Vector3 _startRot;
    List<Rigidbody> _allGunPart = new List<Rigidbody>();
    EnemyController enemyController;

    private void Start()
    {
        enemyController = EnemyController.instance;    
        var temp = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < temp.Length; i++)
        {
            _allGunPart.Add(temp[i]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemyController.StopAllEnemy();
            for (int i = 0; i < _allGunPart.Count; i++)
            {
                _allGunPart[i].isKinematic = false;
            }
            LevelManager.instance.FailLevel();
        }
    }

    public void Shoot(int count)
    {
        var pos = endPoint.position;
        StartCoroutine(ShootBullet(count));
        //var enemyController = EnemyController.instance;
        /*Enemy enemy = null;
        if (enemyController.allEnemy.Count != 0)
        {
            enemy = enemyController.allEnemy.Find(x => x.isTargeted == false);
            if (enemy)
            {
                pos = enemy.damagePoint.position;
            }
        }
        gun.transform.DOLookAt(pos, 0.2f).OnComplete(() =>
        {
            bullet.transform.position = startPoint.position;
            bullet.gameObject.Show();
            bullet.transform.LookAt(pos);
            bullet.transform.DOMove(pos, 0.7f).SetEase(Ease.Linear).OnComplete(() =>
            {
                gun.transform.DORotate(Vector3.zero, 0.2f);
                bullet.gameObject.Hide();
                demage.transform.position = pos;
                demage.Play();
                if (enemy)
                {
                    enemy.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
                    {
                        enemyController.allEnemy.Remove(enemy);
                        Destroy(enemy.gameObject);
                        enemyController.CheckForNewWave();
                    });
                }

            });
        });*/
    }

    IEnumerator ShootBullet(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(0.5f);
            var pos = endPoint.position;
            Enemy enemy = null;
            if (enemyController.allEnemy.Count != 0)
            {
                enemy = enemyController.allEnemy.Find(x => x.isTargeted == false);
                if (enemy)
                {
                    enemy.isTargeted = true;
                    pos = enemy.damagePoint.position;
                }
            }
            gun.transform.DOLookAt(pos, 0.2f).OnComplete(() =>
            {
                var temp = Instantiate(bullet, startPoint.position, Quaternion.identity);
                //bullet.transform.position = startPoint.position;
                temp.gameObject.Show();
                temp.transform.LookAt(pos);
                temp.transform.DOMove(pos, 0.7f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    gun.transform.DORotate(Vector3.zero, 0.2f);
                    //bullet.gameObject.Hide();
                    Destroy(temp.gameObject);
                    /*demage.transform.position = pos;
                    demage.Play();*/
                    var efect = Instantiate(demage, pos, Quaternion.identity);
                    efect.Play();
                    if (enemy)
                    {
                        enemy.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
                        {
                            enemyController.allEnemy.Remove(enemy);
                            Destroy(enemy.gameObject);
                            enemyController.CheckForNewWave();
                            Destroy(efect);
                        });
                    }

                });
            });
            /*if (i.Equals(count - 1))
            {
                GameController.instance.diceController.diceSpeenButton.Show();
            }*/
        }
    }
}
