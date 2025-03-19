using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class CountryDetailsPage : ContentPage
    {
        public CountryDetailsPage(Country country)
        {
            // Заголовок страницы
            Title = country.Name;

            // Изображение флага
            var flagImage = new Image
            {
                Source = country.Flag,
                HeightRequest = 100,
                WidthRequest = 150,
                HorizontalOptions = LayoutOptions.Center
            };

            // Информация о стране
            var nameLabel = new Label { Text = $"Riik: {country.Name}", FontSize = 20 };
            var capitalLabel = new Label { Text = $"Pealinn: {country.Capital}", FontSize = 16 };
            var populationLabel = new Label { Text = $"Rahvaarv: {country.Population}", FontSize = 16 };

            // Компоновка страницы
            Content = new StackLayout
            {
                Padding = new Thickness(20),
                Children = { flagImage, nameLabel, capitalLabel, populationLabel }
            };
        }
    }
}