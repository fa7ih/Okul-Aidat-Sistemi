Devexpress araçlarını kullanarak yapıldı. proje ile ilgili bilgiler;
Veri Tabanı işlemleri için SqlBaglantisi adlı bir sınıf oluşturuldu.
Özel ve sık kullanılan fonksiyonlar için örneğin birden fazla Textbox’ın temizlenmesi için,
verilerin gösterilmesi için, combobox veya devexpresste benzer bir araç olan comboBoxEdit te il adlarının gözükmesi için, 
ve bazı formlarda öğretmen isimleri, öğrenci isimleri, ödeme şekli, eğitim yılı verilerinin gözükmesi için fonksiyonlar oluşturuldu.
SqlBaglantisi sınıfında ise, sqlconnection tanımlandı. Daha sonra ise 1. formun (ana form) tasarımı için önce ribbonPageGroup(Devexpress aracı)
ve ana form  üzerine diğer formların gelebilmesi için xtraTabbedMdiManager(Devexpress aracı) eklendi. Daha sonra diğer formlara geçebilmek için
ribbonPageGroup üzerine 17 adet barButtonItem(Devexpress aracı) eklendi . Bu buttonlar;  Ana Sayfa, Öğrenciler, Veliler, Öğretmenler, kırtasiye,
Kırtasiye ürünleri, Personeller, Giderler, Kasa, Açıklamalar, Bankalar, Rehber, Şehir Bilgileri, Ödeme Şekli Girişi, Öğrenciye Ait Ödeme Planı,
Ayarlar, Eğitim Yılı dır. Ana Sayfa butonu: Bu butona basmak “FrmAnaSayfa” adında bir formu açıyor bu formda şube mevcudu, şubedeki cinsiyet dağılımı,
döviz kurları, haber başlıkları ve öğretmenlerin verdikleri ders sayfasını gösterir. Öğrenciler butonu: Bu butona basmak “FrmOgrenciler” adında bir
formu açıyor bu formda öğrenci listesi, öğrenci kayıt işlemi ve öğrenci bulma işlemi bulunmaktadır. Veliler butonu: Bu butona basmak “FrmVeliler”
adında bir formu açıyor bu formda veli tablosu, veli kayıt işlemi bu alanda hangi öğrencinin velisini kayıt etmek için lookUpEdit(Devexpress aracı)
bulunmaktadır. Öğretmenler butonu: Bu butona basmak “FrmOgretmen” adında bir formu açıyor bu formda öğretmen listesi, öğretmen kayıt işlemi ve öğretmen
bulma işlemi bulunmaktadır. kırtasiye butonu: Bu butona basmak “FrmKirtasiye” adında bir formu açıyor bu formda kırtasiye listesi ve kırtasiye kayıt
işlemi bulunmaktadır. Kırtasiye ürünleri butonu: Bu butona basmak “FrmKirtasiyeUrunleri” adında bir formu açıyor bu formda kırtasiye listesi ve kırtasiye
kayıt işlemi bu alanda hangi kırtasiyenin ürününü kayıt etmek için lookUpEdit(Devexpress aracı) bulunmaktadır . Personeller butonu: Bu butona basmak
“FrmPersonel” adında bir formu açıyor bu formda personel listesi, personel kayıt işlemi ve personel bulma işlemi bulunmaktadır. Giderler butonu: 
Bu butona basmak “FrmGiderler” adında bir formu açıyor bu formda gider listesi ve gider kayıt işlemi bulunmaktadır. Kasa butonu: Bu butona basmak
“FrmKasa” adında bir formu açıyor bu formda kasa giriş ve çıkış hareketleri bulunmaktadır. Açıklamalar butonu: Bu butona basmak “FrmAcilklama”
adında bir formu açıyor bu formda açıklama listesi ve açıklama kayıt işlemi bu alanda hangi öğrencinin açıklamasını kayıt etmek için 
lookUpEdit(Devexpress aracı) bulunmaktadır. Bankalar butonu: Bu butona basmak “FrmBanka” adında bir formu açıyor bu formda banka listesi
ve banka kayıt işlemi bu alanda hangi öğrencinin bankasını kayıt etmek için lookUpEdit(Devexpress aracı) bulunmaktadır. Rehber butonu:
Bu butona basmak “FrmRehber” adında bir formu açıyor bu formda öğrenci, öğretmen, veli, personeller, kırtasiye ve banka iletişim bilgileri 
bulunmaktadır. Ayrıca bu formda kişiye mail göndermek için smpt kullanıldı rehberdeki kişilere mail göndermek için GridControl(Devexpress aracı)’e
çift tıklayınca “FrmMail” formuna gönderir bu formda mail gönderme işlemi yapılmaktadır. Şehir Bilgileri butonu: Bu butona basmak “FrmSehirBilgileri”
adında bir formu açıyor bu formda öğrenci, öğretmen, personeller, kırtasiye ve banka şehir bilgileri bulunmaktadır. 
Ödeme Şekli Girişi butonu: Bu butona basmak “OdemeSekliGirisi” adında bir formu açıyor bu formda ödeme şekli kayıt ve silme işlemi bulunmaktadır.
Öğrenciye Ait Ödeme Planı butonu: Bu butona basmak “FrmOdemePlani” adında bir formu açıyor bu formda ödeme planı listesi, öğrenci bulma ve ödeme 
planı kayıt işlemi bu alanda hangi öğrencinin ödeme planını kayıt etmek için lookUpEdit(Devexpress aracı) bulunmaktadır. Ayarlar butonu:
Bu butona basmak “FrmAyarlar” adında bir formu açıyor bu formda şifre, kullanıcı adı ve kurtarma mailini değiştirme alanları bulunmaktadır
bu 3 itemi değiştirmek için hepsi bir panel üzerine çağırılan başka formlardır şifre için “newpassword”, kullanıcı adı için “newusername” 
ve mail için “newemail” formlarına geçiş yapar. Eğitim Yılı butonu: Bu butona basmak “FrmEgitimYili” adında bir formu açıyor bu formda eğitim
yılı kayıt ve silme işlemi bulunmaktadır. Ve son olarak Login Formu bulunmaktadır bu formda kullanıcı adı ve şifre girişi için textbox bulunmaktadır
ayrıca kullanıcı adımı unuttum ve şifremi unuttum gibi alanlarda mevcuttur. Bu alanlara tıklanıldığı zaman kullanıcı adını ve şifresini yenilemek
için ayrı formlar yaptım kullanıcı adını ve şifresini yenilemek için kurtarma maili girilmesi gerekmektedir.
