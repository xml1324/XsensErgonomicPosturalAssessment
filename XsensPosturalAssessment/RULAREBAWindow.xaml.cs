using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Axes;
using LineSeries = OxyPlot.Series.LineSeries;

namespace XsensPosturalAssessment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RULAREBAWindow : Window
    {

        OpenFileDialog dlgOpen = new OpenFileDialog();

        Dictionary<Tuple<int,int>, List<double>> jointAngleData = new Dictionary<Tuple<int, int>, List<double>>();

        Dictionary<int, string> jointName = new Dictionary<int, string>();
        Dictionary<int, string> axisName = new Dictionary<int, string>();

        string[][] data;

        #region List of Angles and Scores

        List<double> upperArmLeftFlexionList = new List<double>();
        List<double> upperArmLeftAbductionList = new List<double>();
        List<double> upperArmLeftRotationList = new List<double>();
        List<double> upperArmLeftElevationList = new List<double>();
        List<double> upperArmRightFlexionList = new List<double>();
        List<double> upperArmRightAbductionList = new List<double>();
        List<double> upperArmRightRotationList = new List<double>();
        List<double> upperArmRightElevationList = new List<double>();

        List<double> lowerArmLeftFlexionList = new List<double>();
        List<double> lowerArmRightFlexionList = new List<double>();

        List<double> wristLeftFlexionList = new List<double>();
        List<double> wristLeftDeviationList = new List<double>();
        List<double> wristLeftRotationList = new List<double>();
        List<double> wristRightFlexionList = new List<double>();
        List<double> wristRightDeviationList = new List<double>();
        List<double> wristRightRotationList = new List<double>();

        List<double> neckFlexionList = new List<double>();
        List<double> neckBendingList = new List<double>();
        List<double> neckRotationList = new List<double>();
        
        List<double> trunkFlexionList = new List<double>();
        List<double> trunkBendingList = new List<double>();
        List<double> trunkRotationList = new List<double>();

        List<double> legLeftFlexionList = new List<double>();
        List<double> legRightFlexionList = new List<double>();

        List<int> upperArmRulaScoreList = new List<int>();
        List<int> lowerArmRulaScoreList = new List<int>();
        List<int> wristRulaScoreList = new List<int>();
        List<int> wristTwistRulaScoreList = new List<int>();
        List<int> neckRulaScoreList = new List<int>();
        List<int> trunkRulaScoreList = new List<int>();
        List<int> legRulaScoreList = new List<int>();

        List<int> muscleAScoreList = new List<int>();
        List<int> forceAScoreList = new List<int>();
        List<int> muscleBScoreList = new List<int>();
        List<int> forceBScoreList = new List<int>();
        List<int> grandRulaScoreList = new List<int>();

        List<int> upperArmRebaScoreList = new List<int>();
        List<int> lowerArmRebaScoreList = new List<int>();
        List<int> wristRebaScoreList = new List<int>();
        List<int> neckRebaScoreList = new List<int>();
        List<int> trunkRebaScoreList = new List<int>();
        List<int> legRebaScoreList = new List<int>();

        List<int> couplingScoreList = new List<int>();
        List<int> loadForceScoreList = new List<int>();
        List<int> activityScoreList = new List<int>();
        
        List<int> grandRebaScoreList = new List<int>();

        List<int> truncatedRula = new List<int>();
        List<int> truncatedReba = new List<int>();

        #endregion

        private int startTime = 1;


        int upperArmSupportRulaScore = 0,
            lowerArmMidlineRulaScore = 0,
            legSupportRulaScore = 1,
            muscleARulaScore = 0,
            forceARulaScore = 0,
            muscleBRulaScore = 0,
            forceBRulaScore = 0,

            upperArmSupportRebaScore = 0,
            legSupportRebaScore = 0,
            loadForceRebaScore = 0,
            shockRebaScore = 0,
            couplingRebaScore = 0,
            activity1RebaScore = 0,
            activity2RebaScore = 0,
            activity3RebaScore = 0;

        bool trunkSupportRulaBool = false,
             legSittingRebaBool = false;

        #region Angle Threshold

        private float upperArmAbductionThreshold = 180f/3f; // Ref: Range of motion evaluation chart from Dept. of Social & Health Services (DSHS) in Washington state
        private float upperArmRotationThreshold = 131f/3f; // Ref: Occupational Biomechanics book p56 Table 4.2
        private float upperArmElevationThreshold = 30f/3f; // Ref: Pilot test
        private float wristDeviationThreshold = 50f/3f; // Ref: DSHS
        private float wristRotationThreshold = 80f/3f; // Ref: DSHS
        private float neckBendingThreshold = 45f/3f; // Ref: DSHS
        private float neckRotationThreshold = 80f/3f; // Ref: DSHS
        private float trunkBendingThreshold = 30f/3f; // Ref: DSHS
        private float trunkRotationThreshold = 25f/3f; // Ref: http://pjroxburgh.tripod.com/new_page_5.htm


        #endregion



        public RULAREBAWindow()
        {
            this.MyModelRula = PlotRula;
            this.MyModelReba = PlotReba;
            this.DataContext = this;

            InitializeComponent();

            dlgOpen.DefaultExt = ".csv"; // Default file extension
            dlgOpen.Filter = "csv files (.csv)|*.csv"; // Filter files by extension

            #region Joint and Axis Names

            jointName.Add(0, "L5S1");
            jointName.Add(1, "L4L3");
            jointName.Add(2, "L1T12");
            jointName.Add(3, "T9T8");
            jointName.Add(4, "T1C7");
            jointName.Add(5, "C1Head");
            jointName.Add(6, "RightC7Shoulder");
            jointName.Add(7, "RightShoulder");
            jointName.Add(8, "RightElbow");
            jointName.Add(9, "RightWrist");
            jointName.Add(10, "LeftC7Shoulder");
            jointName.Add(11, "LeftShoulder");
            jointName.Add(12, "LeftElbow");
            jointName.Add(13, "LeftWrist");
            jointName.Add(14, "RightHip");
            jointName.Add(15, "RightKnee");
            jointName.Add(16, "RightAnkle");
            jointName.Add(17, "RightBallFoot");
            jointName.Add(18, "LeftHip");
            jointName.Add(19, "LeftKnee");
            jointName.Add(20, "LeftAnkle");
            jointName.Add(21, "LeftBallFoot");

            axisName.Add(0, "x");
            axisName.Add(1, "y");
            axisName.Add(2, "z");

            #endregion

        }

        // Translate joint name to number for easier coding
        private int Joints(string joint)
        {
            switch (joint)
            {
                case "L5S1": return 0;
                case "L4L3": return 1;
                case "L1T12": return 2;
                case "T9T8": return 3;
                case "T1C7": return 4;
                case "C1Head": return 5;
                case "RightC7Shoulder": return 6;
                case "RightShoulder": return 7;
                case "RightElbow": return 8;
                case "RightWrist": return 9;
                case "LeftC7Shoulder": return 10;
                case "LeftShoulder": return 11;
                case "LeftElbow": return 12;
                case "LeftWrist": return 13;
                case "RightHip": return 14;
                case "RightKnee": return 15;
                case "RightAnkle": return 16;
                case "RightBallFoot": return 17;
                case "LeftHip": return 18;
                case "LeftKnee": return 19;
                case "LeftAnkle": return 20;
                case "LeftBallFoot": return 21;
                default: return 21;
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

        // Import Xsens data
        private void OpenButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (jointAngleData.Count != 0) jointAngleData.Clear();

            for (int i = 0; i < jointName.Count; i++)
            {
                for (int j = 0; j < axisName.Count; j++)
                {
                    jointAngleData.Add(Tuple.Create(i, j), new List<double>());
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
                                jointAngleData[Tuple.Create(i, j)].Add(double.Parse(data[k][i * axisName.Count + j]));
                            }
                            catch (Exception)
                            {
                                jointAngleData[Tuple.Create(i, j)].Add(0);
                            }
                        }
                    }
                }

                // Enable buttons when data is imported.
                AnalyzeButton.IsEnabled = true;
                ExportButton.IsEnabled = true;
                MaxButton.IsEnabled = true;
                TimelineSlider.IsEnabled = true;

                TimelineSlider.Maximum = data.GetLength(0) - 2;
            }




        }

        private void ClearLists()
        {
            PlotRula.Series.Clear();
            PlotRula.Axes.Clear();
            PlotReba.Series.Clear();
            PlotReba.Axes.Clear();

            upperArmLeftFlexionList.Clear();
            upperArmLeftAbductionList.Clear();
            upperArmLeftRotationList.Clear();
            upperArmLeftElevationList.Clear();
            upperArmRightFlexionList.Clear();
            upperArmRightAbductionList.Clear();
            upperArmRightRotationList.Clear();
            upperArmRightElevationList.Clear();

            lowerArmLeftFlexionList.Clear();
            lowerArmRightFlexionList.Clear();

            wristLeftFlexionList.Clear();
            wristLeftDeviationList.Clear();
            wristLeftRotationList.Clear();
            wristRightFlexionList.Clear();
            wristRightDeviationList.Clear();
            wristRightRotationList.Clear();

            neckFlexionList.Clear();
            neckBendingList.Clear();
            neckRotationList.Clear();

            trunkFlexionList.Clear();
            trunkBendingList.Clear();
            trunkRotationList.Clear();

            legLeftFlexionList.Clear();
            legRightFlexionList.Clear();

            upperArmRulaScoreList.Clear();
            lowerArmRulaScoreList.Clear();
            wristRulaScoreList.Clear();
            wristTwistRulaScoreList.Clear();
            neckRulaScoreList.Clear();
            trunkRulaScoreList.Clear();
            legRulaScoreList.Clear();

            muscleAScoreList.Clear();
            forceAScoreList.Clear();
            muscleBScoreList.Clear();
            forceBScoreList.Clear();
            grandRulaScoreList.Clear();

            upperArmRebaScoreList.Clear();
            lowerArmRebaScoreList.Clear();
            wristRebaScoreList.Clear();
            neckRebaScoreList.Clear();
            trunkRebaScoreList.Clear();
            legRebaScoreList.Clear();

            couplingScoreList.Clear();
            loadForceScoreList.Clear();
            activityScoreList.Clear();

            grandRebaScoreList.Clear();
        }


        private void AnalyzeButton_OnClick(object sender, RoutedEventArgs e)
        {
            ClearLists();

            // Calculating and creating lists of angles and scores
            for (int i = 0; i < data.GetLength(0); i++)
            {
                // Upper Arms

                // Flexion
                var upperArmLeftFlexion = jointAngleData[Tuple.Create(Joints("LeftShoulder"), Angles("z"))][i];
                var upperArmRightFlexion = jointAngleData[Tuple.Create(Joints("RightShoulder"), Angles("z"))][i];

                upperArmLeftFlexionList.Add(upperArmLeftFlexion);
                upperArmRightFlexionList.Add(upperArmRightFlexion);

                int upperArmLeftFlexionScore = 0, upperArmRightFlexionScore = 0;

                if ((0 <= upperArmLeftFlexion && upperArmLeftFlexion < 20) || (upperArmLeftFlexion < 0 && -20 <= upperArmLeftFlexion)) upperArmLeftFlexionScore = 1;
                else if ((20 <= upperArmLeftFlexion && upperArmLeftFlexion < 45) || (upperArmLeftFlexion < -20)) upperArmLeftFlexionScore = 2;
                else if (45 <= upperArmLeftFlexion && upperArmLeftFlexion < 90) upperArmLeftFlexionScore = 3;
                else if (90 <= upperArmLeftFlexion) upperArmLeftFlexionScore = 4;

                if ((0 <= upperArmRightFlexion && upperArmRightFlexion < 20) || (upperArmRightFlexion < 0 && -20 <= upperArmRightFlexion)) upperArmRightFlexionScore = 1;
                else if ((20 <= upperArmRightFlexion && upperArmRightFlexion < 45) || (upperArmRightFlexion < -20)) upperArmRightFlexionScore = 2;
                else if (45 <= upperArmRightFlexion && upperArmRightFlexion < 90) upperArmRightFlexionScore = 3;
                else if (90 <= upperArmRightFlexion) upperArmRightFlexionScore = 4;

                int upperArmFlexionScore = Math.Max(upperArmLeftFlexionScore, upperArmRightFlexionScore);

                // Abduction
                var upperArmLeftAbduction = jointAngleData[Tuple.Create(Joints("LeftShoulder"), Angles("x"))][i];
                var upperArmRightAbduction = jointAngleData[Tuple.Create(Joints("RightShoulder"), Angles("x"))][i];

                upperArmLeftAbductionList.Add(upperArmLeftAbduction);
                upperArmRightAbductionList.Add(upperArmRightAbduction);

                int upperArmAbductionScore = AddIf(upperArmLeftAbduction, upperArmRightAbduction, upperArmAbductionThreshold);

                // Rotation
                var upperArmLeftRotation = jointAngleData[Tuple.Create(Joints("LeftShoulder"), Angles("y"))][i];
                var upperArmRightRotation = jointAngleData[Tuple.Create(Joints("RightShoulder"), Angles("y"))][i];

                upperArmLeftRotationList.Add(upperArmLeftRotation);
                upperArmRightRotationList.Add(upperArmRightRotation);

                int upperArmRotationScore = AddIf(upperArmLeftRotation, upperArmRightRotation, upperArmRotationThreshold);

                // Elevation
                var upperArmLeftElevation = jointAngleData[Tuple.Create(Joints("LeftC7Shoulder"), Angles("x"))][i];
                var upperArmRightElevation = jointAngleData[Tuple.Create(Joints("RightC7Shoulder"), Angles("x"))][i];

                upperArmLeftElevationList.Add(upperArmLeftElevation);
                upperArmRightElevationList.Add(upperArmRightElevation);

                int upperArmElevationScore = AddIf(upperArmLeftElevation, upperArmRightElevation, upperArmElevationThreshold);

                // Scoring
                var upperArmRulaScore = upperArmFlexionScore + upperArmAbductionScore + upperArmElevationScore + upperArmSupportRulaScore;
                var upperArmAbductionRotationScore = Math.Max(upperArmAbductionScore, upperArmRotationScore);
                var upperArmRebaScore = upperArmFlexionScore + upperArmAbductionRotationScore + upperArmElevationScore + upperArmSupportRebaScore;

                upperArmRulaScoreList.Add(upperArmRulaScore);
                upperArmRebaScoreList.Add(upperArmRebaScore);


                // Lower Arm

                // Flexion
                var lowerArmLeftFlexion = jointAngleData[Tuple.Create(Joints("LeftElbow"), Angles("z"))][i];
                var lowerArmRightFlexion = jointAngleData[Tuple.Create(Joints("RightElbow"), Angles("z"))][i];

                lowerArmLeftFlexionList.Add(lowerArmLeftFlexion);
                lowerArmRightFlexionList.Add(lowerArmRightFlexion);

                int lowerArmLeftFlexionScore = 0, lowerArmRightFlexionScore = 0;

                if (60 <= lowerArmLeftFlexion && lowerArmLeftFlexion < 100) lowerArmLeftFlexionScore = 1;
                else if (100 <= lowerArmLeftFlexion || lowerArmLeftFlexion < 60) lowerArmLeftFlexionScore = 2;

                if (60 <= lowerArmRightFlexion && lowerArmRightFlexion < 100) lowerArmRightFlexionScore = 1;
                else if (100 <= lowerArmRightFlexion || lowerArmRightFlexion < 60) lowerArmRightFlexionScore = 2;

                int lowerArmFlexionScore = Math.Max(lowerArmLeftFlexionScore, lowerArmRightFlexionScore);

                // Scoring
                var lowerArmRulaScore = lowerArmFlexionScore + lowerArmMidlineRulaScore;
                var lowerArmRebaScore = lowerArmFlexionScore;

                lowerArmRulaScoreList.Add(lowerArmRulaScore);
                lowerArmRebaScoreList.Add(lowerArmRebaScore);


                // Wrist

                // Flexion
                var wristLeftFlexion = jointAngleData[Tuple.Create(Joints("LeftWrist"), Angles("z"))][i];
                var wristRightFlexion = jointAngleData[Tuple.Create(Joints("RightWrist"), Angles("z"))][i];

                wristLeftFlexionList.Add(wristLeftFlexion);
                wristRightFlexionList.Add(wristRightFlexion);

                int wristLeftFlexionRulaScore = 0, wristRightFlexionRulaScore = 0;

                if (wristLeftFlexion < 5 && -5 <= wristLeftFlexion) wristLeftFlexionRulaScore = 1;
                else if ((5 <= wristLeftFlexion && wristLeftFlexion < 15) || (wristLeftFlexion < -5 && -15 <= wristLeftFlexion)) wristLeftFlexionRulaScore = 2;
                else if (15 <= wristLeftFlexion || -15 > wristLeftFlexion) wristLeftFlexionRulaScore = 3;

                if (wristRightFlexion < 5 && -5 <= wristRightFlexion) wristRightFlexionRulaScore = 1;
                else if ((5 <= wristRightFlexion && wristRightFlexion < 15) || (wristRightFlexion < -5 && -15 <= wristRightFlexion)) wristRightFlexionRulaScore = 2;
                else if (15 <= wristRightFlexion || -15 > wristRightFlexion) wristRightFlexionRulaScore = 3;

                int wristFlexionRulaScore = Math.Max(wristLeftFlexionRulaScore, wristRightFlexionRulaScore);

                int wristLeftFlexionRebaScore = 0, wristRightFlexionRebaScore = 0;

                if (wristLeftFlexion < 15 && -15 <= wristLeftFlexion) wristLeftFlexionRebaScore = 1;
                else if (15 <= wristLeftFlexion || -15 > wristLeftFlexion) wristLeftFlexionRebaScore = 2;

                if (wristRightFlexion < 15 && -15 <= wristRightFlexion) wristRightFlexionRebaScore = 1;
                else if (15 <= wristRightFlexion || -15 > wristRightFlexion) wristRightFlexionRebaScore = 2;

                int wristFlexionRebaScore = Math.Max(wristLeftFlexionRebaScore, wristRightFlexionRebaScore);

                // Deviation
                var wristLeftDeviation = jointAngleData[Tuple.Create(Joints("LeftWrist"), Angles("x"))][i];
                var wristRightDeviation = jointAngleData[Tuple.Create(Joints("RightWrist"), Angles("x"))][i];

                wristLeftDeviationList.Add(wristLeftDeviation);
                wristRightDeviationList.Add(wristRightDeviation);

                int wristDeviationScore = AddIf(wristLeftDeviation, wristRightDeviation, wristDeviationThreshold);

                // Rotation
                var wristLeftRotation = jointAngleData[Tuple.Create(Joints("LeftWrist"), Angles("y"))][i];
                var wristRightRotation = jointAngleData[Tuple.Create(Joints("RightWrist"), Angles("y"))][i];

                wristLeftRotationList.Add(wristLeftRotation);
                wristRightRotationList.Add(wristRightRotation);

                int wristRotationScore = AddIf(wristLeftRotation, wristRightRotation, wristRotationThreshold);

                // Scoring
                var wristRulaScore = wristFlexionRulaScore + wristDeviationScore;
                var wristTwistRulaScore = wristRotationScore + 1;
                var wristDeviationRotationScore = Math.Max(wristDeviationScore, wristRotationScore);
                var wristRebaScore = wristFlexionRebaScore + wristDeviationRotationScore;

                wristRulaScoreList.Add(wristRulaScore);
                wristTwistRulaScoreList.Add(wristTwistRulaScore);
                wristRebaScoreList.Add(wristRebaScore);


                // Neck

                // Flexion
                var neckFlexion = jointAngleData[Tuple.Create(Joints("C1Head"), Angles("z"))][i];

                neckFlexionList.Add(neckFlexion);

                int neckFlexionRulaScore = 0;
                if (-10 <= neckFlexion && neckFlexion < 10) neckFlexionRulaScore = 1;
                else if (10 <= neckFlexion && neckFlexion < 20) neckFlexionRulaScore = 2;
                else if (20 <= neckFlexion) neckFlexionRulaScore = 3;
                else if (neckFlexion < -10) neckFlexionRulaScore = 4;

                int neckFlexionRebaScore = 0;
                if (0 <= neckFlexion && neckFlexion < 20) neckFlexionRebaScore = 1;
                else if (20 <= neckFlexion || neckFlexion < 0) neckFlexionRebaScore = 2;

                // Bending
                var neckBending = jointAngleData[Tuple.Create(Joints("C1Head"), Angles("x"))][i];

                neckBendingList.Add(neckBending);

                int neckBendingScore = AddIf(neckBending, neckBendingThreshold);

                // Rotation
                var neckRotation = jointAngleData[Tuple.Create(Joints("C1Head"), Angles("y"))][i];

                neckRotationList.Add(neckRotation);

                int neckRotationScore = AddIf(neckRotation, neckRotationThreshold);

                // Scoringop
                var neckRulaScore = neckFlexionRulaScore + neckBendingScore + neckRotationScore;
                var neckBendingRotationScore = Math.Max(neckBendingScore, neckRotationScore);
                var neckRebaScore = neckFlexionRebaScore + neckBendingRotationScore;

                neckRulaScoreList.Add(neckRulaScore);
                neckRebaScoreList.Add(neckRebaScore);


                // Trunk

                // Flexion
                var trunkFlexion = jointAngleData[Tuple.Create(Joints("L5S1"), Angles("z"))][i]
                                   + jointAngleData[Tuple.Create(Joints("L4L3"), Angles("z"))][i]
                                   + jointAngleData[Tuple.Create(Joints("L1T12"), Angles("z"))][i]
                                   + jointAngleData[Tuple.Create(Joints("T9T8"), Angles("z"))][i];

                trunkFlexionList.Add(trunkFlexion);

                int trunkFlexionScore = 0;
                if ((0 <= trunkFlexion && trunkFlexion < 10) || (trunkFlexion < 0 && -10 <= trunkFlexion)) trunkFlexionScore = 1;
                else if ((10 <= trunkFlexion && trunkFlexion < 20) || (trunkFlexion < -10 && -20 <= trunkFlexion)) trunkFlexionScore = 2;
                else if ((20 <= trunkFlexion && trunkFlexion < 60) || trunkFlexion < -20) trunkFlexionScore = 3;
                else if (60 <= trunkFlexion) trunkFlexionScore = 4;

                // Bending
                var trunkBending = jointAngleData[Tuple.Create(Joints("L5S1"), Angles("x"))][i];

                trunkBendingList.Add(trunkBending);

                int trunkBendingScore = AddIf(trunkBending, trunkBendingThreshold);

                // Rotation
                var trunkRotation = jointAngleData[Tuple.Create(Joints("L5S1"), Angles("y"))][i];

                trunkRotationList.Add(trunkRotation);

                int trunkRotationScore = AddIf(trunkRotation, trunkRotationThreshold);

                // Scoring
                var trunkRulaScore = trunkSupportRulaBool ? 1 : trunkFlexionScore + trunkBendingScore + trunkRotationScore;
                var trunkBendingRotationScore = Math.Max(trunkBendingScore, trunkRotationScore);
                var trunkRebaScore = trunkFlexionScore + trunkBendingRotationScore;

                trunkRulaScoreList.Add(trunkRulaScore);
                trunkRebaScoreList.Add(trunkRebaScore);

                // Legs

                // Flexion

                var legLeftFlexion = jointAngleData[Tuple.Create(Joints("LeftKnee"), Angles("z"))][i];
                var legRightFlexion = jointAngleData[Tuple.Create(Joints("RightKnee"), Angles("z"))][i];

                legLeftFlexionList.Add(legLeftFlexion);
                legRightFlexionList.Add(legRightFlexion);

                int legLeftFlexionScore = 0, legRightFlexionScore = 0;

                if (legLeftFlexion < 30) legLeftFlexionScore = 0;
                else if (30 <= legLeftFlexion && legLeftFlexion < 60) legLeftFlexionScore = 1;
                else if (60 <= legLeftFlexion) legLeftFlexionScore = 2;

                if (legRightFlexion < 30) legRightFlexionScore = 0;
                else if (30 <= legRightFlexion && legRightFlexion < 60) legRightFlexionScore = 1;
                else if (60 <= legRightFlexion) legRightFlexionScore = 2;

                int legFlexionScore = Math.Max(legLeftFlexionScore, legRightFlexionScore);

                // Scoring
                var legRulaScore = legSupportRulaScore;
                var legRebaScore = legSupportRebaScore + (legSittingRebaBool ? 0 : legFlexionScore);

                legRulaScoreList.Add(legRulaScore);
                legRebaScoreList.Add(legRebaScore);
                
                #region Rula table scoring
                // Rula Score A and C

                int scoreARula = 1;

                // 1
                     if (upperArmRulaScore == 1 && lowerArmRulaScore == 1 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 1;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 1 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 2;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 1 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 2;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 1 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 2;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 1 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 2;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 1 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 3;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 1 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 3;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 1 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 3;

                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 2 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 2;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 2 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 2;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 2 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 2;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 2 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 2;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 2 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 3;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 2 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 3;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 2 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 3;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 2 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 3;

                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 3 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 2;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 3 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 3;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 3 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 3;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 3 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 3;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 3 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 3;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 3 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 3;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 3 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 1 && lowerArmRulaScore == 3 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 4;

                // 2
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 1 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 2;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 1 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 3;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 1 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 3;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 1 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 3;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 1 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 3;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 1 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 1 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 1 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 4;

                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 2 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 3;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 2 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 3;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 2 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 3;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 2 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 3;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 2 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 3;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 2 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 2 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 2 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 4;

                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 3 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 3;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 3 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 3 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 3 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 3 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 3 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 3 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 5;
                else if (upperArmRulaScore == 2 && lowerArmRulaScore == 3 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 5;

                // 3
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 1 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 3;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 1 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 3;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 1 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 1 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 1 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 1 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 1 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 5;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 1 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 5;

                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 2 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 3;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 2 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 2 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 2 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 2 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 2 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 2 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 5;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 2 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 5;

                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 3 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 3 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 3 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 3 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 3 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 3 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 5;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 3 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 5;
                else if (upperArmRulaScore == 3 && lowerArmRulaScore == 3 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 5;

                // 4
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 1 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 1 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 1 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 1 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 1 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 1 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 5;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 1 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 5;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 1 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 5;

                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 2 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 2 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 2 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 2 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 2 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 2 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 5;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 2 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 5;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 2 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 5;

                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 3 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 3 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 4;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 3 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 4;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 3 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 5;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 3 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 5;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 3 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 5;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 3 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 6;
                else if (upperArmRulaScore == 4 && lowerArmRulaScore == 3 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 6;

                // 5
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 1 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 5;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 1 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 5;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 1 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 5;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 1 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 5;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 1 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 5;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 1 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 6;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 1 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 6;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 1 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 7;

                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 2 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 5;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 2 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 6;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 2 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 6;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 2 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 6;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 2 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 6;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 2 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 7;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 2 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 7;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 2 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 7;

                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 3 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 6;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 3 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 6;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 3 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 6;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 3 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 7;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 3 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 7;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 3 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 7;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 3 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 7;
                else if (upperArmRulaScore == 5 && lowerArmRulaScore == 3 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 8;

                // 6
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 1 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 7;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 1 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 7;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 1 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 7;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 1 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 7;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 1 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 7;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 1 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 8;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 1 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 8;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 1 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 9;

                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 2 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 8;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 2 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 8;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 2 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 8;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 2 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 8;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 2 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 8;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 2 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 9;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 2 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 9;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 2 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 9;

                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 3 && wristRulaScore == 1 && wristTwistRulaScore == 1) scoreARula = 9;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 3 && wristRulaScore == 1 && wristTwistRulaScore == 2) scoreARula = 9;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 3 && wristRulaScore == 2 && wristTwistRulaScore == 1) scoreARula = 9;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 3 && wristRulaScore == 2 && wristTwistRulaScore == 2) scoreARula = 9;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 3 && wristRulaScore == 3 && wristTwistRulaScore == 1) scoreARula = 9;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 3 && wristRulaScore == 3 && wristTwistRulaScore == 2) scoreARula = 9;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 3 && wristRulaScore == 4 && wristTwistRulaScore == 1) scoreARula = 9;
                else if (upperArmRulaScore == 6 && lowerArmRulaScore == 3 && wristRulaScore == 4 && wristTwistRulaScore == 2) scoreARula = 9;

                int scoreCRula = scoreARula + muscleARulaScore + forceARulaScore;

                // Rula Score B and D

                int scoreBRula = 0;

                // 1
                     if (neckRulaScore == 1 && trunkRulaScore == 1 && legRulaScore == 1) scoreBRula = 1;
                else if (neckRulaScore == 1 && trunkRulaScore == 1 && legRulaScore == 2) scoreBRula = 3;
                else if (neckRulaScore == 1 && trunkRulaScore == 2 && legRulaScore == 1) scoreBRula = 2;
                else if (neckRulaScore == 1 && trunkRulaScore == 2 && legRulaScore == 2) scoreBRula = 3;
                else if (neckRulaScore == 1 && trunkRulaScore == 3 && legRulaScore == 1) scoreBRula = 3;
                else if (neckRulaScore == 1 && trunkRulaScore == 3 && legRulaScore == 2) scoreBRula = 4;
                else if (neckRulaScore == 1 && trunkRulaScore == 4 && legRulaScore == 1) scoreBRula = 5;
                else if (neckRulaScore == 1 && trunkRulaScore == 4 && legRulaScore == 2) scoreBRula = 5;
                else if (neckRulaScore == 1 && trunkRulaScore == 5 && legRulaScore == 1) scoreBRula = 6;
                else if (neckRulaScore == 1 && trunkRulaScore == 5 && legRulaScore == 2) scoreBRula = 6;
                else if (neckRulaScore == 1 && trunkRulaScore == 6 && legRulaScore == 1) scoreBRula = 7;
                else if (neckRulaScore == 1 && trunkRulaScore == 6 && legRulaScore == 2) scoreBRula = 7;

                // 2
                else if (neckRulaScore == 2 && trunkRulaScore == 1 && legRulaScore == 1) scoreBRula = 2;
                else if (neckRulaScore == 2 && trunkRulaScore == 1 && legRulaScore == 2) scoreBRula = 3;
                else if (neckRulaScore == 2 && trunkRulaScore == 2 && legRulaScore == 1) scoreBRula = 2;
                else if (neckRulaScore == 2 && trunkRulaScore == 2 && legRulaScore == 2) scoreBRula = 3;
                else if (neckRulaScore == 2 && trunkRulaScore == 3 && legRulaScore == 1) scoreBRula = 4;
                else if (neckRulaScore == 2 && trunkRulaScore == 3 && legRulaScore == 2) scoreBRula = 5;
                else if (neckRulaScore == 2 && trunkRulaScore == 4 && legRulaScore == 1) scoreBRula = 5;
                else if (neckRulaScore == 2 && trunkRulaScore == 4 && legRulaScore == 2) scoreBRula = 5;
                else if (neckRulaScore == 2 && trunkRulaScore == 5 && legRulaScore == 1) scoreBRula = 6;
                else if (neckRulaScore == 2 && trunkRulaScore == 5 && legRulaScore == 2) scoreBRula = 7;
                else if (neckRulaScore == 2 && trunkRulaScore == 6 && legRulaScore == 1) scoreBRula = 7;
                else if (neckRulaScore == 2 && trunkRulaScore == 6 && legRulaScore == 2) scoreBRula = 7;

                // 3
                else if (neckRulaScore == 3 && trunkRulaScore == 1 && legRulaScore == 1) scoreBRula = 3;
                else if (neckRulaScore == 3 && trunkRulaScore == 1 && legRulaScore == 2) scoreBRula = 3;
                else if (neckRulaScore == 3 && trunkRulaScore == 2 && legRulaScore == 1) scoreBRula = 3;
                else if (neckRulaScore == 3 && trunkRulaScore == 2 && legRulaScore == 2) scoreBRula = 4;
                else if (neckRulaScore == 3 && trunkRulaScore == 3 && legRulaScore == 1) scoreBRula = 4;
                else if (neckRulaScore == 3 && trunkRulaScore == 3 && legRulaScore == 2) scoreBRula = 5;
                else if (neckRulaScore == 3 && trunkRulaScore == 4 && legRulaScore == 1) scoreBRula = 5;
                else if (neckRulaScore == 3 && trunkRulaScore == 4 && legRulaScore == 2) scoreBRula = 6;
                else if (neckRulaScore == 3 && trunkRulaScore == 5 && legRulaScore == 1) scoreBRula = 6;
                else if (neckRulaScore == 3 && trunkRulaScore == 5 && legRulaScore == 2) scoreBRula = 7;
                else if (neckRulaScore == 3 && trunkRulaScore == 6 && legRulaScore == 1) scoreBRula = 7;
                else if (neckRulaScore == 3 && trunkRulaScore == 6 && legRulaScore == 2) scoreBRula = 7;

                // 4
                else if (neckRulaScore == 4 && trunkRulaScore == 1 && legRulaScore == 1) scoreBRula = 5;
                else if (neckRulaScore == 4 && trunkRulaScore == 1 && legRulaScore == 2) scoreBRula = 5;
                else if (neckRulaScore == 4 && trunkRulaScore == 2 && legRulaScore == 1) scoreBRula = 5;
                else if (neckRulaScore == 4 && trunkRulaScore == 2 && legRulaScore == 2) scoreBRula = 6;
                else if (neckRulaScore == 4 && trunkRulaScore == 3 && legRulaScore == 1) scoreBRula = 6;
                else if (neckRulaScore == 4 && trunkRulaScore == 3 && legRulaScore == 2) scoreBRula = 7;
                else if (neckRulaScore == 4 && trunkRulaScore == 4 && legRulaScore == 1) scoreBRula = 7;
                else if (neckRulaScore == 4 && trunkRulaScore == 4 && legRulaScore == 2) scoreBRula = 7;
                else if (neckRulaScore == 4 && trunkRulaScore == 5 && legRulaScore == 1) scoreBRula = 7;
                else if (neckRulaScore == 4 && trunkRulaScore == 5 && legRulaScore == 2) scoreBRula = 7;
                else if (neckRulaScore == 4 && trunkRulaScore == 6 && legRulaScore == 1) scoreBRula = 8;
                else if (neckRulaScore == 4 && trunkRulaScore == 6 && legRulaScore == 2) scoreBRula = 8;

                // 5
                else if (neckRulaScore == 5 && trunkRulaScore == 1 && legRulaScore == 1) scoreBRula = 7;
                else if (neckRulaScore == 5 && trunkRulaScore == 1 && legRulaScore == 2) scoreBRula = 7;
                else if (neckRulaScore == 5 && trunkRulaScore == 2 && legRulaScore == 1) scoreBRula = 7;
                else if (neckRulaScore == 5 && trunkRulaScore == 2 && legRulaScore == 2) scoreBRula = 7;
                else if (neckRulaScore == 5 && trunkRulaScore == 3 && legRulaScore == 1) scoreBRula = 7;
                else if (neckRulaScore == 5 && trunkRulaScore == 3 && legRulaScore == 2) scoreBRula = 8;
                else if (neckRulaScore == 5 && trunkRulaScore == 4 && legRulaScore == 1) scoreBRula = 8;
                else if (neckRulaScore == 5 && trunkRulaScore == 4 && legRulaScore == 2) scoreBRula = 8;
                else if (neckRulaScore == 5 && trunkRulaScore == 5 && legRulaScore == 1) scoreBRula = 8;
                else if (neckRulaScore == 5 && trunkRulaScore == 5 && legRulaScore == 2) scoreBRula = 8;
                else if (neckRulaScore == 5 && trunkRulaScore == 6 && legRulaScore == 1) scoreBRula = 8;
                else if (neckRulaScore == 5 && trunkRulaScore == 6 && legRulaScore == 2) scoreBRula = 8;

                // 6
                else if (neckRulaScore == 6 && trunkRulaScore == 1 && legRulaScore == 1) scoreBRula = 8;
                else if (neckRulaScore == 6 && trunkRulaScore == 1 && legRulaScore == 2) scoreBRula = 8;
                else if (neckRulaScore == 6 && trunkRulaScore == 2 && legRulaScore == 1) scoreBRula = 8;
                else if (neckRulaScore == 6 && trunkRulaScore == 2 && legRulaScore == 2) scoreBRula = 8;
                else if (neckRulaScore == 6 && trunkRulaScore == 3 && legRulaScore == 1) scoreBRula = 8;
                else if (neckRulaScore == 6 && trunkRulaScore == 3 && legRulaScore == 2) scoreBRula = 8;
                else if (neckRulaScore == 6 && trunkRulaScore == 4 && legRulaScore == 1) scoreBRula = 8;
                else if (neckRulaScore == 6 && trunkRulaScore == 4 && legRulaScore == 2) scoreBRula = 9;
                else if (neckRulaScore == 6 && trunkRulaScore == 5 && legRulaScore == 1) scoreBRula = 9;
                else if (neckRulaScore == 6 && trunkRulaScore == 5 && legRulaScore == 2) scoreBRula = 9;
                else if (neckRulaScore == 6 && trunkRulaScore == 6 && legRulaScore == 1) scoreBRula = 9;
                else if (neckRulaScore == 6 && trunkRulaScore == 6 && legRulaScore == 2) scoreBRula = 9;

                int scoreDRula = scoreBRula + muscleBRulaScore + forceBRulaScore;

                // Rula grand score
                int grandRulaScore = 0;

                     if (scoreCRula == 1 && scoreDRula == 1) grandRulaScore = 1;
                else if (scoreCRula == 1 && scoreDRula == 2) grandRulaScore = 2;
                else if (scoreCRula == 1 && scoreDRula == 3) grandRulaScore = 3;
                else if (scoreCRula == 1 && scoreDRula == 4) grandRulaScore = 3;
                else if (scoreCRula == 1 && scoreDRula == 5) grandRulaScore = 4;
                else if (scoreCRula == 1 && scoreDRula == 6) grandRulaScore = 5;
                else if (scoreCRula == 1 && scoreDRula == 7) grandRulaScore = 5;

                else if (scoreCRula == 2 && scoreDRula == 1) grandRulaScore = 2;
                else if (scoreCRula == 2 && scoreDRula == 2) grandRulaScore = 2;
                else if (scoreCRula == 2 && scoreDRula == 3) grandRulaScore = 3;
                else if (scoreCRula == 2 && scoreDRula == 4) grandRulaScore = 4;
                else if (scoreCRula == 2 && scoreDRula == 5) grandRulaScore = 4;
                else if (scoreCRula == 2 && scoreDRula == 6) grandRulaScore = 5;
                else if (scoreCRula == 2 && scoreDRula >= 7) grandRulaScore = 5;

                else if (scoreCRula == 3 && scoreDRula == 1) grandRulaScore = 3;
                else if (scoreCRula == 3 && scoreDRula == 2) grandRulaScore = 3;
                else if (scoreCRula == 3 && scoreDRula == 3) grandRulaScore = 3;
                else if (scoreCRula == 3 && scoreDRula == 4) grandRulaScore = 4;
                else if (scoreCRula == 3 && scoreDRula == 5) grandRulaScore = 4;
                else if (scoreCRula == 3 && scoreDRula == 6) grandRulaScore = 5;
                else if (scoreCRula == 3 && scoreDRula >= 7) grandRulaScore = 6;

                else if (scoreCRula == 4 && scoreDRula == 1) grandRulaScore = 3;
                else if (scoreCRula == 4 && scoreDRula == 2) grandRulaScore = 3;
                else if (scoreCRula == 4 && scoreDRula == 3) grandRulaScore = 3;
                else if (scoreCRula == 4 && scoreDRula == 4) grandRulaScore = 4;
                else if (scoreCRula == 4 && scoreDRula == 5) grandRulaScore = 5;
                else if (scoreCRula == 4 && scoreDRula == 6) grandRulaScore = 6;
                else if (scoreCRula == 4 && scoreDRula >= 7) grandRulaScore = 6;

                else if (scoreCRula == 5 && scoreDRula == 1) grandRulaScore = 4;
                else if (scoreCRula == 5 && scoreDRula == 2) grandRulaScore = 4;
                else if (scoreCRula == 5 && scoreDRula == 3) grandRulaScore = 4;
                else if (scoreCRula == 5 && scoreDRula == 4) grandRulaScore = 5;
                else if (scoreCRula == 5 && scoreDRula == 5) grandRulaScore = 6;
                else if (scoreCRula == 5 && scoreDRula == 6) grandRulaScore = 7;
                else if (scoreCRula == 5 && scoreDRula >= 7) grandRulaScore = 7;

                else if (scoreCRula == 6 && scoreDRula == 1) grandRulaScore = 4;
                else if (scoreCRula == 6 && scoreDRula == 2) grandRulaScore = 4;
                else if (scoreCRula == 6 && scoreDRula == 3) grandRulaScore = 5;
                else if (scoreCRula == 6 && scoreDRula == 4) grandRulaScore = 6;
                else if (scoreCRula == 6 && scoreDRula == 5) grandRulaScore = 6;
                else if (scoreCRula == 6 && scoreDRula == 6) grandRulaScore = 7;
                else if (scoreCRula == 6 && scoreDRula >= 7) grandRulaScore = 7;

                else if (scoreCRula == 7 && scoreDRula == 1) grandRulaScore = 5;
                else if (scoreCRula == 7 && scoreDRula == 2) grandRulaScore = 5;
                else if (scoreCRula == 7 && scoreDRula == 3) grandRulaScore = 6;
                else if (scoreCRula == 7 && scoreDRula == 4) grandRulaScore = 6;
                else if (scoreCRula == 7 && scoreDRula == 5) grandRulaScore = 7;
                else if (scoreCRula == 7 && scoreDRula == 6) grandRulaScore = 7;
                else if (scoreCRula == 7 && scoreDRula >= 7) grandRulaScore = 7;

                else if (scoreCRula >= 8 && scoreDRula == 1) grandRulaScore = 5;
                else if (scoreCRula >= 8 && scoreDRula == 2) grandRulaScore = 5;
                else if (scoreCRula >= 8 && scoreDRula == 3) grandRulaScore = 6;
                else if (scoreCRula >= 8 && scoreDRula == 4) grandRulaScore = 7;
                else if (scoreCRula >= 8 && scoreDRula == 5) grandRulaScore = 7;
                else if (scoreCRula >= 8 && scoreDRula == 6) grandRulaScore = 7;
                else if (scoreCRula >= 8 && scoreDRula >= 7) grandRulaScore = 7;

                #endregion

                #region Reba table scoring
                // Reba Score A
                int tableAReba = 0;

                // 1
                     if (trunkRebaScore == 1 && neckRebaScore == 1 && legRebaScore == 1) tableAReba = 1;
                else if (trunkRebaScore == 1 && neckRebaScore == 1 && legRebaScore == 2) tableAReba = 2;
                else if (trunkRebaScore == 1 && neckRebaScore == 1 && legRebaScore == 3) tableAReba = 3;
                else if (trunkRebaScore == 1 && neckRebaScore == 1 && legRebaScore == 4) tableAReba = 4;
                else if (trunkRebaScore == 1 && neckRebaScore == 2 && legRebaScore == 1) tableAReba = 1;
                else if (trunkRebaScore == 1 && neckRebaScore == 2 && legRebaScore == 2) tableAReba = 2;
                else if (trunkRebaScore == 1 && neckRebaScore == 2 && legRebaScore == 3) tableAReba = 3;
                else if (trunkRebaScore == 1 && neckRebaScore == 2 && legRebaScore == 4) tableAReba = 4;
                else if (trunkRebaScore == 1 && neckRebaScore == 3 && legRebaScore == 1) tableAReba = 3;
                else if (trunkRebaScore == 1 && neckRebaScore == 3 && legRebaScore == 2) tableAReba = 3;
                else if (trunkRebaScore == 1 && neckRebaScore == 3 && legRebaScore == 3) tableAReba = 5;
                else if (trunkRebaScore == 1 && neckRebaScore == 3 && legRebaScore == 4) tableAReba = 6;

                // 2
                else if (trunkRebaScore == 2 && neckRebaScore == 1 && legRebaScore == 1) tableAReba = 2;
                else if (trunkRebaScore == 2 && neckRebaScore == 1 && legRebaScore == 2) tableAReba = 3;
                else if (trunkRebaScore == 2 && neckRebaScore == 1 && legRebaScore == 3) tableAReba = 4;
                else if (trunkRebaScore == 2 && neckRebaScore == 1 && legRebaScore == 4) tableAReba = 5;
                else if (trunkRebaScore == 2 && neckRebaScore == 2 && legRebaScore == 1) tableAReba = 3;
                else if (trunkRebaScore == 2 && neckRebaScore == 2 && legRebaScore == 2) tableAReba = 4;
                else if (trunkRebaScore == 2 && neckRebaScore == 2 && legRebaScore == 3) tableAReba = 5;
                else if (trunkRebaScore == 2 && neckRebaScore == 2 && legRebaScore == 4) tableAReba = 6;
                else if (trunkRebaScore == 2 && neckRebaScore == 3 && legRebaScore == 1) tableAReba = 4;
                else if (trunkRebaScore == 2 && neckRebaScore == 3 && legRebaScore == 2) tableAReba = 5;
                else if (trunkRebaScore == 2 && neckRebaScore == 3 && legRebaScore == 3) tableAReba = 6;
                else if (trunkRebaScore == 2 && neckRebaScore == 3 && legRebaScore == 4) tableAReba = 7;

                // 3
                else if (trunkRebaScore == 3 && neckRebaScore == 1 && legRebaScore == 1) tableAReba = 2;
                else if (trunkRebaScore == 3 && neckRebaScore == 1 && legRebaScore == 2) tableAReba = 4;
                else if (trunkRebaScore == 3 && neckRebaScore == 1 && legRebaScore == 3) tableAReba = 5;
                else if (trunkRebaScore == 3 && neckRebaScore == 1 && legRebaScore == 4) tableAReba = 6;
                else if (trunkRebaScore == 3 && neckRebaScore == 2 && legRebaScore == 1) tableAReba = 4;
                else if (trunkRebaScore == 3 && neckRebaScore == 2 && legRebaScore == 2) tableAReba = 5;
                else if (trunkRebaScore == 3 && neckRebaScore == 2 && legRebaScore == 3) tableAReba = 6;
                else if (trunkRebaScore == 3 && neckRebaScore == 2 && legRebaScore == 4) tableAReba = 7;
                else if (trunkRebaScore == 3 && neckRebaScore == 3 && legRebaScore == 1) tableAReba = 5;
                else if (trunkRebaScore == 3 && neckRebaScore == 3 && legRebaScore == 2) tableAReba = 6;
                else if (trunkRebaScore == 3 && neckRebaScore == 3 && legRebaScore == 3) tableAReba = 7;
                else if (trunkRebaScore == 3 && neckRebaScore == 3 && legRebaScore == 4) tableAReba = 8;

                // 4
                else if (trunkRebaScore == 4 && neckRebaScore == 1 && legRebaScore == 1) tableAReba = 3;
                else if (trunkRebaScore == 4 && neckRebaScore == 1 && legRebaScore == 2) tableAReba = 5;
                else if (trunkRebaScore == 4 && neckRebaScore == 1 && legRebaScore == 3) tableAReba = 6;
                else if (trunkRebaScore == 4 && neckRebaScore == 1 && legRebaScore == 4) tableAReba = 7;
                else if (trunkRebaScore == 4 && neckRebaScore == 2 && legRebaScore == 1) tableAReba = 5;
                else if (trunkRebaScore == 4 && neckRebaScore == 2 && legRebaScore == 2) tableAReba = 6;
                else if (trunkRebaScore == 4 && neckRebaScore == 2 && legRebaScore == 3) tableAReba = 7;
                else if (trunkRebaScore == 4 && neckRebaScore == 2 && legRebaScore == 4) tableAReba = 8;
                else if (trunkRebaScore == 4 && neckRebaScore == 3 && legRebaScore == 1) tableAReba = 6;
                else if (trunkRebaScore == 4 && neckRebaScore == 3 && legRebaScore == 2) tableAReba = 7;
                else if (trunkRebaScore == 4 && neckRebaScore == 3 && legRebaScore == 3) tableAReba = 8;
                else if (trunkRebaScore == 4 && neckRebaScore == 3 && legRebaScore == 4) tableAReba = 9;

                // 5
                else if (trunkRebaScore == 5 && neckRebaScore == 1 && legRebaScore == 1) tableAReba = 4;
                else if (trunkRebaScore == 5 && neckRebaScore == 1 && legRebaScore == 2) tableAReba = 6;
                else if (trunkRebaScore == 5 && neckRebaScore == 1 && legRebaScore == 3) tableAReba = 7;
                else if (trunkRebaScore == 5 && neckRebaScore == 1 && legRebaScore == 4) tableAReba = 8;
                else if (trunkRebaScore == 5 && neckRebaScore == 2 && legRebaScore == 1) tableAReba = 6;
                else if (trunkRebaScore == 5 && neckRebaScore == 2 && legRebaScore == 2) tableAReba = 7;
                else if (trunkRebaScore == 5 && neckRebaScore == 2 && legRebaScore == 3) tableAReba = 8;
                else if (trunkRebaScore == 5 && neckRebaScore == 2 && legRebaScore == 4) tableAReba = 9;
                else if (trunkRebaScore == 5 && neckRebaScore == 3 && legRebaScore == 1) tableAReba = 7;
                else if (trunkRebaScore == 5 && neckRebaScore == 3 && legRebaScore == 2) tableAReba = 8;
                else if (trunkRebaScore == 5 && neckRebaScore == 3 && legRebaScore == 3) tableAReba = 9;
                else if (trunkRebaScore == 5 && neckRebaScore == 3 && legRebaScore == 4) tableAReba = 9;

                int scoreAReba = tableAReba + loadForceRebaScore;

                // Reba Score B
                int tableBReba = 0;

                // 1
                     if (upperArmRebaScore == 1 && lowerArmRebaScore == 1 && wristRebaScore == 1) tableBReba = 1;
                else if (upperArmRebaScore == 1 && lowerArmRebaScore == 1 && wristRebaScore == 2) tableBReba = 2;
                else if (upperArmRebaScore == 1 && lowerArmRebaScore == 1 && wristRebaScore == 3) tableBReba = 2;
                else if (upperArmRebaScore == 1 && lowerArmRebaScore == 2 && wristRebaScore == 1) tableBReba = 1;
                else if (upperArmRebaScore == 1 && lowerArmRebaScore == 2 && wristRebaScore == 2) tableBReba = 2;
                else if (upperArmRebaScore == 1 && lowerArmRebaScore == 2 && wristRebaScore == 3) tableBReba = 3;

                // 2
                else if (upperArmRebaScore == 2 && lowerArmRebaScore == 1 && wristRebaScore == 1) tableBReba = 1;
                else if (upperArmRebaScore == 2 && lowerArmRebaScore == 1 && wristRebaScore == 2) tableBReba = 2;
                else if (upperArmRebaScore == 2 && lowerArmRebaScore == 1 && wristRebaScore == 3) tableBReba = 3;
                else if (upperArmRebaScore == 2 && lowerArmRebaScore == 2 && wristRebaScore == 1) tableBReba = 2;
                else if (upperArmRebaScore == 2 && lowerArmRebaScore == 2 && wristRebaScore == 2) tableBReba = 3;
                else if (upperArmRebaScore == 2 && lowerArmRebaScore == 2 && wristRebaScore == 3) tableBReba = 4;

                // 3
                else if (upperArmRebaScore == 3 && lowerArmRebaScore == 1 && wristRebaScore == 1) tableBReba = 3;
                else if (upperArmRebaScore == 3 && lowerArmRebaScore == 1 && wristRebaScore == 2) tableBReba = 4;
                else if (upperArmRebaScore == 3 && lowerArmRebaScore == 1 && wristRebaScore == 3) tableBReba = 5;
                else if (upperArmRebaScore == 3 && lowerArmRebaScore == 2 && wristRebaScore == 1) tableBReba = 4;
                else if (upperArmRebaScore == 3 && lowerArmRebaScore == 2 && wristRebaScore == 2) tableBReba = 5;
                else if (upperArmRebaScore == 3 && lowerArmRebaScore == 2 && wristRebaScore == 3) tableBReba = 5;

                // 4
                else if (upperArmRebaScore == 4 && lowerArmRebaScore == 1 && wristRebaScore == 1) tableBReba = 4;
                else if (upperArmRebaScore == 4 && lowerArmRebaScore == 1 && wristRebaScore == 2) tableBReba = 5;
                else if (upperArmRebaScore == 4 && lowerArmRebaScore == 1 && wristRebaScore == 3) tableBReba = 5;
                else if (upperArmRebaScore == 4 && lowerArmRebaScore == 2 && wristRebaScore == 1) tableBReba = 5;
                else if (upperArmRebaScore == 4 && lowerArmRebaScore == 2 && wristRebaScore == 2) tableBReba = 6;
                else if (upperArmRebaScore == 4 && lowerArmRebaScore == 2 && wristRebaScore == 3) tableBReba = 7;

                // 5
                else if (upperArmRebaScore == 5 && lowerArmRebaScore == 1 && wristRebaScore == 1) tableBReba = 6;
                else if (upperArmRebaScore == 5 && lowerArmRebaScore == 1 && wristRebaScore == 2) tableBReba = 7;
                else if (upperArmRebaScore == 5 && lowerArmRebaScore == 1 && wristRebaScore == 3) tableBReba = 8;
                else if (upperArmRebaScore == 5 && lowerArmRebaScore == 2 && wristRebaScore == 1) tableBReba = 7;
                else if (upperArmRebaScore == 5 && lowerArmRebaScore == 2 && wristRebaScore == 2) tableBReba = 8;
                else if (upperArmRebaScore == 5 && lowerArmRebaScore == 2 && wristRebaScore == 3) tableBReba = 8;

                // 6
                else if (upperArmRebaScore == 6 && lowerArmRebaScore == 1 && wristRebaScore == 1) tableBReba = 7;
                else if (upperArmRebaScore == 6 && lowerArmRebaScore == 1 && wristRebaScore == 2) tableBReba = 8;
                else if (upperArmRebaScore == 6 && lowerArmRebaScore == 1 && wristRebaScore == 3) tableBReba = 8;
                else if (upperArmRebaScore == 6 && lowerArmRebaScore == 2 && wristRebaScore == 1) tableBReba = 8;
                else if (upperArmRebaScore == 6 && lowerArmRebaScore == 2 && wristRebaScore == 2) tableBReba = 9;
                else if (upperArmRebaScore == 6 && lowerArmRebaScore == 2 && wristRebaScore == 3) tableBReba = 9;

                int scoreBReba = tableBReba + couplingRebaScore;

                // Reba Score C

                int scoreCReba = 0;

                // 1
                     if (scoreAReba == 1 && scoreBReba == 1) scoreCReba = 1;
                else if (scoreAReba == 1 && scoreBReba == 2) scoreCReba = 1;
                else if (scoreAReba == 1 && scoreBReba == 3) scoreCReba = 1;
                else if (scoreAReba == 1 && scoreBReba == 4) scoreCReba = 2;
                else if (scoreAReba == 1 && scoreBReba == 5) scoreCReba = 3;
                else if (scoreAReba == 1 && scoreBReba == 6) scoreCReba = 3;
                else if (scoreAReba == 1 && scoreBReba == 7) scoreCReba = 4;
                else if (scoreAReba == 1 && scoreBReba == 8) scoreCReba = 5;
                else if (scoreAReba == 1 && scoreBReba == 9) scoreCReba = 6;
                else if (scoreAReba == 1 && scoreBReba == 10) scoreCReba = 7;
                else if (scoreAReba == 1 && scoreBReba == 11) scoreCReba = 7;
                else if (scoreAReba == 1 && scoreBReba == 12) scoreCReba = 7;

                // 2
                else if (scoreAReba == 2 && scoreBReba == 1) scoreCReba = 1;
                else if (scoreAReba == 2 && scoreBReba == 2) scoreCReba = 2;
                else if (scoreAReba == 2 && scoreBReba == 3) scoreCReba = 2;
                else if (scoreAReba == 2 && scoreBReba == 4) scoreCReba = 3;
                else if (scoreAReba == 2 && scoreBReba == 5) scoreCReba = 4;
                else if (scoreAReba == 2 && scoreBReba == 6) scoreCReba = 4;
                else if (scoreAReba == 2 && scoreBReba == 7) scoreCReba = 5;
                else if (scoreAReba == 2 && scoreBReba == 8) scoreCReba = 6;
                else if (scoreAReba == 2 && scoreBReba == 9) scoreCReba = 6;
                else if (scoreAReba == 2 && scoreBReba == 10) scoreCReba = 7;
                else if (scoreAReba == 2 && scoreBReba == 11) scoreCReba = 7;
                else if (scoreAReba == 2 && scoreBReba == 12) scoreCReba = 8;

                // 3
                else if (scoreAReba == 3 && scoreBReba == 1) scoreCReba = 2;
                else if (scoreAReba == 3 && scoreBReba == 2) scoreCReba = 3;
                else if (scoreAReba == 3 && scoreBReba == 3) scoreCReba = 3;
                else if (scoreAReba == 3 && scoreBReba == 4) scoreCReba = 3;
                else if (scoreAReba == 3 && scoreBReba == 5) scoreCReba = 4;
                else if (scoreAReba == 3 && scoreBReba == 6) scoreCReba = 5;
                else if (scoreAReba == 3 && scoreBReba == 7) scoreCReba = 6;
                else if (scoreAReba == 3 && scoreBReba == 8) scoreCReba = 7;
                else if (scoreAReba == 3 && scoreBReba == 9) scoreCReba = 7;
                else if (scoreAReba == 3 && scoreBReba == 10) scoreCReba = 8;
                else if (scoreAReba == 3 && scoreBReba == 11) scoreCReba = 8;
                else if (scoreAReba == 3 && scoreBReba == 12) scoreCReba = 8;

                // 4
                else if (scoreAReba == 4 && scoreBReba == 1) scoreCReba = 3;
                else if (scoreAReba == 4 && scoreBReba == 2) scoreCReba = 4;
                else if (scoreAReba == 4 && scoreBReba == 3) scoreCReba = 4;
                else if (scoreAReba == 4 && scoreBReba == 4) scoreCReba = 4;
                else if (scoreAReba == 4 && scoreBReba == 5) scoreCReba = 5;
                else if (scoreAReba == 4 && scoreBReba == 6) scoreCReba = 6;
                else if (scoreAReba == 4 && scoreBReba == 7) scoreCReba = 7;
                else if (scoreAReba == 4 && scoreBReba == 8) scoreCReba = 8;
                else if (scoreAReba == 4 && scoreBReba == 9) scoreCReba = 8;
                else if (scoreAReba == 4 && scoreBReba == 10) scoreCReba = 9;
                else if (scoreAReba == 4 && scoreBReba == 11) scoreCReba = 9;
                else if (scoreAReba == 4 && scoreBReba == 12) scoreCReba = 9;

                // 5
                else if (scoreAReba == 5 && scoreBReba == 1) scoreCReba = 4;
                else if (scoreAReba == 5 && scoreBReba == 2) scoreCReba = 4;
                else if (scoreAReba == 5 && scoreBReba == 3) scoreCReba = 4;
                else if (scoreAReba == 5 && scoreBReba == 4) scoreCReba = 5;
                else if (scoreAReba == 5 && scoreBReba == 5) scoreCReba = 6;
                else if (scoreAReba == 5 && scoreBReba == 6) scoreCReba = 7;
                else if (scoreAReba == 5 && scoreBReba == 7) scoreCReba = 8;
                else if (scoreAReba == 5 && scoreBReba == 8) scoreCReba = 8;
                else if (scoreAReba == 5 && scoreBReba == 9) scoreCReba = 9;
                else if (scoreAReba == 5 && scoreBReba == 10) scoreCReba = 9;
                else if (scoreAReba == 5 && scoreBReba == 11) scoreCReba = 9;
                else if (scoreAReba == 5 && scoreBReba == 12) scoreCReba = 9;

                // 6
                else if (scoreAReba == 6 && scoreBReba == 1) scoreCReba = 6;
                else if (scoreAReba == 6 && scoreBReba == 2) scoreCReba = 6;
                else if (scoreAReba == 6 && scoreBReba == 3) scoreCReba = 6;
                else if (scoreAReba == 6 && scoreBReba == 4) scoreCReba = 7;
                else if (scoreAReba == 6 && scoreBReba == 5) scoreCReba = 8;
                else if (scoreAReba == 6 && scoreBReba == 6) scoreCReba = 8;
                else if (scoreAReba == 6 && scoreBReba == 7) scoreCReba = 9;
                else if (scoreAReba == 6 && scoreBReba == 8) scoreCReba = 9;
                else if (scoreAReba == 6 && scoreBReba == 9) scoreCReba = 10;
                else if (scoreAReba == 6 && scoreBReba == 10) scoreCReba = 10;
                else if (scoreAReba == 6 && scoreBReba == 11) scoreCReba = 10;
                else if (scoreAReba == 6 && scoreBReba == 12) scoreCReba = 10;

                // 7
                else if (scoreAReba == 7 && scoreBReba == 1) scoreCReba = 7;
                else if (scoreAReba == 7 && scoreBReba == 2) scoreCReba = 7;
                else if (scoreAReba == 7 && scoreBReba == 3) scoreCReba = 7;
                else if (scoreAReba == 7 && scoreBReba == 4) scoreCReba = 8;
                else if (scoreAReba == 7 && scoreBReba == 5) scoreCReba = 9;
                else if (scoreAReba == 7 && scoreBReba == 6) scoreCReba = 9;
                else if (scoreAReba == 7 && scoreBReba == 7) scoreCReba = 9;
                else if (scoreAReba == 7 && scoreBReba == 8) scoreCReba = 10;
                else if (scoreAReba == 7 && scoreBReba == 9) scoreCReba = 10;
                else if (scoreAReba == 7 && scoreBReba == 10) scoreCReba = 11;
                else if (scoreAReba == 7 && scoreBReba == 11) scoreCReba = 11;
                else if (scoreAReba == 7 && scoreBReba == 12) scoreCReba = 11;

                // 8
                else if (scoreAReba == 8 && scoreBReba == 1) scoreCReba = 8;
                else if (scoreAReba == 8 && scoreBReba == 2) scoreCReba = 8;
                else if (scoreAReba == 8 && scoreBReba == 3) scoreCReba = 8;
                else if (scoreAReba == 8 && scoreBReba == 4) scoreCReba = 9;
                else if (scoreAReba == 8 && scoreBReba == 5) scoreCReba = 10;
                else if (scoreAReba == 8 && scoreBReba == 6) scoreCReba = 10;
                else if (scoreAReba == 8 && scoreBReba == 7) scoreCReba = 10;
                else if (scoreAReba == 8 && scoreBReba == 8) scoreCReba = 10;
                else if (scoreAReba == 8 && scoreBReba == 9) scoreCReba = 10;
                else if (scoreAReba == 8 && scoreBReba == 10) scoreCReba = 11;
                else if (scoreAReba == 8 && scoreBReba == 11) scoreCReba = 11;
                else if (scoreAReba == 8 && scoreBReba == 12) scoreCReba = 11;

                // 9
                else if (scoreAReba == 9 && scoreBReba == 1) scoreCReba = 9;
                else if (scoreAReba == 9 && scoreBReba == 2) scoreCReba = 9;
                else if (scoreAReba == 9 && scoreBReba == 3) scoreCReba = 9;
                else if (scoreAReba == 9 && scoreBReba == 4) scoreCReba = 10;
                else if (scoreAReba == 9 && scoreBReba == 5) scoreCReba = 10;
                else if (scoreAReba == 9 && scoreBReba == 6) scoreCReba = 10;
                else if (scoreAReba == 9 && scoreBReba == 7) scoreCReba = 11;
                else if (scoreAReba == 9 && scoreBReba == 8) scoreCReba = 11;
                else if (scoreAReba == 9 && scoreBReba == 9) scoreCReba = 11;
                else if (scoreAReba == 9 && scoreBReba == 10) scoreCReba = 12;
                else if (scoreAReba == 9 && scoreBReba == 11) scoreCReba = 12;
                else if (scoreAReba == 9 && scoreBReba == 12) scoreCReba = 12;

                // 10
                else if (scoreAReba == 10 && scoreBReba == 1) scoreCReba = 10;
                else if (scoreAReba == 10 && scoreBReba == 2) scoreCReba = 10;
                else if (scoreAReba == 10 && scoreBReba == 3) scoreCReba = 10;
                else if (scoreAReba == 10 && scoreBReba == 4) scoreCReba = 11;
                else if (scoreAReba == 10 && scoreBReba == 5) scoreCReba = 11;
                else if (scoreAReba == 10 && scoreBReba == 6) scoreCReba = 11;
                else if (scoreAReba == 10 && scoreBReba == 7) scoreCReba = 11;
                else if (scoreAReba == 10 && scoreBReba == 8) scoreCReba = 12;
                else if (scoreAReba == 10 && scoreBReba == 9) scoreCReba = 12;
                else if (scoreAReba == 10 && scoreBReba == 10) scoreCReba = 12;
                else if (scoreAReba == 10 && scoreBReba == 11) scoreCReba = 12;
                else if (scoreAReba == 10 && scoreBReba == 12) scoreCReba = 12;

                // 11
                else if (scoreAReba == 11 && scoreBReba == 1) scoreCReba = 11;
                else if (scoreAReba == 11 && scoreBReba == 2) scoreCReba = 11;
                else if (scoreAReba == 11 && scoreBReba == 3) scoreCReba = 11;
                else if (scoreAReba == 11 && scoreBReba == 4) scoreCReba = 11;
                else if (scoreAReba == 11 && scoreBReba == 5) scoreCReba = 12;
                else if (scoreAReba == 11 && scoreBReba == 6) scoreCReba = 12;
                else if (scoreAReba == 11 && scoreBReba == 7) scoreCReba = 12;
                else if (scoreAReba == 11 && scoreBReba == 8) scoreCReba = 12;
                else if (scoreAReba == 11 && scoreBReba == 9) scoreCReba = 12;
                else if (scoreAReba == 11 && scoreBReba == 10) scoreCReba = 12;
                else if (scoreAReba == 11 && scoreBReba == 11) scoreCReba = 12;
                else if (scoreAReba == 11 && scoreBReba == 12) scoreCReba = 12;

                // 12
                else if (scoreAReba == 12 && scoreBReba == 1) scoreCReba = 12;
                else if (scoreAReba == 12 && scoreBReba == 2) scoreCReba = 12;
                else if (scoreAReba == 12 && scoreBReba == 3) scoreCReba = 12;
                else if (scoreAReba == 12 && scoreBReba == 4) scoreCReba = 12;
                else if (scoreAReba == 12 && scoreBReba == 5) scoreCReba = 12;
                else if (scoreAReba == 12 && scoreBReba == 6) scoreCReba = 12;
                else if (scoreAReba == 12 && scoreBReba == 7) scoreCReba = 12;
                else if (scoreAReba == 12 && scoreBReba == 8) scoreCReba = 12;
                else if (scoreAReba == 12 && scoreBReba == 9) scoreCReba = 12;
                else if (scoreAReba == 12 && scoreBReba == 10) scoreCReba = 12;
                else if (scoreAReba == 12 && scoreBReba == 11) scoreCReba = 12;
                else if (scoreAReba == 12 && scoreBReba == 12) scoreCReba = 12;


                // Reba grand score
                int activityRebaScore = activity1RebaScore + activity2RebaScore + activity3RebaScore;
                int grandRebascore = scoreCReba + activityRebaScore;

                #endregion

                // Others scoring
                muscleAScoreList.Add(muscleARulaScore);
                forceAScoreList.Add(forceARulaScore);
                muscleBScoreList.Add(muscleBRulaScore);
                forceBScoreList.Add(forceBRulaScore);

                couplingScoreList.Add(couplingRebaScore);
                loadForceScoreList.Add(loadForceRebaScore);
                activityScoreList.Add(activityRebaScore);

                grandRulaScoreList.Add(grandRulaScore);
                grandRebaScoreList.Add(grandRebascore);

            }

            UpdateAnglesAndScores(startTime);
            TimelineSlider.Value = startTime;

            // RULA, REBA Score Graph
            LineSeries SeriesRula = new LineSeries();
            LineSeries SeriesReba = new LineSeries();

            for (int i = 0; i < grandRulaScoreList.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesRula.Points.Add(new DataPoint(time,grandRulaScoreList[i]));
            }

            for (int i = 0; i < grandRebaScoreList.Count; i++)
            {
                var time = Convert.ToDouble(i) / 240f;
                SeriesReba.Points.Add(new DataPoint(time, grandRebaScoreList[i]));
            }

            PlotRula.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = 7,
                Minimum = 0,
                MajorStep = 1,
                MinorTickSize = 0,
            });

            PlotRula.Series.Add(SeriesRula);
            this.MyModelRula = PlotRula;
            this.DataContext = this;
            this.MyModelRula.InvalidatePlot(true);


            PlotReba.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Maximum = 15,
                Minimum = 0,
                MajorStep = 1,
                MinorTickSize = 0,
            });

            PlotReba.Series.Add(SeriesReba);
            this.MyModelReba = PlotReba;
            this.DataContext = this;
            this.MyModelReba.InvalidatePlot(true);


            // RULA, REBA Measures

            int rulaStartPoint = 0;
            for (int i = 120; i < grandRulaScoreList.Count; i++)
            {
                if (grandRulaScoreList[i - 1] != grandRulaScoreList[i])
                {
                    rulaStartPoint = i;
                    break;
                }
            }

            int rulaEndPoint = 0;
            for (int i = grandRulaScoreList.Count - 120; i > 0; i--)
            {
                if (grandRulaScoreList[i + 1] != grandRulaScoreList[i])
                {
                    rulaEndPoint = i;
                    break;
                }
            }

            var rulaDurationCount = rulaEndPoint - rulaStartPoint;
            var rulaDuration = Convert.ToDouble(rulaDurationCount) / 240f;

            if (rulaDuration != 0)
            {
                try
                {
                    truncatedRula = grandRulaScoreList.GetRange(rulaStartPoint, rulaDurationCount);
                }
                catch (Exception)
                {
                    MessageBox.Show("No peaks detected!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    truncatedRula = grandRulaScoreList;
                }
            }

            else
            {
                MessageBox.Show("No changes detected!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                truncatedRula = grandRulaScoreList;
            }


            int rula34Count = 0,
                rula56Count = 0,
                rula7Count = 0;

            for (int i = 0; i < truncatedRula.Count; i++)
            {
                switch (truncatedRula[i])
                {
                    case 3:
                    case 4:
                        rula34Count++;
                        break;
                    case 5:
                    case 6:
                        rula56Count++;
                        break;
                    case 7:
                        rula7Count++;
                        break;
                }
            }

            var rula34Duration = Convert.ToDouble(rula34Count) / 240f;
            var rula56Duration = Convert.ToDouble(rula56Count) / 240f;
            var rula7Duration = Convert.ToDouble(rula7Count) / 240f;

            var rula34Percentage = rula34Duration / rulaDuration * 100f;
            var rula56Percentage = rula56Duration / rulaDuration * 100f;
            var rula7Percentage = rula7Duration / rulaDuration * 100f;


            RulaAverageScoreText.Text = truncatedRula.Average().ToString("N2");
            RulaDurationText.Text = rulaDuration.ToString("N2");
            Rula34DurationText.Text = rula34Duration.ToString("N2") + "\n(" + rula34Percentage.ToString("N2") + "%)";
            Rula56DurationText.Text = rula56Duration.ToString("N2") + "\n(" + rula56Percentage.ToString("N2") + "%)";
            Rula7DurationText.Text = rula7Duration.ToString("N2") + "\n(" + rula7Percentage.ToString("N2") + "%)";



            int rebaStartPoint = 0;
            for (int i = 120; i < grandRebaScoreList.Count; i++)
            {
                if (grandRebaScoreList[i - 1] != grandRebaScoreList[i])
                {
                    rebaStartPoint = i;
                    break;
                }
            }

            int rebaEndPoint = 0;
            for (int i = grandRebaScoreList.Count - 120; i > 0; i--)
            {
                if (grandRebaScoreList[i + 1] != grandRebaScoreList[i])
                {
                    rebaEndPoint = i;
                    break;
                }
            }

            var rebaDurationCount = rebaEndPoint - rebaStartPoint;
            var rebaDuration = Convert.ToDouble(rebaDurationCount) / 240f;

            if (rebaDuration != 0)
            {
                try
                {
                    truncatedReba = grandRebaScoreList.GetRange(rebaStartPoint, rebaDurationCount);
                }
                catch (Exception)
                {
                    MessageBox.Show("No peaks detected!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    truncatedReba = grandRebaScoreList;
                }
            }
            else
            {
                MessageBox.Show("No changes detected!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                truncatedReba = grandRebaScoreList;
            }

            int reba47Count = 0,
                reba810Count = 0,
                reba1115Count = 0;

            for (int i = 0; i < truncatedReba.Count; i++)
            {
                switch (truncatedReba[i])
                {
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        reba47Count++;
                        break;
                    case 8:
                    case 9:
                    case 10:
                        reba810Count++;
                        break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        reba1115Count++;
                        break;
                }
            }

            var reba47Duration = Convert.ToDouble(reba47Count) / 240f;
            var reba810Duration = Convert.ToDouble(reba810Count) / 240f;
            var reba1115Duration = Convert.ToDouble(reba1115Count) / 240f;

            var reba47Percentage = reba47Duration / rebaDuration * 100f;
            var reba810Percentage = reba810Duration / rebaDuration * 100f;
            var reba1115Percentage = reba1115Duration / rebaDuration * 100f;

            RebaAverageScoreText.Text = truncatedReba.Average().ToString("N2");
            RebaDurationText.Text = rebaDuration.ToString("N2");
            Reba47DurationText.Text = reba47Duration.ToString("N2") + "\n(" + reba47Percentage.ToString("N2") + "%)";
            Reba810DurationText.Text = reba810Duration.ToString("N2") + "\n(" + reba810Percentage.ToString("N2") + "%)";
            Reba1115DurationText.Text = reba1115Duration.ToString("N2") + "\n(" + reba1115Percentage.ToString("N2") + "%)";

        }

        private void UpdateAnglesAndScores(int focusTime)
        {
            // Angle
            RulaUpperArmLeftFlexionText.Text = upperArmLeftFlexionList[focusTime].ToString("N0") + "°";
            RulaUpperArmLeftAbductionText.Text = upperArmLeftAbductionList[focusTime].ToString("N0") + "°";
            RulaUpperArmLeftRotationText.Text = upperArmLeftRotationList[focusTime].ToString("N0") + "°";
            RulaUpperArmLeftElevationText.Text = upperArmLeftElevationList[focusTime].ToString("N0") + "°";
            RulaUpperArmRightFlexionText.Text = upperArmRightFlexionList[focusTime].ToString("N0") + "°";
            RulaUpperArmRightAbductionText.Text = upperArmRightAbductionList[focusTime].ToString("N0") + "°";
            RulaUpperArmRightRotationText.Text = upperArmRightRotationList[focusTime].ToString("N0") + "°";
            RulaUpperArmRightElevationText.Text = upperArmRightElevationList[focusTime].ToString("N0") + "°";

            RulaLowerArmLeftFlexionText.Text = lowerArmLeftFlexionList[focusTime].ToString("N0") + "°";
            RulaLowerArmRightFlexionText.Text = lowerArmRightFlexionList[focusTime].ToString("N0") + "°";

            RulaWristLeftFlexionText.Text = wristLeftFlexionList[focusTime].ToString("N0") + "°";
            RulaWristLeftDeviationText.Text = wristLeftDeviationList[focusTime].ToString("N0") + "°";
            RulaWristLeftRotationText.Text = wristLeftRotationList[focusTime].ToString("N0") + "°";
            RulaWristRightFlexionText.Text = wristRightFlexionList[focusTime].ToString("N0") + "°";
            RulaWristRightDeviationText.Text = wristRightDeviationList[focusTime].ToString("N0") + "°";
            RulaWristRightRotationText.Text = wristRightRotationList[focusTime].ToString("N0") + "°";

            RulaNeckFlexionText.Text = neckFlexionList[focusTime].ToString("N0") + "°";
            RulaNeckBendingText.Text = neckBendingList[focusTime].ToString("N0") + "°";
            RulaNeckRotationText.Text = neckRotationList[focusTime].ToString("N0") + "°";

            RulaTrunkFlexionText.Text = trunkFlexionList[focusTime].ToString("N0") + "°";
            RulaTrunkBendingText.Text = trunkBendingList[focusTime].ToString("N0") + "°";
            RulaTrunkRotationText.Text = trunkRotationList[focusTime].ToString("N0") + "°";

            RebaLegLeftFlexionText.Text = legLeftFlexionList[focusTime].ToString("N0") + "°";
            RebaLegRightFlexionText.Text = legRightFlexionList[focusTime].ToString("N0") + "°";

            // Score
            RulaUpperArmScoreText.Text = upperArmRulaScoreList[focusTime].ToString();
            switch (upperArmRulaScoreList[focusTime])
            {
                case 1:
                case 2:
                    RulaUpperArmScoreText.Background = Brushes.LightGreen;
                    break;
                case 3:
                case 4:
                    RulaUpperArmScoreText.Background = Brushes.Khaki;
                    break;
                case 5:
                case 6:
                    RulaUpperArmScoreText.Background = Brushes.LightPink;
                    break;
            }

            RulaLowerArmScoreText.Text = lowerArmRulaScoreList[focusTime].ToString();
            switch (lowerArmRulaScoreList[focusTime])
            {
                case 1:
                    RulaLowerArmScoreText.Background = Brushes.LightGreen;
                    break;
                case 2:
                    RulaLowerArmScoreText.Background = Brushes.Khaki;
                    break;
                case 3:
                    RulaLowerArmScoreText.Background = Brushes.LightPink;
                    break;
            }

            RulaWristScoreText.Text = wristRulaScoreList[focusTime].ToString();
            switch (wristRulaScoreList[focusTime])
            {
                case 1:
                    RulaWristScoreText.Background = Brushes.LightGreen;
                    break;
                case 2:
                case 3:
                    RulaWristScoreText.Background = Brushes.Khaki;
                    break;
                case 4:
                    RulaWristScoreText.Background = Brushes.LightPink;
                    break;
            }

            RulaWristTwistScoreText.Text = wristTwistRulaScoreList[focusTime].ToString();
            switch (wristTwistRulaScoreList[focusTime])
            {
                case 1:
                    RulaWristTwistScoreText.Background = Brushes.LightGreen;
                    break;
                case 2:
                    RulaWristTwistScoreText.Background = Brushes.LightPink;
                    break;
            }

            RulaNeckScoreText.Text = neckRulaScoreList[focusTime].ToString();
            switch (neckRulaScoreList[focusTime])
            {
                case 1:
                case 2:
                    RulaNeckScoreText.Background = Brushes.LightGreen;
                    break;
                case 3:
                case 4:
                    RulaNeckScoreText.Background = Brushes.Khaki;
                    break;
                case 5:
                case 6:
                    RulaNeckScoreText.Background = Brushes.LightPink;
                    break;
            }

            RulaTrunkScoreText.Text = trunkRulaScoreList[focusTime].ToString();
            switch (trunkRulaScoreList[focusTime])
            {
                case 1:
                case 2:
                    RulaTrunkScoreText.Background = Brushes.LightGreen;
                    break;
                case 3:
                case 4:
                    RulaTrunkScoreText.Background = Brushes.Khaki;
                    break;
                case 5:
                case 6:
                    RulaTrunkScoreText.Background = Brushes.LightPink;
                    break;
            }

            RulaLegScoreText.Text = legRulaScoreList[focusTime].ToString();
            switch (legRulaScoreList[focusTime])
            {
                case 1:
                    RulaLegScoreText.Background = Brushes.LightGreen;
                    break;
                case 2:
                    RulaLegScoreText.Background = Brushes.LightPink;
                    break;
            }

            RulaMuscleAScoreText.Text = "Muscle A: " + muscleAScoreList[focusTime];
            switch (muscleAScoreList[focusTime])
            {
                case 0:
                    RulaMuscleAScoreText.Background = Brushes.LightGreen;
                    break;
                case 1:
                    RulaMuscleAScoreText.Background = Brushes.LightPink;
                    break;
            }

            RulaForceAScoreText.Text = "Force A: " + forceAScoreList[focusTime];
            switch (forceAScoreList[focusTime])
            {
                case 0:
                    RulaForceAScoreText.Background = Brushes.LightGreen;
                    break;
                case 1:
                case 2:
                    RulaForceAScoreText.Background = Brushes.Khaki;
                    break;
                case 3:
                    RulaForceAScoreText.Background = Brushes.LightPink;
                    break;
            }

            RulaMuscleBScoreText.Text = "Muscle B: " + muscleBScoreList[focusTime];
            switch (muscleBScoreList[focusTime])
            {
                case 0:
                    RulaMuscleBScoreText.Background = Brushes.LightGreen;
                    break;
                case 1:
                    RulaMuscleBScoreText.Background = Brushes.LightPink;
                    break;
            }

            RulaForceBScoreText.Text = "Force B: " + forceBScoreList[focusTime];
            switch (forceBScoreList[focusTime])
            {
                case 0:
                    RulaForceBScoreText.Background = Brushes.LightGreen;
                    break;
                case 1:
                case 2:
                    RulaForceBScoreText.Background = Brushes.Khaki;
                    break;
                case 3:
                    RulaForceBScoreText.Background = Brushes.LightPink;
                    break;
            }

            RulaGrandScoreText.Text = "RULA Score: " + grandRulaScoreList[focusTime];
            switch (grandRulaScoreList[focusTime])
            {
                case 1:
                case 2:
                    RulaGrandScoreText.Background = Brushes.LawnGreen;
                    RulaActionText.Text = "Posture is acceptable if it is not maintained or repeated for long periods.";
                    RulaActionText.Background = Brushes.LawnGreen;
                    break;
                case 3:
                case 4:
                    RulaGrandScoreText.Background = Brushes.Yellow;
                    RulaActionText.Text = "Further investigation is needed and changes may be required.";
                    RulaActionText.Background = Brushes.Yellow;
                    break;
                case 5:
                case 6:
                    RulaGrandScoreText.Background = Brushes.Orange;
                    RulaActionText.Text = "Investigation and changes are required soon.";
                    RulaActionText.Background = Brushes.Orange;
                    break;
                case 7:
                    RulaGrandScoreText.Background = Brushes.Red;
                    RulaActionText.Text = "Investigation and changes are required immediately.";
                    RulaActionText.Background = Brushes.Red;
                    break;
            }

            
            RebaUpperArmScoreText.Text = upperArmRebaScoreList[focusTime].ToString();
            switch (upperArmRebaScoreList[focusTime])
            {
                case 1:
                case 2:
                    RebaUpperArmScoreText.Background = Brushes.LightGreen;
                    break;
                case 3:
                case 4:
                    RebaUpperArmScoreText.Background = Brushes.Khaki;
                    break;
                case 5:
                case 6:
                    RebaUpperArmScoreText.Background = Brushes.LightPink;
                    break;
            }

            RebaLowerArmScoreText.Text = lowerArmRebaScoreList[focusTime].ToString();
            switch (lowerArmRebaScoreList[focusTime])
            {
                case 1:
                    RebaLowerArmScoreText.Background = Brushes.LightGreen;
                    break;
                case 2:
                    RebaLowerArmScoreText.Background = Brushes.Khaki;
                    break;
                case 3:
                    RebaLowerArmScoreText.Background = Brushes.LightPink;
                    break;
            }

            RebaWristScoreText.Text = wristRebaScoreList[focusTime].ToString();
            switch (wristRebaScoreList[focusTime])
            {
                case 1:
                    RebaWristScoreText.Background = Brushes.LightGreen;
                    break;
                case 2:
                case 3:
                    RebaWristScoreText.Background = Brushes.Khaki;
                    break;
                case 4:
                    RebaWristScoreText.Background = Brushes.LightPink;
                    break;
            }

            RebaNeckScoreText.Text = neckRebaScoreList[focusTime].ToString();
            switch (neckRebaScoreList[focusTime])
            {
                case 1:
                case 2:
                    RebaNeckScoreText.Background = Brushes.LightGreen;
                    break;
                case 3:
                case 4:
                    RebaNeckScoreText.Background = Brushes.Khaki;
                    break;
                case 5:
                case 6:
                    RebaNeckScoreText.Background = Brushes.LightPink;
                    break;
            }

            RebaTrunkScoreText.Text = trunkRebaScoreList[focusTime].ToString();
            switch (trunkRebaScoreList[focusTime])
            {
                case 1:
                case 2:
                    RebaTrunkScoreText.Background = Brushes.LightGreen;
                    break;
                case 3:
                case 4:
                    RebaTrunkScoreText.Background = Brushes.Khaki;
                    break;
                case 5:
                case 6:
                    RebaTrunkScoreText.Background = Brushes.LightPink;
                    break;
            }

            RebaLegScoreText.Text = legRebaScoreList[focusTime].ToString();
            switch (legRebaScoreList[focusTime])
            {
                case 1:
                    RebaLegScoreText.Background = Brushes.LightGreen;
                    break;
                case 2:
                    RebaLegScoreText.Background = Brushes.LightPink;
                    break;
            }

            RebaCouplingScoreText.Text = "Coupling: " + couplingScoreList[focusTime];
            switch (couplingScoreList[focusTime])
            {
                case 0:
                    RebaCouplingScoreText.Background = Brushes.LightGreen;
                    break;
                case 1:
                case 2:
                    RebaCouplingScoreText.Background = Brushes.Khaki;
                    break;
                case 3:
                    RebaCouplingScoreText.Background = Brushes.LightPink;
                    break;
            }

            RebaLoadForceScoreText.Text = "Load/Force: " + loadForceScoreList[focusTime];
            switch (loadForceScoreList[focusTime])
            {
                case 0:
                    RebaLoadForceScoreText.Background = Brushes.LightGreen;
                    break;
                case 1:
                case 2:
                    RebaLoadForceScoreText.Background = Brushes.Khaki;
                    break;
                case 3:
                    RebaLoadForceScoreText.Background = Brushes.LightPink;
                    break;
            }

            RebaActivityScoreText.Text = "Activity: " + activityScoreList[focusTime];
            switch (activityScoreList[focusTime])
            {
                case 0:
                    RebaActivityScoreText.Background = Brushes.LightGreen;
                    break;
                case 1:
                case 2:
                    RebaActivityScoreText.Background = Brushes.Khaki;
                    break;
                case 3:
                    RebaActivityScoreText.Background = Brushes.LightPink;
                    break;
            }

            RebaGrandScoreText.Text = "REBA Score: " + grandRebaScoreList[focusTime];
            switch (grandRebaScoreList[focusTime])
            {
                case 1:
                    RebaGrandScoreText.Background = Brushes.LimeGreen;
                    RebaActionText.Text = "Risk level: Negligible\nAction: None necessary";
                    RebaActionText.Background = Brushes.LimeGreen;
                    break;
                case 2:
                case 3:
                    RebaGrandScoreText.Background = Brushes.LawnGreen;
                    RebaActionText.Text = "Risk level: Low\nAction: May be necessary";
                    RebaActionText.Background = Brushes.LawnGreen;
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                    RebaGrandScoreText.Background = Brushes.Yellow;
                    RebaActionText.Text = "Risk level: Medium\nAction: Necessary";
                    RebaActionText.Background = Brushes.Yellow;
                    break;
                case 8:
                case 9:
                case 10:
                    RebaGrandScoreText.Background = Brushes.Orange;
                    RebaActionText.Text = "Risk level: High\nAction: Necessary soon";
                    RebaActionText.Background = Brushes.Orange;
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                    RebaGrandScoreText.Background = Brushes.Red;
                    RebaActionText.Text = "Risk level: Very high\nAction: Necessary NOW";
                    RebaActionText.Background = Brushes.Red;
                    break;
            }
        }

        private int AddIf(double angle, double criteria)
        {
            int score;

            if (Math.Abs(angle) < criteria) score = 0;
            else score = 1;

            return score;
        }

        private int AddIf(double L_angle, double R_angle, double criteria)
        {
            int L_score, R_score;

            if (Math.Abs(L_angle) < criteria) L_score = 0;
            else L_score = 1;

            if (Math.Abs(R_angle) < criteria) R_score = 0;
            else R_score = 1;

            return Math.Max(L_score, R_score);
        }



        #region Settings

        private void UpperArmSupportCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            upperArmSupportRulaScore = UpperArmSupportCheckBox.IsChecked == true ? -1 : 0;
        }

        private void LowerArmMidlineCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            lowerArmMidlineRulaScore = LowerArmMidlineCheckBox.IsChecked == true ? 1 : 0;
        }

        private void MuscleACheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            muscleARulaScore = MuscleCheckBox.IsChecked == true ? 1 : 0;
        }

        private void ForceAComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            forceARulaScore = ForceComboBox.SelectedIndex;
        }

        private void MuscleBCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            muscleBRulaScore = MuscleCheckBox.IsChecked == true ? 1 : 0;
        }

        private void ForceBComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            forceBRulaScore = ForceComboBox.SelectedIndex;
        }

        private void ShockCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            shockRebaScore = ShockCheckBox.IsChecked == true ? 1 : 0;
        }

        private void CouplingComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            couplingRebaScore = CouplingComboBox.SelectedIndex;
        }

        private void Activity1CheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            activity1RebaScore = Activity1CheckBox.IsChecked == true ? 1 : 0;
        }

        private void Activity2CheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            activity2RebaScore = Activity2CheckBox.IsChecked == true ? 1 : 0;
        }

        private void Activity3CheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            activity3RebaScore = Activity3CheckBox.IsChecked == true ? 1 : 0;
        }

        private void TrunkSupportCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            trunkSupportRulaBool = TrunkSupportCheckBox.IsChecked == true;
        }

        private void LegSupportCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            legSupportRulaScore = LegSupportComboBox.SelectedIndex + 1;
        }

        private void LegSittingCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            legSittingRebaBool = LegSittingCheckBox.IsChecked == true;
        }

        private void LegSupportComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            legSupportRebaScore = LegSupportComboBox.SelectedIndex + 1;
        }

        private void LoadForceComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadForceRebaScore = LoadForceComboBox.SelectedIndex;
        }

        #endregion

        
        private void MaxButton_OnClick(object sender, RoutedEventArgs e)
        {
            List<double> sumScoreList = new List<double>();

            for (int i = 0; i < grandRulaScoreList.Count; i++)
            {
                var rulaRatio = Convert.ToDouble(grandRulaScoreList[i]) / 7f;
                var rebaRatio = Convert.ToDouble(grandRebaScoreList[i] / 15f);
                sumScoreList.Add(rulaRatio + rebaRatio);
            }
            
            var maxValue = sumScoreList.Max();
            var maxIndex = sumScoreList.IndexOf(maxValue);

            TimelineSlider.Value = maxIndex;

            UpdateAnglesAndScores(maxIndex);

        }

        private void TimelineSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int focusTime = Convert.ToInt32(TimelineSlider.Value);
            TimelineText.Text = (Convert.ToDouble(focusTime) / 240f).ToString("N2"); 

            UpdateAnglesAndScores(focusTime);
        }


        public PlotModel MyModelRula { get; private set; }
        public PlotModel PlotRula = new PlotModel();
        public PlotModel MyModelReba { get; private set; }
        public PlotModel PlotReba = new PlotModel();


        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ExportButton_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
