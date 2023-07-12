using System;
using System.Collections.Specialized;

namespace ScrollToBottom
{
    public class ScrollToBottomBehavior : Behavior<CollectionView>
    {
        private CollectionView _collectionView;

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);

            _collectionView = bindable as CollectionView;

            bindable.PropertyChanged += BindablePropertyChanged;
            bindable.PropertyChanging += BindablePropertyChanging;
        }

        private void BindablePropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            var collectionView = sender as CollectionView;
            
            if (e.PropertyName == ItemsView.ItemsSourceProperty.PropertyName)
            {
                if (collectionView.ItemsSource != null && collectionView.ItemsSource
                    is INotifyCollectionChanged collection)
                {
                    collection.CollectionChanged -= ItemsSourceCollectionChanged;
                }
            }
        }

        private void BindablePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var collectionView = sender as CollectionView;

            if (e.PropertyName == ItemsView.ItemsSourceProperty.PropertyName)
            {
                if (collectionView.ItemsSource != null && collectionView.ItemsSource
                    is INotifyCollectionChanged collection)
                {
                    collection.CollectionChanged -= ItemsSourceCollectionChanged;
                    collection.CollectionChanged += ItemsSourceCollectionChanged;
                }
            }
        }

        private void ItemsSourceCollectionChanged(object sender, EventArgs e)
        {
            if (e is NotifyCollectionChangedEventArgs args
                && args.NewStartingIndex > 0
                && _collectionView != null
                && (args.Action == NotifyCollectionChangedAction.Add || args.Action == NotifyCollectionChangedAction.Replace))
            {
                Dispatcher.Dispatch(() =>
                {
                    var index = args.NewStartingIndex;
                    _collectionView.ScrollTo(index, position: ScrollToPosition.End, animate: true);
                });
            }
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.PropertyChanged -= BindablePropertyChanged;
            bindable.PropertyChanging -= BindablePropertyChanging;

            _collectionView = null;
        }
    }
}

