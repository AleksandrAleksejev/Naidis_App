using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiApp1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerImagePage : ContentPage
    {
        private Grid gr4x1, gr3x3;
        private Picker picker;
        private Image img;
        private Switch s_pilt, s_grid;
        private Random rnd = new Random();

        public PickerImagePage()
        {
            gr4x1 = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };

            picker = new Picker
            {
                Title = "Pildid",
                HorizontalOptions = LayoutOptions.Center
            };
            picker.Items.Add("1. Pilt");
            picker.Items.Add("2. Pilt");
            picker.Items.Add("3. Pilt");
            picker.Items.Add("Enda valitud foto");
            picker.SelectedIndexChanged += Piltide_Valik;

            img = new Image
            {
                Source = "dotnet_bot.png",
                HorizontalOptions = LayoutOptions.Center
            };

            s_pilt = new Switch
            {
                IsToggled = true,
                HorizontalOptions = LayoutOptions.Center
            };
            s_pilt.Toggled += Kuva_Peida_pilt;

            s_grid = new Switch
            {
                IsToggled = false,
                HorizontalOptions = LayoutOptions.Center
            };
            s_grid.Toggled += Kuva_Peida_grid;

            gr4x1.Children.Add(picker);
            Grid.SetRow(picker, 0);
            Grid.SetColumn(picker, 0);
            Grid.SetColumnSpan(picker, 2);

            gr4x1.Children.Add(img);
            Grid.SetRow(img, 1);
            Grid.SetColumn(img, 0);
            Grid.SetColumnSpan(img, 2);

            gr4x1.Children.Add(s_pilt);
            Grid.SetRow(s_pilt, 3);
            Grid.SetColumn(s_pilt, 0);

            gr4x1.Children.Add(s_grid);
            Grid.SetRow(s_grid, 3);
            Grid.SetColumn(s_grid, 1);

            Content = gr4x1;
        }

        private void Kuva_Peida_grid(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                gr3x3 = new Grid();
                for (int i = 0; i < 3; i++)
                {
                    gr3x3.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    gr3x3.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var frame = new Frame
                        {
                            BackgroundColor = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255))
                        };
                        gr3x3.Children.Add(frame);
                        Grid.SetRow(frame, i);
                        Grid.SetColumn(frame, j);
                    }
                }
                gr4x1.Children.Add(gr3x3);
                Grid.SetRow(gr3x3, 2);
                Grid.SetColumnSpan(gr3x3, 2);
            }
            else
            {
                gr4x1.Children.Remove(gr3x3);
            }
        }

        private void Kuva_Peida_pilt(object sender, ToggledEventArgs e)
        {
            img.IsVisible = e.Value;
        }

        private async void Piltide_Valik(object sender, EventArgs e)
        {
            if (picker.SelectedIndex == 3)
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images
                });
                if (result != null)
                {
                    img.Source = ImageSource.FromFile(result.FullPath);
                }
            }
            else if (picker.SelectedIndex == 2)
            {
                img.Source = "road.jpg";
            }
            else if (picker.SelectedIndex == 1)
            {
                img.Source = "coconut.jpg";
            }
            else if (picker.SelectedIndex == 0)
            {
                img.Source = "dotnet_bot.png";
            }
        }
    }
}
