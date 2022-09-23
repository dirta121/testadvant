namespace Advant
{
    using Advant.SO;
    using Advant.Intefaces;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;
    public class BusinessesController : MonoBehaviour
    {
        const string _path = "Businesses";
        [SerializeField] GameObject _businessItemPREFAB;
        [SerializeField] RectTransform _container;

        private Dictionary<uint, IBusiness> _businessItems = new();
        private Dictionary<uint, Business> _saveBusinessItems;
        #region Unity
        void Start()
        {
            GameManager.OnSaveLoad.AddListener(OnSaveLoad);
        }
        void Update()
        {

        }
        private void OnDestroy()
        {
            GameManager.OnSaveLoad.RemoveListener(OnSaveLoad);
        }
        #endregion
        /// <summary>
        /// Generates all Business from resources and apllies save
        /// </summary>
        private void GenerateBusinessItems()
        {
            var sos = Resources.LoadAll<BusinessSO>(_path);
            foreach (var so in sos)
            {
                //Generate an empty business item from prefab
                var business = GenerateBusinessItem();
                //try to get saved version of business and applysettings
                var hasSavedBusiness = _saveBusinessItems.TryGetValue(so.Id, out Business saveBusiness);
                business.ApplySettings(hasSavedBusiness ? saveBusiness : so);
                business.SetContent();  
                _businessItems[business.Id]= business;
                business.EnableBusiness();
            }
        }
        private IBusiness GenerateBusinessItem()
        {
            var obj = Instantiate(_businessItemPREFAB, _container);
            var component = obj.GetComponent<BusinessItem>();
            return component;
        }
        /// <summary>
        /// Use this method to return all available businesses with actual data. Use it for save.
        /// </summary>
        /// <returns>Dict of IBusiness objects</returns>
        public Dictionary<uint, Business> GetBusinesses() 
        {
            Dictionary<uint, Business> dict = new();
            foreach (var item in _businessItems.Values)
            {
                var business=new Business();
                item.CopyPropertiesTo(business);

                for (int i = 0; i < item.Updates.Count; i++)
                {
                    item.Updates[i].CopyPropertiesTo(business.Updates[i]);
                }             
                dict[item.Id] = business;
            }
            return dict;
        }
        #region Callbacks
        /// <summary>
        /// Fired after Save loaded.
        /// </summary>
        /// <param name="save"></param>
        /// <param name="loaded"></param>
        private void OnSaveLoad(Save save, bool loaded)
        {
            _saveBusinessItems = save.Businesses;
            GenerateBusinessItems();
        }
        #endregion
    }
}
