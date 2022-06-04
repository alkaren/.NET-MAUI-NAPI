using System.Text.Json;

namespace NETMauiApp;

public partial class MainPage : ContentPage
{
	private const string url = "https://safe.merdekacoppergold.com:5652/api/ddr/list_master_rig";
	private HttpClient _Client = new HttpClient();
	private string EndpointEntry;

	int count = 0;
	public MainPage()
	{
		InitializeComponent();
	}

	public async Task Foo()
	{
		try
		{
			//Uri uri = new Uri(string.Format(url, string.Empty));
			EndpointEntry = entry.Text;
			if(EndpointEntry == null || EndpointEntry == "" || EndpointEntry == "https://")
            {
				CounterLabel.Text = "Please insert the endpoint first, peeps! 😁";
			}
            else
            {
				Uri uri = new Uri(string.Format(EndpointEntry, string.Empty));
				HttpResponseMessage response = await _Client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					string content = await response.Content.ReadAsStringAsync();
					var options = new JsonSerializerOptions()
					{
						WriteIndented = true
					};
					var jsonElement = JsonSerializer.Deserialize<JsonElement>(content);
					var x = JsonSerializer.Serialize(jsonElement, options);
					CounterLabel.Text = x;
				}
			}
		}
		catch (Exception ex)
		{
			var msg = ex.InnerException.Message;
			CounterLabel.Text = msg.ToString() + " 😥";
			SemanticScreenReader.Announce(CounterLabel.Text);
		}
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
        try
        {
			await Foo();
		}
        catch (Exception ex)
        {
			var msg = ex.InnerException.Message;
			CounterLabel.Text = msg.ToString() + " 😥";
			SemanticScreenReader.Announce(CounterLabel.Text);
		}
		SemanticScreenReader.Announce(CounterLabel.Text);
	}
}

