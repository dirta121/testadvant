namespace Advant
{
    using TMPro;
    using UnityEngine;
    public class Balance : MonoBehaviour
    {
        #region Unity fields
        [SerializeField] TextMeshProUGUI _balanceText;
        #endregion
        #region Unity
        private void OnEnable()
        {
            GameManager.OnBalanceUpdate.AddListener(OnBalanceUpdate);
        }
        private void OnDisable()
        {
            GameManager.OnBalanceUpdate.RemoveListener(OnBalanceUpdate);
        }
        #endregion
        #region Callbacks
        /// <summary>
        /// Fired when balance changed.
        /// </summary>
        /// <param name="oldBalance"></param>
        /// <param name="newBalance"></param>
        private void OnBalanceUpdate(float newBalance)
        {
            _balanceText.text = $"Баланс: {newBalance}$";
        }
        #endregion
    }
}
