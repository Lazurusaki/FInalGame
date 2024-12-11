using System;
using System.Collections.Generic;
using System.Linq;
using FinalGame.Develop.CommonServices.DataManagement.DataProviders;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.CommonServices.Wallet
{
    public class WalletService :IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly Dictionary<CurrencyTypes, ReactiveVariable<int>> _currencies = new();

        
        public WalletService(PlayerDataProvider playerDataProvider)
        {
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }
        
        public List<CurrencyTypes> AvailableCurrencies => _currencies.Keys.ToList();
        
        public IReadOnlyVariable<int> GetCurrency(CurrencyTypes types) => _currencies[types];
        
        public bool HasEnough(CurrencyTypes types, int amount)
            => _currencies[types].Value >= amount;

        public void Spend(CurrencyTypes types, int amount)
        {
            if (HasEnough(types, amount) == false)
                throw new ArgumentException(types.ToString());

            _currencies[types].Value -= amount;
        }

        public void Add(CurrencyTypes type, int amount)
        {
            if (_currencies.ContainsKey(type) == false)
                _currencies.Add(type, new ReactiveVariable<int>());
            
            _currencies[type].Value += amount;
        }
        
        
        public void ReadFrom(PlayerData data)
        {
            foreach (KeyValuePair<CurrencyTypes, int> currency in data.WalletData)
            {
                if (_currencies.ContainsKey(currency.Key))
                    _currencies[currency.Key].Value = currency.Value;
                else
                    _currencies.Add(currency.Key, new ReactiveVariable<int>(currency.Value));
            }
        }

        public void WriteTo(PlayerData data)
        {
            foreach (KeyValuePair<CurrencyTypes, ReactiveVariable<int>> currency in _currencies)
            {
                if (data.WalletData.ContainsKey(currency.Key))
                    data.WalletData[currency.Key] = currency.Value.Value;
            }
        }
    }
}
