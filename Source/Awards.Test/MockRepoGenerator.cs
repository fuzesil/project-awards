namespace Awards.Test
{
    using System.Collections.Generic;
    using Awards.Data;

    /// <summary>
    /// Containes list generators for entities.
    /// </summary>
    public static class MockRepoGenerator
    {
        /// <summary>
        /// Returns a list of brands for testing.
        /// </summary>
        /// <returns>List of brands.</returns>
        public static List<Brand> GenerateBrandList()
        {
            List<Brand> brandList = new List<Brand>()
            {
                new Brand { BrandId = 1, Name = "NAD", CountryID = 3, Address = "Pickering", Homepage = "nad.com" },
                new Brand { BrandId = 2, Name = "Bang & Olufsen", CountryID = 4, Address = "Struer", Homepage = "bo.com" },
                new Brand { BrandId = 3, Name = "DALI", CountryID = 4, Address = "Norager", Homepage = "dali.dk" },
                new Brand { BrandId = 4, Name = "DxO Labs", CountryID = 5, Address = "Paris", Homepage = "dxo.fr" },
                new Brand { BrandId = 5, Name = "Focal", CountryID = 5, Address = "Paris", Homepage = "focal.fr" },
                new Brand { BrandId = 6, Name = "Ground Zero", CountryID = 6, Address = "Egmting", Homepage = "gzero.de" },
                new Brand { BrandId = 7, Name = "Sennheiser", CountryID = 6, Address = "Wedemark", Homepage = "sennheiser.com" },
                new Brand { BrandId = 8, Name = "Volumio", CountryID = 8, Address = "Firenze", Homepage = "volumio.it" },
                new Brand { BrandId = 9, Name = "Canon", CountryID = 9, Address = "Tokyo", Homepage = "canon.jp" },
                new Brand { BrandId = 10, Name = "Fujifilm", CountryID = 9, Address = "Tokyo", Homepage = "fujifilm.jp" },
                new Brand { BrandId = 11, Name = "Sony", CountryID = 9, Address = "Tokyo", Homepage = "sony.jp" },
                new Brand { BrandId = 12, Name = "LG", CountryID = 9, Address = "Seoul", Homepage = "lg.com" },
                new Brand { BrandId = 13, Name = "Samsung", CountryID = 9, Address = "Seoul", Homepage = "samsung.com" },
            };
            return brandList;
        }

        /// <summary>
        /// Retruns a list of <see cref="Country"/> objects.
        /// </summary>
        /// <returns>List of <see cref="Country"/> objects.</returns>
        public static List<Country> GenerateCountryList()
        {
            List<Country> countryList = new List<Country>
            {
                new Country { CountryID = 1, Name = "Australia", CapitalCity = "Canberra", CallingCode = 61, PPPperCapita = 51885 },
                new Country { CountryID = 2, Name = "Belgium", CapitalCity = "Brussels", CallingCode = 32, PPPperCapita = 48224 },
                new Country { CountryID = 3, Name = "Canada", CapitalCity = "Ottawa", CallingCode = 1, PPPperCapita = 52144 },
                new Country { CountryID = 4, Name = "Denmark", CapitalCity = "Copenhagen", CallingCode = 45, PPPperCapita = 51643 },
                new Country { CountryID = 5, Name = "France", CapitalCity = "Paris", CallingCode = 33, PPPperCapita = 45454 },
                new Country { CountryID = 6, Name = "Germany", CapitalCity = "Berlin", CallingCode = 49, PPPperCapita = 53571 },
                new Country { CountryID = 7, Name = "Hungary", CapitalCity = "Budapest", CallingCode = 36, PPPperCapita = 35941 },
                new Country { CountryID = 8, Name = "Italy", CapitalCity = "Roma", CallingCode = 39, PPPperCapita = 40470 },
                new Country { CountryID = 9, Name = "Japan", CapitalCity = "Tokyo", CallingCode = 81, PPPperCapita = 43194 },
                new Country { CountryID = 10, Name = "South Korea", CapitalCity = "Seoul", CallingCode = 82, PPPperCapita = 44292 },
                new Country { CountryID = 11, Name = "Poland", CapitalCity = "Warszawa", CallingCode = 48, PPPperCapita = 35651 },
                new Country { CountryID = 12, Name = "USA", CapitalCity = "Washington, D.C.", CallingCode = 1, PPPperCapita = 63051 },
            };
            return countryList;
        }

        /// <summary>
        /// Retruns a list of <see cref="ExpertGroup"/> objects.
        /// </summary>
        /// <returns>List of <see cref="ExpertGroup"/> objects.</returns>
        public static List<ExpertGroup> GenerateExpertGroupList()
        {
            List<ExpertGroup> expertgroupList = new List<ExpertGroup>
            {
                new ExpertGroup { ExpertGroupID = 1, Name = "Hi-Fi" },
                new ExpertGroup { ExpertGroupID = 2, Name = "Home Theatre Audio" },
                new ExpertGroup { ExpertGroupID = 3, Name = "Home Theatre Display & Video" },
                new ExpertGroup { ExpertGroupID = 6, Name = "Photography" },
            };
            return expertgroupList;
        }

        /// <summary>
        /// Retruns a list of <see cref="Member"/> objects.
        /// </summary>
        /// <returns>List of <see cref="Member"/> objects.</returns>
        public static List<Member> GenerateMemberList()
        {
            List<Member> memberList = new List<Member>
            {
                new Member { MemberID = 1, Name = "Australian Hi-Fi", CountryID = 1, ExpertGroupID = 1, OfficeLocation = "Suite 3, Level 10, 100 Walker Street, North Sydney, NSW 2060," },
                new Member { MemberID = 2, Name = "FWD Magazine", CountryID = 2, ExpertGroupID = 1, OfficeLocation = "Van den Hautelei 101,   2100 Deurne" },
                new Member { MemberID = 3, Name = "SOUNDSTAGE! HI-FI", CountryID = 3, ExpertGroupID = 1, OfficeLocation = "1953 Meldrum Avenue Ottawa, ON K1J 7V6" },
                new Member { MemberID = 4, Name = "SOUNDSTAGE! HI-FI", CountryID = 4, ExpertGroupID = 1 },
                new Member { MemberID = 5, Name = "Les Années Laser", CountryID = 5, ExpertGroupID = 2, OfficeLocation = "20, passage Turquetil, - 75011 Paris" },
                new Member { MemberID = 6, Name = "Heimkino", CountryID = 6, ExpertGroupID = 2, OfficeLocation = "Gartroper Strasse 42, D-47138 - Duisburg" },
                new Member { MemberID = 7, Name = "Hifi Test TV Video", CountryID = 6, ExpertGroupID = 3, OfficeLocation = "Gartroper Strasse 42, D-47138 - Duisburg", },
                new Member { MemberID = 8, Name = "Stereo", CountryID = 6, ExpertGroupID = 1, OfficeLocation = "Eifelring 28, D-53879 Euskirchen" },
                new Member { MemberID = 9, Name = "Sztereó Sound&Vision", CountryID = 7, ExpertGroupID = 1, OfficeLocation = "Attila út 101, H-1012 Budapest" },
                new Member { MemberID = 10, Name = "Sztereó Sound&Vision", CountryID = 7, ExpertGroupID = 2, OfficeLocation = "Attila út 101, H-1012 Budapest" },
                new Member { MemberID = 11, Name = "Sztereó Sound&Vision", CountryID = 7, ExpertGroupID = 3, OfficeLocation = "Attila út 101, H-1012 Budapest" },
                new Member { MemberID = 12, Name = "AUDIOreview", CountryID = 8, ExpertGroupID = 2, OfficeLocation = "Via Nomentana, 1018  00137 Roma" },
                new Member { MemberID = 13, Name = "Digital Video HT", CountryID = 8, ExpertGroupID = 3, OfficeLocation = "Via Nomentana, 1018  00137 Roma" },
                new Member { MemberID = 14, Name = "Audio Accessory", CountryID = 9, ExpertGroupID = 1, OfficeLocation = "7th AZUMA BLDG, 1-9, Kanda Sakuma-cho, Chiyoda-ku, Tokyo, 101-0025" },
                new Member { MemberID = 15, Name = "Audio", CountryID = 11, ExpertGroupID = 1, OfficeLocation = "Warszawa" },
                new Member { MemberID = 16, Name = "Sound & Vision", CountryID = 12, ExpertGroupID = 2, OfficeLocation = "Madison Avenue, 8 th floor, New York, NY 10016" },
                new Member { MemberID = 17, Name = "Stereophile", CountryID = 9, ExpertGroupID = 1, OfficeLocation = "Madison Avenue, 8 th floor, New York, NY 10016" },
            };
            return memberList;
        }

        /// <summary>
        /// Retruns a list of <see cref="Product"/> objects.
        /// </summary>
        /// <returns>List of <see cref="Product"/> objects.</returns>
        public static List<Product> GenerateProductList()
        {
            List<Product> productList = new List<Product>
            {
                new Product { ProductID = 1, BrandId = 2, Name = "Beosound Stage", Category = "BEST PREMIUM SOUNDBAR", ExpertGroupID = 2, Price = 2000 },
                new Product { ProductID = 2, BrandId = 9, Name = "EOS-1D X Mark III", Category = "BEST PROFESSIONAL CAMERA", ExpertGroupID = 6, Price = 9183 },
                new Product { ProductID = 3, BrandId = 3, Name = "Epikon", Category = "BEST DALI FLOORSTANDER", ExpertGroupID = 1, Price = 1999 },
                new Product { ProductID = 4, BrandId = 4, Name = "Nik Collection 3", Category = "BEST PHOTO SOFTWARE", ExpertGroupID = 6, Price = 5000 },
                new Product { ProductID = 5, BrandId = 4, Name = "Chora 826", Category = "BEST VALUE FLOORSTANDING LOUDSPEAKER", ExpertGroupID = 1, Price = 1899 },
                new Product { ProductID = 6, BrandId = 10, Name = "X100V", Category = "BEST COMPACT CAMERA", ExpertGroupID = 6, Price = 3500 },
                new Product { ProductID = 7, BrandId = 12, Name = "SN8YG", Category = "BEST SOUNDBAR", ExpertGroupID = 2 },
                new Product { ProductID = 8, BrandId = 12, Name = "OLED65GX", Category = "BEST PREMIUM OLED TV", ExpertGroupID = 3, Price = 29999 },
                new Product { ProductID = 9, BrandId = 12, Name = "75NANO99", Category = "BEST 8K TV", ExpertGroupID = 3, Price = 2199 },
                new Product { ProductID = 10, BrandId = 13, Name = "QE75Q950TS", Category = "BEST LARGE SCREEN TV", ExpertGroupID = 3, Price = 2299 },
                new Product { ProductID = 11, BrandId = 11, Name = "Vlog Camera ZV-1", Category = "Best Vlogging Camera", ExpertGroupID = 6, Price = 2500 },
            };
            return productList;
        }
    }
}