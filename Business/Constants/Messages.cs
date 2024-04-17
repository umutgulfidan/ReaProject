using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        //Listing
        public static string ListingAdded = "İlan Başarıyla Eklendi";
        public static string ListingListed = "İlanlar Başarıyla Listelendi";
        public static string ListingDeleted = "İlanlar başarıyla silindi";
        public static string GetListingDetails = "İlanlar başarıyla getirildi";
        public static string ListingUpdated = "İlanlar başarıyla güncellendi";



        //User

        public static string UserAdded = "Kullanıcı başarıyla eklendi.";
        public static string UserDeleted = "Kullanıcı başarıyla silindi.";
        public static string UserUpdated = "Kullanıcı başarıyla güncellendi.";
        public static string UsersListed = "Kullanıcılar başarıyla listelendi.";
        public static string PasswordRequirements = "Şifre en az bir harf, bir sayı ve bir özel karakter içermelidir.";
        public static string FirstNameMustContainOnlyLetter = "Ad sadece karakterlerden oluşmalıdır";
        public static string LastNameMustContainOnlyLetter = "Soyad sadece karakterlerden oluşmalıdır";

        //Auth

        public static string AuthorizationDenied = "Yetkiniz Yok";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";

        //City

        public static string CityAdded = "Şehir başarıyla eklendi";
        public static string CityDeleted = "Şehir başarıyla silindi";
        public static string CityListed = "Şehirler başarıyla listelendi";
        public static string CityUpdated = "Şehir başarıyla güncellendi";

        //District

        public static string DistrictAdded = "İlçe başarıyla eklendi";
        public static string DistrictDeleted = "İlçe başarıyla silindi";
        public static string DistrictListed = "İlçeler başarıyla listelendi";
        public static string DistrictUpdated = "İlçe başarıyla güncellendi";

        //HouseListing

        public static string HouseListingAdded = "Ev ilanı başarıyla eklendi";
        public static string HouseListingDeleted = "Ev ilanı başarıyla silindi";
        public static string HouseListingListed = "Ev ilanları başarıyla listelendi";
        public static string HouseListingUpdated = "Ev ilanları başarıyla güncellendi";
        public static string GetHouseListing= "Ev ilanları başarıyla getirildi";
        public static string GetHouseListingDetails = "Detaylar başarıyla getirildi";


    }
}
