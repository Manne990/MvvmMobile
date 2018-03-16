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
        private readonly Action<IMotorcycle> _selectListener;
        private readonly Action<IMotorcycle> _deleteListener;


        // -----------------------------------------------------------------------------

        // Constructors
        public StartAdapter(Action<IMotorcycle> selectListener, Action<IMotorcycle> deleteListener, LayoutInflater inflater)
        {
            _selectListener = selectListener;
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
            StartItemViewHolder holder;

            var view = convertView ?? _inflater.Inflate(Resource.Layout.StartActivityItemLayout, null);

            if (convertView == null)
            {
                holder = new StartItemViewHolder(view, SelectCartItem, DeleteCartItem);

                view.Tag = holder;
            }
            else
            {
                holder = convertView.Tag as StartItemViewHolder;
            }

            holder.Update(_motorcycles[position], position);

            return view;
        }


        // -----------------------------------------------------------------------------

        // Private Members
        private void SelectCartItem(int position)
        {
            _selectListener(_motorcycles[position]);
        }

        private void DeleteCartItem(int position)
        {
            _deleteListener?.Invoke(_motorcycles[position]);
        }
    }
}