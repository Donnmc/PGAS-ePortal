using System.DirectoryServices;
using System.Runtime.Versioning;

namespace ADIntegratedAPI.Services
{
    public class AdService
    {
        private readonly string _ldapPath;

        public AdService(string ldapPath)
        {
            _ldapPath = ldapPath;
        }

        [SupportedOSPlatform("windows")]
        public bool AuthenticateUser(string username, string password)
        {
            try
            {
                using (var entry = new DirectoryEntry(_ldapPath, username, password))
                {
                    // Force authentication by accessing the NativeObject property
                    var obj = entry.NativeObject;
                    return true;
                }
            }
            catch
            {
                // Authentication failed
                return false;
            }
        }

        [SupportedOSPlatform("windows")]
        public List<string> GetUserRoles(string username)
        {
            var roles = new List<string>();
            try
            {
                using (var entry = new DirectoryEntry(_ldapPath))
                using (var searcher = new DirectorySearcher(entry))
                {
                    searcher.Filter = $"(&(objectClass=user)(sAMAccountName={username}))";
                    searcher.PropertiesToLoad.Add("memberOf");

                    var result = searcher.FindOne();
                    if (result != null)
                    {
                        foreach (string role in result.Properties["memberOf"])
                        {
                            // Extract role name from LDAP path
                            var roleParts = role.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            var roleName = roleParts[0].Substring(3); // Removes "CN=" prefix
                            roles.Add(roleName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                throw new Exception("An error occurred while retrieving user roles from Active Directory.", ex);
            }

            return roles;
        }
    }
}
