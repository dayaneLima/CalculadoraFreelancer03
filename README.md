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
   public class ViewModelBase
    {
        
    }
````

Então vamos implementar da interface INotifyPropertyChanged. Quando implementamos dessa interface ela nos ajuda a notificar a 
View e a ViewModel que houve alterações nas variáveis que configurarmos, ou seja, ela notifica a todos que estão referenciando a variável 
que houve alteração em seu conteúdo.

```c#
   public class ViewModelBase : INotifyPropertyChanged
    {
    
    }
````

Ao implementar dessa interface, ela nos forçará a implementar o PropertyChangedEventHandler. Esse objeto será responsável por 
notificar as Views vinculadas a ViewModel quando houver alteração de valor na propriedade especificada.

```c#
   public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
````

Vamos por partes, primeiramente precisaremos de uma função que receberá o nome da propriedade que foi alterada, e então utilizaremos o 
nosso objeto PropertyChanged para notificar que houve alteração nessa propriedade. É necessário o nome da propriedade pois a View e a ViewModel
referenciará esse nome.

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
  public class ViewModelBase : INotifyPropertyChanged
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
