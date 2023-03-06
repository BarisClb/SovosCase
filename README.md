# SovosCase - Barış Çelebi  
  
  ### Projenin Çalıştırılması:  
  
  Proje içerisindeki Docker-Compose dosyasını Startup Project olarak çalıştırabilirsiniz. Herhangi başka bir adım atılmasına gerek yoktur. Ancak, dikkat edilmesi gereken bazı noktalar:  
  
  - Dosya Boyutları; Proje için kullanılan Docker Imageları 500mb ile 1.5gb arası boyutlara sahip.  
  - Memory; Ozellikle Elasticsearch yüksek bir Memory harcamasına sahip, Docker-Compose içersinde 512mb ile sınırlanmış durumda, harici yüklemelerde dikkate alınmalı.  
  - Port; Proje için kullanılan Containerların expose oldukları ile aynı Portlarda çalışan Containerlar kapatılmalı.  
    - Port ve Memory'nin bireysel ve beraber oluşturdukları sorunlar sebebiyle Proje test edilirken diğer Containerların durdurulması öneririm.  
  
  ### Projenin Ayarlanması:  
  
  Projenin çalıştırılması için bir ayarlama yapılmasına ihtiyaç yok, ancak:  
  
  - Mail Servisi kullanılmak isteniyorsa, SovosCase.WebAPI/appsettings.json içerisindeki 'EmailSettings' alanları doldurulmalı.  
  
  ### Servis Portları:  
  
  WebAPI/Swagger:    http://localhost:3377/swagger/index.html  
  WebAPI/Hangfire:   http://localhost:3377/hangfire  
  RabbitMq:          http://localhost:15672/  
  Kibana:            http://localhost:5601/  
  
  ### Proje Diagram:  
  
  <img alt="SovosCase-Diagram" src="/SovosCase-Diagram.png">  
  