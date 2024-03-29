﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SinglePlayerClick(object sender, RoutedEventArgs e)
        {
            SinglePlayer singlePlayer = new SinglePlayer();
            singlePlayer.ShowDialog();
        }

        private void MultiPlayerClick(object sender, RoutedEventArgs e)
        {
            MultiPlayer multiPlayer = new MultiPlayer();
            multiPlayer.ShowDialog();
        }


    }
}
