   M S B A M L   `   `   `  ���� H  DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls   Evran Barkod   Evran_Barkod.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     R NMahApps.Metro.IconPacks, Version=2.3.0.4, Culture=neutral, PublicKeyToken=null=	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks     AccentColorBrush(   "MahApps.Metro.Controls.MetroWindow    	GlowBrush#  �    � Title$ EVRAN BARKOD��$	��600��$	��800��   RightWindowCommands +   %MahApps.Metro.Controls.WindowCommands  .@��� $��ayarlar���� -   .����  �� Orientation$ 
Horizontal=�.+�.  (MahApps.Metro.IconPacks.PackIconMaterial  $��24��$��24��E AControlzEx, Version=3.0.2.4, Culture=neutral, PublicKeyToken=null�  �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconMaterialKind, MahApps.Metro.IconPacks, Version=2.3.0.4, Culture=neutral, PublicKeyToken=null]]   Kind$ Barcode=��� $��4 0 0 0q�$��Center=�$��ürün ekle��.��� .+�,   &MahApps.Metro.Controls.MetroTabControl  -   $��tcTabs��.@�)   #MahApps.Metro.Controls.MetroTabItem  $��Satış��.���� -    ��Name$ satisContentControl��  $��Ürün��.��� .+�  -   $��tcUrun��.@�  $
��Ekle��.��� .+��� $��100 50 100 0q�.+��� -   $��txUrunEkleBarkod��$��0 0 0 10q�*   $MahApps.Metro.Controls.TextBoxHelper   AutoWatermark .    	Watermark$ Barkod��   UseFloatingWatermark . $��24(��� -   $��txUrunEkleAd��$��0 0 0 10q� . $ Ad�� . $��24(��� -   $��txUrunEkleFiyat��$��0 0 0 10q� . $ Fiyat�� . $��24(��� -   $��cbUrunEkleBarkodluTerazi��$��0 0 0 10q�$��18(�.��Barkodlu Terazi Ürünü�� -	   $��cbUrunEkleHizli��$��0 0 0 10q�$��18(�.��Hızlı Satışa Ekle!   MahApps.Metro.Controls.Tile  -
   $��0 20 0 0q�	   Title$	 Ürün Ekle��.��  $��80��$��80��$ Barcode=�  $��Düzenle��.��� 
 � RowDefinitions
 �� $��1* ��� $	��10* �.+��� -   $��txUrunDuzenleAraBarkod��$��40�� .  . $ Barkod��&	   System.Windows.Controls.DataGrid	  -   $��dgUrunDuzenle�� 	  AutoGenerateColumns .   	  CanUserDeleteRows .   	  CanUserReorderColumns .   	  CanUserAddRows .  $��1�� 	  Columns 0
  *System.Windows.Controls.DataGridTextColumn
  ,  &System.Windows.Controls.DataGridColumn   Header$ Barkod��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Barkod+ �� Mode$ OneWay=�
  $ Ad��$ 2*  �� *Ad+$ OneWay=�
  $ Fiyat��$ 1*  �� *Fiyat+$ OneWay=�
  $ KGFiyat��$ 2*  �� *	KGFiyat+$ OneWay=�� -   $��gDuzenle��$y�Hidden=�$��2��.+��� -    / +MahApps.Metro.Styles.MetroCircleButtonStyle#���  $��20q�$
��Left=�$	��Top=�.��1  +MahApps.Metro.IconPacks.PackIconFontAwesome  $��40��$��40���  �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconFontAwesomeKind, MahApps.Metro.IconPacks, Version=2.3.0.4, Culture=neutral, PublicKeyToken=null]]   Kind$ ArrowCircleLeftSolid=��� $��100 50 100 0q�.+��� -   $��txUrunDuzenleBarkod��$��0 0 0 10q� . $ Barkod�� . $��24(��� -   $��txUrunDuzenleAd��$��0 0 0 10q� . $ Ad�� . $��24(��� -   $��txUrunDuzenleFiyat��$��0 0 0 10q� . $ Fiyat�� . $��24(��� -   $!��cbUrunDuzenleBarkodluTerazi��$��0 0 0 10q�$��18(�.��Barkodlu Terazi Ürünü�� -   $��cbUrunDuzenleHizli��$��0 0 0 10q�$��18(�.��Hızlı Satışa Ekle  -   $��0 20 0 0q�$	 Ürün Düzenle��.��  $��80��$��80��$ 
QrcodeEdit=�  $��Kaldır��.���  � ColumnDefinitions �� $	��10* ��� $��1* �.+�	  -   $��dgUrunKaldir�� .   .   .   .   
  $ Barkod��$ 2*  �� *Barkod+$ OneWay=�
  $ Ad��$ 2*  �� *Ad+$ OneWay=�
  $ Fiyat��$ 1*  �� *Fiyat+$ OneWay=�
  $ KGFiyat��$ 2*  �� *	KGFiyat+$ OneWay=��� -   $��5q�$��1��
���  ��
�������$��72(�.��X  $��Rapor��.��� .+�  .@�  $
��Gün��.��� .+�� -   $��	gRaporGun�� �� $��4* ��� $��1* �.+�	  -   $��
dgRaporGun�� .   .   .   .   
  $
 Saat�� �� *Saat+$ OneWay=�
  $ Satilan Urun Sayisi�� �� *SatilanUrunSayisi+$ OneWay=�
  $ Ucret�� �� *ToplamUcret+$ OneWay=��� $��1��.+�  -   $��tRaporGunSatilan��$��10q�$	 Satılan Ürün��$��48(�.��1337  -   $��tRaporGunKazanc��$��10q�$	 Kazanılan ₺��$��36(�.��1337  $��10q�$	  ��$��48(�� -   $��gRaporGunDetay��$��2��$y�Hidden=� �� $��4* ��� $��1* �.+�	  -   $��dgRaporGunUrunler�� .   .   .   .   
  $ Barkod��$ 2*  �� *Barkod+$ OneWay=�
  $ Ad��$ 2*  �� *Ad+$ OneWay=�
  $
 Adet��$ 1*  �� *Adet+$ OneWay=�
  $ Fiyat��$ 2*  �� *Fiyat+$ OneWay=��� -    J� Opacity$	 0.4Q�#���  $��20 5 0 0q�$
��Left=�$	��Top=�.��  $��40��$��40��$ ArrowCircleLeftSolid=��� $��1��.+�  -   $��tRaporGunDetayUrun��$��10q�$	 Toplam Ürün��$��48(�.��1337  -   $��tRaporGunDetayToplam��$��10q�$	 
Toplam ₺��$��48(�.��1337  $��10q�$��48(�  $��Ay��.��� .+�� -    $��gRaporAy�� �� $��4* ��� $��1* �.+�	  -!   $��	dgRaporAy�� .   .   .   .   
  $ Tarih�� �� *TarihT+$ OneWay=�
  $ Satilan Urun Sayisi�� �� *SatilanUrunSayisi+$ OneWay=�
  $ Ucret�� �� *ToplamUcret+$ OneWay=��� $��1��.+�  -"   $��tRaporAySatilan��$��10q�$	 Satılan Ürün��$��48(�.��1337  -#   $��tRaporAyKazanc��$��10q�$	 Kazanılan ₺��$��36(�.��1337  $��10q�$	  ��$��48(�� -$   $��gRaporAyDetay��$��2��$y�Hidden=� �� $��4* ��� $��1* �.+�	  -%   $��dgRaporAyUrunler�� .   .   .   .   
  $ Barkod��$ 2*  �� *Barkod+$ OneWay=�
  $ Ad��$ 2*  �� *Ad+$ OneWay=�
  $
 Adet��$ 1*  �� *Adet+$ OneWay=�
  $ Fiyat��$ 2*  �� *Fiyat+$ OneWay=��� -&   $	 0.4Q�#���  $��20 5 0 0q�$
��Left=�$	��Top=�.��  $��40��$��40��$ ArrowCircleLeftSolid=��� $��1��.+�  -'   $��tRaporAyDetayUrun��$��10q�$	 Toplam Ürün��$��48(�.��1337  -(   $��tRaporAyDetayToplam��$��10q�$	 
Toplam ₺��$��48(�.��1337  $��10q�$��48(�  $��Seçim��.��� 
 �� $��1* ��� $	��12* � �� $��1* ��� $��1* �.+�(  "System.Windows.Controls.DatePicker  -)   $��dpRaporSecimBaslangic��$��5q�  -*   $��dpRaporSecimSon��$��5q�$��1��� $��1��$��2��$��	0,0,0,-30q�.+�� -+   $��gRaporSecim�� �� $��4* ��� $��1* �.+�	  -,   $��dgRaporSecim�� .   .   .   .   
  $ Tarih�� �� *TarihT+$ OneWay=�
  $ Satilan Urun Sayisi�� �� *SatilanUrunSayisi+$ OneWay=�
  $ Ucret�� �� *ToplamUcret+$ OneWay=��� $��1��.+�  --   $��tRaporSecimSatilan��$��10q�$	 Satılan Ürün��$��48(�.��1337  -.   $��tRaporSecimKazanc��$��10q�$	 Kazanılan ₺��$��36(�.��1337  $��10q�$	  ��$��48(�� -/   $��gRaporSecimDetay��$��2��$��1��$y�Hidden=� �� $��4* ��� $��1* �.+�	  -0   $��dgRaporSecimUrunler�� .   .   .   .   
  $ Barkod��$ 2*  �� *Barkod+$ OneWay=�
  $ Ad��$ 2*  �� *Ad+$ OneWay=�
  $
 Adet��$ 1*  �� *Adet+$ OneWay=�
  $ Fiyat��$ 2*  �� *Fiyat+$ OneWay=��� -1   $	 0.4Q�#���  $��20 5 0 0q�$
��Left=�$	��Top=�.��  $��40��$��40��$ ArrowCircleLeftSolid=��� $��1��.+�  -2   $��tRaporSecimDetayUrun��$��10q�$	 Toplam Ürün��$��48(�.��1337  -3   $��tRaporSecimDetayToplam��$��10q�$	 
Toplam ₺��$��48(�.��1337  $��10q�$��48(�  $��İstatistikler��.��� $��5q�$��Center=� � Orientation$ 
Horizontal=�.+�  -4   $��tRaporIstatistikGunRekor��$	 Gün Rekoru��$��24(�  -5   $!��tRaporIstatistikKayitliUrun��$	 Kayıtlı Ürün��$��24(�  -6   $!��tRaporIstatistikSatisSayisi��$	 Satış Sayısı��$��24(�    $	  ��$��24(�  $	  ��$��24(�  $	  ��$��24(�  $	  ��$��24(�  $	  ��$��24(�  $	  ��$��24(�  $	  ��$��24(�  $	  ��$��24(�  $	  ��$��24(�  $	  ��$��24(�  $	  ��$��24(�  $��Right=�$��Ayarlar��$y�Hidden=