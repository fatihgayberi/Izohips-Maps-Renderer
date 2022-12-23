# Izohips-Maps-Renderer


https://user-images.githubusercontent.com/46620361/209395065-d1b04806-1eba-4848-bee5-96221358895b.mp4

=====İzohips Haritanın İskeletinin Oluşturulma Aşamaları=====
  * Verilen İzohips haritası Laplacian dönüşümü kullanılarak edge detection uygulanır.
  * Texture'ın edge detection yapılmış hali proje içerisine kaydedilir.
  * Kaydedilen png için okuma yazma izinleri aktif edilir.
  * Png pixellerinin değiştirilebilmesi için RGBA32 seçilir.
  * Dağın halkaları kurulan algoritma sayesinde her halka ayrı liste olacak şekilde saklanır bu sayede halkalar arasındaki yükselti farkını ayarlayabilmiş oluruz.
  * Halkaların bulunduğu konumlar baz alınarak kullanıcıdan alınan yükselti farkı değerlendirilerek dağ oluşturulur.
  * Oluşturulan halkalar UI da anlaşılabilmesi için sırası ile yeşil renklere dönüşür.
  * Oluşturulan dağın halkalarının pozisyonları isteğe bağlı olarak sonradan kullanılabilecek şekilde json datasına kaydedilmiştir.



Uploading LaplacianGrowth.mp4…

==== 1. Texture ====
  *  Fotoğrafın orijinal halidir.
==== 2. Texture ====
  * Fotoğrafın gray formatıdır.
  * Texture üzerindeki shader sayesinde gray formata geçiş sağlamıştır.
==== 3. Texture ====
  * Fotoğrafın Siyah-Beyaz halidir.
  * Texture üzerindeki shader ile beraber Siyah-Beyaz formata geçiş sağlamıştır.
  
  Bu Texturelar'a ek olarak üzerine camera kendisine atanan edge detection shader'ı ile kenarları algılamaktadır.
