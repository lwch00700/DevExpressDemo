   M S B A M L   `   `   `  ����f  bDevExpress.DemoData.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aqQclr-namespace:DevExpress.DemoData.Helpers;assembly=DevExpress.DemoData.v15.2.CoreDevExpress.DemoData.Helpers  5	      D @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo 5
      &    GridDemo.DemoModuleExportControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a=dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      e aDevExpress.Xpf.Printing.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aBdxp6http://schemas.devexpress.com/winfx/2008/xaml/printing   5      j	 fDevExpress.Xpf.LayoutControl.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol 	 5      a	ddhelpersQclr-namespace:DevExpress.DemoData.Helpers;assembly=DevExpress.DemoData.v15.2.Core 	 5	      "localclr-namespace:GridDemo 	 5
      -   5          >�Name$
  root��5   C   $	��230��67    � 	Resources 5      /  )GridDemo.PrintingIconImageSourceConverter "  exportIconImageSourceConverter  exportButtonTemplate �� 	Converter �� TextAlignment  exportButtonStyle �� 
TargetType �� Property �� Value0   *DevExpress.DemoData.Helpers.GlobalResource  DemoScrollViewerResource   Assembly   XamlPath
	   Key��%?  &	        &	      7   &	 �     7  &	 �      5   
   61   �� 5   
   .R�5      �� .+�5      �� $
��None=�6e   ��6   �� 8   6e   �� 5      $
��Wrap=�6-   $ Center=�6A   ��6   �� 6A   5      5      �� 5   
   " ��6*   .�5      ��  � ��$ 60��6&   �� 5       � ��$ 85��6'   �� 5       � ��$ 3q�6'   �� 5       � ��$ 0q�6(   �� 5       � ��8   60   5        5   
   $$ DevExpress.DemoData.v15.2.Core��6D   $# DemoWindow/DemoScrollBar.xaml��6n   $	 DemoScrollViewer��6�   5      .��5      �� $��Disabled=�6   $
��Auto=�6<   ��6_   �� 
 �� Path$
 Resource� �� Source# [ .��5   
   �� $��0,0,10,0q�6   .+�5       �� -   5         ��Name$ printPreviewButton��5       $��Print Preview��61   $��0,0,0,12q�6w   $��38��6�   ��6I   �� *Commands.ShowPrintPreview+6�   $  DevExpress.Xpf.Core.GroupFrame  5!      -   5          Name$ dataAwareExportPanel��5!      $��Data Aware Export��6:   $��0,0,0,12q�6U   ��5"      �� 5#      .R�5$      �� $��1,0,0,0q�65   ��6$   �� 65   5%      .��5'      �� .+�5(      �� $��2,0,0,12q�6    $
��Wrap=�62   [YAn advanced data export engine that supports a lot of MS Excel data analysis capabilites.5*      � 5+      $��Center=�6    $
��-3,0q�6=   .+�5,      �� -   5        #��[ 5,   ?   $��Export
 To XLSX��6j   �� 5-      -   5        #��[ 5-   >   $��Export
 To XLS��6i   �� 5.      -   5        #��[ 5.   >   $��Export
 To CSV��6i   5/      50      51        52      -   5        $ wysiwygExportPanel��52      $��WYSIWYG Export��68   $��0,0,0,12q�6P   ��53      �� 54      .R�55      �� $��2,0,0,0q�65   ��6$   �� 65   56      .��58      �� .+�59      �� $��2,0,0,12q�6    $
��Wrap=�62   [YAllows you to export data in various formats and provides numerous customization options.5;      � 5<      $��Center=�6    $
��-3,0q�6=   .+�5=      �� $	��Pdf��6U   #��[ 6l   $��Export
 To PDF��6�   ��6!   �� *ExportCommand+ �� ElementName$
 root��6�   �� 5>      $	��Htm��6U   #��[ 6l   $��Export
 To HTML��6�   ��6!   �� *ExportCommand+$
 root��6�   �� 5?      $	��Mht��6U   #��[ 6l   $��Export
 To MHT��6�   ��6!   �� *ExportCommand+$
 root��6�   �� 5A      $	��Rtf��6U   #��[ 6l   $��Export
 To RTF��6�   ��6!   �� *ExportCommand+$
 root��6�   �� 5B      $
��Xlsx��6U   #��[ 6m   $��Export
 To XLSX��6�   ��6!   �� *ExportCommand+$
 root��6�   �� 5C      $	��Xls��6U   #��[ 6l   $��Export
 To XLS��6�   ��6!   �� *ExportCommand+$
 root��6�   �� 5E      $	��Txt��6U   #��[ 6l   $��Export
 To Text��6�   ��6!   �� *ExportCommand+$
 root��6�   �� 5F      $��Image��6U   #��[ 6n   $��Export
 To Image��6�   ��6!   �� *ExportCommand+$
 root��6�   �� 5G      $	��Xps��6U   #��[ 6l   $��Export
 To XPS��6�   ��6!   �� *ExportCommand+$
 root��6�   5H      5I      5J      5K      5L      5M      