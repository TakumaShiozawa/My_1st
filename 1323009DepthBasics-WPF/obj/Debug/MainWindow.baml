   M S B A M L   `   `   `  ����  DepthBasics-WPF5    /Microsoft.Samples.Kinect.DepthBasics.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Depth Basics��5   	   $	��735��6   $	��770��6+    � 	Resources 5         MediumGreyBrush �� Color  KinectPurpleBrush  KinectBlueBrush �� 
TargetType �� Property �� Value  SnapshotButton �� 
TargetType �� Orientation ��Name	 �� TextAlignment
 ��Name �� Triggers P� Property P� Value �� 
TargetName  SquareCheckBox �Name��%w
  &	        &	 #     &	 F     '�� i     &	 �     7  7  &	 �    7  7  �� 5   
   $ 	#ff6e6e6e��62   �� 5   
   $ 	#ff52318f��64   �� 5	   
   $ 	#ff00BCF2��62   �� 5
   
   " ��6   .�5      ��  �@J�SnapsToDevicePixels . 64   5      �� 5   
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� $ 
Horizontal=�6)   
������ .+�5   "   �� $ SnapText��6,   #��z��6>   $
	 Left=�6_   $��Center=�6u   8��  $��15(�6�   � 5   "   $��9,0,0,0q�6'   .+�5   &   �� $
 
SnapNormal��6,   $��Images\SnapNormal.png��6@   $
��None=�6_   $��Center=�6n   �� 5   &   $
 	SnapHover��6,   $��Images\SnapHover.png��6?   $
��None=�6]   $��Center=�6l   $y�	Collapsed=�6�   5   #   5      5       5      P� 5       �@J�IsMouseOver . . �5   "   �� $ 
SnapNormal��6Q    � y�$ 	Collapsed=�6?   6Q   �� 5   "   $ 	SnapHover��6O    � y�$ Visible=�6?   6O   �� 5   "   8  $ SnapText��6h    � ��5      5!      5#      5$      �� 5%   
   " ��6   .�5&      ��  � �� 5'      �� 5(      " ��6&   .R�5)      � .+�5*      �� $ 
Horizontal=�6)   
������ .+�5+   "   � $ SquareCheckBoxChecked��6'   .+�5,   &   �� $
 CheckedNormal��6,   $��Images\CheckedNormal.png��6C   $
��None=�6e   $��Center=�6t   �� 5-   &   $
 CheckedHover��6,   $��Images\CheckedHover.png��6B   $
��None=�6c   $��Center=�6r   $y�	Collapsed=�6�   5.   #   � 5/   "   $ SquareCheckBoxUnchecked��6'   $y�	Collapsed=�6H   .+�50   &   �� $
 UncheckedNormal��6,   $ ��Images\UncheckedNormal.png��6E   $
��None=�6i   $��Center=�6x   �� 51   &   $
 UncheckedHover��6,   $��Images\UncheckedHover.png��6D   $
��None=�6g   $��Center=�6v   $y�	Collapsed=�6�   52   #   �� 53   "   $ SquareCheckBoxText��6,   #��z��6H   $
	 Left=�6i   $��Center=�6   8��  $��15(�6�   $��9,0,0,0q�6�   54      55       56      P� 57       �@d�	IsChecked$ falseY�6;   . �58   "   �� $ SquareCheckBoxChecked��6Q    � y�$ 	Collapsed=�6?   6Q   �� 59   "   $ SquareCheckBoxUnchecked��6O    � y�$ Visible=�6?   6O   5:      P� 5;       �@J�IsMouseOver . . �5<   "   �� $ CheckedNormal��6Q    � y�$ 	Collapsed=�6?   6Q   �� 5=   "   $ UncheckedNormal��6Q    � y�$ 	Collapsed=�6?   6Q   �� 5>   "   $ CheckedHover��6O    � y�$ Visible=�6?   6O   �� 5?   "   $ UncheckedHover��6O    � y�$ Visible=�6?   6O   �� 5@   "   8  $ SquareCheckBoxText��6h    � ��5A      5C      5E      5F      5G      .��5I      � -   5        $��
layoutGrid��5I      $��	10 0 10 0q�6    � RowDefinitions 5J   
   �� 5K      $
��Auto �6   �� 5L      $��* �6   �� 5M      $
��Auto �6   �� 5N      $
��Auto �6   .+�5P   
   ]� $��0��6   $��0 0 0 20q�6!   .+�5Q      �� $
��Left=�6   $��Images\Logo.png��6*   $
��Fill=�6C   $��32��6R   $��81��6^   $��0 10 0 5q�6i   �� 5R      $��Right=�6   $��0 0 -1 0q�6/   $��Bottom=�6A   #��[  6\   $��Segoe UI)�6�   $��18(�6�   Depth Basics6�   �� 5S      $��1��6   $��Images\Status.png��6$   $
��None=�6?   $��Center=�6N   $��0 0 0 5q�6k   5T      (� 5U   
   $��1��6    (� Stretch$ Uniform=�6   $��Center=�61   .��5V      � .+�5X      �� -   5        $��Image��5X      $	��640��6%   $	��480��61   � 5Y      -   5        $��BackImageGrid��5Y      .+�5[      �� -   5        $��	BackImage��5[      $	��640��6-   $	��480��69   $
��Fill=�6F   �� 5\      -   5        $��label1��5\   m   
�������$��Label��6/   $
��Left=�6?   $��	0,435,0,0q�6Z   $	��Top=�6{   $��24(�6�   5]      �� 5^      -   5        $��	growImage��5^      $��Center=�6)   $
��Fill=�6F   5_      5a      �� 5b   
   -   5        $��buttonScreenshot��5b   �   $��2��6   #��[ 6   $��
Screenshot��6F   $
��Auto��6[   $��Right=�6i   $��Center=�6�   $��
10 10 0 10q�6�   6�   �� 5c   
   -	   5        $��checkBoxNearMode��5c   �   $��2��6   #��[ 6    $��Default Mode��6H   $
��Auto��6_   $
��Left=�6m   $��Center=�6�   $��
0 10 10 10q�6�   6�   �� 5d   
   -
   5        $��	statusBar��5d   ?   $��3��6   $��Stretch=�6!   $��Bottom=�6P   
�������#��[  6~   .@�5e      �� $��0 0 0 10q�6   .��5f      �� -   5        $��statusBarText��5f      $��-1 0 0 0q�61   JHPress 'Screenshot' to save a screenshot to your 'My Pictures' directory.6�   5g      5h      �� 5i   
   -   5        $��button1��5i   i   $��通常モード��6   $��2��6!   $��23��6.   $
��Left=�6:   $��
164,15,0,0q�6U   $	��Top=�6x   $	��150��6�   6�   �� 5j   
   -   5        $��button3��5j   i   $��角度変更��6   $��2��6    $��23��6-   $��Right=�69   $��
0,15,246,0q�6U   $	��Top=�6x   $	��150��6�   6�   5k      5l      