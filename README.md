# CalculadoraFreelancer03

Se trata da continuação do app <a href="https://github.com/dayaneLima/CalculadoraFreelancer02">CalculadoraFreelancer02</a>

## Criação da ViewModelBase

Vamos criar a nossa ViewModel base, em que todas as outras ViewModels irão herdar dela. Nessa ViewModelBase 
vamos ter a função base para utilização dos data bindings.

### Criação das pastas

Vamos criar uma pasta no projeto principal chamada de ViewModels, e dentro dessa pasta, vamos criar outra chamada Base
para ter a nossa classe base dentro dela.

Ficará assim:

<img src="https://github.com/dayaneLima/CalculadoraFreelancer03/blob/master/Docs/Imgs/01_mvvm_criacao_pastas.PNG" alt="Criação pastas ViewModel" width="260">


### Criação da classe ViewModelBase

Dentro da pasta Base, vamos criar uma classe chamada ViewModelBase. Essa classe será abstrata, ou seja, 
ninguém poderá criar uma instância do tipo ViewModelBase, somente será possível herdar dessa classe.

```c#
   public abstract class ViewModelBase
    {
        
    }
````

Então vamos implementar da interface INotifyPropertyChanged. Quando implementamos dessa interface ela nos ajuda a notificar a 
View e a ViewModel que houve alterações nas variáveis que configurarmos, ou seja, ela notifica a todos que estão referenciando a variável que houve alteração em seu conteúdo.

```c#
   public abstract class ViewModelBase : INotifyPropertyChanged
    {
    
    }
````

Ao implementar dessa interface, ela nos forçará a implementar o PropertyChangedEventHandler. Esse objeto será responsável por 
notificar as Views vinculadas a ViewModel quando houver alteração de valor na propriedade especificada.

```c#
   public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
````

Vamos por partes, primeiramente precisaremos de uma função que receberá o nome da propriedade que foi alterada, e então utilizaremos o 
nosso objeto PropertyChanged para notificar que houve alteração nessa propriedade. É necessário o nome da propriedade pois a View e a ViewModel referenciarão esse nome.

```c#
  public abstract class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
````

O CallerMemberName permite pegar o nome de quem está chamando essa função, 
isso nos ajudará mais a frente, em outra função na verdade, no qual não precisaremos passar parâmetro para essa função, ele vai pegar o nome da 
que está sendo usada para chamar a função.

Agora precisamos criar uma função para representar o nosso set, ou seja, a que será responsável por atribuir o valor recebido a nossa variável,
como sempre será disparado um evento ao alterar uma variável, também é necessário verificar se houve alterações no valor da variável antes de 
chamar a nossa função que criamos anteriormente. Ficará dessa forma:

```c#
  public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
````

Explicando melhor a função SetProperty, recebemos por referêcia a nossa variável que será atribuida o valor, pois se vamos alterar seu conteúdo, 
queremos que isso seja refletido na propriedade da classe que chamou a função, por isso a necessidade da referência. O segundo parâmetro é o valor
que se deseja atribuir a propriedade que recebemos no primeiro parâmetro. Por último precisamos do nome dessa proriedade, pelo mesmo motivo da função anterior,
precisamos saber qual o nome da propriedade para notificar a quem está vinculada a ela saber que houve alteração em seu valor. É verificado se houve 
alteração do valor, caso não houve alteração será retornado false, caso contrário, é atribuido o valor a variável e chamado nossa função para notificar 
aos interessados que houve alteração na propriedade, e por fim retornado true, que representa que foi atribuido o valor recebido a propriedade 
(que realmente houve alteração no conteúdo da variável).


## Criação da CalculoValorHoraPageViewModel

Vamos criar a ViewModel para a nossa View CalculoValorHoraPage. Iremos remover toda a lógica do Code Behind e adicionar na nossa ViewModel. Dentro da pasta ViewModels, crie uma classe chamada CalculoValorHoraPageViewModel que herdará da nossa ViewModelBase criada anteriormente.

```c#
	public class CalculoValorHoraPageViewModel : ViewModelBase
	{
	}
