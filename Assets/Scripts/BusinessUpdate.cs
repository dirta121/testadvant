namespace Advant
{
    using System;
    [Serializable]
    public struct BusinessUpdate 
    {
        public uint Id;
        public string Name;
        public uint Cost;
        public float IncomeMultiplier;
        public bool Purchased;
    }
}