using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Core;
using TripPlanner.Core.DTOs;
using TripPlanner.Core.Entities;
using TripPlanner.Core.Interfaces;

namespace TripPlanner.DAL.Repos
{
    public class ADOReportsRepository : IReportsRepository
    {
        private string _connectionString;

        public ADOReportsRepository()
        {
            _connectionString = "Server=localhost;Database=TripPlanner;User Id=sa;Password=YOUR_strong_*pass4w0rd*";
        }

        public ADOReportsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Response<List<MostReviewedDestinations>> GetMostRatedDestintions()
        {
            Response<List<MostReviewedDestinations>> result = new Response<List<MostReviewedDestinations>>();
            List<MostReviewedDestinations> mostReviewedDestinations = new List<MostReviewedDestinations>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"MostReviewedDestinations";
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.HasRows && dr.Read())
                    {
                        MostReviewedDestinations destination = new MostReviewedDestinations();
                        destination.DestinationID = int.Parse(dr["DestinationID"].ToString());
                        destination.City = dr["City"].ToString();
                        destination.Country = dr["Country"].ToString();
                        destination.NumberOfReviews = int.Parse(dr["Number Of Reviews"].ToString());
                        if (dr["StateProvince"] != DBNull.Value)
                        {
                            destination.StateProvince = dr["StateProvince"].ToString();
                        }

                        mostReviewedDestinations.Add(destination);
                    }

                }
                catch (SqlException ex)
                {
                    result.Message = ex.Message;
                    return result;
                }
            }
            if (mostReviewedDestinations.Count <= 0)
            {
                result.Message = "Could not find most reviewed destinations list.";
                return result;
            }
            result.Data = mostReviewedDestinations;
            return result;
        }

        public Response<List<MostVisitedDestinations>> GetMostVisitedDestintions()
        {
            Response<List<MostVisitedDestinations>> result = new Response<List<MostVisitedDestinations>>();
            List<MostVisitedDestinations> mostVisitedDestinations = new List<MostVisitedDestinations>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"MostVisitedDestinations";
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.HasRows && dr.Read())
                    {
                        MostVisitedDestinations destination = new MostVisitedDestinations();
                        destination.DestinationID = int.Parse(dr["DestinationID"].ToString());
                        destination.City = dr["City"].ToString();
                        destination.Country = dr["Country"].ToString();
                        destination.TotalVisitors = int.Parse(dr["Number Of Visitors"].ToString());
                        if (dr["StateProvince"] != DBNull.Value)
                        {
                            destination.StateProvince = dr["StateProvince"].ToString();
                        }

                        mostVisitedDestinations.Add(destination);
                    }

                }
                catch (SqlException ex)
                {
                    result.Message = ex.Message;
                    return result;
                }
            }
            if (mostVisitedDestinations.Count <= 0)
            {
                result.Message = "Could not find most visited destinations list.";
                return result;
            }
            result.Data = mostVisitedDestinations;
            return result;
        }

        public Response<List<TopRatedDestinations>> GetTopRatedDestintions()
        {
            Response<List<TopRatedDestinations>> result = new Response<List<TopRatedDestinations>>();
            List<TopRatedDestinations> topRatedDestinations = new List<TopRatedDestinations>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"TopRatedDestinations";
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.HasRows && dr.Read())
                    {
                        TopRatedDestinations destination = new TopRatedDestinations();
                        destination.DestinationID = int.Parse(dr["DestinationID"].ToString());
                        destination.City = dr["City"].ToString();
                        destination.Country = dr["Country"].ToString();
                        destination.AverageRating = int.Parse(dr["Average Rating"].ToString());
                        if (dr["StateProvince"] != DBNull.Value)
                        {
                            destination.StateProvince = dr["StateProvince"].ToString();
                        }

                        topRatedDestinations.Add(destination);
                    }

                }
                catch (SqlException ex)
                {
                    result.Message = ex.Message;
                    return result;
                }
            }
            if (topRatedDestinations.Count <= 0)
            {
                result.Message = "Could not find top rated destinations list.";
                return result;
            }
            result.Data = topRatedDestinations;
            return result;
        }
    }
}
