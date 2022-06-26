namespace NewShop.Additional
{

    public class Wishlists
    {
        public string Name { get; }

        public string Qty { get; }

        public string Viewed { get; }

        public string Created { get; }

        public string DirectLink { get; }

        public string Delete { get; }


        public Wishlists(string name, string qty, string viewed, string created, string directLink, string delete)
        {
            Name = name;
            Qty = qty;
            Viewed = viewed;
            Created = created;
            DirectLink = directLink;
            Delete = delete;
        }
    }
}