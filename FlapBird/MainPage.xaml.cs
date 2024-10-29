
namespace FlapBird;

public partial class MainPage : ContentPage
{
	const int gravidade = 20;
	const int tempoEntreFrames = 50;
	const int maxtempoPulando= 3;
	const int forcaPulo =40;
	const int aberturaMinima=200;
	bool estaPulando = false;
	bool morto = true;
	double larguraJanela = 0;
	double alturaJanela = 0;
	int velocidade = 10;
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
				SoundHelper.Play("die.wav");
				labelover.Text="Parabéns você passou por \n  "+score.ToString("D3")+" canos";
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

	bool VerificaColisaoCanoCima()
	{
		var posHpas=(larguraJanela-50)-(pas.WidthRequest/2);
		var posVpas=(alturaJanela/2)-(pas.HeightRequest/2)+pas.TranslationY;
		if(posHpas >= Math.Abs(canoc.TranslationX)-canoc.WidthRequest&&
		posHpas <= Math.Abs(canoc.TranslationX)+canoc.WidthRequest&&
		posVpas <= canoc.HeightRequest+canoc.TranslationY)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	bool VerificaColisaoCanoBaixo()
	{
		var posHpas=(larguraJanela-50)-(pas.WidthRequest/2);
		var posVpas=(alturaJanela/2)+(pas.HeightRequest/2)+pas.TranslationY;
		var yMaxCano= canoc.HeightRequest+canoc.TranslationY+aberturaMinima;
		if(posHpas >= Math.Abs(canob.TranslationX)-canob.WidthRequest&&
		posHpas <= Math.Abs(canob.TranslationX)+canob.WidthRequest&&
        posVpas >= yMaxCano)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	bool VerificaColisao ()
	{
		return VerificaColisaoTeto()||
			VerificaColisaoChao()||
			VerificaColisaoCanoCima()||
			VerificaColisaoCanoBaixo();
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
		canoc.HeightRequest=height;
		canob.HeightRequest=height;
    }

	void GerenciaCanos ()
	{
		canoc.TranslationX-= velocidade;
		canob.TranslationX-= velocidade;
		if (canob.TranslationX<-larguraJanela)
		{
			canob.TranslationX=0;
			canoc.TranslationX=0;
			var alturaMax=-(canob.HeightRequest*0.1);
			var alturaMin= -canob.HeightRequest;
			canoc.TranslationY=Random.Shared.Next((int)alturaMin,(int)alturaMax);
			canob.TranslationY=canoc.TranslationY+aberturaMinima+canob.HeightRequest;
			labelscore.Text="Canos: "+score.ToString("D3");
			score++;
			SoundHelper.Play("point.wav");
			if(score % 2==0)
			velocidade++;
			
		}
	}
	void Inicializar()
	{
		SoundHelper.Play("start.wav");
		SoundHelper.Play("fundo.wav");
		canoc.TranslationX=-larguraJanela;
		canob.TranslationX=-larguraJanela;
		pas.TranslationX=0;
	    pas.TranslationY=0;
		score=0;
		morto=false;
		GerenciaCanos();
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

