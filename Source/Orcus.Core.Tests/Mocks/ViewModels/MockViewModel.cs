using Orcus.Core.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orcus.Core.Tests.Mocks.ViewModels
{
    public class MockViewModel : BaseNotifyPropertyChanged
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
