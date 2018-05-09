using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageValuePool : MonoBehaviour
{
    private static DamageValuePool sInstance = null;
    public static DamageValuePool Instace
    {
        get
        {
            if (sInstance == null)
            {
                GameObject gObject = new GameObject("_DamagePool");
                sInstance = gObject.AddComponent<DamageValuePool>();
            }
            return sInstance;
        }
    }

    private GameObject objDamage;               // 사용할 데미지 오브젝트
    private List<GameObject> listDamagePool;    // 데미지 풀

    private int nPoolNum = 20;

    public void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void Setup()
    {
        objDamage = Resources.Load(Util.ResourcePath.UI_DAMAGE) as GameObject;
        listDamagePool = new List<GameObject>();

        for (int i = 0; i < nPoolNum; ++i)
        {
            GameObject newObject = Instantiate(objDamage);
            newObject.SetActive(false);
            newObject.transform.SetParent(this.transform);
            listDamagePool.Add(newObject);
        }
    }

    public void ShowDamage(Vector3 pos, float damage, bool isPlayer)
    {
        int index = GetCurrPoolIndex();

        listDamagePool[index].SetActive(true);
        listDamagePool[index].GetComponent<DamageValue>().SetInfo(pos, damage, isPlayer ? Color.red : Color.yellow);

        StartCoroutine(DelayDiable(index));
    }

    private IEnumerator DelayDiable(int index)
    {
        yield return new WaitForSeconds(0.5f);

        listDamagePool[index].SetActive(false);
    }

    private int GetCurrPoolIndex()
    {
        for (int i = 0; i < listDamagePool.Count; ++i)
        {
            if (!listDamagePool[i].activeSelf)
                return i;
        }

        MakeMore();

        return listDamagePool.Count - 1;
    }
    private void MakeMore()
    {
        GameObject newObject = Instantiate(objDamage);
        newObject.SetActive(false);
        newObject.transform.SetParent(this.transform);
        listDamagePool.Add(newObject);
    }

    public void DisableAll()
    {
        for (int i = 0; i < listDamagePool.Count; ++i)
        {
            listDamagePool[i].SetActive(false);
        }
    }
}
