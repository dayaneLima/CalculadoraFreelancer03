using CalcFreelancer.Repository;
using CalcFreelancer.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CalcFreelancer.ViewModels
{
    public class ProjetoPageViewModel : ViewModelBase
    {
        private string nome;
        public string Nome
        {
            get { return nome; }
            set { SetProperty(ref nome, value); }
        }

        private double valorPorHora;
        public double ValorPorHora
        {
            get { return valorPorHora; }
            set
            {
                SetProperty(ref valorPorHora, value);
                CalcularValorTotal();
            }
        }

        private int horasPorDia;
        public int HorasPorDia
        {
            get { return horasPorDia; }
            set
            {
                SetProperty(ref horasPorDia, value);
                CalcularValorTotal();
            }
        }

        private int diasDuracaoProjeto;
        public int DiasDuracaoProjeto
        {
            get { return diasDuracaoProjeto; }
            set
            {
                SetProperty(ref diasDuracaoProjeto, value);
                CalcularValorTotal();
            }
        }

        private double valorTotal;
        public double ValorTotal
        {
            get { return valorTotal; }
            set { SetProperty(ref valorTotal, value); }
        }

        public Command GravarCommand { get; }
        public Command LimparCommand { get; }

        public ProjetoPageViewModel()
        {
            GravarCommand = new Command(ExecuteGravarCommand);
            LimparCommand = new Command(ExecuteLimparCommand);
        }

        private void ExecuteLimparCommand()
        {
            Nome = string.Empty;
            HorasPorDia = 0;
            DiasDuracaoProjeto = 0;
        }

        private async void ExecuteGravarCommand()
        {
            var projetoAzureClient = new AzureProjetoRepository();

            projetoAzureClient.Insert(new Models.Projeto()
            {
                ValorPorHora = ValorPorHora,
                HorasPorDia = HorasPorDia,
                DiasDuracaoProjeto = DiasDuracaoProjeto,
                ValorTotal = ValorTotal
            });


            ExecuteLimparCommand();

            await App.Current.MainPage.DisplayAlert("Sucesso", "Projeto gravado!", "Ok");
        }

        private void CalcularValorTotal()
        {
            if (ValorPorHora > 0 && HorasPorDia > 0 && DiasDuracaoProjeto > 0)
            {
                ValorTotal = ValorPorHora * HorasPorDia * DiasDuracaoProjeto;
            }
            else
            {
                ValorTotal = 0;
            }
        }

    }
}