````

### Variáveis Bindables

Vamos criar para cada elemento que tinhamos na View (ValorGanhoMes, HorasTrabalhadasPorDia, DiasTrabalhadosPorMes, DiasFeriasPorAno, ValorDaHora e o Profissional) uma propriedade bindable na nossa ViewModel. Uma propriedade bindable quer dizer que é uma propriedade que ao ser alterada deverá ser notificada sua alteração. Primeiramente criamos uma variável privada e depois uma pública, nessa última, no set deveremos chamar o SetProperty, que verificará se houve alteração no valor da variável e realizar a notificação informando que teve alteração no conteúdo da variável. Por convensão, a variável privada é escrita em minúscula e a pública maiúscula. O ValorGanhoMes ficará dessa forma:


```c#
   public class CalculoValorHoraPageViewModel : ViewModelBase
    {
        private double valorGanhoMes;
        public double ValorGanhoMes
        {
            get { return valorGanhoMes; }
            set
            {
                SetProperty(ref valorGanhoMes, value);
            }
        }
    }
````

Agora vamos fazer para o restante das variáveis:

```c#
    public class CalculoValorHoraPageViewModel : ViewModelBase
    {
        private double valorGanhoMes;
        public double ValorGanhoMes
        {
            get { return valorGanhoMes; }
            set
            {
                SetProperty(ref valorGanhoMes, value);
            }
        }

        private int horasTrabalhadasPorDia;
        public int HorasTrabalhadasPorDia
        {
            get { return horasTrabalhadasPorDia; }
            set
            {
                SetProperty(ref horasTrabalhadasPorDia, value);
            }
        }

        private int diasTrabalhadosPorMes;
        public int DiasTrabalhadosPorMes
        {
            get { return diasTrabalhadosPorMes; }
            set
            {
                SetProperty(ref diasTrabalhadosPorMes, value);
            }
        }

        private int diasFeriasPorAno;
        public int DiasFeriasPorAno
        {
            get { return diasFeriasPorAno; }
            set
            {
                SetProperty(ref diasFeriasPorAno, value);
            }
        }

        private double valorDaHora;
        public double ValorDaHora
        {
            get { return valorDaHora; }
            set { SetProperty(ref valorDaHora, value); }
        }
        
        private Profissional profissional;
        public Profissional Profissional
        {
            get { return profissional; }
            set
            {
                SetProperty(ref profissional, value);
            }
        }

    }
````

### Função CalcularValorHora

Antes tinhamos a função CalcularValorHoraButton_Clicked, agora vamos recriá-la. Vamos criar na ViewModel uma função chamada CalcularValorHora, ela terá a mesma lógica da CalcularValorHoraButton_Clicked, mas sem a necessidade de chamar a função de gravar e de fazer as conversões de tipagem, já que nossas propriedades já são tipadas. Ficará dessa forma:

```c#
 private void CalcularValorHora()
        {

          if (ValorGanhoMes > 0 && DiasTrabalhadosPorMes > 0 && HorasTrabalhadasPorDia > 0) {
                double valorGanhoAnual = ValorGanhoMes * 12;
                int totalDiasTrabalhadosPorAno = DiasTrabalhadosPorMes * 12;

                if (DiasFeriasPorAno > 0)
                {
                    totalDiasTrabalhadosPorAno -= DiasFeriasPorAno;
                }

                ValorDaHora = valorGanhoAnual / (totalDiasTrabalhadosPorAno * HorasTrabalhadasPorDia);
            }
        }
````

### Command GravarCommand

Como dito durante a aula, além da View referenciar as variáveis, ela pode chamar diretamente funções da nossa ViewModel, chamadas de Commands. Como ao clicar em gravar na nossa tela, queremos que grave o valor da hora do profissional, o click desse botão irá acionar um comando na nossa ViewModel para ela cuidar dessa operação. Na nossa ViewModel vamos criar uma propriedade do tipo Command e vamos a chamar de GravarCommand, dessa forma: 

```c#
  public Command GravarCommand { get; }
````

