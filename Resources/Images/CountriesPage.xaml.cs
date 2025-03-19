using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MauiApp1
{
    public partial class CountriesPage : ContentPage
    {
        // Коллекция стран
        public ObservableCollection<Country> Countries { get; set; }

        // Элементы управления
        ListView listView;
        Button addButton, deleteButton, editButton;

        public CountriesPage()
        {
            // Инициализация списка стран
            Countries = new ObservableCollection<Country>
            {
                new Country { Name = "Estonia", Capital = "Tallinn", Population = 1328000, Flag = "estonia.png" },
                new Country { Name = "Latvia", Capital = "Riga", Population = 1907000, Flag = "latvia.png" },
                new Country { Name = "Lithuania", Capital = "Vilnius", Population = 2794000, Flag = "lithuania.png" },
                new Country { Name = "USA", Capital = "Washington", Population = 347275807, Flag = "usa.png" },
                new Country { Name = "Russia", Capital = "Moscow", Population = 143997393, Flag = "rus.png" }
            };

            // Заголовок страницы
            var titleLabel = new Label
            {
                Text = "Euroopa riigid",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };

            // Создание ListView
            listView = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = Countries,
                ItemTemplate = new DataTemplate(() =>
                {
                    // Создание ячейки с флагом, названием и столицей
                    ImageCell imageCell = new ImageCell {TextColor = Colors.Black, DetailColor = Colors.Gray };
                    imageCell.SetBinding(ImageCell.TextProperty, "Name");
                    imageCell.SetBinding(ImageCell.DetailProperty, "Capital");
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "Flag");
                    return imageCell;
                })
            };

            // Обработчик нажатия на элемент списка
            listView.ItemTapped += ListView_ItemTapped;

            // Кнопки для добавления, удаления и редактирования
            addButton = new Button { Text = "Lisa riik" };
            addButton.Clicked += AddButton_Clicked;

            deleteButton = new Button { Text = "Kustuta riik" };
            deleteButton.Clicked += DeleteButton_Clicked;

            editButton = new Button { Text = "Muuda riiki" };
            editButton.Clicked += EditButton_Clicked;

            // Компоновка страницы
            Content = new StackLayout
            {
                Children = { titleLabel, listView, addButton, deleteButton, editButton }
            };
        }

        // Обработчик нажатия на элемент списка
        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Country selectedCountry = e.Item as Country;
            if (selectedCountry != null)
            {
                // Переход на страницу с деталями страны
                await Navigation.PushAsync(new CountryDetailsPage(selectedCountry));
            }
        }

        // Обработчик кнопки добавления
        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            string name = await DisplayPromptAsync("Lisa riik", "Sisesta riigi nimi:");
            if (string.IsNullOrEmpty(name)) return;

            // Проверка, существует ли страна в списке
            if (Countries.Any(c => c.Name == name))
            {
                await DisplayAlert("Viga", "See riik on juba olemas!", "OK");
                return;
            }

            string capital = await DisplayPromptAsync("Lisa riik", "Sisesta pealinn:");
            string population = await DisplayPromptAsync("Lisa riik", "Sisesta rahvaarv:");

            if (int.TryParse(population, out int populationValue))
            {
                // Выбор флага из галереи
                string flagPath = await PickFlagImageAsync();
                if (flagPath == null) return; // Если пользователь отменил выбор

                Countries.Add(new Country { Name = name, Capital = capital, Population = populationValue, Flag = flagPath });
            }
            else
            {
                await DisplayAlert("Viga", "Vigane rahvaarv!", "OK");
            }
        }

        // Обработчик кнопки удаления
        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            Country country = listView.SelectedItem as Country;
            if (country != null)
            {
                Countries.Remove(country);
                listView.SelectedItem = null;
            }
        }

        // Обработчик кнопки редактирования
        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            Country country = listView.SelectedItem as Country;
            if (country != null)
            {
                string name = await DisplayPromptAsync("Muuda riiki", "Sisesta uus riigi nimi:", initialValue: country.Name);
                string capital = await DisplayPromptAsync("Muuda riiki", "Sisesta uus pealinn:", initialValue: country.Capital);
                string population = await DisplayPromptAsync("Muuda riiki", "Sisesta uus rahvaarv:", initialValue: country.Population.ToString());

                if (int.TryParse(population, out int populationValue))
                {
                    // Выбор нового флага из галереи
                    string flagPath = await PickFlagImageAsync();
                    if (flagPath != null) // Если пользователь выбрал новый флаг
                    {
                        country.Flag = flagPath;
                    }

                    country.Name = name;
                    country.Capital = capital;
                    country.Population = populationValue;
                }
                else
                {
                    await DisplayAlert("Viga", "Vigane rahvaarv!", "OK");
                }
            }
        }

        // Метод для выбора изображения флага из галереи
        private async Task<string> PickFlagImageAsync()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync();
                if (result != null)
                {
                    // Сохраняем изображение в локальное хранилище
                    string localFilePath = Path.Combine(FileSystem.AppDataDirectory, $"{Guid.NewGuid()}.png");
                    using (var stream = await result.OpenReadAsync())
                    using (var fileStream = File.Create(localFilePath))
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                    return localFilePath;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Pildi valimine ebaõnnestus: {ex.Message}", "OK");
            }

            return null;
        }
    }
}