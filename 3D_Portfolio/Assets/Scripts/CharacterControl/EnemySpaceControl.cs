using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceControl : MonoBehaviour
{
    public int nEnemyNo;
    public int nMaxCount;
    public int nRecreateSeconds;
    public bool isRecreate;
    public bool isFixedRot;

    private List<GameObject> lstEnemy;

    private void Start()
    {
        lstEnemy = new List<GameObject>();

        for (int i = 0; i < nMaxCount; ++i)
        {
            if (isFixedRot)
                lstEnemy.Add(EnemyPool.Instace.ActiveEnemy(
                    (E_CHARACTER_TYPE)nEnemyNo, this.transform.position, this.transform.rotation));
            else
                lstEnemy.Add(EnemyPool.Instace.ActiveEnemy(
                    (E_CHARACTER_TYPE)nEnemyNo, this.transform.position, Quaternion.Euler(Random.Range(0, 360), 0, Random.Range(0, 360))));
        }
    }

    private void Update()
    {
        if (!isRecreate) return;

        for (int i = 0; i < lstEnemy.Count; ++i)
        {
            // 비활성화 되었다면 지우고 일정 시간 경과 후 생성
            if (!lstEnemy[i].activeSelf)
            {
                lstEnemy.RemoveAt(i);
                StartCoroutine(Recreate());
            }
        }
    }

    private IEnumerator Recreate()
    {
        yield return new WaitForSeconds(nRecreateSeconds);

        lstEnemy.Add(EnemyPool.Instace.ActiveEnemy(
            (E_CHARACTER_TYPE)nEnemyNo, this.transform.position, Quaternion.Euler(Random.Range(0, 360), 0, Random.Range(0, 360))));
    }
}
