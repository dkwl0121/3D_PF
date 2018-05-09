using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private static EnemyPool sInstance = null;
    public static EnemyPool Instace
    {
        get
        {
            if (sInstance == null)
            {
                GameObject gObject = new GameObject("_EnemyPool");
                sInstance = gObject.AddComponent<EnemyPool>();
            }
            return sInstance;
        }
    }

    private GameObject[] arrObjEnemy;               // 사용할 에너미 오브젝트
    private List<GameObject>[] arrEnemyPool;        // 에너미 풀
    private Transform[] arrTfParent;                // 오브젝트를 담을 부모

    private int nPoolNum = 10;                      // 첫 풀링 갯수

    public void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void Setup()
    {
        arrObjEnemy = new GameObject[(int)E_CHARACTER_TYPE.MAX];
        for (int i = 0; i < arrObjEnemy.Length; ++i)
        {
            switch ((E_CHARACTER_TYPE)i)
            {
                case E_CHARACTER_TYPE.MUSHROOM_01:
                    {
                        arrObjEnemy[i] = Resources.Load(Util.ResourcePath.MUSHROOM_01) as GameObject;
                    }
                    break;
                case E_CHARACTER_TYPE.MUSHROOM_02:
                    {
                        arrObjEnemy[i] = Resources.Load(Util.ResourcePath.MUSHROOM_02) as GameObject;
                    }
                    break;
                case E_CHARACTER_TYPE.MUSHROOM_03:
                    {
                        arrObjEnemy[i] = Resources.Load(Util.ResourcePath.MUSHROOM_03) as GameObject;
                    }
                    break;
                case E_CHARACTER_TYPE.STONE_MONSTER:
                    {
                        arrObjEnemy[i] = Resources.Load(Util.ResourcePath.STONE_MONSTER) as GameObject;
                    }
                    break;
                case E_CHARACTER_TYPE.PUMPKIN:
                    {
                        arrObjEnemy[i] = Resources.Load(Util.ResourcePath.PUMPKIN) as GameObject;
                    }
                    break;
            }
        }

        arrTfParent = new Transform[arrObjEnemy.Length];
        for (int i = 0; i < arrObjEnemy.Length; ++i)
        {
            switch ((E_CHARACTER_TYPE)i)
            {
                case E_CHARACTER_TYPE.MUSHROOM_01:
                    {
                        arrTfParent[i] = new GameObject(Util.ResourcePath.MUSHROOM_01).transform;
                    }
                    break;
                case E_CHARACTER_TYPE.MUSHROOM_02:
                    {
                        arrTfParent[i] = new GameObject(Util.ResourcePath.MUSHROOM_02).transform;
                    }
                    break;
                case E_CHARACTER_TYPE.MUSHROOM_03:
                    {
                        arrTfParent[i] = new GameObject(Util.ResourcePath.MUSHROOM_03).transform;
                    }
                    break;
                case E_CHARACTER_TYPE.STONE_MONSTER:
                    {
                        arrTfParent[i] = new GameObject(Util.ResourcePath.STONE_MONSTER).transform;
                    }
                    break;
                case E_CHARACTER_TYPE.PUMPKIN:
                    {
                        arrTfParent[i] = new GameObject(Util.ResourcePath.PUMPKIN).transform;
                    }
                    break;
            }

            arrTfParent[i].transform.SetParent(this.transform);
        }

        arrEnemyPool = new List<GameObject>[arrObjEnemy.Length];
        for (int i = 0; i < arrEnemyPool.Length; ++i)
        {
            arrEnemyPool[i] = new List<GameObject>();

            // 풀링 갯수 만큼 미리 생성
            for (int j = 0; j < nPoolNum; ++j)
            {
                GameObject newObject = Instantiate(arrObjEnemy[i]);
                newObject.SetActive(false);
                newObject.transform.SetParent(arrTfParent[i]);
                arrEnemyPool[i].Add(newObject);
            }
        }
    }

    public GameObject ActiveEnemy(E_CHARACTER_TYPE eType, Vector3 pos, Quaternion rot)
    {
        if (eType <= E_CHARACTER_TYPE.INVALID || eType >= E_CHARACTER_TYPE.MAX) return null;

        int nCurrIndex = GetCurrPoolIndex(eType);

        GameObject objActive = null;
        
        objActive = arrEnemyPool[(int)eType][nCurrIndex];
        objActive.transform.position = pos;
        objActive.transform.rotation = rot;
        objActive.SetActive(true);

        return objActive;
    }

    private int GetCurrPoolIndex(E_CHARACTER_TYPE eType)
    {
        int index = (int)eType;

        // 비활성화 에너미가 있으면
        for (int i = 0; i < arrEnemyPool[index].Count; ++i)
        {
            if (!arrEnemyPool[index][i].activeSelf)
                return i;
        }

        MakeEnemyMore(eType);

        return arrEnemyPool[index].Count - 1;
    }

    private void MakeEnemyMore(E_CHARACTER_TYPE eType)
    {
        int index = (int)eType;

        // 비활성화 에너미가 없으면 - 새로 생성
        arrEnemyPool[index] = new List<GameObject>();
        GameObject newObject = Instantiate(arrObjEnemy[index]);
        newObject.SetActive(false);
        newObject.transform.SetParent(arrTfParent[index]);
        arrEnemyPool[index].Add(newObject);
    }

    public void DisableAll()
    {
        for (int i = 0; i < arrEnemyPool.Length; ++i)
        {
            for (int j = 0; j < arrEnemyPool[i].Count; ++j)
            {
                arrEnemyPool[i][j].SetActive(false);
            }
        }
    }
}
