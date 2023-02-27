using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.Messaging;


namespace AppControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{

    private ITransactionRepository _repository;
    public TransactionList(ITransactionRepository repository)
    {

        this._repository = repository;

        InitializeComponent();

        Reload();
        WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
        {
            Reload();
        });
    }

    private void Reload()
    {
        var items = _repository.GetAll();
        CollectionViewTransactions.ItemsSource = items;

        double income = items.Where(x => x.Type == Models.TransactionType.Income).Sum(a => a.Value);
        double expense = items.Where(x => x.Type == Models.TransactionType.Expense).Sum(a => a.Value);
        double balance = income - expense;

        LabelIncome.Text = income.ToString("C");
        LabelExpense.Text = expense.ToString("C");
        LabelBalance.Text = balance.ToString("C");
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        
        var grid = (Grid)sender;
        var gesture = (TapGestureRecognizer)grid.GestureRecognizers[0];
        Transaction transaction = (Transaction)gesture.CommandParameter;

        var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
        transactionEdit.SetTransactionToEdit(transaction);
        Navigation.PushModalAsync(transactionEdit);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var transactionAdd = Handler.MauiContext.Services.GetService<TransactionAdd>();
        Navigation.PushModalAsync(transactionAdd);
    }

    private async void TapGestureRecognizerTapped_ToDelete(object sender, EventArgs e)
    {
        bool result = await App.Current.MainPage.DisplayAlert("Excluir!", "Tem certeza que deseja excluir?", "Sim", "Não");

        if (result)
        {            
            var border = (Border)sender;
            var gesture = (TapGestureRecognizer)border.GestureRecognizers[0];
            Transaction transaction = (Transaction)gesture.CommandParameter;
            _repository.Delete(transaction);

            Reload();
        }
    }
}