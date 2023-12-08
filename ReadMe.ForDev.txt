Support for ASP.NET Core Identity was added to your project.

For setup and configuration information, see https://go.microsoft.com/fwlink/?linkid=2116645.

In the project folder, run the Identity scaffolder with the options you want. 
For example, to setup identity with the default UI and the minimum number of files, run the following command. 
Use the correct fully qualified name for your DB context:

      dotnet aspnet-codegenerator identity -dc MyApplication.Data.ApplicationDbContext --files "Account.Register;Account.Login;Account.Logout"

If you run the Identity scaffolder without specifying the --files flag or the --useDefaultUI flag, all the available Identity UI pages are created in the project.

For more information about Role-based Identity:

      https://www.yogihosting.com/aspnet-core-identity-roles/