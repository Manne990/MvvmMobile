using System;
using System.Collections.ObjectModel;
using System.Linq;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmMobile.Sample.Core.Common;
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

            _motorcycles = new ObservableCollection<IMotorcycle>();
        }


        // -----------------------------------------------------------------------------

        // Public Methods
        public void LoadData(ObservableCollection<IMotorcycle> motorcycles)
        {
            // Check for null
            if (motorcycles == null)
            {
                return;
            }

            // If incoming list is empty -> Clear local cache
            if (motorcycles.Count == 0)
            {
                _motorcycles = motorcycles;
                NotifyDataSetChanged();
            }

            // Sort incoming list
            var sorted = motorcycles
                .OrderBy(m => m.Brand)
                .ThenBy(m => m.Model)
                .ThenBy(m => m.Year)
                .ToObservableCollection();

            // If local cache is empty -> Update cache from local list
            if (_motorcycles.Count == 0)
            {
                _motorcycles = sorted;
                NotifyDataSetChanged();
                return;
            }

            // Find removed items
            var removed = _motorcycles.Except(sorted).ToObservableCollection();
            foreach (var item in removed)
            {
                NotifyItemRemoved(_motorcycles.IndexOf(item));
            }

            // Find added items
            var added = sorted.Except(_motorcycles).ToObservableCollection();
            foreach (var item in added)
            {
                NotifyItemInserted(sorted.IndexOf(item));
            }

            // Update local cache
            _motorcycles = sorted;

            if (removed.Count == 0 && added.Count == 0)
            {
                NotifyDataSetChanged();
            }
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
        private void SelectCartItem(Guid id)
        {
            _selectListener(_motorcycles?.FirstOrDefault(m => m.Id == id));
        }

        private void DeleteCartItem(Guid id)
        {
            _deleteListener?.Invoke(_motorcycles?.FirstOrDefault(m => m.Id == id));
        }
    }
}