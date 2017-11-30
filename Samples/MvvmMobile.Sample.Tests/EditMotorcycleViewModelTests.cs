using System;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Sample.Core.Model;
using MvvmMobile.Sample.Core.ViewModel;
using NSubstitute;
using NUnit.Framework;
using TinyIoC;
using XLabs.Ioc;
using XLabs.Ioc.TinyIOC;

namespace MvvmMobile.Sample.Tests
{
    [TestFixture, Parallelizable]
    public class EditMotorcycleViewModelTests
    {
        private INavigation _navigation;
        private IPayloads _payloads;
        private IMotorcyclePayload _payload;
        private IEditMotorcycleViewModel _subject;


        [SetUp]
        public void SetUp()
        {
            // Init Tiny IoC
            var container = new TinyIoCContainer();

            container.Register<IDependencyContainer>(new TinyContainer(container));

            var resolver = new TinyResolver(container);

            // Init IoC Builder
            var builder = new TestContainerBuilder(resolver);

            MvvmMobile.Core.Mvvm.Api.Init(builder);

            // Register
            builder.Register<IEditMotorcycleViewModel, EditMotorcycleViewModel>();

            builder.Register<IMotorcyclePayload, MotorcyclePayload>();

            _navigation = Substitute.For<INavigation>();
            builder.Register(_navigation);

            builder.Register<IMotorcyclePayload, MotorcyclePayload>();
            _payload = builder.Resolver.Resolve<IMotorcyclePayload>();

            builder.Register<IPayloads, Payloads>();
            _payloads = builder.Resolver.Resolve<IPayloads>();

            _subject = builder.Resolver.Resolve<IEditMotorcycleViewModel>();
        }

        [Test]
        public void MissingPayloadShouldCreateNewMotorcycle()
        {
            // ARRANGE

            // ACT
            _subject.InitWithPayload(Guid.Empty);

            // ASSERT
            Assert.IsNotNull(_subject.Motorcycle);
        }

        [Test]
        public void InitWithPayloadShouldPopulateViewModel()
        {
            // ARRANGE
            var mc = new Motorcycle
            {
                Id = Guid.NewGuid(),
                Brand = "Honda",
                Model = "VFR",
                Year = 1999
            };

            var payloadGuid = Guid.NewGuid();

            _payload.Motorcycle = mc;
            _payloads.Add(payloadGuid, _payload);

            // ACT
            _subject.InitWithPayload(payloadGuid);

            // ASSERT
            Assert.IsNotNull(_subject.Motorcycle);
            Assert.AreEqual(mc.Id, _subject.Motorcycle.Id);
            Assert.AreEqual(mc.Brand, _subject.Motorcycle.Brand);
            Assert.AreEqual(mc.Model, _subject.Motorcycle.Model);
            Assert.AreEqual(mc.Year, _subject.Motorcycle.Year);
        }

        [Test]
        public void CancelShouldCallStartViewModelWithoutPayload()
        {
            // ARRANGE
            var mc = new Motorcycle
            {
                Id = Guid.NewGuid(),
                Brand = "Honda",
                Model = "VFR",
                Year = 1999
            };

            var payloadGuid = Guid.NewGuid();

            _payload.Motorcycle = mc;
            _payloads.Add(payloadGuid, _payload);

            _subject.CallbackAction = (payloadId) => {};

            // ACT
            _subject.InitWithPayload(payloadGuid);

            _subject.CancelCommand.Execute();

            // ASSERT
            _navigation.Received(1).NavigateBack(Arg.Any<Action>());
        }

        [Test]
        public void SaveShouldCallStartViewModelWithPayload()
        {
            // ARRANGE
            var mc = new Motorcycle
            {
                Id = Guid.NewGuid(),
                Brand = "Honda",
                Model = "VFR",
                Year = 1999
            };

            var payloadGuid = Guid.NewGuid();

            _payload.Motorcycle = mc;
            _payloads.Add(payloadGuid, _payload);

            _subject.CallbackAction = (payloadId) => {};

            // ACT
            _subject.InitWithPayload(payloadGuid);

            _subject.SaveMotorcycleCommand.Execute();

            // ASSERT
            _navigation.Received(1).NavigateBack(Arg.Any<Action<Guid>>(), Arg.Any<Guid>(), Arg.Any<Action>());
        }
    }
}