using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace InternalDSL
{
    public interface INoTitledDialogBuilder<in TModel> where TModel : class
    {
        ITitledDialogBuilder<TModel> WithTitle(string title);
        ITitledDialogBuilder<TModel> WithTitleAndSize(string title, double width, double height);
    }

    public interface ITitledDialogBuilder<in TModel> where TModel : class
    {
        ITitledDialogBuilder<TModel> WithField(string name, string path);
        Window Build(TModel model);
    }

    public class DialogBuilder<TModel>:
        INoTitledDialogBuilder<TModel>, ITitledDialogBuilder<TModel>
        where TModel : class
    {
        private class DialogField
        {
            public string Label { get; set; }
            public string Path { get; set; }
        }

        private string Title { get; set; }
        private List<DialogField> Fields { get; set; } = new List<DialogField>();
        private double Height { get; set; } = 300;
        private double Width { get; set; } = 300;

        private DialogBuilder()
        {
        }

        public static INoTitledDialogBuilder<TModel> Create()
        {
            return new DialogBuilder<TModel>();
        }

        public ITitledDialogBuilder<TModel> WithTitle(string title)
        {
            Title = title;
            return this;
        }

        public ITitledDialogBuilder<TModel> WithTitleAndSize(string title, double width, double height)
        {
            Title = title;
            Width = width;
            Height = height;
            return this;
        }

        public ITitledDialogBuilder<TModel> WithField(string label, string path)
        {
            Fields.Add(new DialogField { Label = label, Path = path });
            return this;
        }

        public Window Build(TModel model)
        {
            var window = new Window
            {
                Height = Height,
                Width = Width,
                Title = Title,
                DataContext = model
            };

            var docPanel = new DockPanel() { Margin = new Thickness(10), LastChildFill = true };
            window.Content = docPanel;

            var buttonPanel = new StackPanel()
            {
                Height = 20,
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right
            };

            docPanel.Children.Add(buttonPanel);
            buttonPanel.SetValue(DockPanel.DockProperty, Dock.Bottom);

            var okButton = new Button()
            {
                Content = "OK",
                Width = 40,
                IsDefault = true,
                Margin = new Thickness(0, 0, 10, 0)
            };
            okButton.Click += (sender, args) => { window.DialogResult = true; };

            var cancelButton = new Button() { Content = "Cancel", Width = 40, IsCancel = true };
            cancelButton.Click += (sender, args) => { window.DialogResult = false; };

            buttonPanel.Children.Add(okButton);
            buttonPanel.Children.Add(cancelButton);

            var fieldsPannel = new Grid();
            fieldsPannel.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            fieldsPannel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            int i = 0;
            for (; i < Fields.Count; i++)
            {
                fieldsPannel.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            }
            docPanel.Children.Add(fieldsPannel);
            fieldsPannel.SetValue(DockPanel.DockProperty, Dock.Top);

            i = 0;
            foreach (var field in Fields)
            {
                var label = new Label { Content = field.Label + " :", VerticalAlignment = VerticalAlignment.Center };
                fieldsPannel.Children.Add(label);
                label.SetValue(Grid.ColumnProperty, 0);
                label.SetValue(Grid.RowProperty, i);

                var textBox = new TextBox { VerticalAlignment = VerticalAlignment.Center };
                textBox.SetValue(Grid.ColumnProperty, 1);
                textBox.SetValue(Grid.RowProperty, i);
                textBox.SetBinding(TextBox.TextProperty, new Binding(field.Path) { Mode = BindingMode.TwoWay });
                fieldsPannel.Children.Add(textBox);

                i++;
            }

            return window;
        }
    }
}
