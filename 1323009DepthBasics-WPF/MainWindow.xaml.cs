//------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.DepthBasics
{
    using System.Threading;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Microsoft.Kinect;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor sensor;
        BitmapImage backImage;//背景
        BitmapImage invisibleBackImage;//透過背景
        //Vector4 accelerometer;//加速度センサー用
        BitmapImage[] growImages;//木の画像
        private bool set;//設置モード時のラベル表示用
        private bool angle;//角度
        /// <summary>
        /// Bitmap that will hold color information
        /// </summary>
        private WriteableBitmap colorBitmap;

        /// <summary>
        /// Intermediate storage for the depth data received from the camera
        /// </summary>
        private DepthImagePixel[] depthPixels;

        /// <summary>
        /// Intermediate storage for the depth data converted to color
        /// </summary>
        private byte[] colorPixels;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            backImage = new BitmapImage(new Uri("Images/back.png", UriKind.Relative));//背景
            invisibleBackImage = new BitmapImage(new Uri("Images/invisible.png", UriKind.Relative));//透明背景
            BackImage.Stretch = Stretch.Fill;
            growImages = new BitmapImage[9];
            for(int i=0;i<9;i++){
                growImages[i] = new BitmapImage(new Uri("Images/growImages/No_"+i+".png", UriKind.Relative));//木の画像
            }
            growImage.Stretch = Stretch.Fill;
            angle = true;
            set = false;

        }

        /// <summary>
        /// Execute startup tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            // Look through all sensors and start the first connected one.
            // This requires that a Kinect is connected at the time of app startup.
            // To make your app robust against plug/unplug, 
            // it is recommended to use KinectSensorChooser provided in Microsoft.Kinect.Toolkit (See components in Toolkit Browser).
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

            if (null != this.sensor)
            {
                // Turn on the depth stream to receive depth frames
                // Turn on the depth stream to receive depth frames
                this.sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                
                // Allocate space to put the depth pixels we'll receive
                this.depthPixels = new DepthImagePixel[this.sensor.DepthStream.FramePixelDataLength];

                // Allocate space to put the color pixels we'll create
                this.colorPixels = new byte[this.sensor.DepthStream.FramePixelDataLength * sizeof(int)];

                // This is the bitmap we'll display on-screen
                this.colorBitmap = new WriteableBitmap(this.sensor.DepthStream.FrameWidth, this.sensor.DepthStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);

                // Set the image we display to point to the bitmap where we'll put the image data
                this.Image.Source = this.colorBitmap;

                // Add an event handler to be called whenever there is new depth frame data
                this.sensor.DepthFrameReady += this.SensorDepthFrameReady;

                BackImage.Source = backImage;

                this.sensor.DepthStream.Range = DepthRange.Near;
                //this.sensor.ElevationAngle = 0;

                // Start the sensor!
                try
                {
                    this.sensor.Start();
                }
                catch (IOException)
                {
                    this.sensor = null;
                }
            }

            if (null == this.sensor)
            {
                this.statusBarText.Text = Properties.Resources.NoKinectReady;
            }
        }

        /// <summary>
        /// Execute shutdown tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != this.sensor)
            {
                this.sensor.Stop();
            }
        }

        /// <summary>
        /// Event handler for Kinect sensor's DepthFrameReady event
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void SensorDepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
            {
                if (depthFrame != null)
                {
                    
                    // Copy the pixel data from the image to a temporary array
                    depthFrame.CopyDepthImagePixelDataTo(this.depthPixels);

                    // Get the min and max reliable depth for the current frame
                    int minDepth = depthFrame.MinDepth;
                    int maxDepth = depthFrame.MaxDepth;

                    // Convert the depth to RGB
                    short min = 5000;
                    int colorPixelIndex = 0;
                    for (int i = 0; i < this.depthPixels.Length; ++i)
                    {
                        
                        // Get the depth for this pixel
                        short depth = depthPixels[i].Depth;
                        
                        // To convert to a byte, we're discarding the most-significant
                        // rather than least-significant bits.
                        // We're preserving detail, although the intensity will "wrap."
                        // Values outside the reliable depth range are mapped to 0 (black).

                        // Note: Using conditionals in this loop could degrade performance.
                        // Consider using a lookup table instead when writing production code.
                        // See the KinectDepthViewer class used by the KinectExplorer sample
                        // for a lookup table example.
                        byte intensity = (byte)(depth >= minDepth && depth <= maxDepth ? depth : 0);

                        if (i > 640 * 200&& i < 640 * 280 && i % 640 > 270 && i % 640 < 370)//高さの取得部分（範囲）
                        {
                            if (min > depthPixels[i].Depth && depthPixels[i].Depth != 0)
                            {
                                min = depthPixels[i].Depth;
                            }
                            //青く描画
                            // Write out blue byte
                            this.colorPixels[colorPixelIndex++] = 200;

                            // Write out green byte
                            this.colorPixels[colorPixelIndex++] = 10;

                            // Write out red byte                        
                            this.colorPixels[colorPixelIndex++] = 30;
                            

                        }
                        else
                        {
                            // Write out blue byte
                            this.colorPixels[colorPixelIndex++] = intensity;

                            // Write out green byte
                            this.colorPixels[colorPixelIndex++] = intensity;

                            // Write out red byte                        
                            this.colorPixels[colorPixelIndex++] = intensity;
                        }
                        // We're outputting BGR, the last byte in the 32 bits is unused so skip it
                        // If we were outputting BGRA, we would write alpha here.
                        ++colorPixelIndex;
                        
                    }

                    // Write the pixel data into our bitmap
                    this.colorBitmap.WritePixels(
                        new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight),
                        this.colorPixels,
                        this.colorBitmap.PixelWidth * sizeof(int),
                        0);

                    /*設置モードのときに高さを表示*/
                    if (set)
                    {
                        if (min != 5000)
                        {
                            label1.Content = "高さ：" + min + "mm";
                        }
                        else
                        {
                            label1.Content = "高さ：測定不能";
                        }
                    }
                    else
                    {
                        label1.Content = "";
                    }
                  
                    selectGrowImages(min);

                    //角度変更のボタンの文字変更
                    if (angle)
                    {
                        button3.Content = "角度変更：床置用にする";
                    }
                    else
                    {
                        button3.Content = "角度変更：横向きにする";
                    }
                    //モードの変更ボタンの文字変更
                    if (set)
                    {
                        button1.Content = "通常モードに変更";
                    }
                    else
                    {
                        button1.Content = "設置モードに変更";
                    }
                   
                }
            }
        }

        
        /// <summary>
        /// Handles the user clicking on the screenshot button
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void ButtonScreenshotClick(object sender, RoutedEventArgs e)
        {
            if (null == this.sensor)
            {
                this.statusBarText.Text = Properties.Resources.ConnectDeviceFirst;
                return;
            }

            // create a png bitmap encoder which knows how to save a .png file
            BitmapEncoder encoder = new PngBitmapEncoder();

            // create frame from the writable bitmap and add to encoder
            encoder.Frames.Add(BitmapFrame.Create(this.colorBitmap));

            string time = System.DateTime.Now.ToString("hh'-'mm'-'ss", CultureInfo.CurrentUICulture.DateTimeFormat);

            string myPhotos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            string path = Path.Combine(myPhotos, "KinectSnapshot-" + time + ".png");

            // write the new file to disk
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    encoder.Save(fs);
                }

                this.statusBarText.Text = string.Format(CultureInfo.InvariantCulture, "{0} {1}", Properties.Resources.ScreenshotWriteSuccess, path);
            }
            catch (IOException)
            {
                this.statusBarText.Text = string.Format(CultureInfo.InvariantCulture, "{0} {1}", Properties.Resources.ScreenshotWriteFailed, path);
            }
        }
        
        /// <summary>
        /// Handles the checking or unchecking of the near mode combo box
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void CheckBoxNearModeChanged(object sender, RoutedEventArgs e)
        {
            if (this.sensor != null)
            {
                // will not function on non-Kinect for Windows devices
                try
                {
                    if (this.checkBoxNearMode.IsChecked.GetValueOrDefault())
                    {
                        this.sensor.DepthStream.Range = DepthRange.Default;
                    }
                    else
                    {
                        this.sensor.DepthStream.Range = DepthRange.Near;
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        //モード変更
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (set)
            {
                BackImage.Source = backImage;//背景を表示
                set = false;
            }
            else
            {
                BackImage.Source = invisibleBackImage;//背景を透明にして、Kinectからの距離の画像変更
                set = true;
            }
        }

        //画像の表示　数字で表示する画像を変更
        private void showGrowImages(int num)
        {
            growImage.Source = growImages[num];
        }

        //枠の中の高さで木の高さ（画像）決定
        private void selectGrowImages(short a)
        {
            int num = (int)a;
            if (num < 500 || num > 2600)//表示なし
            {
                showGrowImages(0);
            }
            else if(num <570 ){
                showGrowImages(1);
            }
            else if (num > 670 && num < 740)
            {
                showGrowImages(2);
            }
            else if (num > 840 && num < 910)
            {
                showGrowImages(3);
            }
            else if (num > 1010 && num < 1080)
            {
                showGrowImages(4);
            }
            else if (num > 1180 && num < 1250)
            {
                showGrowImages(5);
            }
            else if (num > 1350 && num < 1420)
            {
                showGrowImages(6);
            }
            else if (num > 1520 && num < 1590)
            {
                showGrowImages(7);
            }
            else if (num > 1690 && num < 1760)
            {
                showGrowImages(8);
            }
        }

        //角度変更　b端を押すたびに変更
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            angle = !angle;
            
            if (angle)
            {
                sensor.ElevationAngle = 0;//水平に
                Thread.Sleep(1350);//Kinectが止まるまで待つ
            }
            else
            {
                sensor.ElevationAngle = -27;//Kinectの足部分が邪魔にならないように
                Thread.Sleep(1350);//Kinectが止まるまで待つ
            }

        }        
    }
}