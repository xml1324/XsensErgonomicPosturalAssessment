   M S B A M L   `   `   `  ���� R  NXsensPosturalAssessment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:XsensPosturalAssessmentXsensPosturalAssessment  ,    &XsensPosturalAssessment.RULAREBAWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     7local%clr-namespace:XsensPosturalAssessment     R NOxyPlot.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf    � Title$-  'Xsens-based postural ergonomic analysis��$
��1080��$
��1920�� � WindowStartupLocation$ CenterScreen=�.���  � RowDefinitions �� $��70 ��� $��60 ��� $��* ��� $��60 � � ColumnDefinitions �� $	��200 ��� $��* ��� $��* �.+��� $��0��$��3��
�������$��3q�
��� E��._��� $��10q�$-��'Xsens-based postural ergonomic analysis��$��30(�$
��Bold"�$��Center=�
��������� -   $��
BackButton��$
��Left=�$��
 ◀ Back ��$��30(�
��� E��$��3q�
�������
��������� -   $��
OpenButton��$��1��$��Open Angle Data��$��16(�$
��Bold"�$��40��$	��180��$��Center=�$��Center=��� -   $��AnalyzeButton��$��1��$��2��$��	0,0,210,0q�$��Analyze��$��20(�$
��Bold"�$��40��$	��180��$��Right=�$��Center=�{�.  �� -   $��ExportButton��$��1��$��2��$��	0,0,010,0q�$��Export Results��$��20(�$
��Bold"�$��40��$	��180��$��Right=�$��Center=�{�.  �� -   $��fileNameBlock��$��1��$��1��$��2��$��
10,0,400,0q�$
��Wrap=�$��File: ��$��16(�$��50��$��Center=��� $��2��$��0��
�������$��5q�
�������._��� $
��Auto=�.���� $	��180��.+��� $��	0,10,0,10q�$��Center=�$��Settings��$��30(�$
��Bold"�
��� E���� $��	0,10,0,10q�$��Center=�$��
Upper Arms��$��16(��� -   $��UpperArmSupportCheckBox��$��
10,0,10,10q�.���� $
��Wrap=�$1��+Leaning or supporting the weight of the arm��$��11(��� $��0,0,0,10q�$��Center=�$��
Lower Arms��$��16(��� -   $��LowerArmMidlineCheckBox��$��
10,0,10,10q�.���� $
��Wrap=�$?��9Working across the midline of the body or out to the side��$��11(��� $��0,0,0,10q�$��Center=�$��Trunk��$��16(��� -   $��TrunkSupportCheckBox��$��
10,0,10,10q�.���� $
��Wrap=�$!��Well supported while seated��$��11(��� $��0,0,0,10q�$��Center=�$
��Legs��$��16(��� -	   $��LegSittingCheckBox��$��	10,0,10,5q�.���� $
��Wrap=�$��Sitting��$��11(��� -
   $��LegSupportComboBox��$��
10,0,10,10q�$��11(�.@��� $��11(� �� 
IsSelected . .��.,Bilateral weight bearing, walking or sitting�� $��11(�.��JHUnilateral weight bearing, feather weight bearing or an unstable posture�� $��0,0,0,10q�$��Center=�$��
Muscle Use��$��16(��� -   $��MuscleCheckBox��$��
10,0,10,10q�.���� $
��Wrap=�$T��NMainly static, e.g. held for longer than 1 min; Repeated more than 4 times/min��$��11(��� $��0,0,0,10q�$��Center=�$��
Force Load��$��16(��� -   $��ForceComboBox��$��	10,0,10,5q�$��11(�.@��� $��11(� . .��;9No resistance or less than 2kg intermittent load or force�� $��11(�.��#!2~10kg intermittent load or force�� $��11(�.��312~10kg static load; 2~10kg repeated load or force�� $��11(�.��hf10kg or more static load; 10kg or more repeated loads or forces; Shock or forces with a rapid build-up�� -   $��LoadForceComboBox��$��	10,0,10,5q�$��11(�.@��� $��11(� . .��Less than 5 kg�� $��11(�.��	5 ~ 10 kg�� $��11(�.��More than 10 kg�� -   $��ShockCheckBox��$��
10,0,10,10q�.���� $
��Wrap=�$��Shock or rapid build up��$��11(��� $��0,0,0,10q�$��Center=�$��Coupling��$��16(��� -   $��CouplingComboBox��$��
10,0,10,10q�$��11(�.@���  . $��11(�.��75Good: Well-fitting handle and a mid-range, power grip�� $��11(�.��a_Fair: Hand hold acceptable but not ideal or coupling is acceptable via another part of the body�� $��11(�.��20Poor: Hand hold not acceptable although possible�� $��11(�.��0.Unacceptable: Awkward, unsafe grip, no handles�� $��0,0,0,10q�$��Center=�$��Activity��$��16(��� -   $��Activity1CheckBox��$��	10,0,10,0q�.���� $
��Wrap=�$F��@1 or more body parts are static, e.g. held for longer than 1 min��$��11(��� -   $��Activity2CheckBox��$��	10,0,10,0q�.���� $
��Wrap=�$f��`Repeated small range actions, e.g. repeated more than 4 times per minute (not including walking)��$��11(��� -   $��Activity3CheckBox��$��	10,0,10,0q�.���� $
��Wrap=�$M��GAction causes rapid large range changes in postures or an unstable base��$��11(��� $��2��$��1��$��2��
�������$��5q�
�������._��  �� $��60 ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� $��3��$��0��$��Center=�$��Center=�$��10q�$��Joint Angles��$��30(�$
��Bold"�
��� E���� $��4��$��0��$��Center=�$��Center=�$��10q�$
��RULA��$��30(�$
��Bold"�
��� E���� $��5��$��0��$��Center=�$��Center=�$��10q�$
��REBA��$��30(�$
��Bold"�
��� E���� $��0��$��1��$��Center=�$��Center=�$��10q�$��
Upper Arms��$��30(�$
��Bold"�
��� E���� $��1��$��1��$��2��$��10q�$��/Resources/UpperArms.png��� $��3��$��1�� �� $��2* ��� $��* ��� $��* �.+��� $��0��.+��� $��	Flexion: ��$��18(�$��Right=��� $��Abduction: ��$��18(�$��Right=��� $��
Rotation: ��$��18(�$��Right=��� $��Elevation: ��$��18(�$��Right=��� $��1��.+��� -   $!��RulaUpperArmLeftFlexionText��$	��0°��$��18(�$��Right=��� -   $#��RulaUpperArmLeftAbductionText��$	��0°��$��18(�$��Right=��� -   $"��RulaUpperArmLeftRotationText��$	��0°��$��18(�$��Right=��� -   $#��RulaUpperArmLeftElevationText��$	��0°��$��18(�$��Right=��� $��2��.+��� -   $"��RulaUpperArmRightFlexionText��$	��0°��$��18(�$��Right=��� -   $$��RulaUpperArmRightAbductionText��$	��0°��$��18(�$��Right=��� -   $#��RulaUpperArmRightRotationText��$	��0°��$��18(�$��Right=��� -   $$��RulaUpperArmRightElevationText��$	��0°��$��18(�$��Right=��� -   $��RulaUpperArmScoreText��$��4��$��1��$��10q�$��Stretch=�$��Stretch=� �� TextAlignment$ Center=�$��50(�$
��Bold"�
�������0�� $��0��$��2��$��Center=�$��Center=�$��10q�$��
Lower Arms��$��30(�$
��Bold"�
��� E���� $��1��$��2��$��2��$��10q�$��/Resources/LowerArms.png��� $��3��$��2�� �� $��2* ��� $��* ��� $��* �.+��� $��Flexion:��$��0��$��20(�$��Right=��� -   $!��RulaLowerArmLeftFlexionText��$��1��$	��0°��$��20(�$��Right=��� -   $"��RulaLowerArmRightFlexionText��$��2��$	��0°��$��20(�$��Right=��� -   $��RulaLowerArmScoreText��$��4��$��2��$��10q�$��Stretch=�$��Stretch=�$ Center=�$��50(�$
��Bold"�
�������0�� $��0��$��3��$��Center=�$��Center=�$��10q�$��Wrists��$��30(�$
��Bold"�
��� E���� $��1��$��3��$��2��$��10q�$��/Resources/Wrists.png��� $��3��$��3�� �� $��2* ��� $��* ��� $��* �.+��� $��0��.+��� $��	Flexion: ��$��20(�$��Right=��� $��Deviation: ��$��20(�$��Right=��� $��
Rotation: ��$��20(�$��Right=��� $��1��.+��� -   $��RulaWristLeftFlexionText��$	��0°��$��20(�$��Right=��� -    $ ��RulaWristLeftDeviationText��$	��0°��$��20(�$��Right=��� -!   $��RulaWristLeftRotationText��$	��0°��$��20(�$��Right=��� $��2��.+��� -"   $��RulaWristRightFlexionText��$	��0°��$��20(�$��Right=��� -#   $!��RulaWristRightDeviationText��$	��0°��$��20(�$��Right=��� -$   $ ��RulaWristRightRotationText��$	��0°��$��20(�$��Right=�� $��4��$��3�� �� $��* ��� $��* �.+��� -%   $��RulaWristScoreText��$��0��$��
10,10,5,10q�$��Stretch=�$��Stretch=�$ Center=�$��50(�$
��Bold"�
�������0�� -&   $��RulaWristTwistScoreText��$��1��$��
5,10,10,10q�$��Stretch=�$��Stretch=�$ Center=�$��50(�$
��Bold"�
�������0�� $��0��$��4��$��Center=�$��Center=�$��10q�$
��Neck��$��30(�$
��Bold"�
��� E���� $��1��$��4��$��2��$��10q�$��/Resources/Neck.png��� $��3��$��4�� �� $��* ��� $��* �.+��� $��0��.+��� $��	Flexion: ��$��20(�$��Right=��� $��	Bending: ��$��20(�$��Right=��� $��
Rotation: ��$��20(�$��Right=��� $��1��.+��� -'   $��RulaNeckFlexionText��$	��0°��$��20(�$��Right=��� -(   $��RulaNeckBendingText��$	��0°��$��20(�$��Right=��� -)   $��RulaNeckRotationText��$	��0°��$��20(�$��Right=��� -*   $��RulaNeckScoreText��$��4��$��4��$��10q�$��Stretch=�$��Stretch=�$ Center=�$��50(�$
��Bold"�
�������0�� $��0��$��5��$��Center=�$��Center=�$��10q�$��Trunk��$��30(�$
��Bold"�
��� E���� $��1��$��5��$��2��$��10q�$��/Resources/Trunk.png��� $��3��$��5�� �� $��* ��� $��* �.+��� $��0��.+��� $��	Flexion: ��$��20(�$��Right=��� $��	Bending: ��$��20(�$��Right=��� $��
Rotation: ��$��20(�$��Right=��� $��1��.+��� -+   $��RulaTrunkFlexionText��$	��0°��$��20(�$��Right=��� -,   $��RulaTrunkBendingText��$	��0°��$��20(�$��Right=��� --   $��RulaTrunkRotationText��$	��0°��$��20(�$��Right=��� -.   $��RulaTrunkScoreText��$��4��$��5��$��10q�$��Stretch=�$��Stretch=�$ Center=�$��50(�$
��Bold"�
�������0�� $��0��$��6��$��Center=�$��Center=�$��10q�$
��Legs��$��30(�$
��Bold"�
��� E���� $��1��$��6��$��2��$��10q�$��/Resources/Legs.png���� -/   $��RulaLegScoreText��$��4��$��6��$��10q�$��Stretch=�$��Stretch=�$ Center=�$��50(�$
��Bold"�
�������0� $��3��$��6�� �� $��2* ��� $��* ��� $��* �.+��� $��Flexion:��$��0��$��20(�$��Right=��� -0   $��RebaLegLeftFlexionText��$��1��$	��0°��$��20(�$��Right=��� -1   $��RebaLegRightFlexionText��$��2��$	��0°��$��20(�$��Right=��� $��0��$��7��$��Center=�$��Center=�$��10q�$��Others��$��30(�$
��Bold"�
��� E���� $��4��$��7��.+��� -2   $��RulaMuscleAScoreText��$��
10,10,10,2q�$��Stretch=�$��Stretch=�$ Center=�$
��Bold"�
�������$��13(�Muscle A: 0�� -3   $��RulaForceAScoreText��$��	10,2,10,2q�$��Stretch=�$��Stretch=�$ Center=�$
��Bold"�
�������$��13(�
Force A: 0�� -4   $��RulaMuscleBScoreText��$��	10,2,10,2q�$��Stretch=�$��Stretch=�$ Center=�$
��Bold"�
�������$��13(�Muscle B: 0�� -5   $��RulaForceBScoreText��$��
10,2,10,10q�$��Stretch=�$��Stretch=�$ Center=�$
��Bold"�
�������$��13(�
Force B: 0�� $��6��$��4��$��0��$��8��
��� E��$��5q�
��������� -6   $��RulaGrandScoreText��$��6��$��0��$��2��$��
10,10,10,0q�$��Stretch=�$��Stretch=�$ Center=�$��50(�$
��Bold"�
�������RULA Score: 0�� -7   $��RulaActionText��$��7��$��0��$��2��$��
10,0,10,10q�$��Stretch=�$��Stretch=�$ Center=�$��21(�$
��Bold"�
�������$
��Wrap=�LJPosture is acceptable if it is not maintained or repeated for long periods�� -8   $��RebaUpperArmScoreText��$��5��$��1��$��10q�$��Stretch=�$��Stretch=�$ Center=�$��50(�$
��Bold"�
�������0�� -9   $��RebaLowerArmScoreText��$��5��$��2��$��10q�$��Stretch=�$��Stretch=�$ Center=�$��50(�$
��Bold"�
�������0�� -:   $��RebaWristScoreText��$��5��$��3��$��10q�$��Stretch=�$��Stretch=�$ Center=�$��50(�$
��Bold"�
�������0�� -;   $��RebaNeckScoreText��$��5��$��4��$��10q�$��Stretch=�$��Stretch=�$ Center=�$��50(�$
��Bold"�
�������0�� -<   $��RebaTrunkScoreText��$��5��$��5��$��10q�$��Stretch=�$��Stretch=�$ Center=�$��50(�$
��Bold"�
�������0�� -=   $��RebaLegScoreText��$��5��$��6��$��10q�$��Stretch=�$��Stretch=�$ Center=�$��50(�$
��Bold"�
�������0�� $��5��$��7��.+��� ->   $��RebaCouplingScoreText��$��10,10,10,2.5q�$��Stretch=�$��Stretch=�$ Center=�$��18(�$
��Bold"�
�������Coupling: 0�� -?   $��RebaLoadForceScoreText��$��10,2.5,10,2.5q�$��Stretch=�$��Stretch=�$ Center=�$��18(�$
��Bold"�
�������Load/Force: 0�� -@   $��RebaActivityScoreText��$��10,2.5,10,10q�$��Stretch=�$��Stretch=�$ Center=�$��18(�$
��Bold"�
�������Activity: 0�� -A   $��RebaGrandScoreText��$��6��$��4��$��2��$��
10,10,10,0q�$��Stretch=�$��Stretch=�$ Center=�$��50(�$
��Bold"�
�������REBA Score: 0�� -B   $��RebaActionText��$��7��$��4��$��2��$��
10,0,10,10q�$��Stretch=�$��Stretch=�$ Center=�$��24(�$
��Bold"�
�������$
��Wrap=�Risk level: Negligible�� Action: None necessary  OxyPlot.Wpf.PlotView  $��6��$��4��$��2��$��2��$��10q�   Model �� *MyModelRula+  $��6��$��4��$��6��$��2��$��10q� �� *MyModelReba+� $��8��$��2��$��0�� �� $��* ��� $��2* � �� $��* ��� $��* �.+��� $��0��$��0��$��	Avg.Score��$��Center=�$��Center=�$��20(�$
��Bold"��� $��0��$��1��$��Duration��$��Center=�$��Center=�$��20(�$
��Bold"��� -C   $��RulaAverageScoreText��$��1��$��0��$��Center=�$��Center=�$��20(��� -D   $��RulaDurationText��$��1��$��1��$��Center=�$��Center=�$��20(�� $��8��$��2��$��1�� �� $��* ��� $��2* � �� $��* ��� $��* ��� $��* �.+��� $��0��$��0��$�� 3~4 ��$��Center=�$��Center=�$��20(�$
��Bold"�
��� ����� $��0��$��1��$�� 5~6 ��$��Center=�$��Center=�$��20(�$
��Bold"�
��� ����� $��0��$��2��$	�� 7 ��$��Center=�$��Center=�$��20(�$
��Bold"�
���  ���� -E   $��Rula34DurationText��$��1��$��0��$��Center=�$��Center=�$ Center=�$��16(��� -F   $��Rula56DurationText��$��1��$��1��$��Center=�$��Center=�$ Center=�$��16(��� -G   $��Rula7DurationText��$��1��$��2��$��Center=�$��Center=�$ Center=�$��16(�� $��8��$��2��$��4�� �� $��* ��� $��2* � �� $��* ��� $��* �.+��� $��0��$��0��$��	Avg.Score��$��Center=�$��Center=�$��20(�$
��Bold"��� $��0��$��1��$��Duration��$��Center=�$��Center=�$��20(�$
��Bold"��� -H   $��RebaAverageScoreText��$��1��$��0��$��Center=�$��Center=�$��20(��� -I   $��RebaDurationText��$��1��$��1��$��Center=�$��Center=�$��20(�� $��8��$��2��$��5�� �� $��* ��� $��2* � �� $��* ��� $��* ��� $��* �.+��� $��0��$��0��$�� 4~7 ��$��Center=�$��Center=�$��20(�$
��Bold"�
��� ����� $��0��$��1��$�� 8~10 ��$��Center=�$��Center=�$��20(�$
��Bold"�
��� ����� $��0��$��2��$�� 11~15 ��$��Center=�$��Center=�$��20(�$
��Bold"�
���  ���� -J   $��Reba47DurationText��$��1��$��0��$��Center=�$��Center=�$ Center=�$��16(��� -K   $��Reba810DurationText��$��1��$��1��$��Center=�$��Center=�$ Center=�$��16(��� -L   $��Reba1115DurationText��$��1��$��2��$��Center=�$��Center=�$ Center=�$��16(��� $��3��$��2��$��Bottom=�$��5q�$����Copyright © 2020 Woojoo Kim and Chunxi Huang. KAIST Human Factors and Ergonomics Lab. All rights reserved. http://hfel.kaist.ac.kr/���� $��3��$��2��$��5,8,0,24q�$
��Left=�$��/Resources/KAIST_logo.png���� $��3��$��2��$��
110,6,0,22q�$
��Left=�$��/Resources/HFE_logo.png��� $��4��$��2�� �� $��* ��� $��* ��� $��* ��� $��3* �.+��� $��0��$��Timeline��$��24(�$
��Bold"�$��Center=�$��Center=��� -M   $��TimelineText��$��2��$��10q�$��0��$��24(�$��Right=�$��Center=��� -N   $��TimelineSlider��$��3��$��	36,0,20,0q�$��Center=�{�.   � SmallChange$ 1Q� � LargeChange$	 240Q�	 �� IsSnapToTickEnabled	 . 
 �� TickFrequency$
 1Q��� -O   $��	MaxButton��$��1��$��10q�$��Find Max��$��20(�$
��Bold"�{�.  