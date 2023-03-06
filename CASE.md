Backend (.NET) Assessment

Merhaba
Bu değerlendirme, işe başvuru sürecindeki adaylar için hazırlanmış olup bizlere kişinin
yetkinliklerinin değerlendirmesinde yardımcı olması için tasarlanmıştır.
Bu kapsamda, belirtilen süre içerisinde aşağıda detayları bulunan case in tamamını veya
geliştirebildiğiniz kısmını tamamlayabilirsiniz.
Çalışmanızın github proje linkini mail üzerinden bizlerle paylaşmanızı beklemekteyiz.

Senaryo
1. Api tasarımı yapılmalı. Bu api, frontend in kullanabileceği aşağıdaki gibi 3 method içermeli.
a. Yeni belge yükleme metodu. Dökümanın sonundaki json örnek formatında belge
yüklenecek sisteme.
b. Belgelerin listelenmesi için başlık(InvoiceHeader) bilgilerinin toplu listesini dönen bir
get methodu.
c. Belge idsi(InvoiceId) parametresi alan ve tekbir belgenin detaylarını dönen get
methodu (belgenin başlık ve detay bilgileri).

2. Api gönderim işleminde yüklenen belge olduğu gibi bir tabloya yazılmalı.
3. Cron Expression ile zamanlanmış bir job ilgili işlenmemiş kayıtları okuyup json deserialize
yapmalı ve uygun şekilde veritabanına kaydetmeli.(Oracle, MsSql, Sqlite vb. istenilen
veritabanı kullanılabilir.). Kurguladığınız yapıya uygun veritabanı tasarımı yapabilirsiniz.
4. Ayrıca belgenin uygun şekilde kayıt işlemi sonrası job ile her belge için ayrı ayrı, configte
tanımlı sabit olan bir mail adresine bilgilendirme maili göndermeli.
5. Projenin nasıl çalıştırılacağına dair README.md dokümantasyonu oluşturulmalı.

Kullanılacak Teknolojiler
Backend : .net Core (.net 5 veya .net 6)

Nice to Have
UnitTest
Logging (Tercihe bağlı Serilog, Log4Net, ELK vb.)

{
   "InvoiceHeader": {
      "InvoiceId": "SVS202300000001",
      "SenderTitle": "Gönderici Firma",
      "ReceiverTitle": "Alıcı Firma",
      "Date": "2023-01-05"
   },
   "InvoiceLine": [
      {
         "Id": 1,
         "Name": "1.Ürün",
         "Quantity": 5,
         "UnitCode": "Adet",
         "UnitPrice": 10
      },
      {
         "Id": 2,
         "Name": "2.Ürün",
         "Quantity": 2,
         "UnitCode": "Litre",
         "UnitPrice": 3
      },
      {
         "Id": 3,
         "Name": "3.Ürün",
         "Quantity": 25,
         "UnitCode": "Kilogram",
         "UnitPrice": 2
      }
   ]
}