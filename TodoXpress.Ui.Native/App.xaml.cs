using Microsoft.AspNetCore.Components.WebView.Maui;
using TodoXpress.Ui.Components;

namespace TodoXpress.Ui.Native;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//MainPage = new MainPage();

		var webView = new BlazorWebView
		{
			HostPage = "wwwroot/index.html"
		};

		webView.RootComponents
		.Add(new RootComponent
		{
			Selector = "#app",
			ComponentType = typeof(Main),
		});

		MainPage = new ContentPage()
		{
			Content = webView
        };
	}
}

