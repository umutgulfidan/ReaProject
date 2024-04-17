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
    }
}
