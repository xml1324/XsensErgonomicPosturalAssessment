   M S B A M L   `   `   `  ����R  NXsensPosturalAssessment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:XsensPosturalAssessmentXsensPosturalAssessment  5   	   (    "XsensPosturalAssessment.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:XsensPosturalAssessment     5   	     � Title$=  7Xsens-based automated WMSDs ergonomic assessment system��5   	   $	��600��6I   $	��800��6V    � 
ResizeMode$ CanMinimize=�5	   	   .��5
      �  � RowDefinitions 5   
   �� 5      $��3* �6   6*   �� 5      $��4* �6   6*   �� 5      $��4* �6   6*   �� 5      $
��1.5* �6   6,   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $��3* �6   6,   �� 5      $��* �6   6+   .+�5   
   �� $��2��6   $��Center=�6(   $��Bottom=�6E    �� TextAlignment$ Center=�6`   $��35(�6w   $��20q�6�   $
��Bold"�6�   $
��Wrap=�6�   
��� E��Xsens-based automated WMSDs�� 5   *   66   ergonomic assessment system5      �� 5   
   -   5        $��RULAREBAButton��5      $��1��6'   $��1��64   $��
0,10,10,10q�6D   $��16(�6X   $
��Bold"�6f   
�������.��5      �� $��2q�6   $
��Wrap=�6#   $ Center=�67   Postural ergonomic analysis�� 5   .   6:   (RULA/REBA)5      5      �� 5    
   -   5        $��LBCF2DButton��5       $��2��6&   $��1��63   $��
0,10,10,10q�6C   $��16(�6W   $
��Bold"�6e   
�������.��5!      �� $��2q�6   $
��Wrap=�6#   $ Center=�67   " 2D static biomechanical analysis�� 5"   3   6?   (Link-segment model)5#      5$      �� 5&   
   $��5q�6   $��3��6   $��2��6,   $
��Wrap=�6@   ��Note: Targeting at improving occupational safety and workers’ health, this system could provide WMSDs risk assessment through postural ergonomic analysis and 2D static biomechanical analysis based on the human motion captured by the Xsens system.�� 5'   �   6  }WMSDs – Work-related Musculoskeletal Disorders, RULA – Rapid Upper Limb Assessment, REBA – Rapid Entire Body Assessment5(      �� 5*   
   $��1��6   $��2��6   $��10,0,0,0q�6.   $'��!/Resources/Main window figure.jpg��6@   6m   �� 5,   
   $��4��6   $��2��6!   $��Bottom=�65   $��5q�6P   $��8(�6[   $��Right=�6h   $����Copyright © 2020 Woojoo Kim and Chunxi Huang. KAIST Human Factors and Ergonomics Lab. All rights reserved. http://hfel.kaist.ac.kr/��6�   �� 5-   
   $��4��6   $��2��6   $��30��61   $
��Left=�6=   $��/Resources/KAIST_logo.png��6X   �� 5.   
   $��4��6   $��2��6   $��30��61   $��	110,0,0,0q�6=   $
��Left=�6P   $��/Resources/HFE_logo.png��6k   5/      50      