Agora vamos criar um construtor para a nossa classe e instanciar esse objeto do tipo Command. Ao instanciar esse objeto, devemos informar a ele o nome da função responsável por cuidar do comando, ou seja, qual função será executada quando o commando for acionado. Vamor aproveitar e instanciar o nosso objeto Profissional também. Dessa forma: 

```c#
   public CalculoValorHoraPageViewModel()
  {
      GravarCommand = new Command(ExecuteGravarCommand);
      Profissional = new Profissional();
  }
````

Agora precisamos criar essa função ExecuteGravarCommand. Ela será similar a Gravar da CalculoValorHoraPage, mas agora atribuiremos os valores ao objeto Profissional ao invés de já criá-lo chamando a função de inserir. Ficará assim:

```c#
  private async void ExecuteGravarCommand(object obj)
        {
            Profissional.ValorGanhoMes = ValorGanhoMes;
            Profissional.HorasTrabalhadasPorDia = HorasTrabalhadasPorDia;
            Profissional.DiasTrabalhadosPorMes = DiasTrabalhadosPorMes;
            Profissional.DiasFeriasPorAno = DiasFeriasPorAno;
            Profissional.ValorPorHora = ValorDaHora;

            var profissionalAzureClient = new AzureRepository();
            profissionalAzureClient.Insert(Profissional);

            await App.Current.MainPage.DisplayAlert("Sucesso", "Valor por hora gravado!", "Ok");
        }
````

### Chamada da função CalcularValorHora

Antes chamavamos a função que calculava o valor do profissional ao clicar em gravar, agora podemos chamar toda vez em que um valor usado no cálculo for alterado, isso graças ao Data Bindings. Então todas as vezes que um valor for setado ( na função set) vamos chamar a função CalcularValorHora(). Então no Set da ValorGanhoMes, HorasTrabalhadasPorDia, DiasTrabalhadosPorMes e DiasFeriasPorAno chamaremos a função CalcularValorHora(). Ficará assim: 

```c#
        private double valorGanhoMes;
        public double ValorGanhoMes
        {
            get { return valorGanhoMes; }
            set
            {
                SetProperty(ref valorGanhoMes, value);
                CalcularValorHora();
            }
        }

        private int horasTrabalhadasPorDia;
        public int HorasTrabalhadasPorDia
        {
            get { return horasTrabalhadasPorDia; }
            set
            {
                SetProperty(ref horasTrabalhadasPorDia, value);
                CalcularValorHora();
            }
        }

        private int diasTrabalhadosPorMes;
        public int DiasTrabalhadosPorMes
        {
            get { return diasTrabalhadosPorMes; }
            set
            {
                SetProperty(ref diasTrabalhadosPorMes, value);
                CalcularValorHora();
            }
        }

        private int diasFeriasPorAno;
        public int DiasFeriasPorAno
        {
            get { return diasFeriasPorAno; }
            set
            {
                SetProperty(ref diasFeriasPorAno, value);
                CalcularValorHora();
            }
        }
````

### CalculoValorHoraPageViewModel

Nossa ViewModel ficará assim:

```c#
 public class CalculoValorHoraPageViewModel : ViewModelBase
    {
        private double valorGanhoMes;
        public double ValorGanhoMes
        {
            get { return valorGanhoMes; }
            set
            {
                SetProperty(ref valorGanhoMes, value);
                CalcularValorHora();
            }
        }

        private int horasTrabalhadasPorDia;
        public int HorasTrabalhadasPorDia
        {
            get { return horasTrabalhadasPorDia; }
            set
            {
                SetProperty(ref horasTrabalhadasPorDia, value);
                CalcularValorHora();
            }
        }

        private int diasTrabalhadosPorMes;
        public int DiasTrabalhadosPorMes
        {
            get { return diasTrabalhadosPorMes; }
            set
            {
                SetProperty(ref diasTrabalhadosPorMes, value);
                CalcularValorHora();
            }
        }

        private int diasFeriasPorAno;
        public int DiasFeriasPorAno
        {
            get { return diasFeriasPorAno; }
            set
            {
                SetProperty(ref diasFeriasPorAno, value);
                CalcularValorHora();
            }
        }

        private double valorDaHora;
        public double ValorDaHora
        {
            get { return valorDaHora; }
            set { SetProperty(ref valorDaHora, value); }
        }

        private Profissional profissional;
        public Profissional Profissional
        {
            get { return profissional; }
            set
            {
                SetProperty(ref profissional, value);
            }
        }

        public Command GravarCommand { get; }

        public CalculoValorHoraPageViewModel()
        {
            GravarCommand = new Command(ExecuteGravarCommand);
            Profissional = new Profissional();
        }

        private void CalcularValorHora()
        {

             if (ValorGanhoMes > 0 && DiasTrabalhadosPorMes > 0 && HorasTrabalhadasPorDia > 0) {
                double valorGanhoAnual = ValorGanhoMes * 12;
                int totalDiasTrabalhadosPorAno = DiasTrabalhadosPorMes * 12;

                if (DiasFeriasPorAno > 0)
                {
                    totalDiasTrabalhadosPorAno -= DiasFeriasPorAno;
                }

                ValorDaHora = valorGanhoAnual / (totalDiasTrabalhadosPorAno * HorasTrabalhadasPorDia);
            }
        }

        private async void ExecuteGravarCommand(object obj)
        {
            Profissional.ValorGanhoMes = ValorGanhoMes;
            Profissional.HorasTrabalhadasPorDia = HorasTrabalhadasPorDia;
            Profissional.DiasTrabalhadosPorMes = DiasTrabalhadosPorMes;
            Profissional.DiasFeriasPorAno = DiasFeriasPorAno;
            Profissional.ValorPorHora = ValorDaHora;

            var profissionalAzureClient = new AzureRepository();
            profissionalAzureClient.Insert(Profissional);

            await App.Current.MainPage.DisplayAlert("Sucesso", "Valor por hora gravado!", "Ok");
        }
    }
