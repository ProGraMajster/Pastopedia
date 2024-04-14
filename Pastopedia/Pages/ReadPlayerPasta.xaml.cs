using Pastopedia.Data;

namespace Pastopedia.Pages;

public partial class ReadPlayerPasta : ContentPage
{
	public ReadPlayerPasta(Pasta pasta)
	{
		InitializeComponent();
		this.Title = pasta.Title;
		lblContent.Text = pasta.Content;
	}
}