
namespace FlapBird;

public partial class MainPage : ContentPage
{
	const int gravidade = 3;
	const int tempoEntreFrames = 25;
	bool morto = true;
	double larguraJanela = 0;
	double alturaJanela = 0;
	int velocidade = 20;


	public MainPage()
	{
		InitializeComponent();
	}

	async Task Desenhar()
	{
		while (!morto)
		{
			AplicarGravidade();
			await Task.Delay(tempoEntreFrames);
			GerenciaCanos();
		}
	}
	void AplicarGravidade()
	{
		pas.TranslationY += gravidade;
	}

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
		larguraJanela=width;
		alturaJanela=height;
    }

	void GerenciaCanos ()
	{
		canoc.TranslationX-= velocidade;
		canob.TranslationX-= velocidade;
		if (canob.TranslationX<-larguraJanela)
		{
			canob.TranslationX=0;
			canoc.TranslationX=0;
		}
	}
	void Inicializar()
	{
		morto=false;
	    pas.TranslationY=0;
	}

	void OnGameOverClicked(object s, TappedEventArgs a)
	{
		frameGameOver.IsVisible=false;
		Inicializar();
		Desenhar();

	}


}