````


## Alteração da CalculoValorHoraPage

Vamos alterar nossa View para ficar vinculada a ViewModel que criamos anteriormente. Vamos editar o arquivo CalculoValorHoraPage.xaml.cs, vamos apagar todas as funções que haviamos criado, deixando apenas o construtor:

```c#
	public partial class CalculoValorHoraPage : ContentPage
	{
		public CalculoValorHoraPage ()
		{
			InitializeComponent ();
            		CalcularValorHoraButton.Clicked += CalcularValorHoraButton_Clicked;
		}       
	}
````

No construtor vamos remover a linha da vinculação do evento de Clicked. Após vamos atribuir ao Binding da nossa View que será a sua fonte de dados, ou seja, a nossa ViewModel, para isso vamos criar uma instância da CalculoValorHoraPageViewModel e atribuir a propriedade BindingContext da nossa classe. Nossa CalculoValorHoraPage.xaml.cs ficará assim:

```c#
[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalculoValorHoraPage : ContentPage
	{
		public CalculoValorHoraPage ()
		{
			InitializeComponent ();
		        var viewModel = new CalculoValorHoraPageViewModel();
		        BindingContext = viewModel;
		}       
    }
````

Agora vamos editar o arquivo CalculoValorHoraPage.xaml. Em nossos Entrys não precisaremos mais dos names, então vamos revomer as propriedades x:Name. O Texto do Entry agora está vinculado a nossa ViewModel pelo Binding. No Entry vamos adicionar a propriedade Text, em seu valor, entre aspas duplas, vamos abrir chaves e escrever a palavra Binding, indicando que é para utilizar Data Binding, então logo a frente da escrita Binding, vamos por o nome da variável bindable que está na nossa ViewModel, para o campo de valor ganho por mês haviamos criado na ViewModel uma propriedade chamada ValorGanhoMes (não confuda com o x:Name, não estamos utilizando ele).

```xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalculadoraFreelancer01.CalculoValorHoraPage"
             Title="Valor da Hora"
             Padding="10">
    <ContentPage.Content>
        <StackLayout>

            <Label Text="Valor ganho por mês" />
            <Entry Placeholder="Valor ganho por mês"
                   Text="{Binding ValorGanhoMes}"
                   Keyboard="Numeric"/>

            <Label Text="Horas trabalhadas por dia" />
            <Entry Placeholder="Horas trabalhadas por dia"
                   Text="{Binding HorasTrabalhadasPorDia}"
                   Keyboard="Numeric"/>

            <Label Text="Dias trabalhados por mês" />
            <Entry Placeholder="Dias trabalhados por mês"
                   Text="{Binding DiasTrabalhadosPorMes}"
                   Keyboard="Numeric"/>

            <Label Text="Dias de férias por ano" />
            <Entry Placeholder="Dias de férias por ano"
                   Text="{Binding DiasFeriasPorAno}"
                   Keyboard="Numeric"/>	

            <Label Text="R$ 00,00 / hora"
                   FontSize="Large"
                   TextColor="Green"/>

            <Button Text="Gravar"
                    BackgroundColor="#6699ff"
                    TextColor="White"
                    x:Name="CalcularValorHoraButton"/>


        </StackLayout>
    </ContentPage.Content>
</ContentPage>
````

Agora no Text da nossa label que representa o Valor da hora podemos referenciar diretamente a nossa propriedade da ViewModel que chamamos de ValorDaHora. Além disso podemos utilizar o StringFormat do C# diretamente no nosso xaml para formatar o valor em reais, semelhante como faziamos no nosso Code Behind, dessa forma:

```xml
     <Label Text="{Binding ValorDaHora, StringFormat='{0:C} / hora'}"
             FontSize="Large"
             TextColor="Green"/>
````

No nosso button de gravar, vamos chamar o Command que criamos na nossa ViewModel, o próprio elemento Button do Xamarin tem uma propriedade chamada Command, para passarmos o nome do comando a ser chamado, dessa forma:

```xml
    <Button Text="Gravar"
           BackgroundColor="#6699ff"
           TextColor="White"                    
           Command="{Binding GravarCommand}"/>
````

Nosso arquivo .xaml ficou dessa forma:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="CalculadoraFreelancer01.CalculoValorHoraPage"
             Title="Valor da Hora"
             Padding="10">
    <ContentPage.Content>
        <StackLayout>

            <Label Text="Valor ganho por mês" />
            <Entry Placeholder="Valor ganho por mês"
                   Text="{Binding ValorGanhoMes}"
                   Keyboard="Numeric"/>

            <Label Text="Horas trabalhadas por dia" />
            <Entry Placeholder="Horas trabalhadas por dia"
                   Text="{Binding HorasTrabalhadasPorDia}"
                   Keyboard="Numeric"/>

            <Label Text="Dias trabalhados por mês" />
            <Entry Placeholder="Dias trabalhados por mês"
                   Text="{Binding DiasTrabalhadosPorMes}"
                   Keyboard="Numeric"/>

            <Label Text="Dias de férias por ano" />
            <Entry Placeholder="Dias de férias por ano"
                   Text="{Binding DiasFeriasPorAno}"
                   Keyboard="Numeric"/>
	
            <Label Text="{Binding ValorDaHora, StringFormat='{0:C} / hora'}"
                   FontSize="Large"
                   TextColor="Green"/>

            <Button Text="Gravar"
                    BackgroundColor="#6699ff"
                    TextColor="White"                    
                    Command="{Binding GravarCommand}"/>


        </StackLayout>
    </ContentPage.Content>
</ContentPage>
````
## Testando o projeto

Execute o projeto e veja se tudo continua funcionando.


## Criação da ProjetoPageViewModel

Agora que adicionamos a ViewModel para o nosso Profissional, vamos também criar uma para o nosso Projeto.

Crie dentro da pasta ViewModels uma classe chamada ProjetoPageViewModel e herde da ViewModelBase.

Crie as variáveis bindables para as propriedades que precisamos para a tela, São elas: Nome (string), ValorPorHora (double), HorasPorDia (int), DiasDuracaoProjeto (int) e ValorTotal (double).

A classe ficará assim:

```c#
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
            }
        }

        private int horasPorDia;
        public int HorasPorDia
        {
            get { return horasPorDia; }
            set
            {
                SetProperty(ref horasPorDia, value);
            }
        }

        private int diasDuracaoProjeto;
        public int DiasDuracaoProjeto
        {
            get { return diasDuracaoProjeto; }
            set
            {
                SetProperty(ref diasDuracaoProjeto, value);
            }
        }

        private double valorTotal;
        public double ValorTotal
        {
            get { return valorTotal; }
            set { SetProperty(ref valorTotal, value); }
        }
    }
