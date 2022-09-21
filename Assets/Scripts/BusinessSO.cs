namespace Advant.SO
{
    using System;
    using UnityEngine;
    [CreateAssetMenu(menuName = "Advant/Business", fileName = "New Business")]
    public class BusinessSO : ScriptableObject
    {
        #region Editor fields
        [Tooltip("Id of current business")]
        [SerializeField] private uint _id;
        [Tooltip("Name of current business")]
        [SerializeField] private string _name;
        [Tooltip("Delay of business income")]
        [Range(0,30)]
        [SerializeField] private float _incomeDelay;
        [Tooltip("Base cost of business")]
        [SerializeField] private uint _baseCost;
        [Tooltip("Base income of business")]
        [SerializeField] private uint _baseIncome;
        [Tooltip("Array of available updates")]
        [SerializeField] private BusinessUpdate[] _updates;
        #endregion
        public uint Id { get { return _id; } }
        public string Name { get { return _name; } }
        public float IncomeDelay { get { return _incomeDelay; } }
        public float BaseCost { get { return _baseCost; } }
        public float BaseIncome { get { return _baseIncome; } }
        public BusinessUpdate[] Updates { get { return _updates; } }
    }
}