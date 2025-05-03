using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital.UI.Controls;

public partial class GoBackButton : UserControl
{

    public static readonly DependencyProperty GoBackCommandProperty =
        DependencyProperty.Register(nameof(GoBackCommand), typeof(ICommand), typeof(GoBackButton));

    public ICommand GoBackCommand
    {
        get => (ICommand)GetValue(GoBackCommandProperty);
        set => SetValue(GoBackCommandProperty, value);
    }

    public GoBackButton()
    {
        InitializeComponent();
    }

}