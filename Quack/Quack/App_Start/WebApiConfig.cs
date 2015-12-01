using AutoMapper;
using Quack.Core.Domain;
using Quack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Quack
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            SetupAutoMapper();
        }
        private static void SetupAutoMapper()
        {
            Mapper.CreateMap<Bookmark, BookmarkModel>();
            Mapper.CreateMap<Cohort, CohortModel>();
            Mapper.CreateMap<MediaType, MediaTypeModel>();
            Mapper.CreateMap<Student, StudentModel>();
            Mapper.CreateMap<Teacher, TeacherModel>();
            Mapper.CreateMap<TeacherCohort, TeacherCohortModel>();
        }
    }
}
