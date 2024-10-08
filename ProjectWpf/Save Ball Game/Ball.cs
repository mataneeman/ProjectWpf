﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace ProjectWpf.Save_Ball_Game
{
    public class Ball
    {
        public enum BallType
        {
            Regular,
            Bomb,
            Virus,
            Coin,
            live
        }

        private Rectangle rectangle;
        private BallType ballType;
        private static readonly Random rand = new Random();

        public Rectangle Rectangle => rectangle;

        public Ball(double canvasWidth, BallType type)
        {
            ballType = type;
            double ballSize = rand.Next(20, 41);
            double ballX = rand.Next(0, (int)(canvasWidth - ballSize));
            string ballImageUri = type switch
            {
                BallType.Bomb => "pack://application:,,,/Save Ball Game/Images/bomb.png",
                BallType.Virus => "pack://application:,,,/Save Ball Game/Images/virus.png",
                BallType.Coin => "pack://application:,,,/Save Ball Game/Images/dollar.png",
                BallType.live => "pack://application:,,,/Save Ball Game/Images/favorite.png",
                _ => $"pack://application:,,,/Save Ball Game/Images/ball{rand.Next(1, 12)}.png"
            };


            rectangle = new Rectangle
            {
                Width = ballSize,
                Height = ballSize,
                Fill = new ImageBrush(new BitmapImage(new Uri(ballImageUri))),
                RadiusX = 50,
                RadiusY = 50
            };

            Canvas.SetLeft(rectangle, ballX);
            Canvas.SetTop(rectangle, -ballSize);
        }

        public void MoveDown(double speed)
        {
            Canvas.SetTop(rectangle, Canvas.GetTop(rectangle) + speed);
        }


        public bool IsOutOfBounds(Canvas canvas)
        {
            return Canvas.GetTop(rectangle) > canvas.ActualHeight;
        }

        public void AddToCanvas(Canvas canvas)
        {
            canvas.Children.Add(rectangle);
        }


        public void Hide()
        {
            // Animate the ball before hiding
            var fadeOutAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.5));
            fadeOutAnimation.Completed += (s, e) => rectangle.Visibility = Visibility.Collapsed;
            rectangle.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
        }

        public void Show()
        {
            rectangle.Visibility = Visibility.Visible;
            rectangle.Opacity = 1; // Ensure full opacity when showing
        }


        public BallType GetBallType()
        {
            return ballType;
        }
    }
}
