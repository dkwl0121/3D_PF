using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceControl : MonoBehaviour
{
    [HideInInspector] public E_CHARACTER_TYPE eEnemyNo;
    public int nMaxCount;
    public int nRecreateSeconds;
    public bool isRecreate;
    public bool isFixedRot;
    public bool isFixed = false;

    private List<GameObject> lstEnemy;

    private void Start()
    {
        if (eEnemyNo == E_CHARACTER_TYPE.INVALID) Destroy(this.gameObject);

        lstEnemy = new List<GameObject>();

        for (int i = 0; i < nMaxCount; ++i)
        {
            if (isFixedRot)
                lstEnemy.Add(EnemyPool.Instace.ActiveEnemy(
                    eEnemyNo, this.transform.position, this.transform.rotation, isFixed));
            else
                lstEnemy.Add(EnemyPool.Instace.ActiveEnemy(
                    eEnemyNo, this.transform.position, Quaternion.Euler(Random.Range(0, 360), 0, Random.Range(0, 360)), isFixed));
        }
    }

    private void Update()
    {
        for (int i = 0; i < lstEnemy.Count; ++i)
        {
            // 비활성화 되었다면 지우고
            if (!lstEnemy[i].activeSelf)
            {
                lstEnemy.RemoveAt(i);
                if (isRecreate) 
                    StartCoroutine(Recreate()); //일정 시간 경과 후 생성
            }
        }
    }

    private IEnumerator Recreate()
    {
        yield return new WaitForSeconds(nRecreateSeconds);

        lstEnemy.Add(EnemyPool.Instace.ActiveEnemy(
            eEnemyNo, this.transform.position, Quaternion.Euler(Random.Range(0, 360), 0, Random.Range(0, 360)), isFixed));
    }

    public bool IsDie()
    {
        return lstEnemy.Count == 0;
    }
}
