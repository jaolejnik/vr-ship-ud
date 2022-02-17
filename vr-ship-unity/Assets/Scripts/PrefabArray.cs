using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabArray : MonoBehaviour
{
    public GameObject prefab;
    public int amount = 2;
    public Vector3 offset;

    void Start()
    {
        for (int i = 0; i < amount; i++)
            Instantiate(prefab, gameObject.transform.position + i * offset, prefab.transform.rotation);
    }
}
