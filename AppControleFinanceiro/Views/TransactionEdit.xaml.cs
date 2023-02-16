namespace AppControleFinanceiro.Views;

public partial class TransactionEdit : ContentPage
{
	public TransactionEdit()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}