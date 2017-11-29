﻿using MvvmMobile.Core.Common;
using MvvmMobile.Sample.Core.IoC;
using MvvmMobile.Sample.Core.Model;
using MvvmMobile.Sample.Core.ViewModel;

namespace MvvmMobile.Sample.Core
{
    public static class Bootstrapper
    {
        public static IContainerBuilder Init(XLabs.Ioc.IResolver resolver)
        {
            var builder = new XlabsContainerBuilder(resolver);

            builder.Register<IMotorcyclePayload>(new MotorcyclePayload());

            builder.Register<IStartViewModel, StartViewModel>();
            builder.Register<IEditMotorcycleViewModel, EditMotorcycleViewModel>();

            return builder;
        }
    }
}