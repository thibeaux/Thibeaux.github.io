using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo_Authentication_System.ViewModels.Commands;
using System.Diagnostics;
using System.IO;
using Zoo_Authentication_System.Models;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;
using System.Data.SqlClient;

namespace Zoo_Authentication_System.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		UserClass user;
		string username { get; set; } = null;
		public string UserName
		{
			get
			{
				return username;
			}
			set
			{
				username = value;
			}
		}
		string password { get; set; } 
		public string Password
		{
			get
			{
				return password;
			}
			set
			{
				password = value;
			}
		}
		ushort attempts { get; set; } = 0;
		string _wrongPasswordWarning = "hidden";
		public string WrongPassWarning
		{
			get
			{
				return _wrongPasswordWarning;
			}
			set
			{
				_wrongPasswordWarning = value;
				OnPropertyChanged(nameof(WrongPassWarning));
			}
		}

		public LoginCommand loginCommand { get; set; }

		public BaseViewModel()
		{
			this.loginCommand = new LoginCommand(this);
			
		}
		public string[] getUserCredentialsFromDatabase(SqlConnection sqlConnection)
		{
			// To prevent SQL injection we are not allowing the user access to queries.
			// We will get the data from the table and process the data manually in our validation function

			string[] datatable = null;
			string query = "SELECT * FROM EmployeeCredentials";
			var command = new SqlCommand(query, sqlConnection);
			int i = 0;
			SqlDataReader sqlDataReader = command.ExecuteReader();
			datatable = new string[sqlDataReader.FieldCount];
			if(sqlDataReader.HasRows)
            {
				// each row has 5 columns
				while(sqlDataReader.Read())
                {
					var res = sqlDataReader.GetString(1) + "\t" + sqlDataReader.GetString(2) + "\t" + sqlDataReader.GetString(3);
					Trace.WriteLine(res);

					datatable[i] = res;
					i++;
				}
            }
			return datatable;
		}
		public void login()
        {
			// Makeing a new instance here when we click login. Building our sql connection in the constructor
			// may cause memory leaks because the UI calls the class constructor on start up. This way we can control the life of these objects. 
			SQLHelp sqlHelper = null;
			SqlConnection sql = null;
			sqlHelper = new SQLHelp();
			sql = sqlHelper.connect();

			attempts++;
			if (attempts < 4)
			{
				Trace.WriteLine(username + " " + password); // debug

				string hashedPassword = hashString(password);
				Trace.WriteLine(username + " " + hashedPassword); // debug
				string[] lines = getUserCredentialsFromDatabase(sql);

				try
				{
					foreach (string line in lines)
					{
						string[] lineBuffer = line.Split('\t');
						string usernameOnFile = lineBuffer[0].Trim();
						string passwordHashOnFile = lineBuffer[1].Trim();
						string roleOnFile = lineBuffer[2].Trim();

						user = new UserClass(UserName, hashedPassword, roleOnFile);
						bool verify = user.userProfile(usernameOnFile, passwordHashOnFile);

						if (verify)
						{
							WrongPassWarning = "Hidden";
							OnPropertyChanged(nameof(WrongPassWarning));
							if (user.Role == "zookeeper")
							{
								attempts = 4;
								ZookeeperWindow zookeeperWindow = new ZookeeperWindow();
								zookeeperWindow.Show();
								//clean up
								sqlHelper.disconnect(sql);
								sqlHelper = null;
								return;

							}
							else if (user.Role == "admin")
							{
								attempts = 4;
								AdminWindow adminWindow = new AdminWindow();
								adminWindow.Show();
								//clean up
								sqlHelper.disconnect(sql);
								sqlHelper = null;
								return;
							}
							else if (user.Role == "veterinarian")
							{
								attempts = 4;
								VeternarianWindow vetWindow = new VeternarianWindow();
								vetWindow.Show();
								//clean up
								sqlHelper.disconnect(sql);
								sqlHelper = null;
								return;
							}

						}
						else
						{
							WrongPassWarning = "Visible";
						}
					}
				}catch (Exception ex)
				{ Trace.WriteLine(ex.ToString());}
			}
            else 
			{
				MessageBox.Show("No more attpemts left, you are locked out from either opening another window, or trying to log in again.", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
			}

			//clean up
			sqlHelper.disconnect(sql);
			sqlHelper = null;

        }

		/// <summary>
		/// Obsolete Function, leaving here in case we ever need it again for debugging in the future. Use the 
		/// getUserCredentialsFromDatabase function or make a new function related that utilizes the database. 
		/// </summary>
		/// <returns></returns>
		string[] getUserData()
        {
            string[] lines = null;

			string filename = "C:\\Users\\brand\\Documents\\SNHU Class files\\CS-499 Capstone\\Authentication App\\C#\\Zoo_Authentication_System\\Zoo_Authentication_System\\Models\\credentials\\credentials.txt";
			try
			{
				lines = File.ReadAllLines(filename);
				if(lines == null)
				{ throw new Exception("Your data source path is incorrect, see getUserData() function in Base View Model"); }
			}
			catch (Exception ex)
            {
				Trace.WriteLine(ex.Message);
				MessageBox.Show(ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
			}

			return lines;
		}


		bool attemptLogin()
        {
			bool verified = false;

			return verified;
        }

		static String hashString(String str)
		{
			//MD5 section 
			String original = str;  //Replace "password" with the actual password inputted by the user
			byte[] hash = null;
			try
			{ 
				var md5 = MD5.Create();
				byte[] inputeByte = Encoding.ASCII.GetBytes(original);
				hash = md5.ComputeHash(inputeByte);
				//md = MessageDigest.getInstance("MD5");
			}
			catch (Exception e)
			{
				// TODO Auto-generated catch block
				Trace.WriteLine(e.Message) ;
			}
			var stringBuilder = new StringBuilder();
			try
			{
				if (hash == null)
					throw new Exception("Login credentials cannot be null");
				for (int i = 0; i < hash.Length; i++)
				{
					stringBuilder.Append(hash[i].ToString("X2"));
				}
			}
			catch(Exception e)
            {
				MessageBox.Show(e.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			return stringBuilder.ToString();
		}

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}

internal class SQLHelp
{
	string location = "C:\\Users\\brand\\Documents\\SNHU Class files\\CS-499 Capstone\\Authentication App\\C#" +
				"\\Zoo_Authentication_System\\Zoo_Authentication_System\\Models\\credentials\\ZooDatabase.mdf";
	public SqlConnection connect()
	{
		SqlConnection sqlConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + location + ";Integrated Security=True");
		try
		{
			if (sqlConnection.State == System.Data.ConnectionState.Closed)
			{
				sqlConnection.Open();
			}
		}
		catch (Exception ex) { sqlConnection.Close(); Trace.WriteLine(ex.Message); }
		return sqlConnection;
	}
	public void disconnect(SqlConnection sqlConnection)
    {
		sqlConnection.Dispose();
    }
}