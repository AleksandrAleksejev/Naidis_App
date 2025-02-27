using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MauiApp1;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScorePage : ContentPage
    {
        Label label;
        public ScorePage()
        {
            label = new Label { FontSize = 20, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };

            StackLayout st = new StackLayout { Children = { label } };
            Content = st;

            st.BackgroundColor = Colors.PeachPuff;
        }
        protected override void OnAppearing()
        {
            if (Preferences.ContainsKey("Rist"))
            {
                label.Text += "\n" + Preferences.Get("Rist", "Pole andmed");
            }
            else if (Preferences.ContainsKey("Null"))
            {
                label.Text += "\n" + Preferences.Get("Null", "Pole andmed");
            }
            base.OnAppearing();
        }
    }
