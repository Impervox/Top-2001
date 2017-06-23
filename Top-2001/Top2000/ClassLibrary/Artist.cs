using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// The name
        /// </summary>
        private string name;
        /// <summary>
        /// The biography
        /// </summary>
        private string biography;
        /// <summary>
        /// The URL
        /// </summary>
        private string url;


        /// <summary>
        /// Initializes a new instance of the <see cref="Artist"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="biography">The biography.</param>
        /// <param name="url">The URL.</param>
        public Artist(string name, string biography = null, string url = null)
        {
            this.name = name;
            this.biography = biography;
            this.url = url;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Gets or sets the biography.
        /// </summary>
        /// <value>
        /// The biography.
        /// </value>
        public string Biography
        {
            get
            {
                return biography;
            }

            set
            {
                biography = value;
            }
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url
        {
            get
            {
                return url;
            }

            set
            {
                url = value;
            }
        }
    }
}
