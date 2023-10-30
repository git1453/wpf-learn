﻿using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wfs = System.Windows.Forms;
namespace VideoBrower
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

        // The list box on the form is 
        // bound to this variable. 

        public ThumbnailList Thumbnails { get; set; } = new ThumbnailList();

        private void videoListBox_SelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            moviePlayer.Close();

            // Get the image name:
            String imageName =
                videoListBox.SelectedItem.ToString();

            // Find the associated movie:
            string movieName = System.IO.Path.
                ChangeExtension(imageName, "wmv");

            // Create a new URI for the selected movie, and play it:
            moviePlayer.PlayMovie(new Uri(movieName));
        }

        private void selectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowser = new wfs.FolderBrowserDialog();
            folderBrowser.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowser.ShowDialog() == wfs.DialogResult.OK)
            {
                Thumbnails.FolderName = folderBrowser.SelectedPath;
            }
        }
    }
}