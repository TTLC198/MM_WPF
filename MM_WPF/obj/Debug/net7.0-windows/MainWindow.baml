   M S B A M L   `   `   `  ����A  =MM_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MM_WPF.ComponentsMM_WPF.Components  5   	       MM_WPF.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6
componentsclr-namespace:MM_WPF.Components     5   	   -   5          � Title$  
MainWindow��5   	   $	��450��6   $	��450��6)    � DataContext 5	   	   ��  �� RelativeSource ��  �� Mode$
 Self=�.��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �5      �� 5      $��* �5      .+�5   
   �� 
������� �� Padding$ 5q�5      Решение КТЗ5      � 5   
   $��1��5      $��10q�5       5      �� 5      $
��Auto �5      �� 5      $
��Auto �5      �� 5      $
��Auto �5       �� 5!      $
��Auto �5"      �� 5#      $
��Auto �5$      .+�5'      &   System.Windows.Controls.DataGrid  -   5          Name$ SuppliesTable��5)      $��0��5(      $��Stretch=�5,      $��Stretch=�5-         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5.         AutoGenerateColumns$ False��5/       5*      �� *.+��5+      �� *SuppliesItems+	 �� Mode$	 TwoWay=�
   Columns
 50      0  *System.Windows.Controls.DataGridTextColumn  51         
Foreground
 �   �,  &System.Windows.Controls.DataGridColumn   Header$ Предложение��54         
IsReadOnly$ false��55      1  +System.Windows.Controls.DataGridBoundColumn   Binding 52      ��  �� Path$ .�$	 TwoWay=�55      57        59      -   5        $ DemandsTable��5;      $��1��5:      $��Stretch=�5>      $��Stretch=�5?      $ * 5@      $ False��5A       5<      �� *.+��5=      �� *DemandsItems+$	 TwoWay=�
 5B        5C      
 �   �$ 
Спрос��5F      $ false��5G       5D      �� $ .�$	 TwoWay=�5G      5I      #   MM_WPF.Components.CustomTable  5K      -   5          Name$ 	MainTable��5O      $��2��5L         Columns$ 2��5M         Rows$ 2��5N         Header$' !Предложение/Спрос��5Q         Items 5P      �� *Items+$	 TwoWay=�5S      � 5U      $��3��5V       � ColumnDefinitions 5W      �� 5X      $��* �5Y      �� 5Z      $��* �5[      .+�5]      �� -   5        $��0��5^      $#��Добавить строку��5`      �� 5a      -   5        $��1��5b      $%��Добавить колонку��5d      5e      �� 5g      $��4��5h       �� Orientation$ 
Horizontal=�5i      .+�5j      �� -   5        .��5k   0   Вычислить5m      5n      5o      5p      5q      