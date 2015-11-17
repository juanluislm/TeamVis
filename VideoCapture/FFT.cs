using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Drawing;
namespace VideoCapture
{
    class FFT
    {
        #region Variables
        Point Object_Location = new Point();
        #endregion
        #region Constructors

        /// <summary>
        /// Track Object with Class Creation
        /// </summary>
        /// <param name="Input_Image"></param>
        /// <param name="Template"></param>
        public FFT(Image<Gray, Byte> Input_Image, Image<Gray, Byte> Template)
        {
            Detect_objects(Input_Image.Copy(), Template.Copy());
        }

        /// <summary>
        /// Blank Creation
        /// </summary>
        public FFT()
        {
        }


        #endregion
        #region Public
        /// <summary>
        /// Returns Location of the tracked object
        /// </summary>
        /// <returns></returns>
        /// ... get the rectangle... 
        public Point GetLocation()
        {
            return Object_Location;
        }
        public bool TrackObject(Image<Gray, Byte> Input_Image, Image<Gray, Byte> Template)
        {
            return Detect_objects(Input_Image.Copy(), Template.Copy());
        }
        #endregion
        #region Private
        private bool Detect_objects(Image<Gray, Byte> Input_Image, Image<Gray, Byte> object_Image)
        {
            Point dftSize = new Point(Input_Image.Width + (object_Image.Width * 2), Input_Image.Height + (object_Image.Height * 2));
            bool Success = false;
            using (Image<Gray, Byte> pad_array = new Image<Gray, Byte>(dftSize.X, dftSize.Y))
            {
                //copy centre
                pad_array.ROI = new Rectangle(object_Image.Width, object_Image.Height, Input_Image.Width, Input_Image.Height);
                CvInvoke.cvCopy(Input_Image.Convert<Gray, Byte>(), pad_array, IntPtr.Zero);
               // CvInvoke.cvMatchTemplate
                //CvInvoke.cvShowImage("pad_array", pad_array);
                pad_array.ROI = (new Rectangle(0, 0, dftSize.X, dftSize.Y));
                using (Image<Gray, float> result_Matrix = pad_array.MatchTemplate(object_Image, TM_TYPE.CV_TM_CCOEFF_NORMED))
                {
                    result_Matrix.ROI = new Rectangle(object_Image.Width, object_Image.Height, Input_Image.Width, Input_Image.Height);

                    Point[] MAX_Loc, Min_Loc;
                    double[] min, max;
                    result_Matrix.MinMax(out min, out max, out Min_Loc, out MAX_Loc);

                    using (Image<Gray, double> RG_Image = result_Matrix.Convert<Gray, double>().Copy())
                    {
                        //#TAG WILL NEED TO INCREASE SO THRESHOLD AT LEAST 0.8...used to have 0.7

                        if (max[0] > 0.85)
                        {
                            Object_Location = MAX_Loc[0];
                            Success = true;
                        }
                    }

                }
            }
            return Success;
        }
        #endregion
    }
}
