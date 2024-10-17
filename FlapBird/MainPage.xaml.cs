
namespace FlapBird;

public partial class MainPage : ContentPage
{
	const int gravidade = 6;
	const int tempoEntreFrames = 20;
	const int maxtempoPulando= 3;
	const int forcaPulo =17;
	const int aberturaMinima=10;
	bool estaPulando = false;
	bool morto = true;
	double larguraJanela = 0;
	double alturaJanela = 0;
	int velocidade = 15;
	int tempoPulando=0;
	int score=0;

	public MainPage()
	{
		InitializeComponent();
	}

	async Task Desenhar()
	{
		while (!morto)
		{
			if (estaPulando)
			AplicarPulo();
			else
			AplicarGravidade();
			GerenciaCanos();
			if (VerificaColisao())
			{
				morto=true;
				frameGameOver.IsVisible=true;
				break;
			}
			await Task.Delay(tempoEntreFrames);
		}
	}

	void AplicarPulo()
	{
		pas.TranslationY-=forcaPulo;
		tempoPulando++;
		if (tempoPulando>= maxtempoPulando)
		{
			estaPulando=false;
			tempoPulando=0;
		}
	}

	bool VerificaColisaoTeto()
	{
		var minY=-alturaJanela/2;
		if (pas.TranslationY <= minY)
		return true;
		else
		return false;

	}
	bool VerificaColisaoChao()
	{
		var maxY=alturaJanela/2 - grama.HeightRequest;
		if (pas.TranslationY >= maxY)
		return true;
		else
		return false;
		
	}

	bool VerificaColisao ()
	{
		if(!morto)
		{
			if (VerificaColisaoTeto()||
			VerificaColisaoChao())
			{
				return true;
			}
		}
		return false;
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
			var alturaMax=-100;
			var alturaMin= -canob.HeightRequest;
			canoc.TranslationY=Random.Shared.Next((int)alturaMin,(int)alturaMax);
			canob.TranslationY=canoc.TranslationY+aberturaMinima+canob.HeightRequest;
			score++;
			labelscore.Text="Canos: "+score.ToString("D3");
			labelover.Text="Parabéns você \n passou por \n  "+score.ToString("D3")+" canos";
		}
	}
	void Inicializar()
	{
		morto=false;
	    pas.TranslationY=0;
		score=0;
	}

	void OnGameOverClicked(object s, TappedEventArgs a)
	{
		frameGameOver.IsVisible=false;
		Inicializar();
		Desenhar();
	

	}

	void OnGridClicked (object s, TappedEventArgs a)
	{
		estaPulando=true;
	}


}

