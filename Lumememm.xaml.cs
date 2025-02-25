using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes; 
using Microsoft.Maui.Graphics;
using System;
using System.Threading.Tasks;

namespace MauiApp1;

    [XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Lumememm : ContentPage
{
    private readonly BoxView bucket ,leftArm, rightArm;
    private readonly Ellipse head, body, leftEye, rightEye;
    private readonly Polygon nose;
    private readonly Random random;

    public Lumememm()
    {
        random = new Random();
        AbsoluteLayout layout = new AbsoluteLayout();

        bucket = new BoxView { Color = Colors.Brown, WidthRequest = 50, HeightRequest = 30 };
        head = new Ellipse { Stroke = Colors.Black, StrokeThickness = 2, Fill = Colors.White, WidthRequest = 60, HeightRequest = 60 };
        body = new Ellipse { Stroke = Colors.Black, StrokeThickness = 2, Fill = Colors.White, WidthRequest = 80, HeightRequest = 80 };
        leftEye = new Ellipse { Stroke = Colors.Black, Fill = Colors.Black, WidthRequest = 5, HeightRequest = 5 };
        rightEye = new Ellipse { Stroke = Colors.Black, Fill = Colors.Black, WidthRequest = 5, HeightRequest = 5 };
        nose = new Polygon { Fill = Colors.Orange, Points = new PointCollection { new Point(0, 0), new Point(30, 15), new Point(15, 30) } };
        leftArm = new BoxView { Color = Colors.Brown, WidthRequest = 50, HeightRequest = 20 };
        rightArm = new BoxView { Color = Colors.Brown, WidthRequest = 50, HeightRequest = 20 };

        AbsoluteLayout.SetLayoutBounds(bucket, new Rect(75, 20, 50, 30));
        AbsoluteLayout.SetLayoutBounds(head, new Rect(70, 50, 60, 60));
        AbsoluteLayout.SetLayoutBounds(body, new Rect(60, 110, 80, 80));
        AbsoluteLayout.SetLayoutBounds(leftEye, new Rect(90, 70, 5, 5));
        AbsoluteLayout.SetLayoutBounds(rightEye, new Rect(106, 70, 5, 5));
        AbsoluteLayout.SetLayoutBounds(nose, new Rect(100, 80, 20, 10));
        AbsoluteLayout.SetLayoutBounds(leftArm, new Rect(30, 130, 30, 30));
        AbsoluteLayout.SetLayoutBounds(rightArm, new Rect(140, 130, 30, 30));

        layout.Children.Add(bucket);
        layout.Children.Add(head);
        layout.Children.Add(body);
        layout.Children.Add(leftEye);
        layout.Children.Add(rightEye);
        layout.Children.Add(nose);
        layout.Children.Add(leftArm);
        layout.Children.Add(rightArm);

        Button toggleButton = new Button { Text = "Peida lumememm" };
        toggleButton.Clicked += (s, e) =>
        {
            bool isVisible = !bucket.IsVisible;
            bucket.IsVisible = head.IsVisible = body.IsVisible = leftEye.IsVisible = rightEye.IsVisible = nose.IsVisible = leftArm.IsVisible = rightArm.IsVisible = isVisible;
            toggleButton.Text = isVisible ? "Peida lumememm" : "Näita lumememme";
        };
        Button randomColorButton = new Button { Text = "Random värvi" };
        randomColorButton.Clicked += (s, e) =>
        {
            head.Fill = new SolidColorBrush(Color.FromRgb(random.Next(256), random.Next(256), random.Next(256)));
            body.Fill = new SolidColorBrush(Color.FromRgb(random.Next(256), random.Next(256), random.Next(256)));
            bucket.Color = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));
            leftEye.Fill = new SolidColorBrush(Color.FromRgb(random.Next(256), random.Next(256), random.Next(256)));
            rightEye.Fill = new SolidColorBrush(Color.FromRgb(random.Next(256), random.Next(256), random.Next(256)));
            nose.Fill = new SolidColorBrush(Color.FromRgb(random.Next(256), random.Next(256), random.Next(256)));
            leftArm.Color = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));
            rightArm.Color = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));
        };

        Slider opacitySlider = new Slider { Minimum = 0, Maximum = 1, Value = 1 };
        opacitySlider.ValueChanged += (s, e) =>
        {
            head.Opacity = body.Opacity = e.NewValue;
        };

        Stepper sizeStepper = new Stepper { Minimum = 0.5, Maximum = 2, Increment = 0.1, Value = 1 };
        sizeStepper.ValueChanged += (s, e) =>
        {
            head.WidthRequest = 60 * e.NewValue;
            head.HeightRequest = 60 * e.NewValue;
            body.WidthRequest = 80 * e.NewValue;
            body.HeightRequest = 80 * e.NewValue;
        };

        StackLayout buttonStack = new StackLayout
        {
            Children = { toggleButton, randomColorButton, opacitySlider, sizeStepper },
            Orientation = StackOrientation.Vertical
        };

        Content = new StackLayout { Children = { layout, buttonStack } };
        Button randomColorButton1 = new Button { Text = "Random värvi" };
        randomColorButton.Clicked += async (s, e) =>
        {
            int r = random.Next(0, 255);
            int g = random.Next(0, 255);
            int b = random.Next(0, 255);

            bool vastus = await DisplayAlert("Värvi muutus",
                $"Kas tahad värvi muuta? Uue värvi väärtused:\nRed: {r}, Green: {g}, Blue: {b}",
                "Jah", "Ei");

            if (vastus)
            {
                head.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
                body.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
                bucket.Color = Color.FromRgb(r, g, b);
                leftEye.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
                rightEye.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
                nose.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
                leftArm.Color = Color.FromRgb(r, g, b);
                rightArm.Color = Color.FromRgb(r, g, b);
            }
            else
            {
                head.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                body.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                bucket.Color = Color.FromRgb(0, 0, 0);
                leftEye.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                rightEye.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                nose.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                leftArm.Color = Color.FromRgb(0, 0, 0);
                rightArm.Color = Color.FromRgb(0, 0, 0);
            }
        };

    }

}

