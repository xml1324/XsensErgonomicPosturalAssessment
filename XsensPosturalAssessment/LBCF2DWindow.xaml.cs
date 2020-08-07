using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using LineSeries = OxyPlot.Series.LineSeries;
using OxyPlot.Annotations;
using MathNet.Numerics.Distributions;

namespace XsensPosturalAssessment
{
    /// <summary>
    /// Interaction logic for LBCF2DWindow.xaml
    /// </summary>
    public partial class LBCF2DWindow : Window
    {

        OpenFileDialog dlgOpen = new OpenFileDialog();

        Dictionary<Tuple<int, int>, List<double>> positionData = new Dictionary<Tuple<int, int>, List<double>>();

        Dictionary<int, string> jointName = new Dictionary<int, string>();
        Dictionary<int, string> axisName = new Dictionary<int, string>();

        string[][] data;

        private double LBCFCount = 0.0;

        private double body_weight, body_height, load_of_lifting;

        private List<double> LBCFList = new List<double>();

        private List<double> wristList = new List<double>();
        private List<double> elbowList = new List<double>();
        private List<double> shoulderList = new List<double>();
        private List<double> torsoList = new List<double>();
        private List<double> hipList = new List<double>();
        private List<double> kneeList = new List<double>();
        private List<double> ankleList = new List<double>();

        private List<double> wristListF = new List<double>();
        private List<double> elbowListF = new List<double>();
        private List<double> shoulderListF = new List<double>();
        private List<double> torsoListF = new List<double>();
        private List<double> hipListF = new List<double>();
        private List<double> kneeListF = new List<double>();
        private List<double> ankleListF = new List<double>();

        public PlotModel MyModelLBCF { get; private set; }
        public PlotModel PlotLBCF = new PlotModel{ Title = "Low back compression force in L5S1" };
        public PlotModel MyModelPercentile { get; private set; }
        public PlotModel PlotPercentile = new PlotModel{ Title = "Strength Percent Capable (%)" };
        public PlotModel MyModelSkeleton { get; private set; }
        public PlotModel PlotSkeleton = new PlotModel{ Title = "Sagittal View" };

        private LineSeries SeriesWristPercentile = new LineSeries{ Title = "Wrist" };
        private LineSeries SeriesElbowPercentile = new LineSeries { Title = "Elbow" };
        private LineSeries SeriesShoulderPercentile = new LineSeries { Title = "Shoulder" };
        private LineSeries SeriesTorsoPercentile = new LineSeries { Title = "Torso" };
        private LineSeries SeriesHipPercentile = new LineSeries { Title = "Hip" };
        private LineSeries SeriesKneePercentile = new LineSeries { Title = "Knee" };
        private LineSeries SeriesAnklePercentile = new LineSeries { Title = "Ankle" };

        private LineSeries SeriesWristPercentileF = new LineSeries { Title = "Wrist" };
        private LineSeries SeriesElbowPercentileF = new LineSeries { Title = "Elbow" };
        private LineSeries SeriesShoulderPercentileF = new LineSeries { Title = "Shoulder" };
        private LineSeries SeriesTorsoPercentileF = new LineSeries { Title = "Torso" };
        private LineSeries SeriesHipPercentileF = new LineSeries { Title = "Hip" };
        private LineSeries SeriesKneePercentileF = new LineSeries { Title = "Knee" };
        private LineSeries SeriesAnklePercentileF = new LineSeries { Title = "Ankle" };

        private LineSeries SeriesLBCF = new LineSeries();
        private LineAnnotation AnnotationLBCF1 = new LineAnnotation();
        private LineAnnotation AnnotationLBCF2 = new LineAnnotation();
        private LineAnnotation AnnotationTimeLBCF = new LineAnnotation{ Color = OxyColors.Blue, Type = LineAnnotationType.Vertical };
        private LineAnnotation AnnotationTimePercentile = new LineAnnotation{ Color = OxyColors.Blue, Type = LineAnnotationType.Vertical };

        private PointAnnotation AnnotationHead = new PointAnnotation{ Shape = MarkerType.Circle, Size = 15, Fill = OxyColors.White, Stroke = OxyColors.SlateGray, StrokeThickness = 3 };
        private PointAnnotation AnnotationRightUpperArm = new PointAnnotation { Shape = MarkerType.Circle, Size = 5, Fill = OxyColors.White, Stroke = OxyColors.SlateGray, StrokeThickness = 3 };
        private PointAnnotation AnnotationRightForearm = new PointAnnotation { Shape = MarkerType.Circle, Size = 5, Fill = OxyColors.White, Stroke = OxyColors.SlateGray, StrokeThickness = 3 };
        private PointAnnotation AnnotationRightHand = new PointAnnotation { Shape = MarkerType.Circle, Size = 5, Fill = OxyColors.White, Stroke = OxyColors.SlateGray, StrokeThickness = 3 };
        private PointAnnotation AnnotationRightUpperLeg = new PointAnnotation { Shape = MarkerType.Circle, Size = 5, Fill = OxyColors.White, Stroke = OxyColors.SlateGray, StrokeThickness = 3 };
        private PointAnnotation AnnotationRightLowerLeg = new PointAnnotation { Shape = MarkerType.Circle, Size = 5, Fill = OxyColors.White, Stroke = OxyColors.SlateGray, StrokeThickness = 3 };
        private PointAnnotation AnnotationRightFoot = new PointAnnotation { Shape = MarkerType.Circle, Size = 5, Fill = OxyColors.White, Stroke = OxyColors.SlateGray, StrokeThickness = 3 };

