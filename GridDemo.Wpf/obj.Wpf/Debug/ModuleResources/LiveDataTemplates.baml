   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections  5      D @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo 5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a  �� MergedDictionaries �� Source   newRowStoryboard"   GridDemo.StoryboardContainer    
Storyboard O� KeyTime O� Value �� 	KeySpline  deleteRowStoryboard ' #memoryUsageIncreasedColorStoryboard g� 	BeginTime �� KeyTime �� Value	 �� 	KeySpline ' #memoryUsageDecreasedColorStoryboard  cpuUsageChangeStoryboard3  -DevExpress.Xpf.Grid.RowPropertyValueConverter  RowPropertyValueConverter  rowTemplate
 �� Color$  DevExpress.Xpf.Core.DXExpander   HorizontalExpand   VerticalExpand
 � Tag �� Path �� 	Converter �� ConverterParameter   AnimationProgress �� RelativeSource/  )DevExpress.Xpf.Core.FormatStringConverter  memoryUsageConverter   FormatString  progressTemplate � Minimum � Maximum 	 InplaceProgressBarTemplate � DataContext � Value 
 memUsageTemplate ��Name �� CornerRadius �� TextAlignment  historyTemplate#  GridDemo.LiveDataChartElement   CpuUsageHistory   
BarBrushes"   System.Collections.ArrayList�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      =dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      @dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 
 5      Bcollections2clr-namespace:System.Collections;assembly=mscorlib  5       localclr-namespace:GridDemo  5        5	      �� 5
   
   $- '/GridDemo;component/Themes/generic.xaml?�6   %�  &	        &	 �     &	 �    &	     &	 ]    &	 8    &	 K    7  &	 �    &	 	    7 	 7  &	
 �    7  7  &	 �	    �� 5      .R�5   
      . 5      �� .��5      U� .\�5      �� $ 0:0:0��6+   $ 0Q�6;   �� 5      $ 0:0:1��6+   $ 1Q�6;   $ 0,0,0,1��6E   5      5      5      5      �� 5      .R�5   
      . 5      �� .��5      U� .\�5      �� $ 0:0:0��6+   $ 1Q�6;   �� 5      $ 0:0:1��6+   $ 0Q�6;   $ 
0,0,1,0.25��6E   5      5      5      5       �� 5!      .R�5"   
      . 5#      �� .��5$      �� $ 00:00:00X�6/   .g�5%      �� $ 00:00:00��6*   $ 	#FF92969C��6=   �� 5&      $ 00:00:00.25��6*   $ 	#FFB83152��6@   $	 0,0,0.799,0.109��6R   �� 5'      $	 0.414,0.899,1,1��6*   $ 00:00:02��6F   $ 	#FF92969C��6Y   5(      5)      5*      5+      �� 5,      .R�5-   
      . 5.      �� .��5/      �� $ 00:00:00X�6/   .g�50      �� $ 00:00:00��6*   $ 	#FF92969C��6=   �� 51      $ 00:00:00.25��6*   $ 	#FF669331��6@   $	 0,0,0.799,0.109��6R   �� 52      $	 0.414,0.899,1,1��6*   $ 00:00:02��6F   $ 	#FF92969C��6Y   53      54      55      56      �� 57      .R�58   
      . 59      �� .��5:      U� $ 00:00:00X�60   .\�5;      �� $ 00:00:00��6+   �� 5<      $ 0.414,0.899,1,1��6+   $ 00:00:01��6G   5=      5>      5?      5@        5A      6$   �� 5B      .R�5C   
   �� $��border��6   ��5D      �� 5E      $
 Transparent��6"   ._�5G        $��reveal��6   $
 None=�6  $ FromBottomToTop=�6   6*   �� $ DataContext�8   $ AnimationElement�� 6�   �� $ Tag.RevealProgress� �� *Self+._�5H      �� $��content��6#   ��62   �� ��6F   �� *View.DefaultDataRowTemplate+5I      5J      5K        5L      $	 0 K��6<   �� 5M      .R�5N   
   � $��2,1,2,1q�6   $ 0Q�6'   $	 100Q�63   8��   6A   �� $
 Data�8  $ AnimationElement�� 6�   �� $ CpuUsage�6�   5O      �� 5P      .R�5Q   
   �� $ 	progress6��6�   $ 3,3,3,3��6�   $��2,1,2,1q�6�   $��Center=�6�    6   �� $
 Data�8   $ AnimationElement����5R      �� 5S      
 6"   �� $ MemoryUsageColor�._�5U      �� $
��Auto��6   $��14(�6%   $ Center=�6�   $��Center=�6�   $��	10,0,10,0q�6�   $��Center=�6�   $
��Auto��6  ���	#FFFFFFFF��63   �� *DataContext.Value+8   �� *TemplatedParent+6(  5V      5W      �� 5X      .R�5Y   
     $	��2,0q�6%    62   �� *Value+ 5[        �� 5\      $
 	#FFC872C0��6&   �� 5]      $
 	#FFC06BB8��6&   �� 5^      $
 	#FFBB67B3��6&   �� 5_      $
 	#FFB35FAB��6&   �� 5`      $
 	#FFAA57A3��6&   �� 5a      $
 	#FFA24F9B��6&   �� 5b      $
 	#FF9B4994��6&   �� 5c      $
 	#FF94438C��6&   5d      5f      5g      5h      