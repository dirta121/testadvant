namespace Advant
{
    using Advant.Intefaces;
using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    [Serializable]
    /// <summary>
    /// Struct for storing players data
    /// </summary>
    public struct Save
    {
        public float Balance;
        public Dictionary<uint, Business> Businesses;
        public Save(float balance)
        {
            Balance = balance;
            Businesses = new();
        }
    }
    [Serializable]
    public class Business : IBusiness
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public float IncomeDelay { get; set; }
        public uint BaseCost { get; set; }
        public uint BaseIncome { get; set; }
        public uint Lvl { get; set; }
        public float IncomeProgress { get; set; }
        public List<BusinessUpdate> Updates { get; set; }
        public void ApplySettings(IBusiness business)
        {
            
        }
        public void DisableBusiness()
        {
            
        }
        public void EnableBusiness()
        {
            
        }
        public void SetContent()
        {
           
        }
        public Business() 
        {
            Updates = new();
        }
    }  
}