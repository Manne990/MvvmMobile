using System;
using System.Collections.ObjectModel;
using Android.Views;
using Android.Widget;
using MvvmMobile.Sample.Core.Model;

namespace MvvmMobile.Sample.Droid.Activities.Start
{
    public class StartAdapter : BaseAdapter<IMotorcycle>
    {
        // Private Members
        private ObservableCollection<IMotorcycle> _motorcycles;
        private readonly LayoutInflater _inflater;
        private Action<IMotorcycle> _deleteListener;


        // -----------------------------------------------------------------------------

        // Constructors
        public StartAdapter(Action<IMotorcycle> deleteListener, LayoutInflater inflater)
        {
            _deleteListener = deleteListener;
            _inflater = inflater;
        }


        // -----------------------------------------------------------------------------

        // Public Methods
        public void LoadData(ObservableCollection<IMotorcycle> motorcycles)
        {
            _motorcycles = motorcycles;
            NotifyDataSetChanged();
        }


        // -----------------------------------------------------------------------------

        // Overrides

        public override int Count => _motorcycles?.Count ?? 0;

        public override IMotorcycle this[int position] => _motorcycles[position];

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _inflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = _motorcycles[position].ToString();

            return view;
        }
    }
}