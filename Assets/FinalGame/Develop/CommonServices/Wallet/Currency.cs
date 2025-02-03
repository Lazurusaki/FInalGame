using System;

namespace FinalGame.Develop.CommonServices.Wallet
{
    [Serializable]
    public struct Currency
    {
        public CurrencyTypes CurrencyType;
        public int Value;
    }
}