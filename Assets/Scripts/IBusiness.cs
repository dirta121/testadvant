using System.Collections.Generic;

namespace Advant.Intefaces
{
    public interface IBusiness
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public float IncomeDelay { get; set; }
        public uint BaseCost { get; set; }
        public uint BaseIncome { get; set; }
        public uint Lvl { get; set; }
        public float IncomeProgress { get; set; }
        public List<BusinessUpdate> Updates { get; set; }
        public void ApplySettings(IBusiness business);
        public void SetContent();
        public void EnableBusiness();
        public void DisableBusiness();
    }
}
