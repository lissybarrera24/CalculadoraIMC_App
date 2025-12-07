# CalculadoraIMC_App
Models
namespace CalculadoraIMCApp.Models
{
    public class IMCData
    {
        public double Estatura { get; set; }
        public double Peso { get; set; }
        public string Sistema { get; set; }
        public double ResultadoIMC { get; set; }
    }
}


ViewModels

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CalculadoraIMCApp.Models;

namespace CalculadoraIMCApp.ViewModels
{
    public partial class IMCViewModel : ObservableObject
    {
        [ObservableProperty]
        double estatura;

        [ObservableProperty]
        double peso;

        [ObservableProperty]
        string sistemaSeleccionado;

        [ObservableProperty]
        double resultadoIMC;

        [ObservableProperty]
        string clasificacion;

        public List<string> Sistemas => new()
        {
            "Sistema Métrico",
            "Sistema Inglés"
        };

        private IMCData data = new IMCData();

        [RelayCommand]
        void CalcularIMC()
        {
            if (Estatura <= 0 || Peso <= 0)
            {
                Clasificacion = "Por favor ingrese valores válidos.";
                return;
            }

            if (SistemaSeleccionado == "Sistema Métrico")
            {
                // Estatura viene en centímetros → convertir a metros
                double estaturaMetros = Estatura / 100;
                ResultadoIMC = Peso / (estaturaMetros * estaturaMetros);
            }
            else if (SistemaSeleccionado == "Sistema Inglés")
            {
                // Fórmula oficial del IMC imperial
                ResultadoIMC = (Peso * 703) / (Estatura * Estatura);
            }

            Clasificacion = ObtenerClasificacion(ResultadoIMC);

            // Guardar datos
            data.Peso = Peso;
            data.Estatura = Estatura;
            data.Sistema = SistemaSeleccionado;
            data.ResultadoIMC = ResultadoIMC;
        }

        string ObtenerClasificacion(double imc)
        {
            if (imc < 18.5) return "Bajo peso";
            if (imc < 24.9) return "Normal";
            if (imc < 29.9) return "Sobrepeso";
            return "Obesidad";
        }
    }
}

Views.cs
namespace CalculadoraIMCApp.Views
{
    public partial class IMCPage : ContentPage
    {
        public IMCPage()
        {
            InitializeComponent();
        }
    }
}

Views.xaml
<ContentPage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:viewModels="clr-namespace:CalculadoraIMCApp.ViewModels"
    x:Class="CalculadoraIMCApp.Views.IMCPage">

    <ContentPage.BindingContext>
        <viewModels:IMCViewModel />
    </ContentPage.BindingContext>

    <VerticalStackLayout Padding="25" Spacing="15">

        <Label Text="Calculadora IMC"
               FontSize="30"
               HorizontalOptions="Center"
               FontAttributes="Bold" />

        <Picker Title="Selecciona el sistema"
                ItemsSource="{Binding Sistemas}"
                SelectedItem="{Binding SistemaSeleccionado}" />

        <Entry Placeholder="Estatura"
               Keyboard="Numeric"
               Text="{Binding Estatura}" />

        <Entry Placeholder="Peso"
               Keyboard="Numeric"
               Text="{Binding Peso}" />

        <Button Text="Calcular IMC"
                Command="{Binding CalcularIMCCommand}" />

        <Label Text="{Binding ResultadoIMC, StringFormat='IMC: {0:F2}'}"
               FontSize="22"
               HorizontalOptions="Center" />

        <Label Text="{Binding Clasificacion}"
               FontSize="22"
               HorizontalOptions="Center"
               TextColor="DarkBlue" />
    </VerticalStackLayout>
</ContentPage>


Vista.xaml
<ContentPage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:viewModels="clr-namespace:CalculadoraIMCApp.ViewModels"
    x:Class="CalculadoraIMCApp.Views.IMCPage">

    <!-- Enlazo esta página con mi ViewModel -->
    <ContentPage.BindingContext>
        <viewModels:IMCViewModel />
    </ContentPage.BindingContext>

    <VerticalStackLayout Padding="25" Spacing="15">

        <!-- Título principal -->
        <Label Text="Calculadora de IMC"
               FontSize="32"
               FontAttributes="Bold"
               HorizontalOptions="Center" />

        <!-- Selector del sistema -->
        <Picker Title="Selecciona el sistema"
                ItemsSource="{Binding Sistemas}"
                SelectedItem="{Binding SistemaSeleccionado}" />

        <!-- Caja para la estatura -->
        <Entry Placeholder="Estatura"
               Keyboard="Numeric"
               Text="{Binding Estatura}" />

        <!-- Caja para el peso -->
        <Entry Placeholder="Peso"
               Keyboard="Numeric"
               Text="{Binding Peso}" />

        <!-- Botón que ejecuta el cálculo -->
        <Button Text="Calcular IMC"
                Command="{Binding CalcularIMCCommand}" />

        <!-- Resultado del IMC -->
        <Label Text="{Binding ResultadoIMC, StringFormat='IMC: {0:F2}'}"
               HorizontalOptions="Center"
               FontSize="22" />

        <!-- Clasificación final -->
        <Label Text="{Binding Clasificacion}"
               HorizontalOptions="Center"
               FontSize="22"
               TextColor="DarkBlue" />

    </VerticalStackLayout>
</ContentPage>


