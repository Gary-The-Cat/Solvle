using Impulse.SharedFramework.Application;
using Impulse.SharedFramework.Plugin;
using Impulse.SharedFramework.Ribbon;
using Impulse.SharedFramework.Services;
using Solvle.Application.SolvleUI;

namespace Solvle.Application;

public class SolvleApplication : IApplication
{
    public string DisplayName => "Solvle";

    public Uri Icon => new Uri(
        "pack://application:,,,/Impulse.Dashboard;component/Icons/Export/Bat.png",
        UriKind.Absolute);

    public IDashboardProvider Dashboard { get; set; }

    public async Task Initialize()
    {
        this.Dashboard.RegisterRequiredService<IRibbonService>();
        this.Dashboard.RegisterRequiredService<IDocumentService>();
        this.Dashboard.RegisterRequiredService<IDialogService>();

        var taylorDemo = new RibbonButtonViewModel
        {
            Title = "Solvle",
            Id = "Developer.Debug.Taylor.Solvle",
            EnabledIcon = "pack://application:,,,/Impulse.Dashboard;Component/Icons/Export/Candy.png",
            DisabledIcon = "pack://application:,,,/Impulse.Dashboard;Component/Icons/Export/Candy_GS.png",
            IsEnabled = true,
            Callback = OpenSolvleDemo,
        };

        this.Dashboard.RibbonService.AddButton(taylorDemo);
    }

    private void OpenSolvleDemo()
    {
        var sfmlDemo = new SolvleViewModel(this.Dashboard.DialogService);
        this.Dashboard.DocumentService.OpenDocument(sfmlDemo);
    }

    public async Task LaunchApplication()
    {
    }

    public async Task OnClose()
    {
    }
}
