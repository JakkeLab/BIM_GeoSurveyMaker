   M S B A M L   `   `   `  ����G  CGeoSurveyRVT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:GeoSurveyRVT.ConvertersGeoSurveyRVT.Converters  5      =    7GeoSurveyRVT.RibbonUIForm.BoringSetting.BoringSettingUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <
converters%clr-namespace:GeoSurveyRVT.Converters     5      -   5          �Name$  wdBoringSetting��5   	    � Title$ 보링 설정��5       � SizeToContent$ WidthAndHeight=�6   $	��250��6;   $	��350��6G    � 
ResizeMode$ NoResize=�6T    � 	Resources 5	      3   -GeoSurveyRVT.Converters.ColorToBrushConverter   ColorToBrushConverter��%   &	          5
   
   6+   5      .��5      � -   5         �Name$ 	gdSetting��5      $	��300��6   $	��Top=�6+   .+�5   
   �� -   5         ��Name$ lbTitle��5      $��보링 설정��6!   $
��Left=�61   $��5,5,0,0q�6L   $	��Top=�6]   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgBoringSettings��5      $	��210��6-   $	��200��6:   $��	5,36,5,54q�6F      AutoGenerateColumns .  	   CanUserAddRows	 .  
   CanUserResizeColumns
 .     SelectionUnit$ FullRow=�6�   ��6Y   �� *
LayerInfos+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 레이어 이름��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 65      CanUserResize .     CanUserReorder .     
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   �� *	LayerName+6�   4  .System.Windows.Controls.DataGridTemplateColumn  5      $ 색상��6)   $ 80 65    .   .     CellTemplate 5      �� 5      .R�5      ��  ��Name$ btnSetColor��6%   -   5        $�� ��5   :   $��1,1,1,1q�6`   
���������6�   �� *
LayerColor+ �� 	Converter# [  5      5      5      �� 5   
   -   5        $ btCancel��5      $��취소��6#   $��50��60   $
��Left=�6;   $��
62,257,0,0q�6V   $	��Top=�6j   6�   �� 5   
   -   5        $
 btOk��5      $��확인��6   $��50��6,   $
��Left=�67   $��	7,257,0,0q�6R   $	��Top=�6e   6}   �� 5   
   -   5        $ btLoadTemplate��5      $��	템플릿��6)   $
��Left=�67   $��50��6R   $��190,257,0,0q�6]   $	��Top=�6r   6�   5      5      