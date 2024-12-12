using System;
using FinalGame.Develop.CommonServices.Wallet;

namespace FinalGame.Develop.ADV_02
{
    [Serializable]
    public struct Currency
    {
        public CurrencyTypes CurrencyType;
        public int Value;
    }
}