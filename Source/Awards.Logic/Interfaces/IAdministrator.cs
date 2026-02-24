namespace Awards.Logic
{
    /// <summary>
    /// Declares 'Delete' and 'Insert' operations for all tables.
    /// </summary>
    public interface IAdministrator
    {
        /// <summary>
        /// Deletes one Brand record from the table.
        /// </summary>
        /// <param name="id">ID of the one brand to be removed.</param>
        void RemoveBrand(int id);

        /// <summary>
        /// Deletes one Country record from the table.
        /// </summary>
        /// <param name="id">ID of the one country to be removed.</param>
        void RemoveCountry(int id);

        /// <summary>
        /// Deletes one ExpertGroup record from the table.
        /// </summary>
        /// <param name="id">ID of the one expert group to be removed.</param>
        void RemoveEG(int id);

        /// <summary>
        /// Deletes one Member record from the table.
        /// </summary>
        /// <param name="id">ID of the one member to be removed.</param>
        void RemoveMember(int id);

        /// <summary>
        /// Deletes one Product record from the table.
        /// </summary>
        /// <param name="id">ID of the one product to be removed.</param>
        void RemoveProduct(int id);

        /// <summary>
        /// Adds one Brand record to the table.
        /// </summary>
        /// <param name="name">Name of the brand.</param>
        /// <param name="country">Country of the brand.</param>
        /// <param name="address">Address of the brand.</param>
        /// <param name="homepage">Home (web) page of the brand.</param>
        void InsertBrand(string name, string country, string address, string homepage);

        /// <summary>
        /// Adds one Country record to the table.
        /// </summary>
        /// <param name="name">Name of the country.</param>
        /// <param name="capital">Capital city of the country.</param>
        /// <param name="callingcode">Calling code (1-3 digits, without + or 00) of the country.</param>
        /// <param name="ppp">Purchase Price Parity per Capita (in USD) of the country.</param>
        void InsertCountry(string name, string capital, int callingcode, int ppp);

        /// <summary>
        /// Adds one ExpertGroup record to the table.
        /// </summary>
        /// <param name="name">Name of the new expert group.</param>
        void InsertEG(string name);

        /// <summary>
        /// Adds one Member record to the table.
        /// </summary>
        /// <param name="name">Name of the member.</param>
        /// <param name="website">Website of the member.</param>
        /// <param name="publisher">Publisher of the member.</param>
        /// <param name="editor">Chief Editor of the member.</param>
        /// <param name="phone">Phone number (without prefix and calling code, e.g. +1 or 001) of the member.</param>
        /// <param name="country">Country of residence of the member.</param>
        /// <param name="office">Office location of the member.</param>
        /// <param name="expertgroup">Expert group membership of the member.</param>
        void InsertMember(string name, string website, string publisher, string editor, string phone, string country, string office, string expertgroup);

        /// <summary>
        /// Adds one Product record to the table.
        /// </summary>
        /// <param name="name">Name of the product.</param>
        /// <param name="brandName">Brand/Make of the product.</param>
        /// <param name="price">Price of the product.</param>
        /// <param name="estLifetime">Estimated lifetime of the product.</param>
        /// <param name="launch">Launch date of the product.</param>
        /// <param name="expGrp">The expert group that awarded the product.</param>
        /// <param name="award">The award (category) title.</param>
        void InsertProduct(string name, string brandName, int price, int estLifetime, System.DateTime launch, string expGrp, string award);

        /// <summary>
        /// Returns a list of Brand-Member pairs that are located at similar addresses.
        /// </summary>
        /// <returns>List of Brand-Member pairs at similar addresses.</returns>
        public System.Collections.Generic.IEnumerable<MemberBrand> ListBrandsAndMembersAtSameAdress();

        /// <summary>
        /// Asynchronously calls to <see cref="ListBrandsAndMembersAtSameAdress"/>.
        /// </summary>
        /// <returns>Call to the non-async method. </returns>
        public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<MemberBrand>> ListBrandsAndMembersAtSameAdressAsync();

        /// <summary>
        /// Updates the name of the selected Member record.
        /// </summary>
        /// <param name="id">ID of the member to be updated.</param>
        /// <param name="newName">New name for the selected member.</param>
        void ChangeMemberName(int id, string newName);

        /// <summary>
        /// Updates the price of the given Product record.
        /// </summary>
        /// <param name="id">ID of the record to be upddated.</param>
        /// <param name="newPrice">New price for the record to be updated.</param>
        void ChangeProductPrice(int id, int newPrice);

        /// <summary>
        /// Updates the name of the web home page of the given Brand record.
        /// </summary>
        /// <param name="id">ID of the Brand to be upddated.</param>
        /// <param name="newHP">New web home page for the Brand to be updated.</param>
        void ChangeBrandHomePage(int id, string newHP);

        /// <summary>
        /// Updates the name of the given ExpertGroup record.
        /// </summary>
        /// <param name="id">ID of the expert group to be upddated.</param>
        /// <param name="name">New name for the  expert group to be updated..</param>
        void ChangeEGName(int id, string name);

        /// <summary>
        /// Updates the Purchase Price Power per Capita data of the given Product record.
        /// </summary>
        /// <param name="id">ID of the country to be upddated.</param>
        /// <param name="newPPP">New PPP/C for the selected country.</param>
        void ChangeCountryPPP(int id, int newPPP);
    }
}
