   M S B A M L   `   `   `  ���� R  NXsensPosturalAssessment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:XsensPosturalAssessmentXsensPosturalAssessment  *    $XsensPosturalAssessment.LBCF2DWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     7local%clr-namespace:XsensPosturalAssessment     R NOxyPlot.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf    � Title$3  -Xsens-based 2D static biomechanical analysis ��$
��1080��$
��1920�� � WindowStartupLocation$ CenterScreen=� � 	Resources    SliderRepeatButton �� 
TargetType �� Property �� Value �� 
TargetType  SliderRepeatButton1 J� SnapsToDevicePixels  SliderThumb �� Orientation 
 Slider	 � RowDefinitions
 m�Name m� Fill m� 	Placement ��Name ]�Name ]� DecreaseRepeatButton ]� Thumb ]� IncreaseRepeatButton �� Triggers P� Property P� Value �� 
TargetName  Horizontal_Slider �� Triggers��%m  &	        &	 �     &	 e    &	 �    7  7  7   &	 H    7  �� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " ��.R��� 
������ �� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� �� " ��.R���  . 
���2͚�$��1q�
���2͚�$��10���� " n�.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� �� " n�.R��� $ Vertical=��� " ��.R�� 	 �� $
��Auto ��� $
��Auto �#��z���� $
��Auto �.+�m� $
 TopTick��
 �����$	��Top=� . $��0��$	 Top=�$��5��$y�Visible=��� 
�������$��0,0,0,1q��� $ TrackBackground��$��Center=�$��	0,-10,0,0q�
���  ��
���  ��$��10��$��1��$��1q�]� $ 
PART_Track��$��1��$��	0,-10,0,0q� �� 8��  $��Slider.DecreaseLarge�� n� 8�� $��	0,-20,0,0q� �� 8�� $��Slider.IncreaseLarge��m� $
 
BottomTick��
 ����� . $��2��$ Bottom=�$��4��$y�	Collapsed=� P�  �@��TickPlacement$ TopLeft=�. ��� $ TopTick�� � y�$ Visible=�P�  �@��TickPlacement$ BottomRight=�. ��� $ 
BottomTick�� � y�$ Visible=�P�  �@��TickPlacement$
 Both=�. ��� $ TopTick�� � y�$ Visible=��� $ 
BottomTick�� � y�$ Visible=��� " ��.���  � |� .  ��  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle .  P�  �@��Orientation$ 
Horizontal=�. ���  � ��$ 21����  � ��$	 104����  � ��8   .��� 	 �� $��70 ��� $��60 ��� $��* ��� $��* ��� $��60 � � ColumnDefinitions �� $	��200 ��� $��* ��� $��* �.+��� $��0��$��3��
�������$��3q�
��� E��._��� $��10q�$3��-Xsens-based 2D static biomechanical analysis ��$��30(�$
��Bold"�$��Center=�
��������� -   $��
BackButton��$
��Left=�$��
 ◀ Back ��$��30(�
��� E��$��3q�
�������
��������� -   $��
OpenButton��$��1��$��Open Position Data��$��16(�$
��Bold"�$��40��$	��180��$��Center=�$��Center=��� -   $��AnalyzeButton��$��1��$��2��$��	0,0,210,0q�$��Analyze��$��20(�$
��Bold"�$��40��$	��180��$��Right=�$��Center=�{�.  �� -   $��ExportButton��$��1��$��2��$��	0,0,010,0q�$��Export Results��$��20(�$
��Bold"�$��40��$	��180��$��Right=�$��Center=�{�.  �� -   $��fileNameBlock��$��1��$��1��$��2��$��
10,0,400,0q�$
��Wrap=�$��File: ��$��16(�$��50��$��Center=��� $��2��$��2��$��0��
�������$��5q�
�������._��� $
��Auto=�.���� $	��180��.+��� $��0,10,0,0q�$��Center=�$��Inputs��$��30(�$
��Bold"�
��� E���� $��10,20,10,10q�$��Center=�$ ��Weight of human body (kg):��$��16(�$
��Wrap=��� -   $��WeightLoadTextBox��$��70��$��
10,0,10,10q�$��16(�$��30��$��Center=��� $��10,10,10,10q�$��Center=�$��Height of human body (m):��$��16(�$
��Wrap=��� -   $��HeightHumanTextBox��$
��1.75��$��
10,0,10,10q�$��16(�$��30��$��Center=��� $��10,10,10,10q�$��Center=�$"��Weight of load to lift (kg):��$��16(�$
��Wrap=��� -   $��WeightHumanTextBox��$��20��$��
10,0,10,10q�$��16(�$��30��$��Center=��� $��	0,20,0,10q�
�������$��5q��� $��
10,10,10,0q�$��Gender��$��20(�$
��Bold"�	� -	   $��MaleRadioButton�� 	� 	GroupName$ GenderRadioButtonGroup��$��Center=�$��10,10,10,10q� d� 	IsChecked$
 TrueY�.���� $
��Wrap=�$
��Male��$��16(�	� -
   $��FemaleRadioButton��$ GenderRadioButtonGroup��$��Center=�$��
10,0,10,10q�.���� $
��Wrap=�$��Female��$��16(��� $��
10,10,10,0q�$��Joints��$��20(�$
��Bold"��� -   $��WristCheckBox��$��Center=�$��10,10,10,10q�$
 TrueY�.���� $
��Wrap=�$��Wrist��$��16(��� -   $��ElbowCheckBox��$��Center=�$��
10,0,10,10q�$
 TrueY�.���� $
��Wrap=�$��Elbow��$��16(��� -   $��ShoulderCheckBox��$��Center=�$��
10,0,10,10q�$
 TrueY�.���� $
��Wrap=�$��Shoulder��$��16(��� -   $��TorsoCheckBox��$��Center=�$��
10,0,10,10q�$
 TrueY�.���� $
��Wrap=�$��Torso��$��16(��� -   $��HipCheckBox��$��Center=�$��
10,0,10,10q�$
 TrueY�.���� $
��Wrap=�$	��Hip��$��16(��� -   $��KneeCheckBox��$��Center=�$��
10,0,10,10q�$
 TrueY�.���� $
��Wrap=�$
��Knee��$��16(��� -   $��AnkleCheckBox��$��Center=�$��
10,0,10,10q�$
 TrueY�.���� $
��Wrap=�$��Ankle��$��16(��� $��2��$��1��$��2��
�������$��5q�
�������._��  �� �� �� �� �� �� �� $��6* �	 �� $��6* ��� �� .+��� $��6��$��3��.+��� $
��10,5q�$1��+Maximum Compression Force on Low back (N): ��$��30(�$
��Wrap=��� -   $��MaximumLBCFTextBlock��$
��10,5q�$��30(�$
��Bold"��� $
��10,0q�$y��sNote: [NIOSH, 1981] Action Limit (AL) = 3400N = 'at risk', Max Permissible Limit (MPL) = 6400N = 'severe condition'��$��14(�$
��Wrap=��� $
��10,0q�$y��sCompression force < AL (safe), AL < Compression force < MPL (be careful) and if Compression force > MPL (dangerous)��$��14(�$
��Wrap=��� $
��10,5q�$U��OAverage Compression Force on low back during task (not including standing) (N):��$��30(�$
��Wrap=��� -   $��AverageLBCFTextBlock��$
��10,5q�$��30(�$
��Bold"��� $
��10,5q�$b��\Duration (during task) with compression force over 3400N, which indicated at risk (seconds):��$��30(�$
��Wrap=��� -   $��DurationLBCFTextBlock��$
��10,5q�$��30(�$
��Bold"�  OxyPlot.Wpf.PlotView  $��0��$��0��$��6��$��10q�   Model �� *MyModelLBCF+�� $��1��$��1��$	��Min��$��24(�$��Center=�$��Center=��� $��1��$��2��$��Q1��$��24(�$��Center=�$��Center=��� $��1��$��3��$	��Med��$��24(�$��Center=�$��Center=��� $��1��$��4��$
��Mean��$��24(�$��Center=�$��Center=��� $��1��$��5��$��Q3��$��24(�$��Center=�$��Center=��� $��1��$��0��$	��Max��$��24(�$��Center=�$��Center=��� -   $��SummaryLBCFTextBlock1��$��2��$��1��$��24(�$
��Bold"�$��Center=�$��Center=��� -   $��SummaryLBCFTextBlock2��$��2��$��2��$��24(�$
��Bold"�$��Center=�$��Center=��� -   $��SummaryLBCFTextBlock3��$��2��$��3��$��24(�$
��Bold"�$��Center=�$��Center=��� -   $��SummaryLBCFTextBlock4��$��2��$��4��$��24(�$
��Bold"�$��Center=�$��Center=��� -   $��SummaryLBCFTextBlock5��$��2��$��5��$��24(�$
��Bold"�$��Center=�$��Center=��� -   $��SummaryLBCFTextBlock6��$��2��$��0��$��24(�$
��Bold"�$��Center=�$��Center=��� $��3��$��1��$��2��
�������$��5q�
�������._�� 	 �� �� �� �� �� �� ��  �� $��6* ��� �� $��4* ��� �� $��4* �.+�  $��0��$��0��$��7��$��10q� �� *MyModelPercentile+  $��0��$��4��$��7��$��10q� �� *MyModelSkeleton+�� $��0��$��1��$��7��$��3��
�������
�������$��10q��� $��0��$��1��$��Wrist��$��20(�$��Center=�$��Center=��� $��1��$��1��$��Elbow��$��20(�$��Center=�$��Center=��� $��2��$��1��$��Shoulder��$��20(�$��Center=�$��Center=��� $��3��$��1��$��Torso��$��20(�$��Center=�$��Center=��� $��4��$��1��$	��Hip��$��20(�$��Center=�$��Center=��� $��5��$��1��$
��Knee��$��20(�$��Center=�$��Center=��� $��6��$��1��$��Ankle��$��20(�$��Center=�$��Center=��� -   $��WristSlider��#��[ $��0��$��2��$��Center=�{�.   � Maximum$	 100Q��� -   $��ElbowSlider��#��[ $��1��$��2��$��Center=�{�.  $	 100Q��� -   $��ShoulderSlider��#��[ $��2��$��2��$��Center=�{�.  $	 100Q��� -   $��TorsoSlider��#��[ $��3��$��2��$��Center=�{�.  $	 100Q��� -   $��	HipSlider��#��[ $��4��$��2��$��Center=�{�.  $	 100Q��� -    $��
KneeSlider��#��[ $��5��$��2��$��Center=�{�.  $	 100Q��� -!   $��AnkleSlider��#��[ $��6��$��2��$��Center=�{�.  $	 100Q��� -"   $��WristPercentTextBlock��$��0��$��3��$�� %��$��20(�$��Center=�$��Right=�$��0,0,30,0q��� -#   $��ElbowPercentTextBlock��$��1��$��3��$�� %��$��20(�$��Center=�$��Right=�$��0,0,30,0q��� -$   $��ShoulderPercentTextBlock��$��2��$��3��$�� %��$��20(�$��Center=�$��Right=�$��0,0,30,0q��� -%   $��TorsoPercentTextBlock��$��3��$��3��$�� %��$��20(�$��Center=�$��Right=�$��0,0,30,0q��� -&   $��HipPercentTextBlock��$��4��$��3��$�� %��$��20(�$��Center=�$��Right=�$��0,0,30,0q��� -'   $��KneePercentTextBlock��$��5��$��3��$�� %��$��20(�$��Center=�$��Right=�$��0,0,30,0q��� -(   $��AnklePercentTextBlock��$��6��$��3��$�� %��$��20(�$��Center=�$��Right=�$��0,0,30,0q��� $��4��$��2��$��Bottom=�$��5q�$����Copyright © 2020 Chunxi Huang and Woojoo Kim. KAIST Human Factors and Ergonomics Lab. All rights reserved. http://hfel.kaist.ac.kr/���� $��4��$��2��$��5,8,0,24q�$
��Left=�$��/Resources/KAIST_logo.png���� $��4��$��2��$��
110,6,0,22q�$
��Left=�$��/Resources/HFE_logo.png��� $��4��$��2�� �� $��* ��� $��* ��� $��6* �.+��� $��0��$��Timeline��$��24(�$
��Bold"�$
��Left=�$��Center=��� -)   $��TimelineText��$��1��$��10q�$��0��$��24(�$��Right=�$��Center=��� -*   $��TimelineSlider��$��2��$��	36,0,20,0q�$��Center=�{�.   � SmallChange$ 1Q� � LargeChange$	 240Q� �� IsSnapToTickEnabled .  �� TickFrequency$ 1Q