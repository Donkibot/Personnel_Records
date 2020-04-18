using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.IO;

namespace WPF_MD_Personnel_Records
{
    class SqlManager
    {
        IShowMsg messageHendler;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Visual Studio Code Projects\C#\WPF_MD_Personnel_Records\Database1.mdf"";Integrated Security=True";

       public SqlManager(IShowMsg shwMsg = null)
        {
            messageHendler = shwMsg;
        }


        public List<Member> GetAllMembers()
        {
            List<Member> members = new List<Member>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                command.CommandText = @"select MemberId, Name, Surname, Patronymic, Phone_number, Members.Position_id, 
                                        Members.Photo_path, Position_name, Sex, Note from Members join positions
                                        on (positions.Position_id = Members.Position_id)";

                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    int memberId = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string surname = reader.GetString(2);
                    string patr = reader.GetString(3);
                    string phone = reader.GetString(4);
                    int positionId = reader.GetInt32(5);
                    string photopath = reader.GetString(6);
                    string positionName = reader.GetString(7);
                    string sex = reader.GetString(8);
                    string note = reader[9] == DBNull.Value ? null: reader.GetString(9);
                    PhotoClass photo = new PhotoClass(photopath);
                    Position position = new Position(positionId, positionName);
                    Member member = new Member(memberId, name, surname, patr, phone, sex, position,photo, note);
                    members.Add(member);
                }
            }
            return members;
        }

        public List<Member> GetAllMembersFound(string nameOrsurOrpatr, string position_name)
        {
            List<Member> members = new List<Member>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "find_member";
                //command.CommandText = $"select * from Members join positionson Members.Position_id = positions.Position_id " +
                //    $"where (Name like(N'%{nameOrsurOrpatr}%') or Surname like(N'%{nameOrsurOrpatr}%') or Patronymic like(N'%{nameOrsurOrpatr}%')) and Position_name Like(N'%{position_name}%')";
                command.Parameters.Add("@name", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@position_name", SqlDbType.NVarChar, 50);
                command.Parameters["@name"].Value = $"{nameOrsurOrpatr}";
                command.Parameters["@position_name"].Value = $"{position_name}";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int memberId = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string surname = reader.GetString(2);
                    string patr = reader.GetString(3);
                    string phone = reader.GetString(4);
                    int positionId = reader.GetInt32(5);
                    string photopath = reader.GetString(6);
                    string positionName = reader.GetString(7);
                    string sex = reader.GetString(8);
                    string note = reader[9] == DBNull.Value ? null: reader.GetString(9);
                    PhotoClass photo = new PhotoClass(photopath);
                    Position position = new Position(positionId, positionName);
                    Member member = new Member(memberId, name, surname, patr, phone, sex, position,photo, note);
                    members.Add(member);
                }
            }
            return members;
        }

        public List<Position> GetAllPosition()
        {
            List<Position> positions = new List<Position>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                command.CommandText = @"select Position_id, Position_name from positions";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    Position position = new Position(id, name);
                    positions.Add(position);
                }
            }
            return positions;
        }

        public void updateMember(Member member)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "update_member";
                command.Parameters.Add("@MemberId", SqlDbType.Int);
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@Surname", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@Patronymic", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@phone", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@note", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@Position_id", SqlDbType.Int);
                command.Parameters.Add("@Photo_path", SqlDbType.NVarChar, 200);
                command.Parameters["@Name"].Value = member.name;
                command.Parameters["@Surname"].Value = member.surname;
                command.Parameters["@Patronymic"].Value = member.patronymic;
                command.Parameters["@phone"].Value = member.phoneNum;
                command.Parameters["@note"].Value = member.note;
                command.Parameters["@Position_id"].Value = member.position.id;
                command.Parameters["@MemberId"].Value = member.id;
                command.Parameters["@Photo_path"].Value = member.photo.PhotoPath;
                command.ExecuteNonQuery();
            }
        }


        public void DeleteMember(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                string query = "delete from Members where MemberId = @id";
                command.CommandText = query;
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;
                command.ExecuteNonQuery();
            }
        }
        public void AddMember(Member member)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "add_member";
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@Surname", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@Patronymic", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@phone", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@note", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@Position_id", SqlDbType.Int);
                command.Parameters.Add("@Photo_path", SqlDbType.NVarChar, 200);
                command.Parameters.Add("@Sex", SqlDbType.NVarChar, 10);
                command.Parameters["@Name"].Value = member.name;
                command.Parameters["@Surname"].Value = member.surname;
                command.Parameters["@Patronymic"].Value = member.patronymic;
                command.Parameters["@phone"].Value = member.phoneNum;
                command.Parameters["@note"].Value = member.note;
                command.Parameters["@Position_id"].Value = member.position.id;
                command.Parameters["@Photo_path"].Value = member.photo.PhotoPath;
                command.Parameters["@Sex"].Value = member.sex;
                command.ExecuteNonQuery();
            }
        }
        private void ShowMsg(string msg)
        {
            messageHendler.ShowMsg(msg);
        }
    }
}
