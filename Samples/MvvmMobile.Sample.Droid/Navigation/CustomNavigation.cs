using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Android.Support.V7.App;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Droid.Navigation;
using MvvmMobile.Sample.Core.Navigation;

namespace MvvmMobile.Sample.Droid.Navigation
{
    public class CustomNavigation : AppNavigation, ICustomNavigation
    {
        public override Context GetContext()
        {
            return ((AppNavigation)MvvmMobile.Core.Mvvm.Api.Resolver.Resolve<INavigation>()).GetContext();
        }

        public override Dictionary<Type, Type> GetViewMapper()
        {
            return ((AppNavigation)MvvmMobile.Core.Mvvm.Api.Resolver.Resolve<INavigation>()).GetViewMapper();
        }

        public async Task NavigateToRoot()
        {
            await Task.Delay(1);

            if (GetContext() is AppCompatActivity activity)
            {
                var intent = new Intent(GetContext(), typeof(Activities.Start.StartActivity));

                intent.AddFlags(ActivityFlags.ClearTop);

                GetContext().StartActivity(intent);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateBack<T>: Context is null or not an AppCompatActivity!");
            }
        }
    }
}