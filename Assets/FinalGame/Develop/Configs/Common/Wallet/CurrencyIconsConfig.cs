using System;
using System.Collections.Generic;
using System.Linq;
using FinalGame.Develop.CommonServices.Wallet;
using UnityEngine;

namespace FinalGame.Develop.Configs.Common.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Common/Wallet/CurrencyIconsConfig", fileName = "CurrencyIconsConfig")]
    public class CurrencyIconsConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyIconConfig> _config;

        public Sprite GetSpriteFor(CurrencyTypes currencyType) =>
            _config.First(config => config.CurrencyType == currencyType).Icon;
        
        [Serializable]
        private class CurrencyIconConfig
        {
            [field: SerializeField] public CurrencyTypes CurrencyType { get; private set; }
            [field: SerializeField] public Sprite Icon { get; private set; }
        }
    }
}
