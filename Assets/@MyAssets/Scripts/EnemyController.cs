using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    public int maxEnemyCount;
    public Enemy enemy;
    public Transform enemyInstantiatePoint;
    //public TicketController ticketController;
    public List<Enemy> allEnemy;
    public List<Enemy> wave2AllEnemy;
    public List<Enemy> wave3AllEnemy;
    public List<Enemy> wave4AllEnemy;
    public Enemy enemyBoss;

    protected void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        GameController.OnGameStart += EnemyControllerOnGameStart;
        GameController.OnGameFinish += EnemyControllerOnGameFinish;
    }
    void EnemyControllerOnGameStart()
    {
        GameController.OnGameStart -= EnemyControllerOnGameStart;
        enabled = true;
        //instanceSpawing(10);
        for (int i = 0; i < allEnemy.Count; i++)
        {
            SetPosition(allEnemy[i]);
        }
    }
    void OnDestroy()
    {
        GameController.OnGameStart -= EnemyControllerOnGameStart;
        GameController.OnGameFinish -= EnemyControllerOnGameFinish;
    }

    void EnemyControllerOnGameFinish(bool state)
    {
        GameController.OnGameFinish -= EnemyControllerOnGameFinish;
        enabled = false;
    }

    IEnumerator SpawnCustomer(int numberOfEnemy)
    {
        for (int i = 0; i < numberOfEnemy; i++)
        {
            yield return new WaitForSeconds(1);
            /*if (allEnemy.Count < maxEnemyCount)
            {*/
            var t = Instantiate(enemy, enemyInstantiatePoint.position, enemyInstantiatePoint.rotation);
            allEnemy.Add(t);
            SetPosition(t);
            //}
        }
    }

    public void instanceSpawing(int numberOfEnemy)
    {
        StartCoroutine(SpawnCustomer(numberOfEnemy));
    }
    int tempCount = 0;
    public void SetPosition(Enemy enemy)
    {
        var pos = GameController.instance.allGunController[tempCount].transform.position;
        tempCount++;
        //var angle = ticketController.stadingPoint.eulerAngles;
        if (tempCount.Equals(6))
        {
            tempCount = 0;
        }

        enemy.SetTarget(pos);
    }

    public void CheckForNewWave()
    {
        if (allEnemy.Count < 10)
        {
            //instanceSpawing(10);
            if (wave2AllEnemy.Count.Equals(0))
            {
                if (wave3AllEnemy.Count.Equals(0))
                {
                    if (wave4AllEnemy.Count.Equals(0))
                    {
                        if (allEnemy.Count.Equals(0))
                        {
                            if (enemyBoss == null)
                            {
                                LevelManager.instance.WinLevel();
                            }
                            else
                            {
                                enemyBoss.transform.DOScale(Vector3.one, 0.2f).OnComplete(() =>
                                {
                                    allEnemy.Add(enemyBoss);
                                    enemyBoss = null;
                                    for (int i = 0; i < allEnemy.Count; i++)
                                    {
                                        SetPosition(allEnemy[i]);
                                    }
                                }); ;
                            }
                        }
                    }
                    else
                    {
                        StartCoroutine(ActiveWave2(wave4AllEnemy));
                    }
                }
                else
                {
                    StartCoroutine(ActiveWave2(wave3AllEnemy));
                }
            }
            else
            {
                StartCoroutine(ActiveWave2(wave2AllEnemy));
            }
        }

    }

    IEnumerator ActiveWave2(List<Enemy> enemys)
    {
        for (int i = enemys.Count - 1; i >= 0; i--)
        {
            var temp = i;
            //wave2AllEnemy[i].Show();
            enemys[temp].transform.DOScale(Vector3.one, 0.2f).OnComplete(() =>
            {
                allEnemy.Add(enemys[temp]);
                enemys.Remove(enemys[temp]);
                //enemys.Remove(enemys[temp]);
            });
            //SetPosition(wave2AllEnemy[temp]);
        }
        yield return new WaitForSeconds(2);
        for (int i = 0; i < allEnemy.Count; i++)
        {
            SetPosition(allEnemy[i]);
        }
    }

    public void StopAllEnemy()
    {
        for (int i = 0; i < allEnemy.Count; i++)
        {
            allEnemy[i].StopAgent();
        }
        for (int i = 0; i < wave2AllEnemy.Count; i++)
        {
            wave2AllEnemy[i].StopAgent();
        }
    }
}
