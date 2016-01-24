namespace DLCS.Client.Model.Images
{
    public class Image
    {
        /**
  

            THIS IS THE OLD IMAGE...
    */
        public string Id { get; set; }
        public int Space { get; set; }
        public string Origin { get; set; }
        public string InitialOrigin { get; set; }

        // these will become StringReference1, 2, 3
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public string Reference3 { get; set; }

        public long NumberReference1 { get; set; }
        public long NumberReference2 { get; set; }
        public long NumberReference3 { get; set; }

        public int MaxUnauthorised { get; set; }

        public string[] Tags { get; set; }
        public string[] Roles { get; set; } // need to be URIs

    }
}
