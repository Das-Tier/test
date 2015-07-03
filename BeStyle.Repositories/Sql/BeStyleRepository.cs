using BeStyle.Repositories.Abstract;
using BeStyle.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;


namespace BeStyle.Repositories.Sql
{
    public class BeStyleRepository : IBeStyleRepository
    {
        private readonly string _connectionString;

        public BeStyleRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public List<AdminInfo> GetAdminAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmd = @"SELECT * FROM tblAdmin";
                using (SqlCommand command = new SqlCommand(cmd, connection))
                {
                    List<AdminInfo> admins = new List<AdminInfo>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AdminInfo admin = ReadAdmin(reader);
                            admins.Add(admin);
                        }
                        return admins;
                    }
                }
            }
        }
        public Credential GetCredential(int id)
        {
            Credential cred = new Credential();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string command = String.Format(@"SELECT * FROM tblAdminCredential WHERE AdminId={0}", id);
                using (SqlCommand cmd = new SqlCommand(command, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return this.ReadCredential(reader);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public Role GetRole(int id)
        {
            Role role = new Role();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string command = String.Format(@"SELECT * FROM tblAdminRole WHERE Id={0}", id);
                using (SqlCommand cmd = new SqlCommand(command, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                       if(reader.Read())
                       {
                           return this.ReadRole(reader);
                       }
                       else
                       {
                           return null;
                       }
                    }
                }
            }
        }
        private AdminInfo ReadAdmin(SqlDataReader reader)
        {
            AdminInfo admin = new AdminInfo();
            
            admin.ID = (int)reader["Id"];
            admin.FirstName=(string)reader["FirstName"];
            admin.LastName = (string)reader["LastName"];
            admin.Email = (string)reader["Email"];
            admin.Phone= (string)reader["Phone"];
            admin.Login = GetCredential(admin.ID).Login;
            admin.Password = GetCredential(admin.ID).Password;
            admin.Role = GetRole(GetCredential(admin.ID).RoleID).Value;
            return admin;
       }
        private Credential ReadCredential(SqlDataReader reader) 
        {
            Credential cred=new Credential();
            cred.ID=(int)reader["Id"];
            cred.AdminID=(int)reader["AdminId"];
            cred.Login=(string)reader["Login"];
            cred.Password=(string)reader["Password"];
            cred.RoleID=(int)reader["RoleId"];
            return cred;
        }

        private Role ReadRole(SqlDataReader reader)
        {
            Role role = new Role();
            role.ID = (int)reader["Id"];
            role.Value = (string)reader["Role"];
            return role;
        }
    }
}