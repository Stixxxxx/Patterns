   M S B A M L   `   `   `  ����B  >Weather, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WeatherWeather  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       Weather.Settings   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Weather     5   	   9System&clr-namespace:System;assembly=mscorlib     5   	     � Title$  Settings��5	   	   $
��250 ��6   $	��250��6(    � 
ResizeMode$ NoResize=�64    � WindowStartupLocation$ CenterOwner=�6J    � WindowStyle$
 None=�6n    � AllowsTransparency . ��5      �� 5   
    �� ImageSource$  image/backgroundsttngs.png��6   .��5      � .+�5   
   �� -   5         ��Name$ buttonClose��5      $
��Left=�6&   $��
230,10,0,0q�6A   $	��Top=�6U   $��10��6m   $��10��6x   .��5      �� $��manag/Close.png��6   $��6��6-   $��7��67   5      �� 5   
   -   5         ��Name$ 	textBlock��5      $
��Left=�6'   $��	30,20,0,0q�6B   $
��Wrap=�6U   $��
Город��6i   $	��Top=�6v   $��20��6�   $��85��6�   �� 5   
   -   5         ��Name$ comboBox��5      $��
30,40,30,0q�6%   $	��Top=�69   .@�5      �� -   5        	 ��Name$		 SPb��5      $#��Санкт-Петербург��6'   $	��132��6B   ��5      �� 5      $ image/background3.png��6!   5      �� 5       -   5        $
	 Mskw��5       $��Москва��6(   $	��132��69   ��5!      �� 5"      $ image/background3.png��6!   5$      �� 5%      -   5        $	 Kv��5%      $��Киев��6&   $	��132��66   ��5&      �� 5'      $ image/background3.png��6!   5)      5/      �� 50   
   -   5        $ textBlockServer��50      $
��Left=�6-   $��	30,67,0,0q�6H   $
��Wrap=�6[   $#��Источник погоды��6o   $	��Top=�6�   $��20��6�   $	��107��6�   �� 52   
   -   5        $ 
cmbxServer��52      $��
30,87,30,0q�6'   $	��Top=�6;   .@�54      �� -	   5        $	 MeteoserviceXML��54      $��meteoservice.ru (XML)��63   $	��137��6T   ��55      �� 56      $ image/background3.png��6!   58      �� 5:      -
   5        $	 MeteoserviceHTML��5:      $��meteoservice.ru (HTML)��64   $	��137��6U   ��5;      �� 5<      $ image/background3.png��6!   5>      �� 5@      -   5        $	 Openweathermap��5@      $��openweathermap.org��62   $	��137��6O   ��5A      �� 5B      $ image/background3.png��6!   5D      5F      �� 5I   
   -   5        
 ��Name$
 checkBox��5I      $3��-Погода по широте/долготе��6%   $
��Left=�6H   $��
30,125,0,0q�6c   $	��Top=�6w   
���   �6�   �� 5K   
   -   5         ��Name$ 
textBoxlat��5K      $
��Left=�6&   $��23��6A   $��117,151,0,0q�6M    �� TextWrapping$
 Wrap=�6b   $�� ��6v   $	��Top=�6~   $	��103��6�   {�.  6�   �� 5L   
   -   5        $ textBox1long��5L      $
��Left=�6(   $��23��6C   $��117,179,0,0q�6O   $
 Wrap=�6d   $�� ��6x   $	��Top=�6�   $	��103��6�   {�.  6�   �� 5M   
   -   5        $ textBlocklat��5M      $
��Left=�6*   $��
30,151,0,0q�6E   $
��Wrap=�6Y   $��Широта��6m   $	��Top=�6{   $��22��6�   $��82��6�   �� 5N   
   -   5        $ textBlock2long��5N      $
��Left=�6,   $��
30,179,0,0q�6G   $
��Wrap=�6[   $��Долгота��6o   $	��Top=�6~   $��22��6�   $��82��6�   �� 5P   
   -   5        $ btnOKserver��5P      $��OK��6&   $��66,215,66,0q�63   $	��Top=�6H   6`   5U      5V      