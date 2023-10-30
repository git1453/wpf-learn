using System;
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

namespace MoviePlayerControlLibrary
{
    /// <summary>
    /// MoviePlayerControl.xaml 的交互逻辑
    /// </summary>
    public partial class MoviePlayerControl : UserControl
    {
        /// <summary>
        /// 指定影片是否在播放
        /// </summary>
        private bool playing;

        /// <summary>
        /// 用于更新位置滑块的当前值
        /// </summary>
        private System.Windows.Threading.DispatcherTimer timer=new System.Windows.Threading.DispatcherTimer();
        public MoviePlayerControl()
        {
            InitializeComponent();

            //初始化计时器的 Tick 事件处理程序：
            timer.Tick += new System.EventHandler(timer_Tick);
        }

        private void MediaElement_OnMediaOpened(object sender, RoutedEventArgs e)
        {
            moviePlayer.Volume = (double) volumeSlider.Value;
            positionSlider.Maximum = moviePlayer.NaturalDuration.TimeSpan.TotalMilliseconds;

            timer.Interval = new TimeSpan(0,0,1);
            timer.Start();
        }

        private void MediaElement_OnMediaEnded(object sender, RoutedEventArgs e)
        {
            StopMovie();
            timer.Stop();
        }

        private void positionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)positionSlider.Value);
            moviePlayer.Position = ts;
        }

        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            moviePlayer.Volume = (double)volumeSlider.Value;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            moviePlayer.Position = moviePlayer.Position.Subtract(new TimeSpan(0, 0, 5));
            positionSlider.Value = moviePlayer.Position.TotalMilliseconds;
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            PlayMovie();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            StopMovie();
        }

        private void forwardButton_Click(object sender, RoutedEventArgs e)
        {
            moviePlayer.Position = moviePlayer.Position.Add(new TimeSpan(0, 0, 5));
            positionSlider.Value = moviePlayer.Position.TotalMilliseconds;
        }

        #region Utility Methods

        void timer_Tick(object sender, EventArgs e)
        {
            // The DispatcherTimer's Tick event handler runs
            // in the UI thread, so you can work with the UI 
            // without worrying about cross-thread issues.
            positionSlider.Value =
                moviePlayer.Position.TotalMilliseconds;
        }

        private void PlayMovie()
        {
            if (!playing)
            {
                // The Play method will begin the media if it is not currently active or 
                // resume media if it is paused. This has no effect if the media is
                // already running.
                moviePlayer.Play();
                playButton.Content = "暂停";
                playing = true;
            }
            else
            {
                moviePlayer.Pause();
                playButton.Content = "播放";
                playing = false;
            }
        }

        private void StopMovie()
        {
            // The Stop method stops and resets the media to be played from
            // the beginning.
            moviePlayer.Stop();
            moviePlayer.Position = TimeSpan.Zero;
            playButton.Content = "播放";
            playing = false;
        }

        #endregion

        public void PlayMovie(Uri movie)
        {
            moviePlayer.Source = movie;
            PlayMovie();
        }

        public void Close()
        {
            StopMovie();
            moviePlayer.Close();
        }
    }
}
