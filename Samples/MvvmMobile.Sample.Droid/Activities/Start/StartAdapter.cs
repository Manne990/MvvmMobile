using System;
using System.Collections.ObjectModel;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmMobile.Sample.Core.Model;

namespace MvvmMobile.Sample.Droid.Activities.Start
{
    public class StartAdapter : RecyclerView.Adapter
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
        public override int ItemCount => _motorcycles?.Count ?? 0;

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.StartActivityItemLayout, parent, false);
            return new StartItemViewHolder(view, SelectCartItem, DeleteCartItem);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as StartItemViewHolder;
            viewHolder?.Update(_motorcycles[position], position);
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