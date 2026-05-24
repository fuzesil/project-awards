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
        private static readonly Random Rnd = new Random();
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
        public static IEnumerable<Member> GetMembers { get => MemberLoader(); }

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
        public static IEnumerable<Product> GetProducts { get => ProductLoader(); }

        private static IEnumerable<Country> CountryLoader(string path = @"DataSeed\Country.csv")
        {
            List<Country> output = new List<Country>();
            int counter = 0;
            string line = string.Empty;
            string[] fields;
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    fields = line.Split(';');
                    output.Add(new Country
                    {
                        CountryID = ++counter,
                        Name = fields[0],
                        CapitalCity = fields[1],
                        CallingCode = ((Func<int>)(() => string.IsNullOrWhiteSpace(fields[2]) ? 0 : Convert.ToInt32(fields[2], System.Globalization.NumberFormatInfo.InvariantInfo)))(),
                        PPPperCapita = ((Func<int>)(() => string.IsNullOrWhiteSpace(fields[3]) ? 0 : Convert.ToInt32(fields[3], System.Globalization.NumberFormatInfo.InvariantInfo)))(),
                    });
                }

                sr.Close();
            }

            return output;
        }

        private static IEnumerable<ExpertGroup> ExpertGroupLoader(string path = @"DataSeed\ExpertGroup.csv")
        {
            List<ExpertGroup> output = new List<ExpertGroup>();
            int counter = 0;
            string line;
            string[] fields;
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    fields = line.Split(';');
                    output.Add(new ExpertGroup
                    {
                        ExpertGroupID = ++counter,
                        Name = fields[0],
                    });
                }

                sr.Close();
            }

            return output;
        }

        private static IEnumerable<Member> MemberLoader(string path = @"DataSeed\Member.csv")
        {
            List<Member> output = new List<Member>();
            int counter = 0;
            string line = string.Empty;
            string[] fields;
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    fields = line.Split(';');
                    output.Add(new Member
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
                    });
                }

                sr.Close();
            }

            return output;
        }

        private static IEnumerable<Brand> BrandLoader(string path = @"DataSeed\Brand.csv")
        {
            List<Brand> output = new List<Brand>();
            int counter = 0;
            string line = string.Empty;
            string[] fields;
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    fields = line.Split(';');
                    output.Add(new Brand
                    {
                        BrandId = ++counter,
                        Name = fields[0],
                        Address = fields[1],
                        CountryID = nameToCountry.TryGetValue(fields[2], out Country c) ? c.CountryID : 0,
                        Homepage = fields[3],
                    });
                }

                sr.Close();
            }

            return output;
        }

        private static IEnumerable<Product> ProductLoader(string path = @"DataSeed\Product.csv")
        {
            List<Product> output = new List<Product>();
            int counter = 0;
            string line = string.Empty;
            string[] fields;
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    fields = line.Split(';');
                    output.Add(new Product
                    {
                        ProductID = ++counter,
                        BrandId = nameToBrand.TryGetValue(fields[1], out Brand b) ? b.BrandId : 0,
                        Name = fields[2],
                        ExpertGroupID = int.Parse(fields[3], System.Globalization.NumberFormatInfo.InvariantInfo),
                        Category = fields[4],
                        Price = Rnd.Next(9999),
                    LaunchDate = StartDt.AddDays(Rnd.Next((EndDt - StartDt).Days)),
                        EstimatedLifetime = Rnd.Next(1, 9),
                    });
                }

                sr.Close();
            }

            return output;
        }
    }
}
