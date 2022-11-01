using System;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;


namespace Application_QS_CheckList.Class
{
    class RegistryTool
    {
        private const string RegKey = @"Software\Microsoft\Windows\CurrentVersion\App Paths";

        public static string GetOfficeVersion()
        {
            string _wordPath = GetComponentPath(OfficeComponent.Word);

            if (GetMajorVersion(_wordPath) == 7) return "Microsoft Office 95";
            else if (GetMajorVersion(_wordPath) == 8) return "Microsoft Office 97";
            else if (GetMajorVersion(_wordPath) == 9) return "Microsoft Office 2000";
            else if (GetMajorVersion(_wordPath) == 10) return "Microsoft Office XP";
            else if (GetMajorVersion(_wordPath) == 11) return "Microsoft Office 2003";
            else if (GetMajorVersion(_wordPath) == 12) return "Microsoft Office 2007";
            else if (GetMajorVersion(_wordPath) == 14) return "Microsoft Office 2010";
            else if (GetMajorVersion(_wordPath) == 15) return "Microsoft Office 2013";
            else if (GetMajorVersion(_wordPath) == 16) return "Microsoft Office 2016";
            else return "Error";
            
        }

        public static string GetComponentPath(OfficeComponent _component)
        {
            string toReturn = string.Empty;
            string _key = string.Empty;

            switch (_component)
            {
                case OfficeComponent.Word:
                    _key = "winword.exe";
                    break;
                case OfficeComponent.Excel:
                    _key = "excel.exe";
                    break;
                case OfficeComponent.PowerPoint:
                    _key = "powerpnt.exe";
                    break;
                case OfficeComponent.Outlook:
                    _key = "outlook.exe";
                    break;
            }

            //looks inside CURRENT_USER:
            RegistryKey _mainKey = Registry.CurrentUser;
            try
            {
                _mainKey = _mainKey.OpenSubKey(RegKey + "\\" + _key, false);
                if (_mainKey != null)
                {
                    toReturn = _mainKey.GetValue(string.Empty).ToString();
                }
            }
            catch
            { }

            //if not found, looks inside LOCAL_MACHINE:
            _mainKey = Registry.LocalMachine;
            if (string.IsNullOrEmpty(toReturn))
            {
                try
                {
                    _mainKey = _mainKey.OpenSubKey(RegKey + "\\" + _key, false);
                    if (_mainKey != null)
                    {
                        toReturn = _mainKey.GetValue(string.Empty).ToString();
                    }
                }
                catch
                {
                }
            }

            //closing the handle:
            if (_mainKey != null)
                _mainKey.Close();

            return toReturn;
        }

        public  static int GetMajorVersion(string _path)
        {
            int toReturn = 0;
            if (File.Exists(_path))
            {
                try
                {
                    FileVersionInfo _fileVersion = FileVersionInfo.GetVersionInfo(_path);
                    toReturn = _fileVersion.FileMajorPart;
                }
                catch
                { }
            }

            return toReturn;
        }

        public static string CollectTeamviewerId()
        { 
            string TVid = string.Empty;
            foreach (var path in new[] { "SOFTWARE\\Wow6432Node\\TeamViewer" })
            {
              
                if (Registry.LocalMachine.OpenSubKey(path) != null)
                {
                    var subKey = path;
                    if (Registry.LocalMachine.OpenSubKey(subKey) != null)
                    {
                        var clientID = Registry.LocalMachine.OpenSubKey(subKey).GetValue("ClientID");
                        if (clientID != null)
                        {
                            TVid = clientID.ToString();
                        }
                    }
                   
                }
            }
            return TVid;
        }

        private bool showError = false;
		/// <summary>
		/// A property to show or hide error messages 
		/// (default = false)
		/// </summary>
		public bool ShowError
		{
			get { return showError; }
			set	{ showError = value; }
		}

		public static string subKey = "SOFTWARE\\" + Application.ProductName.ToUpper();
		/// <summary>
		/// A property to set the SubKey value
		/// (default = "SOFTWARE\\" + Application.ProductName.ToUpper())
		/// </summary>
		public static string SubKey
		{
			get { return subKey; }
			set	{ subKey = value; }
		}

		public static RegistryKey baseRegistryKey = Registry.LocalMachine;
		/// <summary>
		/// A property to set the BaseRegistryKey value.
		/// (default = Registry.LocalMachine)
		/// </summary>
		public static RegistryKey BaseRegistryKey
		{
			get { return baseRegistryKey; }
			set	{ baseRegistryKey = value; }
		}

		/* **************************************************************************
		 * **************************************************************************/

