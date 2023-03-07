Backend (.NET) Assessment  
  
Senaryo  
1. Api tasarımı yapılmalı. Bu api, frontend in kullanabileceği aşağıdaki gibi 3 method içermeli.  
a. Yeni belge yükleme metodu. Dökümanın sonundaki json örnek formatında belge yüklenecek sisteme.  
b. Belgelerin listelenmesi için başlık(InvoiceHeader) bilgilerinin toplu listesini dönen bir get methodu.  
c. Belge idsi(InvoiceId) parametresi alan ve tekbir belgenin detaylarını dönen get methodu (belgenin başlık ve detay bilgileri).  
  
2. Api gönderim işleminde yüklenen belge olduğu gibi bir tabloya yazılmalı.  
3. Cron Expression ile zamanlanmış bir job ilgili işlenmemiş kayıtları okuyup json deserialize yapmalı ve uygun şekilde veritabanına kaydetmeli.(Oracle, MsSql, Sqlite vb. istenilen veritabanı kullanılabilir.). Kurguladığınız yapıya uygun veritabanı tasarımı yapabilirsiniz.  
4. Ayrıca belgenin uygun şekilde kayıt işlemi sonrası job ile her belge için ayrı ayrı, configte tanımlı sabit olan bir mail adresine bilgilendirme maili göndermeli.  
5. Projenin nasıl çalıştırılacağına dair README.md dokümantasyonu oluşturulmalı.  
  
Kullanılacak Teknolojiler  
Backend : .net Core (.net 5 veya .net 6)  
  
Nice to Have  
UnitTest  
Logging (Tercihe bağlı Serilog, Log4Net, ELK vb.)  
  
{  
&emsp;"InvoiceHeader": {  
&emsp;&emsp;"InvoiceId": "SVS202300000001",  
&emsp;&emsp;"SenderTitle": "Gönderici Firma",  
&emsp;&emsp;"ReceiverTitle": "Alıcı Firma",  
&emsp;&emsp;"Date": "2023-01-05"  
&emsp;},  
&emsp;"InvoiceLine": [  
&emsp;{  
&emsp;&emsp;"Id": 1,  
&emsp;&emsp;"Name": "1.Ürün",  
&emsp;&emsp;"Quantity": 5,  
&emsp;&emsp;"UnitCode": "Adet",  
&emsp;&emsp;"UnitPrice": 10  
&emsp;},  
&emsp;{  
&emsp;&emsp;"Id": 2,  
&emsp;&emsp;"Name": "2.Ürün",  
&emsp;&emsp;"Quantity": 2,  
&emsp;&emsp;"UnitCode": "Litre",  
&emsp;&emsp;"UnitPrice": 3  
&emsp;},  
&emsp;{  
&emsp;&emsp;"Id": 3,  
&emsp;&emsp;"Name": "3.Ürün",  
&emsp;&emsp;"Quantity": 25,  
&emsp;&emsp;"UnitCode": "Kilogram",  
&emsp;&emsp;"UnitPrice": 2  
&emsp;}  
&ensp;]  
}  