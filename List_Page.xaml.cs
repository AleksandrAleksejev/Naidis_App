using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MauiApp1
{
    public partial class List_Page : ContentPage
    {
        // Коллекция групп телефонов
        public ObservableCollection<Ruhm<string, Telefon>> TelefondRuhmades { get; set; }

        // Элементы управления
        Label lbl_list;
        ListView list;
        Button lisa, kustuta, muuda;

        public List_Page()
        {
            // Инициализация списка телефонов
            var telefond = new List<Telefon>
            {
                new Telefon { Nimetus = "Samsung Galaxy S22 Ultra", Tootja = "Samsung", Hind = 1349, Pilt = "samsung.png" },
                new Telefon { Nimetus = "Xiaomi Mi 11 Lite 5G NE", Tootja = "Xiaomi", Hind = 399, Pilt = "xiaomi.png" },
                new Telefon { Nimetus = "Xiaomi Mi 11 Lite 5G", Tootja = "Xiaomi", Hind = 339, Pilt = "xiaomi.png" },
                new Telefon { Nimetus = "iPhone 13 mini", Tootja = "Apple", Hind = 1179, Pilt = "iphone.png" },
                new Telefon { Nimetus = "iPhone 12", Tootja = "Apple", Hind = 1179, Pilt = "iphone12.png" }
            };

            // Группировка телефонов по производителю
            var ruhmad = telefond.GroupBy(p => p.Tootja)
                                 .Select(g => new Ruhm<string, Telefon>(g.Key, g));

            TelefondRuhmades = new ObservableCollection<Ruhm<string, Telefon>>(ruhmad);

            // Заголовок списка
            lbl_list = new Label
            {
                Text = "Telefonide loetelu",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };

            // Создание ListView с группировкой
            list = new ListView
            {
                SeparatorColor = Colors.Orange,
                Header = "Telefonid rühmades",
                Footer = DateTime.Now.ToString("T"),
                HasUnevenRows = true,
                ItemsSource = TelefondRuhmades,
                IsGroupingEnabled = true,
                GroupHeaderTemplate = new DataTemplate(() =>
                {
                    Label tootja = new Label { FontAttributes = FontAttributes.Bold, FontSize = 18 };
                    tootja.SetBinding(Label.TextProperty, "Nimetus");
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(10, 5),
                            BackgroundColor = Colors.LightGray,
                            Children = { tootja }
                        }
                    };
                }),
                ItemTemplate = new DataTemplate(() =>
                {
                    Label nimetus = new Label { FontSize = 16 };
                    nimetus.SetBinding(Label.TextProperty, "Nimetus");

                    Label hind = new Label { FontSize = 14, TextColor = Colors.Gray };
                    hind.SetBinding(Label.TextProperty, new Binding("Hind", stringFormat: "Hind: {0}€"));

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(10, 5),
                            Children = { nimetus, hind }
                        }
                    };
                })
            };

            // Обработчик нажатия на элемент списка
            list.ItemTapped += List_ItemTapped;

            // Кнопки для добавления, удаления и редактирования
            lisa = new Button { Text = "Lisa telefon" };
            lisa.Clicked += Lisa_Clicked;

            kustuta = new Button { Text = "Kustuta telefon" };
            kustuta.Clicked += Kustuta_Clicked;

            muuda = new Button { Text = "Muuda telefon" };
            muuda.Clicked += Muuda_Clicked;

            // Компоновка страницы
            Content = new StackLayout
            {
                Children = { lbl_list, list, lisa, kustuta, muuda }
            };
        }

        // Обработчик нажатия на элемент списка
        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Telefon selectedPhone = e.Item as Telefon;
            if (selectedPhone != null)
            {
                await DisplayAlert("Valitud mudel", $"{selectedPhone.Tootja} - {selectedPhone.Nimetus}", "OK");
            }
        }

        // Обработчик кнопки добавления
        private async void Lisa_Clicked(object sender, EventArgs e)
        {
            string nimetus = await DisplayPromptAsync("Lisa telefon", "Sisesta telefoni nimi:");
            string tootja = await DisplayPromptAsync("Lisa telefon", "Sisesta tootja:");
            string hind = await DisplayPromptAsync("Lisa telefon", "Sisesta hind:");

            if (int.TryParse(hind, out int hindValue))
            {
                // Находим группу для нового телефона
                var ruhm = TelefondRuhmades.FirstOrDefault(r => r.Nimetus == tootja);
                if (ruhm == null)
                {
                    // Если группы нет, создаем новую
                    ruhm = new Ruhm<string, Telefon>(tootja, new List<Telefon>());
                    TelefondRuhmades.Add(ruhm);
                }

                // Добавляем телефон в группу
                ruhm.Add(new Telefon { Nimetus = nimetus, Tootja = tootja, Hind = hindValue, Pilt = "default.png" });
            }
            else
            {
                await DisplayAlert("Viga", "Vigane hind", "OK");
            }
        }

        // Обработчик кнопки удаления
        private void Kustuta_Clicked(object sender, EventArgs e)
        {
            Telefon phone = list.SelectedItem as Telefon;
            if (phone != null)
            {
                // Находим группу, в которой находится телефон
                var ruhm = TelefondRuhmades.FirstOrDefault(r => r.Contains(phone));
                if (ruhm != null)
                {
                    ruhm.Remove(phone);

                    // Если группа пуста, удаляем ее
                    if (ruhm.Count == 0)
                    {
                        TelefondRuhmades.Remove(ruhm);
                    }
                }

                list.SelectedItem = null;
            }
        }

        // Обработчик кнопки редактирования
        private async void Muuda_Clicked(object sender, EventArgs e)
        {
            Telefon phone = list.SelectedItem as Telefon;
            if (phone != null)
            {
                string nimetus = await DisplayPromptAsync("Muuda telefon", "Sisesta uus telefoni nimi:", initialValue: phone.Nimetus);
                string tootja = await DisplayPromptAsync("Muuda telefon", "Sisesta uus tootja:", initialValue: phone.Tootja);
                string hind = await DisplayPromptAsync("Muuda telefon", "Sisesta uus hind:", initialValue: phone.Hind.ToString());

                if (int.TryParse(hind, out int hindValue))
                {
                    // Обновляем данные телефона
                    phone.Nimetus = nimetus;
                    phone.Tootja = tootja;
                    phone.Hind = hindValue;

                    // Если изменился производитель, перемещаем телефон в другую группу
                    var oldRuhm = TelefondRuhmades.FirstOrDefault(r => r.Contains(phone));
                    if (oldRuhm != null && oldRuhm.Nimetus != tootja)
                    {
                        oldRuhm.Remove(phone);

                        var newRuhm = TelefondRuhmades.FirstOrDefault(r => r.Nimetus == tootja);
                        if (newRuhm == null)
                        {
                            newRuhm = new Ruhm<string, Telefon>(tootja, new List<Telefon>());
                            TelefondRuhmades.Add(newRuhm);
                        }

                        newRuhm.Add(phone);
                    }
                }
                else
                {
                    await DisplayAlert("Viga", "Vigane hind", "OK");
                }
            }
        }
    }
}