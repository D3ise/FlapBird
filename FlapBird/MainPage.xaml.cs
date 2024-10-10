namespace FlapBird;

public partial class MainPage : ContentPage
{
	const int  gravidade=3;
	const int tempoEntreFrames=10;
	bool morto=false;
	
	public MainPage()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
		Desenhar();
    }

    async Task Desenhar()
	{
		while (!morto)
		{
			AplicarGravidade();
			await Task.Delay(tempoEntreFrames);
		}
	}
 void AplicarGravidade ()
 {
	pas.TranslationY+=gravidade;
 }
	
}

