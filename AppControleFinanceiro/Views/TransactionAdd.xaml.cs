namespace AppControleFinanceiro.Views;

public partial class TransactionAdd : ContentPage
{
	public TransactionAdd()
	{
		InitializeComponent();
	}

	private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
	{
		Navigation.PopModalAsync();
	}
}