using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;
    public int maxEnemyCount;
    public Enemy enemy;
    public Transform enemyInstantiatePoint;
    //public TicketController ticketController;
    public List<Enemy> allEnemy;

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
    }

    private void Start()
    {
        instanceSpawing(10);
    }

    IEnumerator SpawnCustomer(int numberOfEnemy)
    {
        for (int i = 0; i < numberOfEnemy; i++)
        {
            yield return new WaitForSeconds(1);
            if (allEnemy.Count < maxEnemyCount)
            {
                var t = Instantiate(enemy, enemyInstantiatePoint.position, enemyInstantiatePoint.rotation);
                allEnemy.Add(t);
                SetPosition(t);
            }
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
}
