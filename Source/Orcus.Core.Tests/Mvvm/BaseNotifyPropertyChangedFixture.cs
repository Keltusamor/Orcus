using NUnit.Framework;
using Orcus.Core.Tests.Mocks.ViewModels;

namespace Orcus.Core.Tests.Mvvm
{
    [TestFixture]
    public class BaseNotifyPropertyChangedFixture
    {
        private MockViewModel ViewModel { get; set; }

        [SetUp]
        public void SetUp()
        {
            ViewModel = new MockViewModel();
        }

        [TestCase(int.MinValue)]
        [TestCase(0)]
        [TestCase(int.MaxValue)]
        public void SetFieldShouldSetTheNewValue(int newValue)
        {
            ViewModel.MockProperty = newValue;
            Assert.AreEqual(newValue, ViewModel.MockProperty);
        }

        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        public void SetFieldShouldReturnTrueIfNewValueWasSet(int newValue)
        {
            ViewModel.MockProperty = newValue;
            Assert.IsTrue(ViewModel.IsNewValueSet);
        }

        [TestCase(int.MinValue)]
        [TestCase(0)]
        [TestCase(int.MaxValue)]
        public void SetFieldShouldReturnFalseIfNewValueEqualsOldValue(int newValue)
        {
            ViewModel.MockProperty = newValue;
            ViewModel.MockProperty = newValue;
            Assert.IsFalse(ViewModel.IsNewValueSet);
        }

        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        public void SetFieldShouldInvokeOnPropertyChangedIfNewValueWasSet(int newValue)
        {
            var wasInvoked = false;
            ViewModel.PropertyChanged += (_, __) => { wasInvoked = true; };
            ViewModel.MockProperty = newValue;
            Assert.IsTrue(wasInvoked);
        }

        [TestCase(0)]
        public void SetFieldShouldNotInvokeOnPropertyChangedIfNewValueEqualsOldValue(int newValue)
        {
            var wasInvoked = false;
            ViewModel.PropertyChanged += (_, __) => { wasInvoked = true; };
            ViewModel.MockProperty = newValue;
            Assert.IsFalse(wasInvoked);
        }

        [TestCase(int.MinValue)]
        public void OnPropertyChangedShouldSetPropertyNameCorrectly(int newValue)
        {
            var propertyName = string.Empty;
            ViewModel.PropertyChanged += (_, e) => { propertyName = e.PropertyName; };
            ViewModel.MockProperty = newValue;
            Assert.AreEqual(nameof(ViewModel.MockProperty), propertyName);
        }

        [TestCase(int.MinValue, "CustomPropertyName")]
        public void OnPropertyChangedShouldSetPropertyNameCorrectlyForCustomNames(int newValue, string customPropertyName)
        {
            var propertyName = string.Empty;
            ViewModel.PropertyChanged += (_, e) => { propertyName = e.PropertyName; };
            ViewModel.SetFieldWithCustomName(newValue, customPropertyName);
            Assert.AreEqual(customPropertyName, propertyName);
        }
    }
}
