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

       public SqlManager(IShowMsg shwMsg)
        {
            messageHendler = shwMsg;
        }

        public void AddImageToDB(string picPath)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                command.CommandText = "insert into Photos (Photo, File_format) values (@photo,@format)";
                command.Parameters.Add("@photo", SqlDbType.VarBinary, -1);
                command.Parameters.Add("@format", SqlDbType.VarChar,  10);
                byte[] imageData;
                using (FileStream fs = new FileStream(picPath, FileMode.Open))
                {
                    imageData = new byte[fs.Length];
                    fs.Read(imageData,0, imageData.Length);
                }
                command.Parameters["@photo"].Value = imageData;
                command.Parameters["@format"].Value = Path.GetExtension(picPath);
                command.ExecuteNonQuery();
                ShowMsg(command.CommandText);
            }
        }

        public DB_Image GetImageFromDB(int id)
        {
            List<DB_Image> image = new List<DB_Image>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                con.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                command.CommandText = "select Photo, File_format from Photos where Photo_id = @id";
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;
                SqlDataReader reader = command.ExecuteReader();
                byte[] imageData;
                string imgFormat;
                while (reader.Read())
                {
                    imageData = (byte[])reader.GetValue(0);
                    imgFormat = reader.GetString(1);
                    image.Add(new DB_Image(id, imageData, imgFormat));
                }
            }
            return image[0];
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
Members.Photo_id, Photo, Position_name, File_format, Sex, Note from Members join Photos on (Members.Photo_id = Photos.Photo_id)  join positions 
on (positions.Position_id = Members.Position_id)";

                SqlDataReader reader = command.ExecuteReader();
                
                string imgFormat;
                while (reader.Read())
                {
                    int memberId = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string surname = reader.GetString(2);
                    string patr = reader.GetString(3);
                    string phone = reader.GetString(4);
                    int positionId = reader.GetInt32(5);
                    int photoId = reader.GetInt32(6);
                    byte[] imageData = reader[7] == DBNull.Value? null:(byte[])reader.GetValue(7);
                    string positionName = reader.GetString(8);
                    string fileForm = reader.GetString(9);
                    string sex = reader.GetString(10);
                    string note = reader[11] == DBNull.Value ? null: reader.GetString(11);
                    DB_Image photo = new DB_Image(photoId,imageData,fileForm);
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
                command.Parameters["@Name"].Value = member.name;
                command.Parameters["@Surname"].Value = member.surname;
                command.Parameters["@Patronymic"].Value = member.patronymic;
                command.Parameters["@phone"].Value = member.phoneNum;
                command.Parameters["@note"].Value = member.note;
                command.Parameters["@Position_id"].Value = member.position.id;
                command.Parameters["@MemberId"].Value = member.id;
                command.ExecuteNonQuery();
            }
        }

        private void ShowMsg(string msg)
        {
            messageHendler.ShowMsg(msg);
        }
    }
}
