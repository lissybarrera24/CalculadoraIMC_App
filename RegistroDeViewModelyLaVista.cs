using CalculadoraIMCApp.ViewModels;
using CalculadoraIMCApp.Views;

namespace CalculadoraIMCApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>();

        // Registro de la Vista y del ViewModel
        builder.Services.AddSingleton<IMCViewModel>();
        builder.Services.AddSingleton<IMCPage>();

        return builder.Build();
    }
}
