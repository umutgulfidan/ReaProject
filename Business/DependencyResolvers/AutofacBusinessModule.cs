using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ListingManager>().As<IListingService>().SingleInstance();
            builder.RegisterType<EfListingDal>().As<IListingDal>().SingleInstance();

            builder.RegisterType<HouseListingManager>().As<IHouseListingService>().SingleInstance();
            builder.RegisterType<EfHouseListingDal>().As<IHouseListingDal>().SingleInstance();

            builder.RegisterType<ListingTypeManager>().As<IListingTypeService>().SingleInstance();
            builder.RegisterType<EfListingTypeDal>().As<IListingTypeDal>().SingleInstance();


            builder.RegisterType<HouseTypeManager>().As<IHouseTypeService>().SingleInstance();
            builder.RegisterType<EfHouseTypeDal>().As<IHouseTypeDal>().SingleInstance();


            builder.RegisterType<ListingImageManager>().As<IListingImageService>().SingleInstance();
            builder.RegisterType<EfListingImageDal>().As<IListingImageDal>().SingleInstance();

            builder.RegisterType<CityManager>().As<ICityService>().SingleInstance();
            builder.RegisterType<EfCityDal>().As<ICityDal>().SingleInstance();

            builder.RegisterType<DistrictManager>().As<IDistrictService>().SingleInstance();
            builder.RegisterType<EfDistrictDal>().As<IDistrictDal>().SingleInstance();



            builder.RegisterType<FileHelperManager>().As<IFileHelper>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
