namespace Advant
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct BusinessUpdate
    {
        #region Editor fields
        [Tooltip("Id of current update")]
        [SerializeField] private uint _id;
        [Tooltip("Name of current update")]
        [SerializeField] private string _name;
        [Tooltip("Cost of update")]
        [SerializeField] private uint _cost;
        [Tooltip("Income multiplier of update")]
        [SerializeField] private float _incomeMultiplier;
        #endregion
        public uint Id { get { return _id; } }
        public string Name { get { return _name; } }
        public uint Cost { get { return _cost; } }
        public float IncomeMultiplier { get { return _incomeMultiplier; } }
    }
}