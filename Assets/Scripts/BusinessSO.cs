namespace Advant.SO
{
    using System;
    using UnityEngine;
    using Advant.Intefaces;
    using System.Collections.Generic;

    [CreateAssetMenu(menuName = "Advant/Business", fileName = "New Business")]
    public class BusinessSO : ScriptableObject, IBusiness
    {
        #region Editor fields
        [Tooltip("Id of current business")]
        [SerializeField] uint _id;
        [Tooltip("Name of current business")]
        [SerializeField] string _name;
        [Tooltip("Delay of business income")]
        [Range(0, 30)]
        [SerializeField] float _incomeDelay;
        [Tooltip("Base cost of business")]
        [SerializeField] uint _baseCost;
        [Tooltip("Base income of business")]
        [SerializeField] uint _baseIncome;
        [Tooltip("Start lvl of current business")]
        [SerializeField] uint _lvl;
        float _incomeProgress;
        [Tooltip("Available updates")]
        [SerializeField] List<BusinessUpdate> _updates;

        #endregion
        public uint Id { get { return _id; }  set { _id = value; } }
        public string Name { get { return _name; }  set { _name = value; } }
        public float IncomeDelay { get { return _incomeDelay; }  set { _incomeDelay = value; } }
        public uint BaseCost { get { return _baseCost; }  set { _baseCost = value; } }
        public uint BaseIncome { get { return _baseIncome; }  set { _baseIncome = value; } }
        public uint Lvl { get { return _lvl; } set { _lvl = value; } }
        public float IncomeProgress { get { return _incomeProgress; }  set { _incomeProgress = value; } }
        public List<BusinessUpdate> Updates { get { return _updates; } set { _updates = value; } }
        public void ApplySettings(IBusiness business) { }      
        public void SetContent() { }
        public void EnableBusiness() { }
        public void DisableBusiness() { }
    }
}