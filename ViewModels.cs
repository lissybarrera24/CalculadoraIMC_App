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