# Natro Case Study

## Yapılanlar
- **Dinamik Sorgu Ekleme**: UI için dinamik sorgulama özellikleri eklendi; pagination, sorting ve filtering özellikleri mevcuttur.
- **HTTP İsteklerinde Resilience**: `Polly` kullanarak HTTP isteklerinde dayanıklılık sağlanmaya çalışıldı (zamandan kaynaklı deep dive olmadı ne yaızkki).
- **Kimlik Doğrulama**: `IdentityLibrary` ile birlikte access token, refresh token ve register işlemleri eklendi.
- **Cache Yönetimi**: `Redis` ve optional `InMemoryCache` mevcuttur.
- **Performans İyileştirmeleri**: `RDAP` sorgulama işlemleri sırasında performans kazanımı için in-memory notification'lar aracılığıyla database'e ekleme işlemi uygulandı.
- **Generic Yapılar**: (Pipeline, Security, Exception Handling vb.) ayrı bir projede DLL olarak kullanıldı. İncelemek isteyebilirsiniz düşüncesiyle henüz NuGet paketi haline getirilmemiştir.
- **Logging**: `Serilog Sinks` ile iseğe bağlı file/db log yapılabilebilmektedir

## Yapılabilecekler
- **Proje Süreci**: Yoğun bir süreç yaşadık firma içerisinde, hafta sonları ve 29 Ekim tarihlerinde çalışmam gerekti. Fırsat buldukça ilerlemeye çalıştım. Baştan savma olmaması açısından, ayırabildiğim zamanı en azından doğru ve titiz bir şekilde harcamaya gayret gösterdim.
- **Loose Coupling**: DomainEvent'lerle daha loose coupling bir yapı kurulabilirdi.
- **Background Jobs**: Kayıtlı domainler belli zaman aralıklarında taranabilirdi (örneğin `Hangfire` veya `Quartz` gibi kütüphanelerle).
- **Dayanıklılık Stratejileri**: HTTP istekleri ve/veya `EF Core` işlemlerinde dayanıklılık ve fallback policy stratejileri daha yoğun olabilirdi (Transaction projede mevcuttur).
- **Elastic Search**: Domain'ler `Elastic`'e eklenip oradan sorgulanabilirdi.

## Kullanım
Postman Collection için https://file.io/giE58xcavUH7. Temel davranış, login olduktan sonra (`UserName: adminuser`, `Password: adminuser` proje ayağa kalktığında otomatik oluşacaktır) `domaincheck` endpoint'i ile  domain sorgulanır. Kullanıcı isterse `AddFavoriteDomain` endpoint'i ile favorilerine ekleyebilir. Silme ve dynamic query servisleri de mevcuttur.

## İletişim
Sorularınız olursa benimle iletişime geçebilirsiniz.

## Not
Dockerize etmeye çalıştım fakat bugün de biraz yoğunum. Gün içerisinde boşluk bulursam Dockerfile düzenlemesi için commit atacağım ve README'ye son durumu ekleyeceğim.
