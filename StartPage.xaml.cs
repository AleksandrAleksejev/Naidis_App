namespace MauiApp1;

public partial class StartPage : ContentPage
{
	public List<ContentPage> lehed = new List<ContentPage>() { new TextPage(0), new FigurePage(), new Valgusfloor() };
	public List<string> Tekstid = new List<string>{"Tee lahti TekstPage", "Tee lahti Figure", "Tee lahti Valgusfoor" };
	ScrollView sv;
	VerticalStackLayout vsl;
	public StartPage()
	{
		Title = "Avaleht";
		vsl = new VerticalStackLayout { BackgroundColor = Color.FromRgb(0, 160, 255) };
		for (int i = 0; i < Tekstid.Count; i++)
		{
			Button nupp = new Button
			{
				Text = Tekstid[i],
				BackgroundColor = Color.FromRgb(1, 196, 255),
				TextColor = Color.FromRgb(1, 0, 0),
				BorderWidth = 10,
				ZIndex = i,
				FontFamily="Dis"

			};
			vsl.Add(nupp);
			nupp.Clicked += Lehte_avamine;
		}
		sv=new ScrollView { Content=vsl};
		Content = sv;
	}
	private void Lehte_avamine (object sender, EventArgs e)
	{
		Button btn = (Button)sender;
		Navigation.PushAsync(lehed[btn.ZIndex]);
	}
}