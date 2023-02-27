using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Messaging;

namespace AppControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{
   
    private ITransactionRepository _repository;
    public TransactionList( ITransactionRepository repository)
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

    private void OnButtoClicked_To_TransactionAdd(object sender, EventArgs args)
    {
        var transactionAdd = Handler.MauiContext.Services.GetService<TransactionAdd>();
        Navigation.PushModalAsync(transactionAdd);
    }

    private void OnButtoClicked_To_TransactionEdit(object sender, EventArgs e)
    {
        var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
        Navigation.PushModalAsync(transactionEdit);
    }

}