		/// <summary>
		/// To read a registry key.
		/// input: KeyName (string)
		/// output: value (string) 
		/// </summary>
		public static string Read(string KeyName)
		{
			// Opening the registry key
			RegistryKey rk = baseRegistryKey ;
			// Open a subKey as read-only
			RegistryKey sk1 = rk.OpenSubKey(subKey);
			// If the RegistrySubKey doesn't exist -> (null)
			if ( sk1 == null )
			{
				return null;
			}
			else
			{
				try 
				{
					// If the RegistryKey exists I get its value
					// or null is returned.
					return (string)sk1.GetValue(KeyName.ToUpper());
				}
				catch (Exception e)
				{
					// AAAAAAAAAAARGH, an error!
					//ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
					return null;
				}
			}
		}	

		/* **************************************************************************
		 * **************************************************************************/

		/// <summary>
		/// To write into a registry key.
		/// input: KeyName (string) , Value (object)
		/// output: true or false 
		/// </summary>
		public bool Write(string KeyName, object Value)
		{
			try
			{
				// Setting
				RegistryKey rk = baseRegistryKey ;
				// I have to use CreateSubKey 
				// (create or open it if already exits), 
				// 'cause OpenSubKey open a subKey as read-only
				RegistryKey sk1 = rk.CreateSubKey(subKey);
				// Save the value
				sk1.SetValue(KeyName.ToUpper(), Value);

				return true;
			}
			catch (Exception e)
			{
				// AAAAAAAAAAARGH, an error!
				ShowErrorMessage(e, "Writing registry " + KeyName.ToUpper());
				return false;
			}
		}

		/* **************************************************************************
		 * **************************************************************************/

		/// <summary>
		/// To delete a registry key.
		/// input: KeyName (string)
		/// output: true or false 
		/// </summary>
		public bool DeleteKey(string KeyName)
		{
			try
			{
				// Setting
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.CreateSubKey(subKey);
				// If the RegistrySubKey doesn't exists -> (true)
				if ( sk1 == null )
					return true;
				else
					sk1.DeleteValue(KeyName);

				return true;
			}
			catch (Exception e)
			{
				// AAAAAAAAAAARGH, an error!
				ShowErrorMessage(e, "Deleting SubKey " + subKey);
				return false;
			}
		}

		/* **************************************************************************
		 * **************************************************************************/

		/// <summary>
		/// To delete a sub key and any child.
		/// input: void
		/// output: true or false 
		/// </summary>
		public bool DeleteSubKeyTree()
		{
			try
			{
				// Setting
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.OpenSubKey(subKey);
				// If the RegistryKey exists, I delete it
				if ( sk1 != null )
					rk.DeleteSubKeyTree(subKey);

				return true;
			}
			catch (Exception e)
			{
				// AAAAAAAAAAARGH, an error!
				ShowErrorMessage(e, "Deleting SubKey " + subKey);
				return false;
			}
		}

		/* **************************************************************************
		 * **************************************************************************/

		/// <summary>
		/// Retrive the count of subkeys at the current key.
		/// input: void
		/// output: number of subkeys
		/// </summary>
		public int SubKeyCount()
		{
			try
			{
				// Setting
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.OpenSubKey(subKey);
				// If the RegistryKey exists...
				if ( sk1 != null )
					return sk1.SubKeyCount;
				else
					return 0; 
			}
			catch (Exception e)
			{
				// AAAAAAAAAAARGH, an error!
				ShowErrorMessage(e, "Retriving subkeys of " + subKey);
				return 0;
			}
		}

		/* **************************************************************************
		 * **************************************************************************/

		/// <summary>
		/// Retrive the count of values in the key.
		/// input: void
		/// output: number of keys
		/// </summary>
		public int ValueCount()
		{
			try
			{
				// Setting
				RegistryKey rk = baseRegistryKey ;
				RegistryKey sk1 = rk.OpenSubKey(subKey);
				// If the RegistryKey exists...
				if ( sk1 != null )
					return sk1.ValueCount;
				else
					return 0; 
			}
			catch (Exception e)
			{
				// AAAAAAAAAAARGH, an error!
				ShowErrorMessage(e, "Retriving keys of " + subKey);
				return 0;
			}
		}

		/* **************************************************************************
		 * **************************************************************************/
		
		private void ShowErrorMessage(Exception e, string Title)
		{
			if (showError == true)
				MessageBox.Show(e.Message,
								Title
								,MessageBoxButtons.OK
								,MessageBoxIcon.Error);
		}


	}

    public enum OfficeComponent
    {
        Word,
        Excel,
        PowerPoint,
        Outlook
    }

    }


