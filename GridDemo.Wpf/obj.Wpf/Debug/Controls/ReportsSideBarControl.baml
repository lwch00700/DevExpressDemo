   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      f bDevExpress.DemoData.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aqQclr-namespace:DevExpress.DemoData.Helpers;assembly=DevExpress.DemoData.v15.2.CoreDevExpress.DemoData.Helpers 5      $    GridDemo.ReportsSideBarControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a=dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      ] YDevExpress.Mvvm.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm   5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      e	 aDevExpress.Xpf.Printing.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aBdxp6http://schemas.devexpress.com/winfx/2008/xaml/printing 	  5	      j
 fDevExpress.Xpf.LayoutControl.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol 
 5
      #dxdemoclr-namespace:GridDemo 
 5      a	ddhelpersQclr-namespace:DevExpress.DemoData.Helpers;assembly=DevExpress.DemoData.v15.2.Core 
 5      -   5          >�Name$
  root��5   C   $	��230��67    � 	Resources 5      0  *DevExpress.DemoData.Helpers.GlobalResource   DemoScrollViewerResource   Assembly   XamlPath
   Key5  /DevExpress.Mvvm.UI.BooleanToVisibilityConverter  visibilityConverter  reportButtonStyle �� 
TargetType �� Property �� Value  wizardButtonStyle �� BasedOn!  DevExpress.Xpf.Core.DXImage	 �� TextAlignment  showReportButtonStyle  previewButtonStyle  deleteButtonStyle��%  &	        &	 }     &	 �     &	 B    7  &	 �    7  &	     7  &	 [    7    5   
   $$ DevExpress.DemoData.v15.2.Core��6D   $# DemoWindow/DemoScrollBar.xaml��6n   $ DemoScrollViewer��6�     5   
   6.   �� 5   
   " ��6*   .�5      ��  � ��$ 60��6&   �� 5       � ��$ 85��6'   �� 5       � ��$ 3q�6'   �� 5       � ��$ 0q�6(   5      �� 5   
   8   " ��6W   .�5      ��  � �� 5      �� 5      .R�5      �� .+�5        $
��None=�6)   $d��^pack://application:,,,/DevExpress.Images.v15.2;component/Images/Miscellaneous/Wizard_32x32.png��68   �� 5      $
��Wrap=�69   $	 Center=�6M   #��z��6d   ��6(   �� 6d   5      5       5"      5#      �� 5$   
   8   " ��6[   .�5%      ��  � �� 5&      �� 5'      .R�5(      �� .+�5)        $
��None=�6)   $d��^pack://application:,,,/DevExpress.Images.v15.2;component/Images/Reports/DesignReport_32x32.png��68   �� 5*      $
��Wrap=�69   $	 Center=�6M   #��z��6d   ��6(   �� 6d   5+      5,      5.      5/      �� 50   
   8   " ��6X   .�51      ��  � �� 52      �� 53      .R�54      �� .+�55        $
��None=�6)   $]��Wpack://application:,,,/DevExpress.Images.v15.2;component/Images/Print/Preview_32x32.png��68   �� 56      $
��Wrap=�69   $	 Center=�6M   #��z��6d   ��6(   �� 6d   57      58      5:      5;      �� 5<   
   8   " ��6W   .�5=      ��  � �� 5>      �� 5?      .R�5@      �� .+�5A        $