        private ScatterSeries SeriesHead = new ScatterSeries{ MarkerType = MarkerType.Circle, MarkerFill = OxyColors.Plum, Title = "Neck" };
        private ScatterSeries SeriesUpperArm = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColors.Salmon, Title = "Upper arm"};
        private ScatterSeries SeriesForearm = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColors.Gold, Title = "Lower arm" };
        private ScatterSeries SeriesTorso = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColors.DodgerBlue, Title = "Torso"};
        private ScatterSeries SeriesUpperLeg = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColors.MediumTurquoise, Title = "Upper leg" };
        private ScatterSeries SeriesLowerLeg = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerFill = OxyColors.MediumSpringGreen, Title = "Lower leg" };

        // Body weight percentage of different body parts
        private const double lowerarm_weight_percentage = 0.017;
        private const double Upperarm_Weight_percentage = 0.028;
        private const double trunk_Weight_percentage = 0.5;

        private const double Upperleg_weight_percentage = 0.1;
        private const double lowerleg_weight_percentage = 0.043;
        private const double foot_weight_percentage = 0.014;

        // Body height percentage of different body parts
        private const double hand_height_percentage = 0.054;
        private const double lowerarm_height_percentage = 0.146;
        private const double Upperarm_height_percentage = 0.166;
        private const double trunk_height_percentage = 0.288;
        private const double upperleg_height_percentage = 0.255;
        private const double lowerleg_height_percentage = 0.246;
        private const double foot_height_percentage = 0.152;

        // distance percentage for segments COM
        private const double lowerarm_distance_percentage = 0.43;
        private const double upperarm_distance_percentage = 0.436;
        private const double trunk_distance_percentage = 0.67;

        private const double upperleg_distance_percentage = 0.5;
        private const double lowerleg_distance_percentage = 0.47;
        private const double foot_distance_percentage = 0.42;

        private const int normal_Vector_x = 0;
        private const int normal_Vector_y = 0;
        private const int normal_Vector_z = 1;

        private const double D = 0.11; // Distance form Abdominal Force to L5S1 (Source: Nurmianto, 1996)
        private const double E = 0.05; // Long arm of the  erector spinal muscle moment L5/S1 (Source: Nurmianto, 1996)

        public LBCF2DWindow()
        {
            MyModelLBCF = PlotLBCF;
            MyModelPercentile = PlotPercentile;
            MyModelSkeleton = PlotSkeleton;

            DataContext = this;

            InitializeComponent();

            dlgOpen.DefaultExt = ".csv"; // Default file extension
            dlgOpen.Filter = "csv files (.csv)|*.csv"; // Filter files by extension

            #region Joint and Axis Names

            jointName.Add(0, "Pelvis");
            jointName.Add(1, "L5");
            jointName.Add(2, "L3");
            jointName.Add(3, "T12");
            jointName.Add(4, "T8");
            jointName.Add(5, "Neck");
            jointName.Add(6, "Head");
            jointName.Add(7, "RightShoulder");
            jointName.Add(8, "RightUpperArm");
            jointName.Add(9, "RightForearm");
            jointName.Add(10, "RightHand");
            jointName.Add(11, "LeftShoulder");
            jointName.Add(12, "LeftUpperArm");
            jointName.Add(13, "LeftForearm");
            jointName.Add(14, "LeftHand");
            jointName.Add(15, "RightUpperLeg");
            jointName.Add(16, "RightLowerLeg");
            jointName.Add(17, "RightFoot");
            jointName.Add(18, "RightToe");
            jointName.Add(19, "LeftUpperLeg");
            jointName.Add(20, "LeftLowerLeg");
            jointName.Add(21, "LeftFoot");
            jointName.Add(22, "LeftToe");

            axisName.Add(0, "x");
            axisName.Add(1, "y");
            axisName.Add(2, "z");

            #endregion

            PlotLBCF.Annotations.Add(AnnotationLBCF1);
            PlotLBCF.Annotations.Add(AnnotationLBCF2);

            PlotLBCF.Annotations.Add(AnnotationTimeLBCF);

            PlotPercentile.Annotations.Add(AnnotationTimePercentile);

        }



        private void OpenButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (positionData.Count != 0) positionData.Clear();

            for (int i = 0; i < jointName.Count; i++)
            {
                for (int j = 0; j < axisName.Count; j++)
                {
                    positionData.Add(Tuple.Create(i, j), new List<double>());
                }
            }

            // Show open file dialog box
            Nullable<bool> result = dlgOpen.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                string fileName = dlgOpen.FileName;
                fileNameBlock.Text = dlgOpen.FileName;

                // Open document

                StreamReader sr = new StreamReader(fileName);
                var lines = new List<string[]>();
                while (!sr.EndOfStream)
                {
                    string[] Line = sr.ReadLine().Split(',');
                    lines.Add(Line);
                }

                data = lines.ToArray();

                for (int i = 0; i < jointName.Count; i++)
                {
                    for (int j = 0; j < axisName.Count; j++)
                    {
                        for (int k = 0; k < data.GetLength(0); k++)
                        {
                            try
                            {
                                positionData[Tuple.Create(i, j)].Add(double.Parse(data[k][i * axisName.Count + j]));
                            }
                            catch (Exception)
                            {
                                positionData[Tuple.Create(i, j)].Add(0);
                            }
                        }
                    }
                }
                // Enable buttons when data is imported.
                AnalyzeButton.IsEnabled = true;
                ExportButton.IsEnabled = true;

                TimelineSlider.Maximum = data.GetLength(0) - 2;
            }

        }

        // Translate joint name to number for easier coding
        private int Joints(string joint)
        {
            switch (joint)
            {
                case "Pelvis": return 0;
                case "L5": return 1;
                case "L3": return 2;
                case "T12": return 3;
                case "T8": return 4;
                case "Neck": return 5;
                case "Head": return 6;
                case "RightShoulder": return 7;
                case "RightUpperArm": return 8;
                case "RightForearm": return 9;
                case "RightHand": return 10;
                case "LeftShoulder": return 11;
                case "LeftUpperArm": return 12;
                case "LeftForearm": return 13;
                case "LeftHand": return 14;
                case "RightUpperLeg": return 15;
                case "RightLowerLeg": return 16;
                case "RightFoot": return 17;
                case "RightToe": return 18;
                case "LeftUpperLeg": return 19;
                case "LeftLowerLeg": return 20;
                case "LeftFoot": return 21;
                case "LeftToe": return 22;
                default: return 22;
            }
        }

        // Translate joint name to number for easier coding
        private int Angles(string angle)
        {
            switch (angle)
            {
                case "x": return 0;
                case "y": return 1;
                case "z": return 2;
                default: return 3;
            }
        }

        private void ClearLists()
        {
            PlotLBCF.Series.Clear();
            PlotLBCF.Axes.Clear();
            LBCFList.Clear();
            SeriesLBCF.Points.Clear();
            LBCFCount = 0;

            PlotPercentile.Series.Clear();
            PlotPercentile.Axes.Clear();

            wristList.Clear();
            elbowList.Clear();
            shoulderList.Clear();
            torsoList.Clear();
            hipList.Clear();
            kneeList.Clear();
            ankleList.Clear();

            wristListF.Clear();
            elbowListF.Clear();
            shoulderListF.Clear();
            torsoListF.Clear();
            hipListF.Clear();
            kneeListF.Clear();
            ankleListF.Clear();

            SeriesWristPercentile.Points.Clear();
            SeriesElbowPercentile.Points.Clear();
            SeriesShoulderPercentile.Points.Clear();
            SeriesTorsoPercentile.Points.Clear();
            SeriesHipPercentile.Points.Clear();
            SeriesKneePercentile.Points.Clear();
            SeriesAnklePercentile.Points.Clear();

            SeriesWristPercentileF.Points.Clear();
            SeriesElbowPercentileF.Points.Clear();
            SeriesShoulderPercentileF.Points.Clear();
            SeriesTorsoPercentileF.Points.Clear();
            SeriesHipPercentileF.Points.Clear();
            SeriesKneePercentileF.Points.Clear();
            SeriesAnklePercentileF.Points.Clear();

            PlotSkeleton.Series.Clear();
            PlotSkeleton.Axes.Clear();
            
            PlotSkeleton.Annotations.Clear();

            SeriesHead.Points.Clear();
            SeriesUpperArm.Points.Clear();
            SeriesForearm.Points.Clear();
            SeriesTorso.Points.Clear();
            SeriesUpperLeg.Points.Clear();
            SeriesLowerLeg.Points.Clear();
            
        }

        private void AnalyzeButton_OnClick(object sender, RoutedEventArgs e)
        {
            ClearLists();

            #region Calculation

            // 1. Input and Processing
            //1.1 Input the body weight, body height, load of lifting

            try
            {
                body_weight = double.Parse(WeightLoadTextBox.Text); // in (kg)
                body_height = double.Parse(HeightHumanTextBox.Text); // in (m)
                load_of_lifting = double.Parse(WeightHumanTextBox.Text); // in (kg)
            }
            catch (Exception)
            {
                MessageBox.Show("Please input all numbers!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            //1.2 Import the Xsens position data

            // weight of different parts (in Newton)
            var weight_of_humanbody = body_weight * 9.8;
            var weight_of_lowerarm = body_weight * 9.8 * lowerarm_weight_percentage;
            var weight_of_upperarm = body_weight * 9.8 * Upperarm_Weight_percentage;
            var weight_of_trunk = body_weight * 9.8 * trunk_Weight_percentage;
            var weight_of_lifting = load_of_lifting * 9.8;

            var weight_of_upperleg = body_weight * 9.8 * Upperleg_weight_percentage;
            var weight_of_lowerleg = body_weight * 9.8 * lowerleg_weight_percentage;
            var weight_of_foot = body_weight * 9.8 * foot_weight_percentage;

            // total weight invloved in this link-segment model
            var weight_total = weight_of_lifting + weight_of_lowerarm * 2 + weight_of_upperarm * 2 + weight_of_trunk +
                               body_weight * 9.8 * 0.006 * 2;


            // segment lengths based on human height
            var SL_1 = body_height * hand_height_percentage;
            var SL_2 = body_height * lowerarm_height_percentage;
            var SL_3 = body_height * Upperarm_height_percentage;
            var SL_4 = body_height * trunk_height_percentage;
            var SL_5 = body_height * foot_height_percentage;
            var SL_6 = body_height * lowerleg_height_percentage;
            var SL_7 = body_height * upperleg_height_percentage;



            // 2. Use the position data from Xsens to calculate the 5 angles (theta_2, theta_3, theta_4, theta_v, and theta_t)

            for (int i = 1; i < data.GetLength(0); i++)
            {

                // Calculation of theta_2

                var vector_r_hand_forearm_x = positionData[Tuple.Create(Joints("RightHand"), Angles("x"))][i] -
                                              positionData[Tuple.Create(Joints("RightForearm"), Angles("x"))][i];
                var vector_r_hand_forearm_y = positionData[Tuple.Create(Joints("RightHand"), Angles("y"))][i] -
                                              positionData[Tuple.Create(Joints("RightForearm"), Angles("y"))][i];
                var vector_r_hand_forearm_z = positionData[Tuple.Create(Joints("RightHand"), Angles("z"))][i] -
                                              positionData[Tuple.Create(Joints("RightForearm"), Angles("z"))][i];

                var theta_12 = Math.Acos(normal_Vector_z * vector_r_hand_forearm_z /
                                         Math.Sqrt(Math.Pow(vector_r_hand_forearm_x, 2) +
                                                   Math.Pow(vector_r_hand_forearm_y, 2) +
                                                   Math.Pow(vector_r_hand_forearm_z, 2)));
                var theta_2 = Math.Abs(theta_12 - Math.PI / 2);

                var theta_1 = theta_2;

                // Calculation of theta_3

                var vector_r_forearm_upperarm_x = positionData[Tuple.Create(Joints("RightForearm"), Angles("x"))][i] -
                                                  positionData[Tuple.Create(Joints("RightUpperArm"), Angles("x"))][i];
                var vector_r_forearm_upperarm_y = positionData[Tuple.Create(Joints("RightForearm"), Angles("y"))][i] -
                                                  positionData[Tuple.Create(Joints("RightUpperArm"), Angles("y"))][i];
                var vector_r_forearm_upperarm_z = positionData[Tuple.Create(Joints("RightForearm"), Angles("z"))][i] -
                                                  positionData[Tuple.Create(Joints("RightUpperArm"), Angles("z"))][i];

                var theta_13 = Math.Acos(normal_Vector_z * vector_r_forearm_upperarm_z /
                                         Math.Sqrt(Math.Pow(vector_r_forearm_upperarm_x, 2) +
                                                   Math.Pow(vector_r_forearm_upperarm_y, 2) +
                                                   Math.Pow(vector_r_forearm_upperarm_z, 2)));
                var theta_3 = Math.Abs(theta_13 - Math.PI / 2);

                // Calculation of theta_4
                // center of two shoulders
                var center_of_two_shoulders_x = (positionData[Tuple.Create(Joints("LeftShoulder"), Angles("x"))][i] +
                                                 positionData[Tuple.Create(Joints("RightShoulder"), Angles("x"))][i]) /
                                                2;
                var center_of_two_shoulders_y = (positionData[Tuple.Create(Joints("LeftShoulder"), Angles("y"))][i] +
                                                 positionData[Tuple.Create(Joints("RightShoulder"), Angles("y"))][i]) /
                                                2;
                var center_of_two_shoulders_z = (positionData[Tuple.Create(Joints("LeftShoulder"), Angles("z"))][i] +
                                                 positionData[Tuple.Create(Joints("RightShoulder"), Angles("z"))][i]) /
                                                2;

                var vector_center_of_shoulders_to_pelvis_x =
                    center_of_two_shoulders_x - positionData[Tuple.Create(Joints("Pelvis"), Angles("x"))][i];
                var vector_center_of_shoulders_to_pelvis_y =
                    center_of_two_shoulders_y - positionData[Tuple.Create(Joints("Pelvis"), Angles("y"))][i];
                var vector_center_of_shoulders_to_pelvis_z =
                    center_of_two_shoulders_z - positionData[Tuple.Create(Joints("Pelvis"), Angles("z"))][i];

                var theta_14 = Math.Acos(normal_Vector_z * vector_center_of_shoulders_to_pelvis_z /
                                         Math.Sqrt(Math.Pow(vector_center_of_shoulders_to_pelvis_x, 2) +
                                                   Math.Pow(vector_center_of_shoulders_to_pelvis_y, 2) +
                                                   Math.Pow(vector_center_of_shoulders_to_pelvis_z, 2)));

                var theta_4 = Math.Abs(theta_14 - Math.PI / 2);

                // Calculation of theta_v & theta_t
    
                // Calculation of theta_v
                var vector_righthip_to_rightshoudler_x = positionData[Tuple.Create(Joints("RightShoulder"), Angles("x"))][i] -
                                                         positionData[Tuple.Create(Joints("RightUpperLeg"), Angles("x"))][i];
                var vector_righthip_to_rightshoudler_y = positionData[Tuple.Create(Joints("RightShoulder"), Angles("y"))][i] -
                                                         positionData[Tuple.Create(Joints("RightUpperLeg"), Angles("y"))][i];
                var vector_righthip_to_rightshoudler_z = positionData[Tuple.Create(Joints("RightShoulder"), Angles("z"))][i] -
                                                         positionData[Tuple.Create(Joints("RightUpperLeg"), Angles("z"))][i];

                var theta_1v = Math.Acos(normal_Vector_z * vector_righthip_to_rightshoudler_z / Math.Sqrt(
                    Math.Pow(vector_righthip_to_rightshoudler_x, 2) +
                    Math.Pow(vector_righthip_to_rightshoudler_y, 2) +
                    Math.Pow(vector_righthip_to_rightshoudler_z, 2)));

                var theta_v = Math.Abs(theta_1v - Math.PI / 2);

                // Calculation of theta_t
                var vector_lowerleg_to_upperleg_x =
                    positionData[Tuple.Create(Joints("RightLowerLeg"), Angles("x"))][i] -
                    positionData[Tuple.Create(Joints("RightUpperLeg"), Angles("x"))][i];
                var vector_lowerleg_to_upperleg_y =
                    positionData[Tuple.Create(Joints("RightLowerLeg"), Angles("y"))][i] -
                    positionData[Tuple.Create(Joints("RightUpperLeg"), Angles("y"))][i];
                var vector_lowerleg_to_upperleg_z =
                    positionData[Tuple.Create(Joints("RightLowerLeg"), Angles("z"))][i] -
                    positionData[Tuple.Create(Joints("RightUpperLeg"), Angles("z"))][i];

                var theta_1t = Math.Acos(-normal_Vector_z * vector_lowerleg_to_upperleg_z /
                                         Math.Sqrt(Math.Pow(vector_lowerleg_to_upperleg_x, 2) +
                                                   Math.Pow(vector_lowerleg_to_upperleg_y, 2) +
                                                   Math.Pow(vector_lowerleg_to_upperleg_z, 2)));

                var theta_t = Math.Abs(theta_1t - Math.PI / 2);


                // Calculation of theta_5
                var vector_ankle_to_toe_x = positionData[Tuple.Create(Joints("RightToe"), Angles("x"))][i] -
                                            positionData[Tuple.Create(Joints("RightFoot"), Angles("x"))][i];
                var vector_ankle_to_toe_y = positionData[Tuple.Create(Joints("RightToe"), Angles("y"))][i] -
                                                positionData[Tuple.Create(Joints("RightFoot"), Angles("y"))][i];
                var vector_ankle_to_toe_z = positionData[Tuple.Create(Joints("RightToe"), Angles("z"))][i] -
                                            positionData[Tuple.Create(Joints("RightFoot"), Angles("z"))][i];

                var theta_15 = Math.Acos(vector_ankle_to_toe_z * normal_Vector_z / Math.Sqrt(
                    Math.Pow(vector_ankle_to_toe_x, 2) + Math.Pow(vector_ankle_to_toe_y, 2) + Math.Pow(vector_ankle_to_toe_z, 2)));

                var theta_5 = Math.Abs(theta_15 - Math.PI / 2);

                // Calculation of theta_6
                var vector_ankle_to_knee_x = positionData[Tuple.Create(Joints("RightLowerLeg"), Angles("x"))][i] -
                                             positionData[Tuple.Create(Joints("RightFoot"), Angles("x"))][i];
                var vector_ankle_to_knee_y = positionData[Tuple.Create(Joints("RightLowerLeg"), Angles("y"))][i] -
                                             positionData[Tuple.Create(Joints("RightFoot"), Angles("y"))][i];
                var vector_ankle_to_knee_z = positionData[Tuple.Create(Joints("RightLowerLeg"), Angles("z"))][i] -
                                             positionData[Tuple.Create(Joints("RightFoot"), Angles("z"))][i];

                var theta_16 = Math.Acos(vector_ankle_to_knee_z * normal_Vector_z / Math.Sqrt(
                    Math.Pow(vector_ankle_to_knee_x, 2) + Math.Pow(vector_ankle_to_knee_y, 2) + Math.Pow(vector_ankle_to_knee_z, 2)));

                    var theta_6 = Math.Abs(theta_16 - Math.PI / 2);


                // Calculation of theta_7
                var theta_7 = theta_t;

                // 3. Final calculation

                // 1. Calculation of wrist joint
                var F_wrist = body_weight * 9.8 * 0.006 + weight_of_lifting / 2;

                var M_wrist = F_wrist * SL_1 * Math.Cos(theta_1);

                // 2. Calculation of elbow joint
                var F_elbow = F_wrist + weight_of_lowerarm;

                var M_elbow = M_wrist + F_wrist * SL_2 * Math.Cos(theta_2) +
                              weight_of_lowerarm * SL_2 * lowerarm_distance_percentage * Math.Cos(theta_2);

                // 3. Calculation of shoulder joint
                var F_shoulder = F_elbow + weight_of_upperarm;
                var M_shoulder = M_elbow + F_elbow * SL_3 * Math.Cos(theta_3) +
                                 weight_of_upperarm * upperarm_distance_percentage * SL_3 * Math.Cos(theta_3);

                // 4. Calculation of L5/S1 joint
                var F_L5S1 = 2 * F_shoulder + weight_of_trunk;
                var M_L5S1 = 2 * M_shoulder + 2 * F_shoulder * SL_4 * Math.Cos(theta_4) +
                             weight_of_trunk * trunk_distance_percentage * SL_4 * Math.Cos(theta_4);


                // 5. Calculation of hip joint
                var F_hip = F_L5S1 / 2;
                var M_hip = M_L5S1 / 2;


                // 6. Calculation of knee joint
                var F_knee = F_hip + weight_of_upperleg;
                var M_knee = M_hip + F_hip * SL_7 * Math.Cos(theta_7) + weight_of_upperleg * SL_7 * upperleg_distance_percentage * Math.Cos(theta_7);


                // 7. Calculation of ankle joint
                var F_ankle = F_knee + weight_of_lowerleg;
                var M_ankle = M_knee + F_knee * SL_6 * Math.Cos(theta_6) + weight_of_lowerleg * SL_6 * foot_distance_percentage * Math.Cos(theta_6);


                // Calculation of Abdominal Pressure (PA)
                var angle_V = Math.Abs(theta_v) * 180 / Math.PI;
                var angle_t = Math.Abs(theta_t) * 180 / Math.PI;
                var PA = (0.0001 * (43 - 0.36 * (angle_V + angle_t)) * Math.Pow(M_L5S1, 1.8)) / 75;
                var AA = 465; //Diaphragm large
                var FA = PA * AA;

                // calculation of Muscle forces on spinal erector (FM)

                var FM = (M_L5S1 - FA * D) / E;

                // Calculation of the compression force on L5/S1
                var F_compression_L5S1 = weight_total * Math.Cos(theta_4) - FA + FM;


                if (F_compression_L5S1 > 3400)
                {
                    LBCFCount++;
                }


                // Add in Lists

                LBCFList.Add(F_compression_L5S1);

                // 4. percentile value
                // 4.1 For the wrist joint
                var moment_wrist = M_wrist;
                var mean_strength_moment_wrist = 10.9;
                var sd_strength_moment_wrist = 3.5;
                var strength_percentile_wrist = (1 - Normal.CDF(mean_strength_moment_wrist, sd_strength_moment_wrist, moment_wrist)) * 100;

                var mean_strength_moment_wrist_F = 8.1;
                var sd_strength_moment_wrist_F = 2.6;
                var strength_percentile_wrist_F = (1 - Normal.CDF(mean_strength_moment_wrist_F, sd_strength_moment_wrist_F, moment_wrist)) * 100;

                // 4.2 For the elbow joint
                var moment_elbow = M_elbow;
                var mean_strength_moment_elbow = 64.9;
                var sd_strength_moment_elbow = 16.0;
                var strength_percentile_elbow = (1 - Normal.CDF(mean_strength_moment_elbow, sd_strength_moment_elbow, moment_elbow)) * 100;

                var mean_strength_moment_elbow_F = 34.1;
                var sd_strength_moment_elbow_F = 9.0;
                var strength_percentile_elbow_F = (1 - Normal.CDF(mean_strength_moment_elbow_F, sd_strength_moment_elbow_F, moment_elbow)) * 100;

                // 4.3 For the shoulder joint
                var moment_shoulder = M_shoulder;
                var mean_strength_moment_shoulder = 93.8;
                var sd_strength_moment_shoulder = 25.6;
                var strength_percentile_shoulder = (1 - Normal.CDF(mean_strength_moment_shoulder, sd_strength_moment_shoulder, moment_shoulder)) * 100;

                var mean_strength_moment_shoulder_F = 40.5;
                var sd_strength_moment_shoulder_F = 10.6;
                var strength_percentile_shoulder_F = (1 - Normal.CDF(mean_strength_moment_shoulder_F, sd_strength_moment_shoulder_F, moment_shoulder)) * 100;

                // 4.4 For the L5S1 joint
                var moment_L5S1 = M_L5S1;
                var mean_strength_moment_L5S1 = 379.2;
                var sd_strength_moment_L5S1 = 119.5;
                var strength_percentile_L5S1 = (1 - Normal.CDF(mean_strength_moment_L5S1, sd_strength_moment_L5S1, moment_L5S1)) * 100;

                var mean_strength_moment_L5S1_F = 247.4;
                var sd_strength_moment_L5S1_F = 85.5;
                var strength_percentile_L5S1_F = (1 - Normal.CDF(mean_strength_moment_L5S1_F, sd_strength_moment_L5S1_F, moment_L5S1)) * 100;

                // 4.5 For the Hip joint
                var moment_hip = M_hip;
                var mean_strength_moment_hip = 214.0;
                var sd_strength_moment_hip = 85.9;
                var strength_percentile_hip = (1 - Normal.CDF(mean_strength_moment_hip, sd_strength_moment_hip, moment_hip)) * 100;

                var mean_strength_moment_hip_F = 113.2;
                var sd_strength_moment_hip_F = 42.8;
                var strength_percentile_hip_F = (1 - Normal.CDF(mean_strength_moment_hip_F, sd_strength_moment_hip_F, moment_hip)) * 100;

                // 4.6 For the Knee joint
                var moment_knee = M_knee;
                var mean_strength_moment_knee = 169.7;
                var sd_strength_moment_knee = 59.4;
                var strength_percentile_knee = (1 - Normal.CDF(mean_strength_moment_knee, sd_strength_moment_knee, moment_knee)) * 100;

                var mean_strength_moment_knee_F = 114.0;
                var sd_strength_moment_knee_F = 39.5;
                var strength_percentile_knee_F = (1 - Normal.CDF(mean_strength_moment_knee_F, sd_strength_moment_knee_F, moment_knee)) * 100;

                // 4.7 For the Ankle joint
                var moment_ankle = M_ankle;
                var mean_strength_moment_ankle = 160.5;
                var sd_strength_moment_ankle = 53.1;
                var strength_percentile_ankle = (1 - Normal.CDF(mean_strength_moment_ankle, sd_strength_moment_ankle, moment_ankle)) * 100;

                var mean_strength_moment_ankle_F = 96.2;
                var sd_strength_moment_ankle_F = 26.4;
                var strength_percentile_ankle_F = (1 - Normal.CDF(mean_strength_moment_ankle_F, sd_strength_moment_ankle_F, moment_ankle)) * 100;

                wristList.Add(strength_percentile_wrist);
                elbowList.Add(strength_percentile_elbow);
                shoulderList.Add(strength_percentile_shoulder);
                torsoList.Add(strength_percentile_L5S1);
                hipList.Add(strength_percentile_hip);
                kneeList.Add(strength_percentile_knee);
                ankleList.Add(strength_percentile_ankle);

                wristListF.Add(strength_percentile_wrist_F);
                elbowListF.Add(strength_percentile_elbow_F);
                shoulderListF.Add(strength_percentile_shoulder_F);
                torsoListF.Add(strength_percentile_L5S1_F);
                hipListF.Add(strength_percentile_hip_F);
                kneeListF.Add(strength_percentile_knee_F);
                ankleListF.Add(strength_percentile_ankle_F);
            }

            #endregion


            #region Presentation

            var LBCFMedian = LBCFList.Median();
            var LBCFListQ1 = LBCFList.Where(x => x < LBCFMedian).ToList();
            var LBCFListQ3 = LBCFList.Where(x => x > LBCFMedian).ToList();
            
            MaximumLBCFTextBlock.Text = LBCFList.Max().ToString("0.000");
            AverageLBCFTextBlock.Text = LBCFList.Average().ToString("0.000");

            var durationRatio = LBCFCount / (data.GetLength(0) - 1);
            DurationLBCFTextBlock.Text = (LBCFCount / 240.0).ToString("0.0") + "(" + (durationRatio * 100.0).ToString("0.0") + "%)";

            SummaryLBCFTextBlock1.Text = LBCFList.Min().ToString("0.0");
            SummaryLBCFTextBlock2.Text = LBCFListQ1.Median().ToString("0.0");
            SummaryLBCFTextBlock3.Text = LBCFMedian.ToString("0.0");
            SummaryLBCFTextBlock4.Text = LBCFList.Average().ToString("0.0");
            SummaryLBCFTextBlock5.Text = LBCFListQ3.Median().ToString("0.0");
            SummaryLBCFTextBlock6.Text = LBCFList.Max().ToString("0.0");

            // Plotting

            AnnotationLBCF1.Type = LineAnnotationType.Horizontal;
            AnnotationLBCF1.Y = 3400;
            AnnotationLBCF1.Text = "Action Limit (3400 N)";
            AnnotationLBCF1.TextColor = OxyColors.Orange;
            AnnotationLBCF1.Color = OxyColors.Orange;

            AnnotationLBCF2.Type = LineAnnotationType.Horizontal;
            AnnotationLBCF2.Y = 6400;
            AnnotationLBCF2.Text = "Max Permissible Limit (6400 N)";
            AnnotationLBCF2.TextColor = OxyColors.Red;
            AnnotationLBCF2.Color = OxyColors.Red;

            for (int i = 0; i < LBCFList.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesLBCF.Points.Add(new DataPoint(time, LBCFList[i]));
            }

            PlotLBCF.Axes.Add(new LinearAxis
            {
                Title = "Compression force of low back (N)",
                Position = AxisPosition.Left,
            });

            PlotLBCF.Axes.Add(new LinearAxis
            {
                Title = "Time (second)",
                Position = AxisPosition.Bottom
            });

            PlotLBCF.Series.Add(SeriesLBCF);

            MyModelLBCF.InvalidatePlot(true);



            for (int i = 0; i < wristList.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesWristPercentile.Points.Add(new DataPoint(time, wristList[i]));
            }

            for (int i = 0; i < elbowList.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesElbowPercentile.Points.Add(new DataPoint(time, elbowList[i]));
            }

            for (int i = 0; i < shoulderList.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesShoulderPercentile.Points.Add(new DataPoint(time, shoulderList[i]));
            }

            for (int i = 0; i < torsoList.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesTorsoPercentile.Points.Add(new DataPoint(time, torsoList[i]));
            }

            for (int i = 0; i < hipList.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesHipPercentile.Points.Add(new DataPoint(time, hipList[i]));
            }

            for (int i = 0; i < kneeList.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesKneePercentile.Points.Add(new DataPoint(time, kneeList[i]));
            }

            for (int i = 0; i < ankleList.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesAnklePercentile.Points.Add(new DataPoint(time, ankleList[i]));
            }



            for (int i = 0; i < wristListF.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesWristPercentileF.Points.Add(new DataPoint(time, wristListF[i]));
            }

            for (int i = 0; i < elbowListF.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesElbowPercentileF.Points.Add(new DataPoint(time, elbowListF[i]));
            }

            for (int i = 0; i < shoulderListF.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesShoulderPercentileF.Points.Add(new DataPoint(time, shoulderListF[i]));
            }

            for (int i = 0; i < torsoListF.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesTorsoPercentileF.Points.Add(new DataPoint(time, torsoListF[i]));
            }

            for (int i = 0; i < hipListF.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesHipPercentileF.Points.Add(new DataPoint(time, hipListF[i]));
            }

            for (int i = 0; i < kneeListF.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesKneePercentileF.Points.Add(new DataPoint(time, kneeListF[i]));
            }

            for (int i = 0; i < ankleListF.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesAnklePercentileF.Points.Add(new DataPoint(time, ankleListF[i]));
            }

            PlotPercentile.Axes.Add(new LinearAxis
            {
                Title = "Strength Percent Capable (%)",
                Position = AxisPosition.Left,
                AbsoluteMaximum = 100,
                AbsoluteMinimum = 0
                //MajorGridlineStyle = LineStyle.Automatic,
                //MinorGridlineStyle = LineStyle.Automatic
            });

            PlotPercentile.Axes.Add(new LinearAxis
            {
                Title = "Time (second)",
                Position = AxisPosition.Bottom
            });

            PlotPercentile.LegendPosition = LegendPosition.RightBottom;
            PlotPercentile.LegendBorder = OxyColors.Black;
            PlotPercentile.LegendBackground = OxyColors.White;

            ResetPlotPercentile();

            TimelineSlider.IsEnabled = true;
            var focusTime = Convert.ToInt32(TimelineSlider.Value);

            UpdatePercentileValues(focusTime);


            PlotSkeleton.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 2.0,
                IsAxisVisible = false
            });
            PlotSkeleton.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = -0.5,
                Maximum = 1.5,
                IsAxisVisible = false
            });

            PlotSkeleton.LegendPosition = LegendPosition.RightBottom;
            PlotSkeleton.LegendBorder = OxyColors.SlateGray;
            PlotSkeleton.LegendBackground = OxyColors.White;

            PlotSkeleton.Series.Add(SeriesHead);
            PlotSkeleton.Series.Add(SeriesTorso);
            PlotSkeleton.Series.Add(SeriesUpperLeg);
            PlotSkeleton.Series.Add(SeriesLowerLeg);
            PlotSkeleton.Series.Add(SeriesUpperArm);
            PlotSkeleton.Series.Add(SeriesForearm);

            PlotSkeleton.Annotations.Add(AnnotationHead);
            PlotSkeleton.Annotations.Add(AnnotationRightUpperLeg);
            PlotSkeleton.Annotations.Add(AnnotationRightLowerLeg);
            PlotSkeleton.Annotations.Add(AnnotationRightFoot);
            PlotSkeleton.Annotations.Add(AnnotationRightUpperArm);
            PlotSkeleton.Annotations.Add(AnnotationRightForearm);
            PlotSkeleton.Annotations.Add(AnnotationRightHand);

            ResetPlotSkeleton(focusTime + 1);
            MyModelSkeleton.InvalidatePlot(true);

            #endregion
        }



        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void TimelineSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var focusTime = Convert.ToInt32(TimelineSlider.Value);
            var realTime = Convert.ToDouble(focusTime) / 240f;
            TimelineText.Text = realTime.ToString("N2");

            AnnotationTimeLBCF.X = realTime;
            AnnotationTimePercentile.X = realTime; 

            MyModelLBCF.InvalidatePlot(true);
            MyModelPercentile.InvalidatePlot(true);

            ResetPlotSkeleton(focusTime);
            
            MyModelSkeleton.InvalidatePlot(true);

            UpdatePercentileValues(focusTime);
        }

        private void UpdatePercentileValues(int focusTime)
        {
            if (MaleRadioButton.IsChecked == true)
            {
                WristSlider.Value = wristList[focusTime];
                ElbowSlider.Value = elbowList[focusTime];
                ShoulderSlider.Value = shoulderList[focusTime];
                TorsoSlider.Value = torsoList[focusTime];
                HipSlider.Value = hipList[focusTime];
                KneeSlider.Value = kneeList[focusTime];
                AnkleSlider.Value = ankleList[focusTime];

                WristPercentTextBlock.Text = wristList[focusTime].ToString("N0") + " %"; 
                ElbowPercentTextBlock.Text = elbowList[focusTime].ToString("N0") + " %";
                ShoulderPercentTextBlock.Text = shoulderList[focusTime].ToString("N0") + " %";
                TorsoPercentTextBlock.Text = torsoList[focusTime].ToString("N0") + " %";
                HipPercentTextBlock.Text = hipList[focusTime].ToString("N0") + " %";
                KneePercentTextBlock.Text = kneeList[focusTime].ToString("N0") + " %";
                AnklePercentTextBlock.Text = ankleList[focusTime].ToString("N0") + " %";
            }

            else
            {
                WristSlider.Value = wristListF[focusTime];
                ElbowSlider.Value = elbowListF[focusTime];
                ShoulderSlider.Value = shoulderListF[focusTime];
                TorsoSlider.Value = torsoListF[focusTime];
                HipSlider.Value = hipListF[focusTime];
                KneeSlider.Value = kneeListF[focusTime];
                AnkleSlider.Value = ankleListF[focusTime];

                WristPercentTextBlock.Text = wristListF[focusTime].ToString("N0") + " %";
                ElbowPercentTextBlock.Text = elbowListF[focusTime].ToString("N0") + " %";
                ShoulderPercentTextBlock.Text = shoulderListF[focusTime].ToString("N0") + " %";
                TorsoPercentTextBlock.Text = torsoListF[focusTime].ToString("N0") + " %";
                HipPercentTextBlock.Text = hipListF[focusTime].ToString("N0") + " %";
                KneePercentTextBlock.Text = kneeListF[focusTime].ToString("N0") + " %";
                AnklePercentTextBlock.Text = ankleListF[focusTime].ToString("N0") + " %";
            }
        }

        
        private void MaleRadioButton_OnClick(object sender, RoutedEventArgs e)
        {
            ResetPlotPercentile();
        }

        private void FemaleRadioButton_OnClick(object sender, RoutedEventArgs e)
        {
            ResetPlotPercentile();
        }

        private void WristCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            ResetPlotPercentile();
        }

        private void ElbowCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            ResetPlotPercentile();
        }

        private void ShoulderCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            ResetPlotPercentile();
        }

        private void TorsoCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            ResetPlotPercentile();
        }

        private void HipCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            ResetPlotPercentile();
        }

        private void KneeCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            ResetPlotPercentile();
        }

        private void AnkleCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            ResetPlotPercentile();
        }

        private void ResetPlotPercentile()
        {
            PlotPercentile.Series.Clear();

            if (MaleRadioButton.IsChecked == true)
            {
                if (WristCheckBox.IsChecked == true) PlotPercentile.Series.Add(SeriesWristPercentile);
                if (ElbowCheckBox.IsChecked == true) PlotPercentile.Series.Add(SeriesElbowPercentile);
                if (ShoulderCheckBox.IsChecked == true) PlotPercentile.Series.Add(SeriesShoulderPercentile);
                if (TorsoCheckBox.IsChecked == true) PlotPercentile.Series.Add(SeriesTorsoPercentile);
                if (HipCheckBox.IsChecked == true) PlotPercentile.Series.Add(SeriesHipPercentile);
                if (KneeCheckBox.IsChecked == true) PlotPercentile.Series.Add(SeriesKneePercentile);
                if (AnkleCheckBox.IsChecked == true) PlotPercentile.Series.Add(SeriesAnklePercentile);
            }

            else
            {
                if (WristCheckBox.IsChecked == true) PlotPercentile.Series.Add(SeriesWristPercentileF);
                if (ElbowCheckBox.IsChecked == true) PlotPercentile.Series.Add(SeriesElbowPercentileF);
                if (ShoulderCheckBox.IsChecked == true) PlotPercentile.Series.Add(SeriesShoulderPercentileF);
                if (TorsoCheckBox.IsChecked == true) PlotPercentile.Series.Add(SeriesTorsoPercentileF);
                if (HipCheckBox.IsChecked == true) PlotPercentile.Series.Add(SeriesHipPercentileF);
                if (KneeCheckBox.IsChecked == true) PlotPercentile.Series.Add(SeriesKneePercentileF);
                if (AnkleCheckBox.IsChecked == true) PlotPercentile.Series.Add(SeriesAnklePercentileF);
            }

            MyModelPercentile.InvalidatePlot(true);
        }

        private void ResetPlotSkeleton(int focusTime)
        {
            var headX = positionData[Tuple.Create(Joints("Head"), Angles("x"))][focusTime];
            var headZ = positionData[Tuple.Create(Joints("Head"), Angles("z"))][focusTime];

            var upperArmX = positionData[Tuple.Create(Joints("RightUpperArm"), Angles("x"))][focusTime];
            var upperArmZ = positionData[Tuple.Create(Joints("RightUpperArm"), Angles("z"))][focusTime];

            var forearmX = positionData[Tuple.Create(Joints("RightForearm"), Angles("x"))][focusTime];
            var forearmZ = positionData[Tuple.Create(Joints("RightForearm"), Angles("z"))][focusTime];

            var handX = positionData[Tuple.Create(Joints("RightHand"), Angles("x"))][focusTime];
            var handZ = positionData[Tuple.Create(Joints("RightHand"), Angles("z"))][focusTime];

            var upperLegX = positionData[Tuple.Create(Joints("RightUpperLeg"), Angles("x"))][focusTime];
            var upperLegZ = positionData[Tuple.Create(Joints("RightUpperLeg"), Angles("z"))][focusTime];

            var lowerLegX = positionData[Tuple.Create(Joints("RightLowerLeg"), Angles("x"))][focusTime];
            var lowerLegZ = positionData[Tuple.Create(Joints("RightLowerLeg"), Angles("z"))][focusTime];

            var footX = positionData[Tuple.Create(Joints("RightFoot"), Angles("x"))][focusTime];
            var footZ = positionData[Tuple.Create(Joints("RightFoot"), Angles("z"))][focusTime];


            AnnotationHead.X = headX;
            AnnotationHead.Y = headZ;

            AnnotationRightUpperArm.X = upperArmX;
            AnnotationRightUpperArm.Y = upperArmZ;

            AnnotationRightForearm.X = forearmX;
            AnnotationRightForearm.Y = forearmZ;

            AnnotationRightHand.X = handX;
            AnnotationRightHand.Y = handZ;

            AnnotationRightUpperLeg.X = upperLegX;
            AnnotationRightUpperLeg.Y = upperLegZ;

            AnnotationRightLowerLeg.X = lowerLegX;
            AnnotationRightLowerLeg.Y = lowerLegZ;

            AnnotationRightFoot.X = footX;
            AnnotationRightFoot.Y = footZ;


            SeriesHead.Points.Clear();
            SeriesUpperArm.Points.Clear();
            SeriesForearm.Points.Clear();
            SeriesTorso.Points.Clear();
            SeriesUpperLeg.Points.Clear();
            SeriesLowerLeg.Points.Clear();

            var scatterHead = new ScatterPoint(headX, headZ);
            var scatterUpperArm = new ScatterPoint(upperArmX, upperArmZ);
            var scatterForearm = new ScatterPoint(forearmX, forearmZ);
            var scatterHand = new ScatterPoint(handX, handZ);
            var scatterUpperLeg = new ScatterPoint(upperLegX, upperLegZ);
            var scatterLowerLeg = new ScatterPoint(lowerLegX, lowerLegZ);
            var scatterFoot = new ScatterPoint(footX, footZ);

            int dots = 30;
            SplitLine(scatterHead, scatterUpperArm, dots, SeriesHead.Points);
            SplitLine(scatterUpperArm, scatterForearm, dots, SeriesUpperArm.Points);
            SplitLine(scatterForearm, scatterHand, dots, SeriesForearm.Points);
            SplitLine(scatterUpperArm, scatterUpperLeg, dots, SeriesTorso.Points);
            SplitLine(scatterUpperLeg, scatterLowerLeg, dots, SeriesUpperLeg.Points);
            SplitLine(scatterLowerLeg, scatterFoot, dots, SeriesLowerLeg.Points);

        }

        private void SplitLine( ScatterPoint a, ScatterPoint b, int count, List<ScatterPoint> points)
        {
            count = count + 1;

            var d = Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y)) / count;
            var fi = Math.Atan2(b.Y - a.Y, b.X - a.X);
            
            for (int i = 0; i <= count; ++i)
                points.Add(new ScatterPoint(a.X + i * d * Math.Cos(fi), a.Y + i * d * Math.Sin(fi)));
        }

        private void ExportButton_OnClick(object sender, RoutedEventArgs e)
        {
           
        }


    }
    
    public static class LINQExtension
    {
        public static double Median(this IEnumerable<double> source)
        {
            if (source.Count() == 0)
            {
                throw new InvalidOperationException("Cannot compute median for an empty set.");
            }

            var sortedList = from number in source
                orderby number
                select number;

            int itemIndex = (int)sortedList.Count() / 2;

            if (sortedList.Count() % 2 == 0)
            {
                // Even number of items.
                return (sortedList.ElementAt(itemIndex) + sortedList.ElementAt(itemIndex - 1)) / 2;
            }
            else
            {
                // Odd number of items.
                return sortedList.ElementAt(itemIndex);
            }
        }
    }
}
