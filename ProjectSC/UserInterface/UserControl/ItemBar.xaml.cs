﻿using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectSC.UserControls.Custom
{
    public partial class ItemBar : UserControl
    {
        public ItemBar()
        {
            InitializeComponent();
        }

        public ItemBar(ToDoListUSC myDay)
        {
            InitializeComponent();

            MyDay = myDay;
        }

        private ToDoListUSC MyDay;

        public int Id { get; set; }

        const int ChaeckBoxIconSize = 35;

        private bool CheckboxLoaded = false;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            textBlock.Text = MyDay.Inventory[MyDay.Inventory.FindIndex(x => x.Id == Id)].Title;
            //textBlock.Text = Id.ToString();

            checkBox.IsChecked = MyDay.Inventory[MyDay.Inventory.FindIndex(x => x.Id == Id)].IsCompleted;
            CheckboxLoaded = true;

            StarToggle.IsChecked = MyDay.Inventory[MyDay.Inventory.FindIndex(x => x.Id == Id)].IsImportant;
        }


        private void ToDoChecked(object sender, RoutedEventArgs e)
        {
            var icon = new PackIcon { Kind = PackIconKind.Check };
            icon.Height = ChaeckBoxIconSize;
            icon.Width = ChaeckBoxIconSize;
            icon.HorizontalAlignment = HorizontalAlignment.Center;
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Foreground = Brushes.Black;

            checkBox.Content = icon;

            textBlock.TextDecorations = TextDecorations.Strikethrough;

            if (CheckboxLoaded)
            {
                DataAccess.UpdateCompletion(Id, true, MyDay.Inventory);
            }
        }

        private void ToDoUnchecked(object sender, RoutedEventArgs e)
        {
            if (cBoxGrid.IsMouseOver)
            {
                var icon = new PackIcon { Kind = PackIconKind.Check };
                icon.Height = 25;
                icon.Width = 25;
                icon.HorizontalAlignment = HorizontalAlignment.Center;
                icon.VerticalAlignment = VerticalAlignment.Center;
                icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));

                checkBox.Content = icon;
            }

            textBlock.TextDecorations = null;

            DataAccess.UpdateCompletion(Id, false, MyDay.Inventory);
        }

        private void StarToggle_Click(object sender, RoutedEventArgs e)
        {
            if (StarToggle.IsChecked == true)
            {
                DataAccess.Update(Id, true, MyDay.Inventory);
            }
            else
            {
                DataAccess.Update(Id, false, MyDay.Inventory);
            }
        }


        #region Mouse down events
        private bool BorderEventCanActivate = true;
        private void Border_MouseDown(object sender, RoutedEventArgs e)
        {
            if (BorderEventCanActivate)
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    MyDay.OpenDetailsPanel(Id, this);
                }
            }
        }

        private void Grid_MouseDown(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (checkBox.IsChecked == true)
                {
                    checkBox.IsChecked = false;
                }
                else
                {
                    checkBox.IsChecked = true;
                }
            }
        }
        #endregion

        #region Mouse over events
        public void MouseEnterHighLight(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Border))
            {
                border.Background = Brushes.LightGray;
                cBoxGrid.Background = Brushes.LightGray;
                StarToggle.Background = Brushes.LightGray;
            }
            if (sender.GetType() == typeof(CheckBox))
            {
                if (checkBox.IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.Check };
                    icon.Height = ChaeckBoxIconSize;
                    icon.Width = ChaeckBoxIconSize;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));

                    checkBox.Content = icon;
                }
            }
            if (sender.GetType() == typeof(Grid))
            {
                BorderEventCanActivate = false;
                cBoxGrid.Background = Brushes.LightGray;
                StarToggle.Background = Brushes.LightGray;

                if (checkBox.IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.Check };
                    icon.Height = ChaeckBoxIconSize;
                    icon.Width = ChaeckBoxIconSize;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));

                    checkBox.Content = icon;
                }
            }
        }

        public void MouseLeaveUnHighLight(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Border))
            {
                border.Background = Brushes.White;
                cBoxGrid.Background = Brushes.White;
                StarToggle.Background = Brushes.White;
            }
            if (sender.GetType() == typeof(CheckBox))
            {
                if (checkBox.IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.Check };
                    icon.Height = ChaeckBoxIconSize;
                    icon.Width = ChaeckBoxIconSize;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));

                    checkBox.Content = icon;
                }
            }
            if (sender.GetType() == typeof(Grid))
            {
                BorderEventCanActivate = true;

                if (cBoxGrid.IsMouseOver == false && border.IsMouseOver == false)
                {
                    cBoxGrid.Background = Brushes.White;
                }

                if (checkBox.IsChecked == false)
                {
                    var icon = new PackIcon { Kind = PackIconKind.CheckboxBlankCircleOutline };
                    icon.Height = ChaeckBoxIconSize;
                    icon.Width = ChaeckBoxIconSize;
                    icon.HorizontalAlignment = HorizontalAlignment.Center;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2196F3"));

                    checkBox.Content = icon;
                }
            }
        }
        #endregion


        public void Update(string title)
        {
            textBlock.Text = title;
        }
    }
}
