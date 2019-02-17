namespace WebApiSample.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 
    /// </summary>
    public partial class tree_paths
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ancestor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long descendant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long path_length { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual categories categories { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual categories categories1 { get; set; }
    }
}
