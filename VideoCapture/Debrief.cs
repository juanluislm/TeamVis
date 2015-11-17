using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using System.Threading;
using System.Diagnostics;

namespace VideoCapture
{
    public partial class Debrief : Form
    {
        #region Variables
        //file IO variable
        OpenFileDialog OF = new OpenFileDialog();
        SaveFileDialog SF = new SaveFileDialog();

        //current video mode and state
        bool playstate = false;
        bool recordstate = false;

        VideoMethod CurrentState = VideoMethod.Viewing; //default state
        public enum VideoMethod
        {
            Viewing,
            Recording
        };

        //Capture Setting and variables
        Capture _Capture;

        double FrameRate = 0;
        double TotalFrames = 0;

        VideoWriter VW;
        Stopwatch SW;

        int Frame_width;
        int Frame_Height;
        int FrameCount;


        #endregion
        #region Optical Flow
        public Image<Gray, Byte> nextGrayFrame { get; set; }
        public Image<Gray, Byte> nextTemplate { get; set; }
        public Image<Gray, Byte> nextFrame { get; set; }
        public PointF[][] ActualFeature;//
        public PointF[] NextFeature;//
        public Byte[] Status;//
        public float[] TrackError;
        public Rectangle trackingArea;
        public PointF[] hull, nextHull;
        public PointF referenceCentroid, nextCentroid;
        #endregion
        #region Template matching variables
        //Images
        Image<Gray, Byte> Template;
        Image<Bgr, Byte> Frame;
        Image<Gray, Byte> GrayFrame;

        //Classes
        FFT _FFT = new FFT();
        //Capture _capture;

        //Drawing
        int Template_Width = 150;//100,60
        int Template_Hieght = 100;

        Rectangle Image_ROI_Location;
        Rectangle Template_Location;
        
        //Object Change
        int NO_Match = 0;
        bool Start = false;

        #endregion
        public Debrief()
        {
            InitializeComponent();           
        }

