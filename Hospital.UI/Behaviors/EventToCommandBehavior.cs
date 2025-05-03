using CommunityToolkit.Mvvm.Input;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace Hospital.UI.Behaviors;

internal class EventToCommandBehavior : Behavior<FrameworkElement>
{

    public string EventName { get; set; } = null!;

    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(EventToCommandBehavior));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();

        if (AssociatedObject != null && !string.IsNullOrWhiteSpace(EventName))
        {
            var eventInfo = AssociatedObject.GetType().GetEvent(EventName);
            if (eventInfo != null)
            {
                var handler = new RoutedEventHandler(async (_, __) =>
                {
                    if (Command is IAsyncRelayCommand asyncCommand && asyncCommand.CanExecute(null))
                        await asyncCommand.ExecuteAsync(null);
                    else if (Command?.CanExecute(null) == true)
                        Command.Execute(null);
                });

                eventInfo.AddEventHandler(AssociatedObject, handler);
            }
        }
    }

}