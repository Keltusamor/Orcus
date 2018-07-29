using System;
using NUnit.Framework;
using Orcus.Core.Mvvm;
using Orcus.Core.Tests.Mocks.ViewModels;

namespace Orcus.Core.Tests.Mvvm
{
    [TestFixture]
    public class ViewModelFactoryFixture
    {
        [TestCase]
        public void NonGenericCustomViewModelFactoryCreatesViewModel()
        {
            var view = new DummyClass();
            ViewModelFactory.RegisterCustomViewModelFactory(typeof(DummyClass), () => new MockViewModel());
            var viewModel = ViewModelFactory.Create(new DummyClass());
            Assert.IsNotNull(viewModel);
            Assert.IsInstanceOf<MockViewModel>(viewModel);
        }

        [TestCase]
        public void RegisterConventionBasedViewModelFactoryDoesNotAcceptNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() => ViewModelFactory.RegisterConventionBasedViewModelFactory(null));
        }

        [TestCase]
        public void CreateDoesNotAcceptNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() => ViewModelFactory.Create(null));
        }

        [TestCase]
        public void DefaultConventionBasedViewModelFactoryCreatesViewModel()
        {
            var viewModel = ViewModelFactory.Create(new DummyClass(), typeof(MockViewModel));
            Assert.IsNotNull(viewModel);
            Assert.IsInstanceOf<MockViewModel>(viewModel);
        }
    }
}