        #region ProcessFrame of recording
        /// Processes Each frame according to CurrentState Viewing/Recording Video
        private void ProcessFrame(object sender, EventArgs arg)/////////////////////////////////////////////////////////////
        {//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (CurrentState == VideoMethod.Viewing)
            {
                try
                {
                    //Show image
                    Frame = _Capture.RetrieveBgrFrame().Copy();
                    GrayFrame = Frame.Convert<Gray, Byte>();
                    DisplayImage1();
                    //DisplayImage(_Capture.RetrieveBgrFrame().ToBitmap());

                    //Show time stamp
                    double time_index = _Capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_POS_MSEC);
                    UpdateTextBox("Time: " + TimeSpan.FromMilliseconds(time_index).ToString(), Time_Label);

                    //show frame number
                    double framenumber = _Capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_POS_FRAMES);
                    UpdateTextBox("Frame: " + framenumber.ToString(), Frame_lbl);

                    //update trackbar
                    UpdateVideo_CNTRL(framenumber);

                    /*Note: We can increase or decrease this delay to fastforward of slow down the display rate
                     if we want a re-wind function we would have to use _Capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_POS_FRAMES, FrameNumber*);
                    //and call the process frame to update the picturebox ProcessFrame(null, null);. This is more complicated.*/

                    //Wait to display correct framerate
                    Thread.Sleep((int)(1000.0 / FrameRate)); //This may result in fast playback if the codec does not tell the truth
                    //Frame = //DisplayImage(_Capture.RetrieveBgrFrame().ToBitmap());                     
                    try
                    {
                        if (Start)
                        {
                            //Apply template to ROI 
                            if (checkrange(Image_ROI_Location.X, Image_ROI_Location.Y))
                            {
                                if (_FFT.TrackObject(GrayFrame.Copy(Image_ROI_Location), Template))
                                {
                                    //Apply Ajustmet and display match location
                                    Account_Movement();
                                    //DoOptical();//////////////////
                                    DisplayImage1();
                                    #region optical2
                                    //Frame = nextFrame;
                                    #endregion
                                }
                            }

                                //if not found apply it to the whole image
                            else if (_FFT.TrackObject(GrayFrame.Copy(), Template))
                            {
                                //Set New Location of Template and Template ROI
                                Template_Location = new Rectangle(_FFT.GetLocation().X, _FFT.GetLocation().Y, Template_Width, Template_Hieght);
                                Image_ROI_Location = new Rectangle(_FFT.GetLocation().X - Template_Width / 2, _FFT.GetLocation().Y - Template_Hieght / 2, Template_Width * 2, Template_Hieght * 2);
                                DisplayImage1();
                            }
                            else
                            {
                                //Track to see if object change
                                int buffer = 50;
                                if (Template_Location.X > buffer && Template_Location.X < Frame.Width - buffer &&
                                    Template_Location.Y > buffer && Template_Location.Y < Frame.Height - buffer)
                                {
                                    NO_Match++;
                                    if (NO_Match > 20)
                                    {
                                        NO_Match = 0;
                                        //Create new template from last know location
                                        SetTemplate(Template_Location.X, Template_Location.Y);
                                        DisplayImage1();
                                    }
                                }
                                //Else just display Image
                                //watch out
                                else DisplayImage1();//pictureBox2.Image = Frame.ToBitmap(); //DisplayImage1();//pictureBox1.Image = Frame.ToBitmap();
                            }


                        }
                        else DisplayImage1();//pictureBox1.Image = Frame.ToBitmap();//DisplayImage(_Capture.RetrieveBgrFrame().ToBitmap());
                        ////DisplayImage(_Capture.RetrieveBgrFrame().ToBitmap());////DisplayImage(_Capture.RetrieveBgrFrame().ToBitmap());//Video_Image.Image = Frame.ToBitmap();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    //Lets check to see if we have reached the end of the video
                    //If we have lets stop the capture and video as in pause button was pressed
                    //and reset the video back to start
                    if (framenumber == TotalFrames)
                    {
                        //pause button update
                        play_pause_BTN_MouseUp(null, null);

                        framenumber = 0;
                        UpdateVideo_CNTRL(framenumber);
                        _Capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_POS_FRAMES, framenumber);
                        //call the process frame to update the picturebox
                        
                        ProcessFrame(null, null);
                    }
                }
                catch
                {
                }
            }
            
        }
        #endregion
        /// <summary>
        /// Play/Pause video or Record/Stop video from default video device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void play_pause_BTN_MouseUp(object sender, MouseEventArgs e)
        {
            //Check to ensure a video file is selected or video capture device is available
            if (_Capture != null)
            {
                if (CurrentState == VideoMethod.Viewing)
                {
                    playstate = !playstate; //change playstate to the opposite
                    /*Update Play panel image*/
                    if (playstate)
                    {
                        play_pause_BTN.BackgroundImage = VideoCapture.Properties.Resources.Pause;
                        UpdateVideo_CNTRL(false); //disable this as it's not safe when running 
                        //this may work in legacy call method and be cause by a cross threading issue
                        _Capture.Start();
                    }
                    else
                    {
                        play_pause_BTN.BackgroundImage = VideoCapture.Properties.Resources.Play;
                        _Capture.Pause();
                        UpdateVideo_CNTRL(true);
                    }
                }
                else if (CurrentState == VideoMethod.Recording)
                {
                    recordstate = !recordstate; //change playstate to the opposite
                    /*Update Play panel image*/
                    if (recordstate)
                    {
                        //Set up image/varibales
                        play_pause_BTN.BackgroundImage = VideoCapture.Properties.Resources.Stop;
                        FrameCount = 0;
                        SW = new Stopwatch();
                        //check to see if we have disposed of the video before
                        if (VW.Ptr == IntPtr.Zero)
                        {
                            //explain to the user what's happening
                            MessageBox.Show("VideoWriter has been finilised, please re-initalise a video file");
                            //lets re-call the recordVideoToolStripMenuItem_Click to save on programing
                           // recordVideoToolStripMenuItem_Click_1(null, null);
                        }
                        SW.Start();
                    }
                    else
                    {
                        //Stop recording and dispose of the VideoWriter this will finialise the video
                        //Some codecs don't dispose correctley use uncompressed to stop this error
                        //VLC video player will play videos where the index has been corrupted. http://www.videolan.org/vlc/index.html
                        VW.Dispose();

                        //set image/variable
                        play_pause_BTN.BackgroundImage = VideoCapture.Properties.Resources.Record;
                        SW.Stop();
                    }
                }
            }
        }

        /// <summary>
        /// Changes the current frame from what the video will start from or look at 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Video_CNTRL_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (_Capture != null)
            {

                //we don't use this when running since it has an unstable call and wil cause a crash
                if (_Capture.GrabProcessState == System.Threading.ThreadState.Running)
                {
                    _Capture.Pause();
                    while (_Capture.GrabProcessState == System.Threading.ThreadState.Running) ;//do nothing wait for stop
                    _Capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_POS_FRAMES, Video_CNTRL.Value);
                    _Capture.Start();
                }
                else
                {
                    _Capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_POS_FRAMES, Video_CNTRL.Value);
                    //call the process frame to update the picturebox
                    ProcessFrame(null, null);
                }

            }

        }
        //private delegate void DisplayImageDelegate(Bitmap Image);
        //private void DisplayImage(Bitmap Image)
        //{
        //    if (pictureBox1.InvokeRequired)
        //    {
        //        try
        //        {
        //            DisplayImageDelegate DI = new DisplayImageDelegate(DisplayImage);
        //            this.BeginInvoke(DI, new object[] { Image });
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //    }
        //    else
        //    {
        //        pictureBox1.Image = Image;
        //    }
        //}
        /// <summary>
        /// Thread safe method to update any of the Label controls on the form
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Control"></param>
        private delegate void UpdateTextBoxDelegate(String Text, Label Control);
        private void UpdateTextBox(String Text, Label Control)
        {
            if (Control.InvokeRequired)
            {
                try
                {
                    UpdateTextBoxDelegate UT = new UpdateTextBoxDelegate(UpdateTextBox);
                    this.BeginInvoke(UT, new object[] { Text, Control });
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                Control.Text = Text;
                this.Refresh();
            }
        }
        /// <summary>
        /// Thread safe method to update the value of the Video_CNTRL trackbar
        /// </summary>
        /// <param name="Value"></param>
        private delegate void UpdateVideo_CNTRLDelegate(double Value);
        private void UpdateVideo_CNTRL(double Value)
        {
            if (Video_CNTRL.InvokeRequired)
            {
                try
                {
                    UpdateVideo_CNTRLDelegate UVC = new UpdateVideo_CNTRLDelegate(UpdateVideo_CNTRL);
                    this.BeginInvoke(UVC, new object[] { Value });
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                //Do a quick in range check as sometime the codec may not tell the truth
                if(Value<Video_CNTRL.Maximum) Video_CNTRL.Value = (int)Value;
            }
        }
        /// <summary>
        /// Threadsafe method toe Enable/Disable the Video_CNTRL trackbar
        /// </summary>
        /// <param name="State"></param>
        private delegate void EnableVideo_CNTRLDelegate(bool State);
        private void UpdateVideo_CNTRL(bool State)
        {
            if (Video_CNTRL.InvokeRequired)
            {
                try
                {
                    EnableVideo_CNTRLDelegate UVC = new EnableVideo_CNTRLDelegate(UpdateVideo_CNTRL);
                    this.BeginInvoke(UVC, new object[] { State });
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                //Do a quick in range check as sometime the codec may not tell the truth
                Video_CNTRL.Enabled = State;
            }
        }       
        /// <summary>
        /// Ensure that the Camera Setting are reset if the form is just clossed and the camera is released
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (_Capture != null)
            {
                if (_Capture.GrabProcessState == System.Threading.ThreadState.Running) _Capture.Stop();
                _Capture.Dispose();
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        #region Template matching

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (_Capture != null)
            {
                //Set template Location
                //Template_Location = new Rectangle(e.X, e.Y, 100, 100);
                //Template = GrayFrame.Copy(Template_Location);

                //Set ROI Location
                //Image_ROI_Location = new Rectangle(e.X - 50, e.Y - 50, 200, 200);
                
                //Set Template
                SetTemplate(e.X, e.Y);
                #region optical
                ////float scalingAreaFactor = 0.6f;
                //int trackingAreaWidth = 60;
                //int trackingAreaHeight = 36;
                //int leftXTrackingCoord = Template_Location.X - (int)(((Template_Location.X + trackingAreaWidth) - (Template_Location.X + Template_Location.Width)) / 2);
                //int leftYTrackingCoord = Template_Location.Y - (int)(((Template_Location.Y + trackingAreaHeight) - (Template_Location.Y + Template_Location.Height)) / 2);

                //ActualFeature = GrayFrame.GoodFeaturesToTrack(400, 0.5d, 5d, 5);
                //GrayFrame.FindCornerSubPix(ActualFeature, new Size(5, 5), new Size(-1, -1), new MCvTermCriteria(25, 1.5d));

                //for (int i = 0; i < ActualFeature[0].Length; i++)
                //{
                //    ActualFeature[0][i].X += trackingArea.X;
                //    ActualFeature[0][i].Y += trackingArea.Y;
                //}
                //using (MemStorage storage = new MemStorage())
                //    hull = PointCollection.ConvexHull(ActualFeature[0], storage, Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE).ToArray();

                //referenceCentroid = FindCentroid(hull);
                #endregion
                //Show Match
                DisplayImage1();

            }
        }
        public void DisplayImage1()
        {
            //Draw 2 Rectangle showing template location and ROIv
            //Frame.Draw(Image_ROI_Location, new Bgr(0, 0, 255), 2);
            Frame.Draw(Template_Location, new Bgr(0, 255, 0), 2);
            pictureBox1.Image = Frame.ToBitmap();
            //pictureBox2.Image = Frame.ToBitmap();
        }

        public void Account_Movement()
        {
            Image_ROI_Location.X += _FFT.GetLocation().X - (Template_Width / 2);
            Image_ROI_Location.Y += _FFT.GetLocation().Y - (Template_Hieght / 2);
            Template_Location.X += _FFT.GetLocation().X - (Template_Width / 2);
            Template_Location.Y += _FFT.GetLocation().Y - (Template_Hieght / 2);
        }

        public void SetTemplate(int X, int Y)
        {
            if (checkrange(X, Y))
            {
                //Set template Location
                Template_Location = new Rectangle(X, Y, Template_Width, Template_Hieght);
                //check to make sure within Image Bounds
                Template = GrayFrame.Copy(Template_Location);

                //Set ROI Location// look at this place
                Image_ROI_Location = new Rectangle(X - Template_Width / 2, Y - Template_Hieght / 2, Template_Width * 2, Template_Hieght * 2);

                //Start Matching
                Start = true;
            }
        }

        public bool checkrange(int X, int Y)
        {
            if (X - Template_Width / 2 < 0) return false;
            if (Y - Template_Hieght / 2 < 0) return false;
            if (X + (Template_Width * 2) > Frame.Width) return false;
            if (Y + (Template_Hieght * 2) > Frame.Height) return false;
            return true;
        }
        #endregion
        #region opticalFlow
        private void ComputeMotionFromSparseOpticalFlow()
        {
            float xCentroidsDifference = referenceCentroid.X - nextCentroid.X;
            float yCentroidsDifference = referenceCentroid.Y - nextCentroid.Y;

            float threshold = trackingArea.Width / 5;
            //label4.Text = "center";
            //if (Math.Abs(xCentroidsDifference) > Math.Abs(yCentroidsDifference))
            //{
            //    if (xCentroidsDifference > threshold)
            //        label4.Text = "right";
            //    if (xCentroidsDifference < -threshold)
            //        label4.Text = "left";
            //}
            //if (Math.Abs(xCentroidsDifference) < Math.Abs(yCentroidsDifference))
            //{
            //    if (yCentroidsDifference > threshold)
            //        label4.Text = "up";
            //    if (yCentroidsDifference < -threshold)
            //        label4.Text = "down";
            //}
        }
        private void DoOptical (){
            ComputeSparseOpticalFlow();
            ComputeMotionFromSparseOpticalFlow();

            Frame.Draw(new CircleF(referenceCentroid, 1.0f), new Bgr(Color.Goldenrod), 2);
            Frame.Draw(new CircleF(nextCentroid, 1.0f), new Bgr(Color.Red), 2);
        }
        private float SignedPolygonArea(PointF[] Hull)
        {
            int num_points = Hull.Length;
            // Get the areas.
            float area = 0;
            for (int i = 0; i < num_points; i++)
            {
                area +=
                    (Hull[(i + 1) % num_points].X - Hull[i].X) *
                    (Hull[(i + 1) % num_points].Y + Hull[i].Y) / 2;
            }
            // Return the result.
            return area;
        }
        private void DrawTrackedFeatures(int i)
        {
            Frame.Draw(new CircleF(new PointF(ActualFeature[0][i].X, ActualFeature[0][i].Y), 1f), new Bgr(Color.Blue), 1);
        }
        private PointF FindCentroid(PointF[] Hull)
        {
            // Add the first point at the end of the array.
            int num_points = Hull.Length;
            PointF[] pts = new PointF[num_points + 1];
            Hull.CopyTo(pts, 0);
            pts[num_points] = Hull[0];

            // Find the centroid.
            float X = 0;
            float Y = 0;
            float second_factor;
            for (int i = 0; i < num_points; i++)
            {
                second_factor = pts[i].X * pts[i + 1].Y - pts[i + 1].X * pts[i].Y;
                X += (pts[i].X + pts[i + 1].X) * second_factor;
                Y += (pts[i].Y + pts[i + 1].Y) * second_factor;
            }
            // Divide by 6 times the polygon's area.
            float polygon_area = Math.Abs(SignedPolygonArea(Hull));
            X /= (6 * polygon_area);
            Y /= (6 * polygon_area);

            // If the values are negative, the polygon is
            // oriented counterclockwise so reverse the signs.
            if (X < 0)
            {
                X = -X;
                Y = -Y;
            }
            return new PointF(X, Y);
        }
        private void ComputeSparseOpticalFlow()
        {
            // Compute optical flow using pyramidal Lukas Kanade Method                
            OpticalFlow.PyrLK(GrayFrame, nextGrayFrame, ActualFeature[0], new System.Drawing.Size(10, 10), 3, new MCvTermCriteria(20, 0.03d), out NextFeature, out Status, out TrackError);
            using (MemStorage storage = new MemStorage())
            nextHull = PointCollection.ConvexHull(ActualFeature[0], storage, Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE).ToArray();
            nextCentroid = FindCentroid(nextHull);
            for (int i = 0; i < ActualFeature[0].Length; i++)
            {
                DrawTrackedFeatures(i);
                DrawFlowVectors(i);
            }
            //Uncomment this to draw optical flow vectors

        }
        private void DrawFlowVectors(int i)
        {
            System.Drawing.Point p = new Point();
            System.Drawing.Point q = new Point();

            p.X = (int)ActualFeature[0][i].X;
            p.Y = (int)ActualFeature[0][i].Y;
            q.X = (int)NextFeature[i].X;
            q.Y = (int)NextFeature[i].Y;

            double angle;
            angle = Math.Atan2((double)p.Y - q.Y, (double)p.X - q.X);

            LineSegment2D line = new LineSegment2D(p, q);
            Frame.Draw(line, new Bgr(255, 0, 0), 3);

            p.X = (int)(q.X + 6 * Math.Cos(angle + Math.PI / 4));
            p.Y = (int)(q.Y + 6 * Math.Sin(angle + Math.PI / 4));
            Frame.Draw(new LineSegment2D(p, q), new Bgr(255, 0, 0), 3);
            p.X = (int)(q.X + 6 * Math.Cos(angle - Math.PI / 4));
            p.Y = (int)(q.Y + 6 * Math.Sin(angle - Math.PI / 4));
            Frame.Draw(new LineSegment2D(p, q), new Bgr(255, 0, 0), 3);
        }
        #endregion
        private void Debrief_Load(object sender, EventArgs e)
        {

        }
        private void openVideoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //set up filter
            //OF.Filter = "Video Files|*.avi;*.mp4;*.mpg";
 
            OF.Multiselect = true;
            //DialogResult result = OF.ShowDialog();
            //open file dialog to select file
            if (OF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
                //dispose of old capture if one exists
                if (OF.FileNames[0].EndsWith(".mpeg4")|| OF.FileNames[1].EndsWith(".mpeg4"))
                {
                    if (_Capture != null)
                    {
                        if (_Capture.GrabProcessState == System.Threading.ThreadState.Running) _Capture.Stop(); //Stop urrent capture if running 
                        _Capture.Dispose();//dispose of current capture
                    }
                }
                //else
                //{
                //    if (_Capture != null)
                //    {
                //        if (_Capture.GrabProcessState == System.Threading.ThreadState.Running) _Capture.Stop(); //Stop urrent capture if running 
                //        _Capture.Dispose();//dispose of current capture
                //    }
                //}
                try
                {
                    this.Text = "Viewing Video: " + OF.FileName; //display the viewing method and location

                    //set the current video state
                    CurrentState = VideoMethod.Viewing;

                    //set up new capture and get video information
                    _Capture = new Capture(OF.FileName);
                    //_Capture = new Capture("asd.avi");
                    _Capture.ImageGrabbed += ProcessFrame; //attache event call to process frames

                    //Get information about the video file
                    FrameRate = _Capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS);
                    TotalFrames = _Capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_COUNT);
                    //The four_cc returns a double so we must convert it
                    double codec_double = _Capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FOURCC);

                    //for more on fourcc video discriptors
                    /* http://www.fourcc.org/codecs.php */

                    //step by step
                    //UInt32 Udouble = Convert.ToUInt32(codec_double);
                    //byte[] bytes = BitConverter.GetBytes(Udouble);
                    //char[] char_array = System.Text.Encoding.UTF8.GetString(bytes).ToCharArray();
                    //string s = new string(char_array);

                    //or in one
                    string s = new string(System.Text.Encoding.UTF8.GetString(BitConverter.GetBytes(Convert.ToUInt32(codec_double))).ToCharArray());
                    Codec_lbl.Text = "Codec: " + s;

                    //set up the trackerbar 
                    UpdateVideo_CNTRL(true); //re-enable incase it is disabled by record video
                    Video_CNTRL.Minimum = 0;
                    Video_CNTRL.Maximum = (int)TotalFrames;

                    //set up the button and images 
                    play_pause_BTN.BackgroundImage = VideoCapture.Properties.Resources.Play;
                    playstate = false;
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }
            if (OF.FileNames[0].EndsWith(".txt"))
                richTextBox1.LoadFile(OF.FileNames[0], RichTextBoxStreamType.PlainText);
            else
                richTextBox1.LoadFile(OF.FileNames[1], RichTextBoxStreamType.PlainText);
        }
        private void buttontext_Click_1(object sender, EventArgs e)
        {
            StreamReader read;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                // MessageBox.Show(sr.ReadToEnd());
                read = new StreamReader(openFileDialog1.FileName);
                richTextBox1.Text = read.ReadToEnd();
                sr.Close();
                System.IO.StreamReader sr2 = new System.IO.StreamReader(openFileDialog1.FileName);
                // MessageBox.Show(sr.ReadToEnd());
                read = new StreamReader(openFileDialog1.FileName);
                string inputString = read.ReadToEnd();
                inputString = inputString.ToLower();
                string[] stripChars = { ";", ",", ".", "-", "_", "^", "(", ")", "[", "]",
						"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "\n", "\t", "\r" };
                foreach (string character in stripChars)
                {
                    inputString = inputString.Replace(character, "");
                }


                List<string> wordList = inputString.Split(' ').ToList();
                string[] stopwords = new string[] { "and", "the", "she", "for", "this", "you", "but", "with", "he", "him", "her", "his", "get",
                "yours", "your", "my", "mine", "hers", "their", "theirs", "our", "ours", "they", "i", "I", "these", "those", "without", "from",
                "there", "of", "to", "such", "have", "are", "this"};
                foreach (string word in stopwords)
                {
                    while (wordList.Contains(word))
                    {
                        wordList.Remove(word);
                    }
                }


                Dictionary<string, int> dictionary = new Dictionary<string, int>();


                foreach (string word in wordList)
                {

                    if (word.Length >= 3)
                    {

                        if (dictionary.ContainsKey(word))
                        {

                            dictionary[word]++;
                        }
                        else
                        {

                            dictionary[word] = 1;
                        }

                    }

                }


                var sortedDict = (from entry in dictionary orderby entry.Value descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);

                int count = 1;
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, int> pair in sortedDict)
                {
                    string text = "\t" + pair.Key + "\t" + pair.Value + "\n";

                    list.Add(text);

                }
                string line = string.Join("\n", list.ToArray());

                richTextBox2.Text = line;

                //richTextBox1.Text = line;
                sr2.Close();
            }

            
        }
        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void play_pause_BTN_Paint(object sender, PaintEventArgs e)
        {

        }
        #region displaytext
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //StreamReader read;
            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
                //System.IO.StreamReader sr = new
                //   System.IO.StreamReader(openFileDialog1.FileName);
                //// MessageBox.Show(sr.ReadToEnd());
                //read = new StreamReader(openFileDialog1.FileName);
                //textBox1.Text = read.ReadToEnd();
                //richTextBox1.Text = read.ReadToEnd();
            //sr.Close();
            
        }

        #endregion

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
