   M S B A M L   `   `   `  ����D  @GridDemo, Version=15.2.4.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GridDemoGridDemo  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al hDevExpress.Xpf.Core.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ae aDevExpress.Xpf.Printing.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49al	 hDevExpress.Xpf.Grid.v15.2.Extensions, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af
 bDevExpress.Xpf.Grid.v15.2.Core, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a6   0DevExpress.Xpf.Core.ConditionalFormatting.Format   	newFormat     
Background    
Foreground  fixedFormat  priorityTemplate �� Path  priorityPrintStyle �� 
TargetType)  #DevExpress.Xpf.Editors.ComboBoxEdit �� BasedOn;  5DevExpress.Xpf.Grid.Themes.TableViewThemeKeyExtension�  �DevExpress.Xpf.Utils.Themes.ThemeKeyExtensionInternalBase`1[[DevExpress.Xpf.Grid.Themes.TableViewThemeKeys, DevExpress.Xpf.Grid.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]   ResourceKey,  &DevExpress.Xpf.Printing.ExportSettings �� Property �� Value1  +DevExpress.Xpf.Printing.ImageExportSettings �� RelativeSource	 �� 	Converter'   !GridDemo.PriorityToImageConverter�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      =dx2http://schemas.devexpress.com/winfx/2008/xaml/core   5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      Gdxgt<http://schemas.devexpress.com/winfx/2008/xaml/grid/themekeys  5      Bdxp6http://schemas.devexpress.com/winfx/2008/xaml/printing   5      @dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid  	 
 5       localclr-namespace:GridDemo  5      %|  &	        &	 /     &	 ^     &	 �     0�� 5   E   *  $ DefaultPrintCellStyle=�+1   5	        �	#FFFAF9B8
 �   �69      5
        �	#FFD4FFAF
 �   �6;   �� 5      .R�5   
   �� 
������ ._�5      �� $
��None=�62   $��Center=�6A   ��6   �� $ Image�6A   5      5      �� 5      "  6'    6E   2  .�5   
   ��  �@ 
TargetType$ Image=�6:   �� 5   
    �@ ImageRenderMode$ UseImageSource=�6D   �� 5   
    �@ ImageSource 6@   �� $ SelectedItem� �� *Self+	   �� 5   
    �@ ForceCenterImageMode . 6I   5      5      