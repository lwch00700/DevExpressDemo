   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089a ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ac
 _DevExpress.Xpf.Charts.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a,   &DevExpress.Xpf.Core.ImageDataConverter   ImageDataConverter * &tabViewDetailDescriptorContentTemplate-  'DevExpress.Xpf.Core.MeasurePixelSnapper  � ColumnDefinitions � RowDefinitions&   DevExpress.Xpf.Editors.ImageEdit%  DevExpress.Xpf.Editors.BaseEdit   
IsReadOnly   ShowMenu   Source �� Path �� 	Converter �� StringFormat " embeddedDetailViewInfoTemplate  employeeStatsTemplate �� Padding( 
 "DevExpress.Xpf.Charts.ChartControl	   CrosshairEnabled
   Diagram' 
 !DevExpress.Xpf.Charts.XYDiagram2D# 
 DevExpress.Xpf.Charts.Diagram   Series( 
 "DevExpress.Xpf.Charts.LineSeries2D" 
 DevExpress.Xpf.Charts.Series   ShowInLegend   ArgumentDataMember&	 
  DevExpress.Xpf.Charts.XYSeries2D 	  CrosshairEnabled   ValueDataMember   
DataSource  cardViewTemplate%
  DevExpress.Xpf.Grid.GridControl)  #DevExpress.Xpf.Grid.DataControlBase   
ShowBorder   ItemsSource 
  Columns$  DevExpress.Xpf.Grid.GridColumn$  DevExpress.Xpf.Grid.ColumnBase   	FieldName 
  View"  DevExpress.Xpf.Grid.CardView&   DevExpress.Xpf.Grid.GridViewBase   ShowGroupPanel   
CardLayout&   DevExpress.Xpf.Grid.DataViewBase   ShowColumnHeaders   NavigationStyle   CardTemplate   Stretch)   #GridDemo.CountToVisibilityConverter   Invert�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5       localclr-namespace:GridDemo  5      @dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid    5      =dx2http://schemas.devexpress.com/winfx/2008/xaml/core  	 5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      >dxc4http://schemas.devexpress.com/winfx/2008/xaml/charts 
 5      %�  &	        &	      7   &	     7   &	 �	    7   &	 2    7      5	      6   �� 5
      .R�5   
     $��	5,5,10,10q�6!   ._�5      �   5      �� 5      $
��Auto �6'   �� 5      $��12 �6'   �� 5      $��* �6'    5      �� 5      �� 5      .+�5        $
��Left=�6    $	��Top=�6;    . $��50��5        .   5       �� $ Photo�8   5   +   � 5      $��2��6     5      �� 5      $
��Auto �6+   �� 5      $��8 �6+   �� 5      $
��Auto �6+   �� 5      $��24 �6+   �� 5      $
��Auto �6+   �� 5       $��8 �6+   �� 5!       5#      �� 5$      �� 6,   �� 6>   .+�5&      �� $��0��6    $��0��60   $��First Name:��6=   �� 5'      $��0��6    $��1��60   $��
Last Name:��6=   �� 5(      $��0��6    $��2��60   $��Address:��6=   �� 5)      $��4��6    $��0��60   $��	Position:��6=   �� 5*      $��4��6    $��1��60   $��
Hire Date:��6=   �� 5+      $��2��6    $��0��60   ��6=   �� *	FirstName+�� 5,      $��2��6    $��1��60   ��6=   �� *
LastName+�� 5-      $��6��6    $��0��60   ��6=   �� *Title+�� 5.      $��6��6    $��1��60   ��6=   �� *
HireDate+$ d���� 5/      $��2��6    $��2��60   ��6=   �� *	Address+50      51      52      53      �� 54      .R�55   
     $��	5,5,10,10q�6!   ._�56      �   57      �� 58      $
��Auto �6'   �� 59      $��12 �6'   �� 5:      $��* �6'    5<      �� 5=      �� 5>      .+�5@        $	��150��6    $
��Left=�6/   $	��Top=�6J    . $��80��5B        .   5A       �� $ Photo�8   5B   +   � 5C      $��2��6     5D      �� 5E      $
��Auto �6+   �� 5F      $��12 �6+   �� 5G       5I      �� 5J      �� 6,   �� 5K      �� 6,   �� 5L      .+�5N      �� $��0��6    $��0��60   $��Name:��6=   �� 5O      $��0��6    $��1��60   $��	Position:��6=   �� 5P      $��0��6    $��2��60   $��
Hire Date:��6=   �� 5Q      $��0��6    $��3��60   $��About:��6=   �� 5R      $��0��6    $��4��60   $��Address:��6=   �� 5S      $��2��6    $��0��60   ��6=   �� *	FirstName+�� 5T      $��2��6    $��1��60   ��6=   �� *Title+�� 5U      $��2��6    $��2��60   ��6=   �� *
HireDate+�� 5V      $��2��6    $��3��60   $
��Left=�6=   $
��Wrap=�6o   ��6X   �� *Notes+6o   �� 5W      $��2��6    $��4��60   ��6=   �� *	Address+5X      5Y      5Z      5[      �� 5\      .R�5]   
   �� $��1,0,1,1q�6   $ 5q�6+   
�������$	��195��6N   ._�5^      �   5_      �� 5`      $
��Auto �6'   �� 5a      .+�5c        $	��120��6     .  $��5,5,0,0q�6=   $
��Left=�6O   $	��Top=�6j    .  5d       �� $ Photo�8     5e      $��1��6#   $��0q�63   $	 FalseX�6G   
 5f        5g       5h        5i   "    .  $ ArgumentMember��5j   4   $ FalseX�6X   $ ValueMember��5k   4    5i   3   �� *ChartSource+5k   4   5m      5o      5p      5q      5r      �� 5s      .R�5t   
   � $	��195��6   .+�5u      
  $	��300��6@    .   6   �� *	Employees+ 5v        5w      $ Photo��6%    5y        5z       .  $ Columns=�6:    .  $	 Row=�6i    5{      �� 5|      .R�5}   "   �� .+�5~   &     $ UniformToFill=�64    .  $	��100��6]   $	��100��6m    .  5   ,   �� $ DataContext.Photo�8   �� 5�   &   ��60   �� $ DataContext.FullName�5�   #   5�      5�      y�5�      �� 5�      $ Employees.Count�6    5�        5�      5�      5�      �� 5�      $-��'This Employee doesn't have Subordinates��6   $��3q�5�      $��Center=�6#   $��Center=�6@   $��20(�6[   y�5�      �� 5�      $ Employees.Count�6    5�        5�       . 6?   5�      5�      5�      5�      5�      