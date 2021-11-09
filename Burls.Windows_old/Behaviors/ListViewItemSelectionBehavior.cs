using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Microsoft.Xaml.Behaviors;
using Unity.Injection;

namespace Burls.Windows.Behaviors
{
    public class ListViewItemSelectionBehavior : Behavior<ListView>
    {
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ListViewItemSelectionBehavior), new PropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            var listView = AssociatedObject as ListView;
            listView.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
            listView.KeyDown += OnKeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            var listView = AssociatedObject as ListView;
            listView.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
            listView.KeyDown -= OnKeyDown;
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            => SelectItem(e);

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SelectItem(e);
                e.Handled = true;
            }
        }

        private void SelectItem(RoutedEventArgs args)
        {
            if (Command != null
            &&  args.OriginalSource is FrameworkElement selectedItem)
            {
                Type dataContextType = selectedItem.DataContext.GetType();
                Type commandParameterType = Command.GetType();
                var canExecuteMethod = Command.GetType().GetMethod(nameof(ICommand.CanExecute));
                var inputParameterType = canExecuteMethod.GetParameters().FirstOrDefault()?.ParameterType;

                if (dataContextType == inputParameterType
                &&  Command.CanExecute(selectedItem.DataContext))
                {
                    Command.Execute(selectedItem.DataContext);
                }
            }
        }
    }
}
