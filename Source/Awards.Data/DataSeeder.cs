namespace Awards.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Helper class to fill <see cref="AwardsDbContext"/>.
    /// </summary>
    public static class DataSeeder
    {
        private static readonly Random Rnd = new();
        private static readonly DateTime StartDt = new(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly DateTime EndDt = new(2020, 12, 31, 23, 59, 59, DateTimeKind.Utc);
        private static Dictionary<string, ExpertGroup> nameToExpertGroup;
        private static Dictionary<string, Country> nameToCountry;
        private static Dictionary<string, Brand> nameToBrand;

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> collection of <see cref="Country"/> objects by calling the <see cref="CountryLoader(string)"/> method.
        /// </summary>
        public static IEnumerable<Country> GetCountries
        {
            get
            {
                nameToCountry ??= CountryLoader().ToDictionary(static c => c.Name, static c => c, StringComparer.OrdinalIgnoreCase);
                return nameToCountry.Values;
            }
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> collection of <see cref="ExpertGroup"/> objects by calling the <see cref="ExpertGroupLoader(string)"/> method.
        /// </summary>
        public static IEnumerable<ExpertGroup> GetExpertGroups
        {
            get
            {
                nameToExpertGroup ??= ExpertGroupLoader().ToDictionary(static eg => eg.Name, static eg => eg, StringComparer.OrdinalIgnoreCase);
                return nameToExpertGroup.Values;
            }
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> collection of <see cref="Member"/> objects by calling the <see cref="MemberLoader(string)"/> method.
        /// </summary>
        public static IEnumerable<Member> GetMembers => MemberLoader();

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> collection of <see cref="Brand"/> objects by calling the <see cref="BrandLoader(string)"/> method.
        /// </summary>
        public static IEnumerable<Brand> GetBrands
        {
            get
            {
                nameToBrand ??= BrandLoader().ToDictionary(static b => b.Name, static b => b, StringComparer.OrdinalIgnoreCase);
                return nameToBrand.Values;
            }
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> collection of <see cref="Brand"/> objects by calling the <see cref="BrandLoader(string)"/> method.
        /// </summary>
        public static IEnumerable<Product> GetProducts => ProductLoader();

        private static IEnumerable<Country> CountryLoader(string path = @"DataSeed\Country.csv")
        {
            int counter = 0;
            using StreamReader sr = new StreamReader(path);
            while (sr.ReadLine() is string line)
            {
                string[] fields = line.Split(';');
                yield return new Country
                {
                    CountryID = ++counter,
                    Name = fields[0],
                    CapitalCity = fields[1],
                    CallingCode = fields[2].ToIntOrZero(),
                    PPPperCapita = fields[3].ToIntOrZero(),
                };
            }
        }

        private static IEnumerable<ExpertGroup> ExpertGroupLoader(string path = @"DataSeed\ExpertGroup.csv")
        {
            using StreamReader sr = new StreamReader(path);
            while (sr.ReadLine() is string line)
            {
                string[] fields = line.Split(';');
                yield return new ExpertGroup
                {
                    ExpertGroupID = fields[0].ToIntOrZero(),
                    Name = fields[1],
                };
            }
        }

        private static IEnumerable<Member> MemberLoader(string path = @"DataSeed\Member.csv")
        {
            int counter = 0;
            using StreamReader sr = new StreamReader(path);
            while (sr.ReadLine() is string line)
            {
                string[] fields = line.Split(';');
                yield return new Member
                {
                    MemberID = ++counter,
                    ExpertGroupID = nameToExpertGroup.TryGetValue(fields[0], out ExpertGroup eg) ? eg.ExpertGroupID : 0,
                    Name = fields[1],
                    OfficeLocation = fields[3],
                    CountryID = nameToCountry.TryGetValue(fields[2], out Country c) ? c.CountryID : 0,
                    ChiefEditor = fields[4],
                    Publisher = fields[5],
                    PhoneNumber = fields[6],
                    Website = fields[7],
                };
            }
        }

        private static IEnumerable<Brand> BrandLoader(string path = @"DataSeed\Brand.csv")
        {
            int counter = 0;
            using StreamReader sr = new StreamReader(path);
            while (sr.ReadLine() is string line)
            {
                string[] fields = line.Split(';');
                yield return new Brand
                {
                    BrandId = ++counter,
                    Name = fields[0],
                    Address = fields[1],
                    CountryID = nameToCountry.TryGetValue(fields[2], out Country c) ? c.CountryID : 0,
                    Homepage = fields[3],
                };
            }
        }

        private static IEnumerable<Product> ProductLoader(string path = @"DataSeed\Product.csv")
        {
            int counter = 0;
            using StreamReader sr = new StreamReader(path);
            while (sr.ReadLine() is string line)
            {
                string[] fields = line.Split(';');
                yield return new Product
                {
                    ProductID = ++counter,
                    BrandId = nameToBrand.TryGetValue(fields[1], out Brand b) ? b.BrandId : 0,
                    Name = fields[2],
                    ExpertGroupID = fields[3].ToIntOrZero(),
                    Category = fields[4],
                    Price = Rnd.Next(9999),
                    LaunchDate = StartDt.AddDays(Rnd.Next((EndDt - StartDt).Days)),
                    EstimatedLifetime = Rnd.Next(1, 9),
                };
            }
        }

        /// <summary>
        /// Returns the integer value of the string if it can be parsed, otherwise returns 0. This method uses invariant culture for parsing and allows for leading and trailing whitespace.
        /// </summary>
        /// <param name="str">The string to parse.</param>
        /// <returns>The integer value of the string if it can be parsed; otherwise, 0.</returns>
        private static int ToIntOrZero(this string str) =>
            int.TryParse(str, System.Globalization.NumberStyles.Integer, System.Globalization.NumberFormatInfo.InvariantInfo, out int result)
                ? result
                : 0;
    }
}
