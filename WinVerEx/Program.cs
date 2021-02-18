using System;
using Microsoft.Win32;

namespace WinVerEx
{
    class Program
    {
        static void Main(string[] args)
        {
            // All of the data we're looking for lives under the same Registry Key
            RegistryKey currentVersion = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion");
            
            // Grab the reported installed build of the OS, before updates
            String buildLabEx = currentVersion.GetValue("BuildLabEx").ToString();
            Console.WriteLine("Installed image: " + buildLabEx);

            // Grab the newest installed version and minor number
            Console.WriteLine("Updated to: " + String.Format("{0}.{1}", currentVersion.GetValue("CurrentBuild").ToString(), currentVersion.GetValue("UBR").ToString()));

            // Grab the product name, as this includes "SKU" information
            Console.WriteLine("Product name: " + currentVersion.GetValue("ProductName"));

            // This is a deeper dive on what "SKU" you're running
            Console.WriteLine("Edition ID: " + currentVersion.GetValue("EditionID"));

            // Grab the installation type as well
            Console.WriteLine("Installation type: " + currentVersion.GetValue("InstallationType"));
            currentVersion.Close();

            if (args.Length > 0)
            {
                // Display a one off format if i or inline is passed in
                switch (args[0]) {
                    case "i":
                    case "inline":
                        // Write out a streamlined version number
                        string[] versionParts = buildLabEx.Split('.');
                        if (versionParts.Length == 5)
                        {
                            Console.WriteLine(String.Format("{0} {1}.{2}.{3} {4}", versionParts[3], versionParts[0], versionParts[1], versionParts[4], versionParts[2]));
                        }
                        break;
                }

                // Output message no matter what extra information came in
                Console.WriteLine();
                Console.WriteLine("Visit http://www.github.com/randyrants/winverex for more information or source code.");
            }
        }
    }
}
