namespace Advant
{
    using Advant.Intefaces;
    using System;
    using System.Linq;
    using System.Collections;
using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class BusinessItem : MonoBehaviour, IBusiness
    {
        #region Editor fields
        [Tooltip("Text component for displaing name property")]
        [SerializeField] TextMeshProUGUI _nameText;
        [Tooltip("Slider component for displaing progress property")]
        [SerializeField] Slider _progressbarSlider;
        [Tooltip("Text component for displaing lvl property")]
        [SerializeField] TextMeshProUGUI _lvlText;
        [Tooltip("Text component for displaing income property")]
        [SerializeField] TextMeshProUGUI _incomeText;
        [Tooltip("Text component for displaing lvlup details")]
        [SerializeField] TextMeshProUGUI _lvlupButtonText;
        [Tooltip("Button component for displaing lvlup details")]
        [SerializeField] Button _lvlupButton;

        [SerializeField] GameObject _updateItemPREFAB;
        [SerializeField] RectTransform _updateItemsContainer;

        //[Tooltip("Update component for displaing update details")]
        //[SerializeField] BusinessUpdateItem _update1;
        //[Tooltip("Update component for displaing update details")]
        //[SerializeField] BusinessUpdateItem _update2;

        #endregion
        public uint Id { get; set; }
        public string Name { get; set; }
        public float IncomeDelay { get; set; }
        //Cost of an Level
        public uint Cost { get { return (Lvl + 1) * BaseCost; } }
        public uint BaseCost { get; set; }
        public uint BaseIncome { get; set; }
        public float Income
        {
            get
            {
                return Lvl * BaseIncome * (1 + (Updates.Where(w=>w.Purchased).Sum(s=>s.IncomeMultiplier)));
            }
        }
        public uint Lvl { get; set; }
        public float IncomeProgress { get; set; }
        public bool Purchased { get { return Lvl > 0; } }
        public List<BusinessUpdate> Updates { get; set; }

        private Dictionary<BusinessUpdateItem, int> _updateItemsDict = new();

        #region Unity
        void Start()
        {

        }
        void Update()
        {

        }
        private void OnEnable()
        {
            GameManager.OnBalanceUpdate.AddListener(OnBalanceUpdate);
        }
        private void OnDisable()
        {
            GameManager.OnBalanceUpdate.RemoveListener(OnBalanceUpdate);
        }
        #endregion
        IEnumerator DoBusinessCoroutine()
        {
            float time = 0;
            while (true)
            {
                while (time < IncomeDelay)
                {
                    time += Time.deltaTime;
                    _progressbarSlider.value = time / IncomeDelay;
                    yield return null;
                }
                time = 0;
                GameManager.CollectIncome(Income, OnIncomeCollected);
            }
        }
        /// <summary>
        /// Set View with model data
        /// </summary>
        public void SetContent()
        {            
            _nameText.text = Name;
            _lvlText.text = $"LVL\n{Lvl}";
            _progressbarSlider.value = IncomeProgress;
            _incomeText.text = $"Доход\n{Income}$";
            _lvlupButtonText.text = $"LVL UP \n{Cost}$";
            _lvlupButton.interactable = GameManager.CanPurchase(Cost);

            for (int i = 0; i < Updates.Count; i++)
            {
                var update = Updates[i];
                var obj = Instantiate(_updateItemPREFAB, _updateItemsContainer);
                var component = obj.GetComponent<BusinessUpdateItem>();
                _updateItemsDict[component] = i;
                component.SetContent(update, GameManager.CanPurchase(update.Cost));
                component.onUpdatePurchaseClick.AddListener(PurchaseUpdate);
            }      
        }
        private void UpdateContent() {
            _lvlText.text = $"LVL\n{Lvl}";
            _progressbarSlider.value = IncomeProgress;
            _incomeText.text = $"Доход\n{Income}$";
            _lvlupButtonText.text = $"LVL UP \n{Cost}$";
            _lvlupButton.interactable = GameManager.CanPurchase(Cost);
            foreach (var updateKVP in _updateItemsDict)
            {
                var i = updateKVP.Value;
                var update = Updates[i];
                updateKVP.Key.UpdateContent(update, GameManager.CanPurchase(update.Cost));
            }
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="business"></param>
        public void ApplySettings(IBusiness business)
        {
            business.CopyPropertiesTo(this);
        }
        /// <summary>
        /// Enable income earning if business is purchased
        /// </summary>
        public void EnableBusiness()
        {
            if (Purchased)
            {
                StopAllCoroutines();
                StartCoroutine(DoBusinessCoroutine());
            }
        }
        /// <summary>
        /// Disable income earning
        /// </summary>
        public void DisableBusiness()
        {
            StopAllCoroutines();
        }
        /// <summary>
        /// Use this method to buy a lvl for business
        /// </summary>
        public void LvlUp()
        {
            if (GameManager.CanPurchase(Cost))
            {
                GameManager.Purchase(Cost, OnLvlUpPurchased);
            }
        }
        private void PurchaseUpdate(BusinessUpdateItem updateItem,BusinessUpdate update) 
        {
            var i = _updateItemsDict[updateItem];
            var canPurchase = GameManager.CanPurchase(update.Cost);
            if (canPurchase)
            {
                var newUpdate = Updates[i];
                newUpdate.Purchased = true;
                Updates[i]= newUpdate;               
                GameManager.Purchase(update.Cost);
                UpdateContent();
            }
        }
        #region Callbacks
        /// <summary>
        /// Fired on balance changed
        /// </summary>
        /// <param name="newBalance"></param>
        private void OnBalanceUpdate(float newBalance)
        {
            UpdateContent();
        }
        /// <summary>
        /// Fired on Lvl up purchased
        /// </summary>
        private void OnLvlUpPurchased()
        {
            Debug.Log($"LVL PURCHASED FOR {Name}");
            Lvl++;
            EnableBusiness();
            UpdateContent();
        }
        
        /// <summary>
        /// Fired on income collected
        /// </summary>
        private void OnIncomeCollected()
        {
            Debug.Log($"INCOME COLLECTED {Income} FROM BUSINESS {Name} LVL {Lvl}");
        }
        #endregion
    }
}
