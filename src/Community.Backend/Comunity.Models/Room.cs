﻿using Comunity.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comunity.Models
{
public class Room: BaseModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the image URL if there is one
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the latitude if there is one
        /// </summary>
        /// <value>The latitude.</value>
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude if there is one
        /// </summary>
        /// <value>The longitude.</value>
        public double? Longitude { get; set; }
    }

}
