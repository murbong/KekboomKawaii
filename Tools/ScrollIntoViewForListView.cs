using Microsoft.Xaml.Behaviors;
using System;
using System.Windows.Controls;

namespace KekboomKawaii.Tools
{
    public class ScrollIntoViewForListView : Behavior<ListView>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
        }
        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView)
            {
                if (listView.SelectedItem != null)
                {
                    listView.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        listView.UpdateLayout();
                        if (listView.SelectedItem != null)
                            listView.ScrollIntoView(listView.SelectedItem);
                    }));
                }
            }
        }


    }
}
