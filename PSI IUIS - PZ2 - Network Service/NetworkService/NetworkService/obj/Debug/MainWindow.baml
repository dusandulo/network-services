   M S B A M L   `   `   `  ����I  ENetworkService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:NetworkServiceNetworkService  5   	   ;"clr-namespace:NetworkService.ViewsNetworkService.Views  5   	   C&clr-namespace:NetworkService.ViewModelNetworkService.ViewModel  5   	       NetworkService.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:NetworkService     5   	   4views"clr-namespace:NetworkService.Views     5   	   <	viewModel&clr-namespace:NetworkService.ViewModel     5   	   $	��670��5
   	   $	��375��5   	     � 
ResizeMode$  NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	   
������  � AllowsTransparency .  � 	Resources 5       �� DataType7   1NetworkService.ViewModel.NetworkEntitiesViewModel.   (NetworkService.Views.NetworkEntitiesView6   0NetworkService.ViewModel.NetworkDisplayViewModel-   'NetworkService.Views.NetworkDisplayView8   2NetworkService.ViewModel.MeasurementGraphViewModel/   )NetworkService.Views.MeasurementGraphView��%�   (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      5       � DataContext 5      2   ,NetworkService.ViewModel.MainWindowViewModel  5   
   .��5      ��  �� CornerRadius$ 30��6   ��5    
   �� 5!       �� ImageSource$ /Icons/back2.jpg��6   6:   ._�5#   
   � 	 � RowDefinitions	 5$      �� 5%      $	��600 �6    �� 5&      $��60 �6    .+�5)      � $��0��6   .+�5*      �� $��
20 20 20 0q�6F   ��6!   �� *CurrentViewModel+6F   5+      �� 5-      $��1��6   
 �� Orientation$
 
Horizontal=�6&   $��Bottom=�6?   .+�5.      ��    
BackButton#��[  6     TransparentButtonStyle#��[ 6?   $��75��6�   $��40��6�   ��6o   �� *ExitCommand+6�   �� 5/        CanvasButton#��[ 6   #��[ 6A   $��NetworkDisplay��6�   $��75��6�   $��40��6�   ��6q   �� *
NavCommand+6�   �� 50        
HomeButton#��[ 6   #��[ 6?   $��NetworkEntities��6�   $��75��6�   $��40��6�   ��6o   �� *
NavCommand+6�   �� 51        ChartButton#��[ 6   #��[ 6@   $��MeasurementGraph��6�   $��75��6�   $��40��6�   ��6p   �� *
NavCommand+6�   �� 52        
UndoButton#��[ 6   #��[ 6?   $��75��6o   $��40��6z   53      54      55      56      