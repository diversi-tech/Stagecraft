﻿using Common;
using Microsoft.Extensions.Configuration;
using Npgsql;
using StagecraftDAL.Interface;
using System.Data;
using System.Data.SqlClient;

namespace StagecraftDAL.Services
{
    public class CourseService: ICourse
    {
        private readonly string _connectionString;

        public CourseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public static List<Classes> GetAllClass()
        {
            var t = PostgreSQLDataAccess.ExecuteFunction<Classes>("get_all_classes");
            //var t = SQLDataAccess.ExecuteStoredProcedure<List<Classes>>("GetAllClass", null);
            return t;
        }
        public  List<Course>  GetAllCourses()
        {
            var t = PostgreSQLDataAccess.ExecuteFunction<Course>("get_all_courses");
            return t;
        }

        public Course GetCourseById(int course_id)
        {
            //SqlParameter param1 = new SqlParameter("@CourseId", course_id);
            //var t = SQLDataAccess.ExecuteStoredProcedure<List<Course>>("GetCourseById", param1).FirstOrDefault();

            NpgsqlParameter param1 = new NpgsqlParameter("course_id", course_id);
            var t = PostgreSQLDataAccess.ExecuteFunction<Course>("get_course_by_id", param1).FirstOrDefault();

            return t;
        
        }

        //public  List<Course> GetCourseDetails()
        //{
        //    //var t = SQLDataAccess.ExecuteStoredProcedure<List<Course>>("getAllCourses", null);
        //    var t = PostgreSQLDataAccess.ExecuteFunction<Course>("get_all_courses", null);
        //    return t;
        //}

        //List<Course> ICourse.GetCoursesByUserId(int userId)
        //{
        //    List<Course> courses = new List<Course>();
        //    using (var connection = new SqlConnection(_connectionString))
        //    using (var command = new SqlCommand("GetCoursesByUserId", connection))
        //    {
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.AddWithValue("@userId", userId);

        //        connection.Open();
        //        using (var reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                courses.Add(new Course
        //                {
        //                    courses_id = (int)reader["courses_id"],
        //                    courses_name = reader["courses_name"] != DBNull.Value ? (string)reader["courses_name"] : null,
        //                    title = reader["title"] != DBNull.Value ? (string)reader["title"] : null,
        //                    description = reader["description"] != DBNull.Value ? (string)reader["description"] : null,
        //                    price = reader["price"] != DBNull.Value ? (int)reader["price"] : 0,
        //                    Several_chapters = reader["Several_chapters"] != DBNull.Value ? (int)reader["Several_chapters"] : 0,
        //                    length = reader["length"] != DBNull.Value ? (string)reader["length"] : null,
        //                    numberOfViewers = reader["numberOfViewers"] != DBNull.Value ? (int)reader["numberOfViewers"] : 0,
        //                    create_at = reader["create_at"] != DBNull.Value ? (DateTime)reader["create_at"] : DateTime.MinValue,
        //                    update_at = reader["update_at"] != DBNull.Value ? (DateTime)reader["update_at"] : DateTime.MinValue,
        //                    expanded = false // assuming the default value for 'expanded' is false
        //                });
        //            }
        //        }
        //    }
        //    return courses;
        //}


    }
}
