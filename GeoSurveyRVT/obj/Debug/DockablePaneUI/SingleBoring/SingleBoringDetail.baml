   M S B A M L   `   `   `  ����G  CGeoSurveyRVT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:GeoSurveyRVT.DockablePaneUI.SingleBoring(GeoSurveyRVT.DockablePaneUI.SingleBoring  5      A    ;GeoSurveyRVT.DockablePaneUI.SingleBoring.SingleBoringDetail   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Hlocal6clr-namespace:GeoSurveyRVT.DockablePaneUI.SingleBoring     5      $	��342��5      $	��180��6   .��5	      � 
�������.+�5
   
   �� -   5          ��Name$  lbBoring��5
      $��보링 상세정보��6"   $
��Left=�64   $	��Top=�6O   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgLayers��5      $	��342��6%   $	��150��61   $	��Top=�6>   $��Stretch=�6V   $��0,30,0,0q�6t      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserResizeRows .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   CanUserResize	 .  
   CanUserSort
 .     CanUserReorder .     Header$ No��6f      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6r   �� *
LayerIndex+6�     5      	 .  
 .   .  $ 레이어 이름��6f      Width5  /System.Windows.Controls.DataGridLengthConverter$ 80 6v    .  6�   �� *	LayerName+6�     5      	 .  
 .   .  $	 Top��6f   $ 60 6s    .  6~   �� *Top+6�     5      	 .  
 .   .  $ Depth��6f   $ 60 6u    .  6�   �� *Depth+6�   5      5      5      