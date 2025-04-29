
## ReaProject - TR
Bu proje, Veritabanı Yönetim Sistemleri dersi kapsamında geliştirilmiştir. Proje kapsamında bir emlak sitesi geliştirilmiştir. Proje ile ilgili tüm detayları rapor dosyasında bulabilirsiniz.

## Kurulum

1. **Veritabanı Kurulumu:**
   - Veritabanını oluşturmak için aşağıdaki SQL sorgusunu çalıştırın:
      [Veri Tabanı Tablo Kodları.txt](https://github.com/umutgulfidan/ReaProject/files/15175657/Veri.Tabani.Tablo.Kodlari.txt)

   - Sorgu çalıştırıldıktan sonra aşağıdaki tabloları elde etmeniz beklenmektedir.
    - ![image](https://github.com/umutgulfidan/ReaProject/assets/127475996/3ea7e77f-1011-4b6f-ae9a-a6aa23a955ae)

   - `DataAccess -> Concrete -> EntityFramework -> ReaContext` sınıfındaki SQL Server bağlantısını doğru yapılandırdığınızdan emin olun.
   - ![image](https://github.com/umutgulfidan/ReaProject/assets/127475996/433e6529-497a-486f-918d-cd3076a73aef)


1. **Sahte Veri Oluşturma:**
   - Sahte veri eklemek için ConsoleUI katmanında `Program.cs` dosyasını çalıştırın.
   - Öncesinde kaç kullanıcı ve her kullanıcı başına kaç adet ev ilanı/arsa ilanı eklemek istediğinizi düzenleyebilirsiniz.
   - ![DataGenerator](https://github.com/umutgulfidan/ReaProject/assets/127475996/f49147b2-dc7b-43c9-8728-6ac3c0a6f0e5)


2. **Web API Çalıştırma:**
   - Kurulum tamamlandıktan sonra Web API'yi çalıştırabilirsiniz.

## Frontend Kurulumu

Frontend için [[Rea-Frontend](https://github.com/Utku-Genc/Rea-Frontend)] adresine gidin ve talimatları takip edin.

## Proje Raporu

Proje raporuna [Rapor](https://github.com/user-attachments/files/19960289/yazlab_rapor.pdf) linkinden erişebilirsiniz.



## Hazırlayanlar

- Ahmet Efe Tosun - ahefto@gmail.com
- Umut Gülfidan - umutgulfidan41@gmail.com
- Utku Genç - utkugenc2003@gmail.com



## ReaProject - EN
This project has been developed within the scope of the Database Management Systems course. An real estate website has been developed as part of the project. You can find all the details about the project in the report file.

## Installation

1. **Database Setup:**
   - Run the following SQL query to create the database:
     [Database Table Codes.txt](https://github.com/umutgulfidan/ReaProject/files/15175657/Database.Table.Codes.txt)

   - After running the query, you are expected to obtain the following tables:
     - ![image](https://github.com/umutgulfidan/ReaProject/assets/127475996/3ea7e77f-1011-4b6f-ae9a-a6aa23a955ae)

   - Make sure you configure the SQL Server connection in the `DataAccess -> Concrete -> EntityFramework -> ReaContext` class correctly.
   - ![image](https://github.com/umutgulfidan/ReaProject/assets/127475996/433e6529-497a-486f-918d-cd3076a73aef)

2. **Generating Fake Data:**
   - Run the `Program.cs` file in the ConsoleUI layer to add fake data.
   - You can adjust the number of users and the number of real estate/land listings per user beforehand.
   - ![DataGenerator](https://github.com/umutgulfidan/ReaProject/assets/127475996/71c9de42-9f7a-4b61-8e48-10dd239a6c6c)

3. **Running the Web API:**
   - After the installation is complete, you can run the Web API.

## Frontend Setup

For the frontend, go to [[Rea-Frontend](https://github.com/Utku-Genc/Rea-Frontend)] and follow the instructions.

## Project Report

You can access the project report via the [Report](https://github.com/umutgulfidan/ReaProject/files/15212558/26_rapor.pdf) link.

## Contributors

- Ahmet Efe Tosun - ahefto@gmail.com
- Umut Gülfidan - umutgulfidan41@gmail.com
- Utku Genç - utkugenc2003@gmail.com
