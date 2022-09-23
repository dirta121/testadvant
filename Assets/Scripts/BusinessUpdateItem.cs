namespace Advant
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class BusinessUpdateItem : MonoBehaviour
    {
        #region Editor fields
        [SerializeField] TextMeshProUGUI _updateText;
        [SerializeField] Button _updateButton;
        #endregion
        private BusinessUpdate _update;
        public BusinessUpdateItemEvent onUpdatePurchaseClick = new();
        public void SetContent(BusinessUpdate update,bool canPurchase)
        {
            _update = update;
            var cost = update.Purchased ? "Куплено" : $"Цена: {update.Cost}";
            _updateText.text = $"\"{update.Name}\"\nДоход:+{update.IncomeMultiplier * 100}%\n{cost}";
            _updateButton.interactable = !update.Purchased && canPurchase;
        }
        public void UpdateContent(BusinessUpdate update, bool canPurchase)
        {
            var cost = update.Purchased ? "Куплено" : $"Цена: {update.Cost}";
            _updateText.text = $"\"{update.Name}\"\nДоход:+{update.IncomeMultiplier * 100}%\n{cost}";
            _updateButton.interactable = !update.Purchased&& canPurchase;
        }
        public void PurchaseUpdate()
        {
            onUpdatePurchaseClick?.Invoke(this, _update);
        }
        public class BusinessUpdateItemEvent : UnityEvent<BusinessUpdateItem, BusinessUpdate> { }
    }
}
