   M S B A M L   `   `   `  ����B  >Weather, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WeatherWeather  5   	       Weather.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Weather     5   	   -   5          � Title$  Weather��5   	   $
��300 ��6   $	��250��6'    � 
ResizeMode$ NoResize=�63    � Icon$ 
sunnyy.ico��6I    � AllowsTransparency .  � WindowStyle$
 None=�6u   ��5	      �� 5
   
    �� ImageSource$ image/background3.png��6   .��5      � .+�5   
   �� -   5         ��Name$ buttonSettings��5      $
��Left=�6)   $��
215,10,0,0q�6D   $	��Top=�6X   $��10��6p   $��10��6{   .��5      �� $��manag/settings.png��6   $��6��60   $��6��6:   5      �� 5   
   -   5        $ buttonClose��5      $
��Left=�6&   $��
230,10,0,0q�6A   $	��Top=�6U   $��10��6m   $��10��6x   .��5      �� $��manag/Close.png��6   $��6��6-   $��7��67   5      �� 5   
   -   5         ��Name$ TBCityResult��5      $
��Left=�6*   $��	41,10,0,0q�6E   $
��Wrap=�6X   $��-��6l   $	��Top=�6u   $��36��6�   $	��161��6�   �� 5   
   -   5         ��Name$ image��5      $��47,14,48,144q�6   �� 5   
   -   5        $ TBtemp��5      $
��Left=�6$   $��
10,156,0,0q�6?   $
��Wrap=�6S   $��Температура:��6g   $	��Top=�6{   $��17��6�   $��86��6�   �� 5   
   -   5        $ TBtempResult��5      $
��Left=�6*   $��103,117,0,0q�6E   $
��Wrap=�6Z   $��0��6n   $	��Top=�6w   $��64��6�   $	��137��6�   $
��Bold"�6�   $��48(�6�   �� 5!   
   -   5        $ TBClouds��5!      $
��Left=�6&   $��
10,186,0,0q�6A   $
��Wrap=�6U   $��Осадки��6i   $	��Top=�6w   $��17��6�   $��57��6�   �� 5"   
   -	   5        $ TBCloudsResult��5"      $
��Left=�6,   $��
72,186,0,0q�6G   $
��Wrap=�6[   $��-��6o   $	��Top=�6x   $��17��6�   $	��168��6�   �� 5$   
   -
   5        $ 
TBPressure��5$      $
��Left=�6(   $��
10,208,0,0q�6C   $
��Wrap=�6W   $��Давление��6k   $	��Top=�6{   $��57��6�   �� 5%   
   -   5        $ TBPressureResult��5%      $
��Left=�6.   $��
72,207,0,0q�6I   $
��Wrap=�6]   $��-��6q   $	��Top=�6z   $��17��6�   $	��146��6�   {�.  6�   �� 5&   
   -   5        $ TBCloudness��5&      $
��Left=�6)   $��
10,229,0,0q�6D   $
��Wrap=�6X   $��Облачность��6l   $	��Top=�6~   $��70��6�   �� 5'   
   -   5        $ TBCloudnessResult��5'      $
��Left=�6/   $��
85,228,0,0q�6J   $
��Wrap=�6^   $��-��6r   $	��Top=�6{   $��17��6�   $	��146��6�   {�.  6�   5(      5)      