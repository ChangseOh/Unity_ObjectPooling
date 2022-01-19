using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField]
    private MonoBehaviour[] poolPrefab;//여기 풀링할 오브젝트가 순서대로 들어간다

    Dictionary<int, Queue<MonoBehaviour>> dics = new Dictionary<int, Queue<MonoBehaviour>>();

    private void Awake()
    {
        Instance = this;

        var initInfo = new Dictionary<int, int>();
        initInfo.Add(0, 10);//0번 프리팹 10개
        initInfo.Add(1, 10);//1번 프리팹 10개

        Initialize(initInfo);
    }

    private void Initialize(Dictionary<int, int> initInfo)
    {
        int _max = (int)Mathf.Max(poolPrefab.Length, initInfo.Count);//초기화 대상 오브젝트의 종류 수
        for (int i = 0; i < _max; i++)
        {
            var dic = new Queue<MonoBehaviour>();
            for (int j = 0; j < initInfo[i]; j++)
            {
                dic.Enqueue(CreateNewObject(i));
            }
            dics.Add(i, dic);
        }
    }
    private MonoBehaviour CreateNewObject(int keyNum)
    {
        var newObj = Instantiate(poolPrefab[keyNum]).GetComponent<MonoBehaviour>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }
    public static MonoBehaviour GetObject(int keyNum)
    {
        var dic = Instance.dics[keyNum];

        if (dic.Count > 0)
        {
            var obj = dic.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject(keyNum);
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }
    public static void ReturnObject(int keyNum, MonoBehaviour obj)
    {
        var dic = Instance.dics[keyNum];

        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        dic.Enqueue(obj);
    }

    private void OnDestroy()
    {

        for (int i = 0; i < poolPrefab.Length; i++)
        {
            var dic = dics[i];// new Queue<MonoBehaviour>();
            while(dic.Count > 0)
            {
                var obj = dic.Dequeue();
                Destroy(obj);
            }
        }
    }
}
