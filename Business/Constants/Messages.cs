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
        public static string UserAlreadyActive = "Kullanıcı hesabı zaten aktif.";
        public static string UserAlreadyInactive = "Kullanıcı hesabı zaten pasif.";
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
        public static string PassiveAccount = "Giriş yapmaya çalıştığınız hesap pasif durumda.Yasaklanmış ya da silinmiş olabilir.";
        public static string LoginRequired = "Bu işlemi yapabilmeniz için giriş yapmanız gerekiyor";

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

        //LandListing

        public static string LandListingAdded = "Arsa ilanı başarıyla eklendi";
        public static string LandListingDeleted = "Arsa ilanı başarıyla silindi";
        public static string LandListingListed = "Arsa ilanları başarıyla listelendi";
        public static string LandListingUpdated = "Arsa ilanları başarıyla güncellendi";
        public static string LandListingFiltered = "Arsa ilanları başarıyla filtrelendi";
        public static string GetLandListingDetail = "Detaylar başarıyla getirildi";

        //HouseType

        public static string HouseTypeAdded = "Ev tipi başarıyla eklendi";
        public static string HouseTypeDeleted = "Ev tipi başarıyla silindi";
        public static string HouseTypeUpdated = "Ev tipleri başarıyla güncellendi";
        public static string HouseTypeListed = "Ev tipleri başarıyla listelendi";

        //ListingImage

        public static string ListingImageAdded = "İlan resmi başarıyla eklendi";
        public static string ListingImageDeleted = "İlan resmi başarıyla silindi";
        public static string ListingImageUpdated = "İlan resmi başarıyla güncellendi";
        public static string ListingImageListed = "İlan resimleri başarıyla listelendi";

        //ListingType

        public static string ListingTypeAdded = "İlan tipi başarıyla eklendi";
        public static string ListingTypeDeleted = "İlan tipi başarıyla silindi";
        public static string ListingTypeUpdated = "İlan tipi başarıyla güncellendi";
        public static string ListingTypeListed = "İlan tipi başarıyla listelendi";

        //OperationClaim

        public static string OperationClaimAdded = "Rol başarıyla eklendi";
        public static string OperationClaimDeleted = "Rol başarıyla silindi";
        public static string OperationClaimUpdated = "Rol başarıyla güncellendi";
        public static string OperationClaimListed = "Roller başarıyla listelendi";

        //UserImage

        public static string UserImageAdded = "Kullanıcı resmi başarıyla eklendi";
        public static string UserImageDeleted = "Kullanıcı resmi başarıyla silindi";
        public static string UserImageUpdated = "Kullanıcı resmi başarıyla güncellendi";
        public static string UserImageListed = "Kullanıcı resimleri başarıyla listelendi";

        //UserOperationClaim

        public static string UserOperationClaimAdded = "Yetki başarıyla verildi";
        public static string UserOperationClaimDeleted = "Yetki başarıyla alındı";
        public static string UserOperationClaimUpdated = "Yetki başarıyla güncellendi";
        public static string UserOperationClaimListed = "Yetkiler başarıyla listelendi";
        public static string UserOperationClaimAlreadyExisting = "Vermeye çalıştığınız rol zaten kullanıcıda mevcut.";

        // Complaint
        public static string ComplaintAdded = "Şikayet başarıyla eklendi";
        public static string ComplaintDeleted = "Şikayet başarıyla silindi";
        public static string ComplaintListed = "Şikayetler başarıyla listelendi";
        public static string ComplaintUpdated = "Şikayet başarıyla güncellendi";
        public static string ComplaintNotFound = "Şikayet bulunamadı";

        // ComplaintStatus
        public static string ComplaintStatusAdded = "Şikayet durumu başarıyla eklendi";
        public static string ComplaintStatusDeleted = "Şikayet durumu başarıyla silindi";
        public static string ComplaintStatusListed = "Şikayet durumları başarıyla listelendi";
        public static string ComplaintStatusUpdated = "Şikayet durumu başarıyla güncellendi";
        public static string ComplaintStatusNotFound = "Şikayet durumu bulunamadı";

        // ListingComplaintReason
        public static string ListingComplaintReasonAdded = "İlan şikayet nedeni başarıyla eklendi";
        public static string ListingComplaintReasonDeleted = "İlan şikayet nedeni başarıyla silindi";
        public static string ListingComplaintReasonListed = "İlan şikayet nedenleri başarıyla listelendi";
        public static string ListingComplaintReasonUpdated = "İlan şikayet nedeni başarıyla güncellendi";
        public static string ListingComplaintReasonNotFound = "İlan şikayet nedeni bulunamadı";

        // UserComplaintReason
        public static string UserComplaintReasonAdded = "Kullanıcı şikayet nedeni başarıyla eklendi";
        public static string UserComplaintReasonDeleted = "Kullanıcı şikayet nedeni başarıyla silindi";
        public static string UserComplaintReasonListed = "Kullanıcı şikayet nedenleri başarıyla listelendi";
        public static string UserComplaintReasonUpdated = "Kullanıcı şikayet nedeni başarıyla güncellendi";
        public static string UserComplaintReasonNotFound = "Kullanıcı şikayet nedeni bulunamadı";

        // ListingComplaint
        public static string ListingComplaintAdded = "İlan şikayeti başarıyla eklendi";
        public static string ListingComplaintDeleted = "İlan şikayeti başarıyla silindi";
        public static string ListingComplaintListed = "İlan şikayetleri başarıyla listelendi";
        public static string ListingComplaintUpdated = "İlan şikayeti başarıyla güncellendi";
        public static string ListingComplaintNotFound = "İlan şikayeti bulunamadı";

        // UserComplaint
        public static string UserComplaintAdded = "Kullanıcı şikayeti başarıyla eklendi";
        public static string UserComplaintDeleted = "Kullanıcı şikayeti başarıyla silindi";
        public static string UserComplaintListed = "Kullanıcı şikayetleri başarıyla listelendi";
        public static string UserComplaintUpdated = "Kullanıcı şikayeti başarıyla güncellendi";
        public static string UserComplaintNotFound = "Kullanıcı şikayeti bulunamadı";

        // ComplaintResponse
        public static string ComplaintResponseAdded = "Şikayet cevabı başarıyla eklendi";
        public static string ComplaintResponseDeleted = "Şikayet cevabı başarıyla silindi";
        public static string ComplaintResponseListed = "Şikayet cevapları başarıyla listelendi";
        public static string ComplaintResponseUpdated = "Şikayet cevabı başarıyla güncellendi";
        public static string ComplaintResponseNotFound = "Şikayet cevabı bulunamadı";




    }
}