````

Na função GravarButton_Clicked realizávamos o cálculo do valor total do projeto e chamávamos a função de gravar. Vamos nos basear nessa função, primeiramente faça a do cálculo, como mostrado a seguir:

```c#
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
````

Agora ao alterar qualquer propriedade que influenciará no cálculo do valor do projeto chame a função CalcularValorTotal(), você fará isso no Set das propriedades, como fizemos na da CalculoValorHoraPageViewModel:

```c#
public class ProjetoPageViewModel : ViewModelBase
{
    ...
    
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
	
	...
}
	
````

Agora vamos criar dois Commands, um para gravar o nosso projeto e o outro para limpar os campos

```c#
        public Command GravarCommand { get; }
        public Command LimparCommand { get; }
````

Crie um construtor para a ViewModel para instancias os dois Commands e crie as funções para gravar e limpar, como mostrado a seguir

```c#
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
````

A ProjetoPageViewModel ficará assim:

```C#
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
````

### Alteração da ProjetoPage.xaml

Edite agora o ProjetoPage.xaml, remova todos os x:Name dos elementos da tela.

Nos Entrys, na propriedade Text, adicione o Binding para a variável da ViewModel.

Na label de valor total referencie sua variável que representa o valor total  do cálculo, e use o string format para formatar para moeda o resultado.

Por último, nos botões de Limpar e de Gravar referenciem os respectivos Commands criados.

Ficará assim:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             Title="Projeto"
             Padding="10"
             x:Class="CalcFreelancer.ProjetoPage">
    <ContentPage.Content>
        <StackLayout>
            <Label Text="Nome do Projeto" />
                <Entry Placeholder="Nome do projeto"
                   Text="{Binding Nome}" />

                <Label Text="Valor por hora" />
                <Entry Placeholder="Valor por hora"
                   Keyboard="Numeric"
                    Text="{Binding ValorPorHora}" />

                <Label Text="Horas por dia" />
                <Entry Placeholder="Horas por dia"
                   Keyboard="Numeric"
                    Text="{Binding HorasPorDia}" />

                <Label Text="Dias de duração do projeto" />
                <Entry Placeholder="Dias de duração do projeto"
                   Keyboard="Numeric"
                    Text="{Binding DiasDuracaoProjeto}" />

                <Label FontSize="Large"
                    Text="{Binding ValorTotal, StringFormat='{0:C} / hora'}" />

                <Grid>
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="*" />
                    </Grid.ColumnDefinitions>

                    <Button Grid.Column="0"
                            BackgroundColor="#cdcdcd"
                            Text="Limpar"
                            Command="{Binding LimparCommand}"/>
                    <Button Grid.Column="1"
                            Text="Gravar"
                            TextColor="White"
                            BackgroundColor="#6699ff"
                            Command="{Binding GravarCommand}"/>
                </Grid>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
````

### Alteração da ProjetoPage.xaml.cs

Altere o construtor da classe ProjetoPage.xaml.cs para vincular o BindingContext da tela a nossa ViewModel que criamos no passo anterior. 

Após, apague o restante das funções da ProjetoPage.xaml.cs.

Ficará assim:

```c#
	public partial class ProjetoPage : ContentPage
	{
		public ProjetoPage ()
		{
			InitializeComponent ();
			var viewModel = new ProjetoPageViewModel();
			BindingContext = viewModel;
		}

	}
````

### Testando o projeto

Agora execute o projeto e veja se continua tudo funcionando corretamente.
