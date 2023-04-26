using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectPool : Singleton<ObjectPool>
{
    public List<Pool> pools = new List<Pool>();
    public Dictionary<string, List<GameObject>> poolDictionary = new Dictionary<string, List<GameObject>>();

    public void Awake()
    {
        foreach (Pool pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject go = Instantiate(pool.prefab);
                if (go.GetComponent<RectTransform>() == null) go.transform.parent = transform;
                go.SetActive(false);
                objectPool.Add(go);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }


    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            print(tag + " tag doesn't exist");
            return null;
        }

        GameObject go = poolDictionary[tag]
            .Where(x => !x.activeSelf && x != null)
            .FirstOrDefault();
        go.transform.position = position;
        go.transform.rotation = rotation;
        go.SetActive(true);

        //poolDictionary[tag].Enqueue(go);
        return go;
    }
    public void RemoveFromPool(string tag, GameObject go)
    {
        if (!poolDictionary[tag].Contains(go)) return;
        poolDictionary[tag].Remove(go);
    }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
}