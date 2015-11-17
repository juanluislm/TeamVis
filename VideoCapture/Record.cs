using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//EMGU
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

//System
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Media;
using System.Speech.Recognition;
//using System.Windows.Controls;


namespace VideoCapture
{
    public partial class Record : Form
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

        SpeechRecognitionEngine speechRecognitionEngine = null;

        List<Word> words = new List<Word>();
        public Record()
        {
            InitializeComponent();
            try
            {
                speechRecognitionEngine = createSpeechEngine("en-US");

                speechRecognitionEngine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(engine_AudioLevelUpdated);
                speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);

                loadGrammarAndCommands();

                speechRecognitionEngine.SetInputToDefaultAudioDevice();

                speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Voice recognition failed");
            }
        }

        private SpeechRecognitionEngine createSpeechEngine(string preferredCulture)
        {
            foreach (RecognizerInfo config in SpeechRecognitionEngine.InstalledRecognizers())
            {
                if (config.Culture.ToString() == preferredCulture)
                {
                    speechRecognitionEngine = new SpeechRecognitionEngine(config);
                    break;
                }
            }

            if (speechRecognitionEngine == null)
            {
                MessageBox.Show("The desired culture is not installed on this machine, the speech-engine will continue using "
                    + SpeechRecognitionEngine.InstalledRecognizers()[0].Culture.ToString() + " as the default culture.",
                    "Culture " + preferredCulture + " not found!");
                speechRecognitionEngine = new SpeechRecognitionEngine(SpeechRecognitionEngine.InstalledRecognizers()[0]);
            }

            return speechRecognitionEngine;
        }

        private void loadGrammarAndCommands()
        {
            try
            {
                Choices texts = new Choices();
                texts.Add("Hello");
                texts.Add("Professor");
                texts.Add("Andrew");
                texts.Add("Lab");
                texts.Add("Welcome");
                texts.Add("Scalpel");
                texts.Add("Baby");
                texts.Add("Operation");
                texts.Add("Delivery");
                texts.Add("Surgery");
                texts.Add("Kinect");
                texts.Add("Hospital");
                texts.Add("Training");
                texts.Add("Downtown");
                texts.Add("Team");
                texts.Add("University");
                texts.Add("Florida");
                texts.Add("Microsoft");
                texts.Add("Doctor");
                texts.Add("Nurse");
                texts.Add("Physicist");
                texts.Add("Psychiatrist");
                texts.Add("Surgeon");
                texts.Add("Cardiologist");
                texts.Add("Experiment");
                texts.Add("Light");
                texts.Add("Bed");
                texts.Add("Room");
                texts.Add("Theatre");
                texts.Add("Orthopaedic");
                texts.Add("Neurologist");
                texts.Add("Therapy");
                texts.Add("Physical");
                texts.Add("Medical");
                texts.Add("Medicine");
                texts.Add("Treatment");
                texts.Add("Testing");
                texts.Add("Tyler");
                texts.Add("Shyam");
                texts.Add("Vamshee");
                texts.Add("Louis");
                texts.Add("Del");
                texts.Add("Research");
                texts.Add("Advanced");
                texts.Add("Speech");
                texts.Add("Recognition");
                texts.Add("Laproscopy");
                texts.Add("Needle");
                texts.Add("Session");
                texts.Add("Appointment");
                texts.Add("Meeting");
                texts.Add("Incision");
                texts.Add("Cut");
                texts.Add("Section");
                texts.Add("Twist");
                texts.Add("Scissor");
                texts.Add("Tray");
                texts.Add("Bandage");
                texts.Add("Appendage");
                texts.Add("Bones");
                texts.Add("Tissue");
                texts.Add("Heart");
                texts.Add("Organs");
                texts.Add("Spine");
                texts.Add("Hospital");
                texts.Add("Nerve");
                texts.Add("Skeleton");
                texts.Add("Muscle");
                texts.Add("Dosage");
                texts.Add("Blood");
                texts.Add("Bloodflow");
                texts.Add("Artery");
                texts.Add("Lungs");
                texts.Add("Liver");
                texts.Add("Disease");
                texts.Add("Heart");
                texts.Add("Cancer");
                texts.Add("Gynaecologist");
                texts.Add("Pulse");
                texts.Add("Immunity");
                texts.Add("Clinic");
                texts.Add("Clinical");
                texts.Add("Embryo");
                texts.Add("Appendix");
                texts.Add("Fracture");
                texts.Add("Infection");
                texts.Add("Health");
                texts.Add("Virus");
                texts.Add("Prescription");
                texts.Add("Human");
                texts.Add("Body");
                texts.Add("Anatomy");
                texts.Add("Foetus");
                texts.Add("Vaccination");

                Grammar wordsList = new Grammar(new GrammarBuilder(texts));
                speechRecognitionEngine.LoadGrammar(wordsList);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private string getKnownTextOrExecute(string command)
        {
            try
            {
                var cmd = words.Where(c => c.Text == command).First();

                if (cmd.IsShellCommand)
                {

                    Process proc = new Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.FileName = cmd.AttachedText;
                    proc.Start();
                    return "you just started : " + cmd.AttachedText;
                }
                else
                {
                    return cmd.AttachedText;
                }
            }
            catch (Exception)
            {
                return command;
            }
        }

        #region speechEngine events


        void engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            txtSpoken.Text += "\r" + getKnownTextOrExecute(e.Result.Text);
            sb.AppendLine(DateTime.Now.ToString("HH:mm:ss    ") + e.Result.Text);
            //scvText.ScrollToEnd();
            //txtSpoken.
        }


        void engine_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
        {
            prgLevel.Value = e.AudioLevel;
        }

        #endregion

        #region GUI events
        string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        StringBuilder sb = new StringBuilder();
        //private void Button_Click(object sender, EventArgs e)
        //{
        // this.Close();

        //  using (StreamWriter outfile = new StreamWriter(mydocpath + @"\Spoken List at " + DateTime.Now.ToString("yyyy MM dd HHmmss") + ".txt", true))
        //{
        //  outfile.Write(sb.ToString());
        //}

        //}

        #endregion

        private void play_pause_BTN1_MouseUp(object sender, MouseEventArgs e)
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
                        play_pause_BTN1.BackgroundImage = VideoCapture.Properties.Resources.Pause;
                        UpdateVideo_CNTRL(false); //disable this as it's not safe when running 
                        //this may work in legacy call method and be cause by a cross threading issue
                        _Capture.Start();
                    }
                    else
                    {
                        play_pause_BTN1.BackgroundImage = VideoCapture.Properties.Resources.Play;
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
                        play_pause_BTN1.BackgroundImage = VideoCapture.Properties.Resources.Stop;
                        FrameCount = 0;
                        InitTimer();
                        SW = new Stopwatch();
                        //check to see if we have disposed of the video before
                        if (VW.Ptr == IntPtr.Zero)
                        {
                            //explain to the user what's happening
                            MessageBox.Show("VideoWriter has been finilised, please re-initalise a video file");
                            //lets re-call the recordVideoToolStripMenuItem_Click to save on programing
                            recordVideoToolStripMenuItem_Click_1(null, null);
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
                        play_pause_BTN1.BackgroundImage = VideoCapture.Properties.Resources.Record;
                        SW.Stop();
                        timer1.Stop();
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

        /// <summary>
        /// Thread safe method to display Image in Pictureboxe
        /// </summary>
        /// <param name="Image"></param>
        private delegate void DisplayImageDelegate(Bitmap Image);
        private void DisplayImage(Bitmap Image)
        {
            if (pictureBox3.InvokeRequired)
            {
                try
                {
                    DisplayImageDelegate DI = new DisplayImageDelegate(DisplayImage);
                    this.BeginInvoke(DI, new object[] { Image });
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                pictureBox3.Image = Image;
            }
        }
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
                if (Value < Video_CNTRL.Maximum) Video_CNTRL.Value = (int)Value;
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

        #region ProcessFrame of recording
        /// Processes Each frame according to CurrentState Viewing/Recording Video
        private void ProcessFrame(object sender, EventArgs arg)/////////////////////////////////////////////////////////////
        {
            if (CurrentState == VideoMethod.Recording)
            {
                Image<Bgr, Byte> frame = _Capture.RetrieveBgrFrame(); //capture to a Image variable so we can use it for writing to the VideoWriter
                DisplayImage(_Capture.RetrieveBgrFrame().ToBitmap()); //Show the image

                //if we wanted to compresse the image to a smaller size to save space on our video we could use
                //frame.Resize(100,100, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR)
                //But the VideoWriter must be set up with the correct size

                if (recordstate && VW.Ptr != IntPtr.Zero)
                {
                    VW.WriteFrame(frame); //If we are recording and videowriter is avaliable add the image to the videowriter 
                    //Update frame number
                    FrameCount++;
                    UpdateTextBox("Frame: " + FrameCount.ToString(), Frame_lbl);

                    //Show time stamp or there abouts
                    UpdateTextBox("Time: " + TimeSpan.FromMilliseconds(SW.ElapsedMilliseconds).ToString(), Time_Label);
                }
            }
        }
        #endregion
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
            speechRecognitionEngine.RecognizeAsyncStop();
            speechRecognitionEngine.Dispose();
        }
        private void recordVideoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //set up filter
            SF.Filter = "Video Files|*.avi;*.mp4;*.mpg";
            //Get information about the video file save location
            if (SF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //check to see if capture exists if it does dispose of it
                if (_Capture != null)
                {
                    if (_Capture.GrabProcessState == System.Threading.ThreadState.Running) _Capture.Stop(); //Stop urrent capture if running 
                    _Capture.Dispose();//dispose of current capture
                }
                try
                {
                    //record the save location
                    this.Text = "Saving Video: " + SF.FileName; //display the save method and location

                    //set the current video state
                    CurrentState = VideoMethod.Recording;

                    //set up new capture
                    _Capture = new Capture(); //Use the default device 
                    _Capture.ImageGrabbed += ProcessFrame; //attach event call to process frames

                    //get/set the capture video information

                    Frame_width = (int)_Capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH);
                    Frame_Height = (int)_Capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT);

                    FrameRate = 15; //Set the framerate manually as a camera would retun 0 if we use GetCaptureProperty()

                    //Set up a video writer component
                    /*                                        ---USE----
                    /* VideoWriter(string fileName, int compressionCode, int fps, int width, int height, bool isColor)
                     *
                     * Compression code. 
                     *      Usually computed using CvInvoke.CV_FOURCC. On windows use -1 to open a codec selection dialog. 
                     *      On Linux, use CvInvoke.CV_FOURCC('I', 'Y', 'U', 'V') for default codec for the specific file name. 
                     * 
                     * Compression code. 
                     *      -1: allows the user to choose the codec from a dialog at runtime 
                     *       0: creates an uncompressed AVI file (the filename must have a .avi extension) 
                     *
                     * isColor.
                     *      true if this is a color video, false otherwise
                     */
                    VW = new VideoWriter(@SF.FileName, -1, (int)FrameRate, Frame_width, Frame_Height, true);

                    //set up the trackerbar 
                    UpdateVideo_CNTRL(false);//disable the trackbar

                    //set up the button and images 
                    play_pause_BTN1.BackgroundImage = VideoCapture.Properties.Resources.Record;
                    recordstate = false;

                    //Start aquring from the webcam
                    _Capture.Start();

                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }
        }
        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
            using (StreamWriter outfile = new StreamWriter(mydocpath + @"\Spoken List at " + DateTime.Now.ToString("yyyy MM dd HHmmss") + ".txt", true))
            {
                outfile.Write(sb.ToString());
            }
        }
        #region nothing really
        private void Video_CNTRL_Scroll(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void prgLevel_Click(object sender, EventArgs e)
        {

        }
        private void Record_Load(object sender, EventArgs e)
        {

        }
        private void txtSpoken_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion
        #region timer
        public void InitTimer()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 300; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // TeamPerformance.Text += "\r" + "  Was heartbeat meassured?\n";
            TeamPerformance.Text += DateTime.Now.ToString("HH:mm:ss   ") + "  Was heartbeat meassured?";
        }
        #endregion

        private void Record_Load_1(object sender, EventArgs e)
        {

        }
    }
}
