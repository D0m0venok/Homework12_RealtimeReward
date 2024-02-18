using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class StorageProvider
{
    private readonly Dictionary<CurrencyType, CurrencyStorage> _storages = new();

    public StorageProvider()
    {
        foreach (CurrencyType type in Enum.GetValues(typeof(CurrencyType)))
        {
            var storage = new CurrencyStorage();
            _storages[type] = storage;

            storage.OnValueEarned += i => Debug.Log($"{i} {type} earned");
        }
    }

    public CurrencyStorage GetStorage(CurrencyType type)
    {
        return _storages[type];
    }
}