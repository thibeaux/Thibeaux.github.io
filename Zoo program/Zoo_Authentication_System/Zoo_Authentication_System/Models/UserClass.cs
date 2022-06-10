using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo_Authentication_System.Models
{
	internal class UserClass
	{
		String UserName { get; set; }
		String Hashcode { get; set; }	
		public String Role { get; set; }

		bool verify = false;

		public UserClass(string username, string hashpass, string role)
        {
			this.UserName = username;
			this.Hashcode = hashpass;
			this.Role = role;
        }
		public bool userProfile(String usernameOnFile, String hashCodeOnFile)
		{

			verify = auth( usernameOnFile, hashCodeOnFile, this.Hashcode, this.UserName);

			if (verify == true)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void getContent()
		{
			setRole(Role);
		}
		public bool auth(String fileUsername, String fileHashcode, String newHash, String newUserName)
		{

			verify = ((newHash.ToLower().Equals((fileHashcode))) && (fileUsername.Equals(newUserName)));

			return verify;
		}

		String roleFile;
		public void setRole(String role)
		{
			this.roleFile = role;
			try
			{
				userContent();
			}
			catch (Exception e)
			{
				Trace.WriteLine(e.Message);
			}
		}

		public void userContent()
		{
			Trace.WriteLine("Hello there zoo keeper!");

		}
	}
}
