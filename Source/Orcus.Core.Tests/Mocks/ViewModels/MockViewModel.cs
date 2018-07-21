using Orcus.Core.Mvvm;

namespace Orcus.Core.Tests.Mocks.ViewModels
{
    public class MockViewModel : NotifyPropertyChangedBase
    {
        public bool IsNewValueSet { get; set; }

        private int mockField;
        public int MockProperty
        {
            get { return mockField; }
            set { IsNewValueSet = SetField(ref mockField, value); }
        }

        public void SetFieldWithCustomName(int newValue, string customName)
        {
            SetField(ref mockField, newValue, customName);
        }
    }
}
