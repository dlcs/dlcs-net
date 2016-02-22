﻿using System;

namespace DLCS.WebClient.Model.Images
{
    public class ImageRegistrationResponse : Image
    {
        // supplied on response
        public int Customer { get; set; }
        public string InfoJs { get; set; }
        public string DegradedInfoJs { get; set; }
        public string ThumbnailInfoJs { get; set; }
        public DateTime DateAdded { get; set; }

        public Error Error { get; internal set; }
    }
}