using System;

public sealed class CurrencyStorage
{
    public event Action<int> OnValueChanged;
    public event Action<int> OnValueEarned;
    public event Action<int> OnValueSpent;

    public int Value => _value;

    private int _value;
    
    public void EarnValue(int amount)
    {
        if (amount == 0)
        {
            return;
        }

        if (amount < 0)
        {
            throw new Exception($"Can not earn negative currency {amount}");
        }

        var previousValue = _value;
        var newValue = previousValue + amount;

        _value = newValue;
        OnValueChanged?.Invoke(newValue);
        OnValueEarned?.Invoke(amount);
    }
    public void SpendValue(int amount)
    {
        if (amount == 0)
        {
            return;
        }

        if (amount < 0)
        {
            throw new Exception($"Can not spend negative currency {amount}");
        }

        var previousValue = _value;
        var newValue = previousValue - amount;
        if (newValue < 0)
        {
            throw new Exception(
                $"Negative currency after spend. Currency in bank: {previousValue}, spend amount {amount} ");
        }

        _value = newValue;
        OnValueChanged?.Invoke(newValue);
        OnValueSpent?.Invoke(amount);
    }
    public void SetupValue(int value)
    {
        _value = value;
        OnValueChanged?.Invoke(value);
    }
    public bool CanSpendValue(int amount)
    {
        return _value >= amount;
    }
}