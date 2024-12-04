using System;
using System.Collections.Generic;
using System.Linq;
using FinalGame.Develop.CommonServices.Wallet;
using UnityEngine;

namespace FinalGame.Develop.Configs.Common.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Common/Wallet/StartWalletConfig", fileName = "StartWalletConfig")]
    public class StartWalletConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _values;

        public int getStartValueFor(CurrencyTypes currencyType) =>
            _values.First(config => config.Type == currencyType).Value;

        [Serializable]
        private class CurrencyConfig
        {
            [field: SerializeField] public CurrencyTypes Type { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }
    }
        
}
