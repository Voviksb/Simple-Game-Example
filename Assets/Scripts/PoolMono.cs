using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
    [SerializeField] private T _prefab { get; }
    [SerializeField] private bool _autoExpand { get; set; }
    [SerializeField] private Transform _container { get; }
    [SerializeField] private int _poolCount;

    private List<T> _pool;

    public PoolMono(T prefab, int count, Transform container, bool autoExpand)
    {
        _prefab = prefab;
        _poolCount = count;
        _container = container;
        _autoExpand = autoExpand;

        CreatePool(_poolCount);
    }

    private void CreatePool(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(_prefab, _container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        _pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var mono in _pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
        {
            return element;
        }

        if (_autoExpand)
        {
            return CreateObject(true);
        }
        throw new Exception($"No free elements in pool of type {typeof(T)}");
    }
}
