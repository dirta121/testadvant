namespace Advant
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    public class GameManager : MonoBehaviour
    {
        #region Editor fields
        [SerializeField] SaveProvider<Save> _saveProvider;
        [SerializeField] BusinessesController _businessesController;
        [SerializeField] uint _defaultBalance;
        #endregion
        public static float Balance { get { return _balance; } private set { _balance = value; OnBalanceUpdate?.Invoke(value); } }
        private static float _balance;
        #region Events
        public static SaveProviderEvent<Save> OnSaveLoad = new();
        public static SaveProviderEvent<Save> OnSaveSaved = new();
        public static BalanceEvent OnBalanceUpdate = new();
        #endregion
        #region Unity
        void Start()
        {
            OnSaveLoad.AddListener(OnSaveLoadCallback);
            Load();
        }
        private void OnDestroy()
        {
            OnSaveLoad.RemoveListener(OnSaveLoadCallback);
        }
        void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                Debug.Log("Paused");
                //Save Player Settings
                Save();
            }
        }
#if UNITY_EDITOR
        private void OnApplicationQuit()
        {
            Save();
        }
#endif    
        #endregion
        /// <summary>
        /// Save data via save provider 
        /// </summary>
        public void Save()
        {
            if (!_saveProvider)
            {
                return;
            }
            if (!_businessesController)
            { 
                return;
            }
            var save = PrepareSave();
            _saveProvider.Save(save);
            OnSaveSaved?.Invoke(save, true);
        }
        /// <summary>
        /// Collect all data from businesses
        /// </summary>
        /// <returns>Return complete save object</returns>
        private Save PrepareSave() 
        {
            Save save = new Save(Balance);
            var businesses = _businessesController.GetBusinesses();
            save.Businesses = businesses;
            return save;
        }
        /// <summary>
        /// Loads data via save provider 
        /// </summary>
        private void Load()
        {
            if (_saveProvider == null)
            {
                return;
            }
            if (_saveProvider.TryLoad(out Save save))
            {
                //save exists and fine
                Debug.Log("SAVE LOADED COMPLETED");
                OnSaveLoad?.Invoke(save,true);
            }
            else
            {
                //save doesnt exist or broken. default save generated
                Debug.Log("SAVE IS BROKEN OR NOT EXISTS. GENERATED NEW ONE");
                OnSaveLoad?.Invoke(save, false);
            }
        }
        /// <summary>
        /// Returns balance > parameter.
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public static bool CanPurchase(uint cost) 
        {
            return Balance >= cost;
        }
        /// <summary>
        /// Use this method to purchase.CanPurchase check method inside.
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="purchaseCallback"></param>
        public static void Purchase(uint cost,Action purchaseCallback=null)
        {
            if (CanPurchase(cost)) 
            {
                Balance -= cost;
                purchaseCallback?.Invoke();
            }
        }
        /// <summary>
        /// Use this method to collect income
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="collectIncomeCallback"></param>
        public static void CollectIncome(float cost, Action collectIncomeCallback = null) 
        {
            Balance += cost;
            collectIncomeCallback?.Invoke();
        }
        #region Callbacks
        /// <summary>
        /// Fired after Save loaded.
        /// </summary>
        /// <param name="save"></param>
        /// <param name="loaded"></param>
        private void OnSaveLoadCallback(Save save, bool loaded)
        {
            Balance = loaded ? save.Balance : _defaultBalance;
        }
        #endregion
    }
    //T0-newBalance
    public class BalanceEvent : UnityEvent<float> { }
}
