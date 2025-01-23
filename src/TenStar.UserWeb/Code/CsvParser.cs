namespace TenStar.UserWeb.Code
{
    internal static class CsvParser
    {
        /// <summary>
        /// Parses a CSV file and returns an array of <see cref="User"/> objects. If any error occurs, an empty array is returned.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task<User[]> ParseUsers(Stream? stream)
        {
            ArgumentNullException.ThrowIfNull(stream);

            if (!stream.CanRead)
            {
                throw new InvalidOperationException("The provided stream is not readable.");
            }

            if (stream.Length == 0)
            {
                throw new InvalidOperationException("The stream is empty.");
            }

            using var reader = new StreamReader(stream);
            var csvContent = await reader.ReadToEndAsync();

            if (string.IsNullOrWhiteSpace(csvContent))
            {
                return [];
            }

            return ParseCsvContent(csvContent);
        }

        private static User[] ParseCsvContent(string csvContent)
        {
            var lines = csvContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length == 0)
            {
                return [];
            }

            var users = new List<User>();
            foreach (var line in lines)
            {
                if (line.ToLower().StartsWith("fullname,username,email,password")) continue;

                var columns = line.Split(',', StringSplitOptions.TrimEntries);

                if (columns.Length != 4) return [];

                users.Add(new User
                {
                    FullName = columns[0],
                    Username = columns[1],
                    Email = columns[2],
                    Password = columns[3]
                });
            }

            return users.ToArray();
        }
    }
}
