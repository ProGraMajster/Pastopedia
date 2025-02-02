using Pastopedia.Data;

namespace Pastopedia.Pages;

public partial class ReadPlayerPasta : ContentPage
{
	public ReadPlayerPasta(Pasta pasta)
	{
		InitializeComponent();
		this.Title = pasta.Title;
		lblContent.Text = pasta.Content;
		stepper.Value = lblContent.FontSize;
        lblFS.Text = "Wielkoœæ tekstu: " + stepper.Value.ToString();
    }

    private void stepper_ValueChanged(object sender, ValueChangedEventArgs e)
    {
		try
		{
            lblContent.FontSize = stepper.Value;
            lblFS.Text = "Wielkoœæ tekstu: " + stepper.Value.ToString();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}