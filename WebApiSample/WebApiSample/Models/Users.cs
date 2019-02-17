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
    public partial class Users
    {
        /// <summary>
        /// 
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(10)]
        public string UserName2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid SessionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdatedTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public string パスワード { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public string Comments { get; set; }
    }
}