��None=�6)   $[��Upack://application:,,,/DevExpress.Images.v15.2;component/Images/Edit/Delete_32x32.png��68   �� 5B      $
��Wrap=�69   $	 Center=�6M   #��z��6d   ��6(   �� 6d   5C      5D      5F      5G      5H      .��5I      �� $��Disabled=�6   $
��Auto=�6<   ��6_   �� 
 �� Path$
 Resource� �� Source# [  .��5J   
   �� $��0,0,10,0q�6   .+�5K      $  DevExpress.Xpf.Core.GroupFrame  $��
New Report��6   $��0,0,0,12q�60   ��5L      �� 5M      .R�5N      �� $��1,0,0,0q�65   ��6$   �� 65   5O      .��5Q      �� .+�5R      �� $
��Wrap=�6    $��2,0,0,0q�64   -+Generate a report based on the Grid's data.6r   �� 5S      $
��Wrap=�6    $��2,0,0,0q�64   \ZShow Wizard allows you to apply a theme to the report before invoking the report designer.6�   �� 5T      $
��Wrap=�6    $��2,0,0,0q�64   ,*Show Designer invokes the report designer.6q   �� 5U      $
��Wrap=�6    $��2,0,0,12q�64   20Show Preview invokes the report's print preview.6x   � 5V      $��Center=�6    $
��-3,0q�6=   .+�5W      �� #��[ 6d   $��Show Wizard��6�   $
��Bold"�6�   ��6!   �� $/
 )Service.ViewModel.ShowReportWizardCommand�6�   �� 5X      #��[ 6^   $��Show Designer��6�   $
��Bold"�6�   ��6!   �� $)
 #Service.ViewModel.EditReportCommand�6�   �� 5Y      #��[ 6e   $��Show Preview��6�   $
��Bold"�6�   ��6!   �� $0
 *Service.ViewModel.ShowReportPreviewCommand�6�   5Z      5[      5\        5]      $��Recent Reports��6   $��0,0,0,12q�64   y�6F   �� $%
 Service.ViewModel.Reports.Count� �� 	Converter >  8DevExpress.Mvvm.UI.NumericToVisibilityConverterExtension  ��5^      �� 5_      .R�5`      �� $��1,0,0,0q�65   ��6$   �� 65   5a      .��5c      �� .+�5d      �� $��2,0,0,12q�6    $
��Wrap=�62   ��Select one of the previously saved reports to edit it via the report designer, preview the print output or remove it from the list.5f      (  "DevExpress.Xpf.Editors.ListBoxEdit  5g      -   5          Name$ lbSelectedReport��5g   &   $	��2,0q�6@      SelectedIndex$ 0��6M      DisplayMember$ 	Info.Name��6�   $	��140��6�   $��Disabled=�6�   $��Stretch=�6�      ItemsSource 6_   �� $
 Service.ViewModel.Reports�   ItemContainerStyle 5h      �� 5i      ,  &DevExpress.Xpf.Editors.ListBoxEditItem"  6$   .�5j   "   �� 2  ,DevExpress.Mvvm.UI.Interactivity.Interaction �@ BehaviorsTemplate 5k   &   �� 5l   *   .R�5m   .   �� .@�5n   2   '	  !DevExpress.Mvvm.UI.EventToCommand	  �
  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]] 
  	EventName$ MouseDoubleClick��6H   +  %DevExpress.Mvvm.UI.EventToCommandBase   Command 6e   �� *PreviewCommand+%  DevExpress.Mvvm.UI.KeyToCommand  5o   2      
KeyGesture$ Enter��6F    6Y   �� *PreviewCommand+  5p   2   $ Delete��6F    6Z   �� *DeleteCommand+5q   /   5r   +   5t   #   5u      5w      � 5x      $��Center=�6    $
��-3,0q�6=    � DataContext 6K   �� $
 SelectedItem� �� ElementName$ lbSelectedReport��.+�5y      �� #��[ 6F   $��Show Designer��6u   ��6!   �� $
 ShowCommand�{�6�   ��  :  4DevExpress.Mvvm.UI.ObjectToBooleanConverterExtension  �� 5z      #��[ 6I   $��Show Preview��6u   ��6!   �� $
 PreviewCommand�{�6�   ��    �� 5{      #��[ 6H   $��Delete Report��6s   ��6!   �� $
 DeleteCommand�{�6�   ��    5|      5}      5~      5      5�      5�      