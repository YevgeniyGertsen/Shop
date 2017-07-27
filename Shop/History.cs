﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("VisitHistory")]
    public class History
    {
        DateTime vizitDate;
       

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        DateTime VizitDate
        {
            get { return vizitDate; }
            set
            {
                if (value == null || value == DateTime.MinValue)
                    vizitDate = DateTime.Now;
                else
                    vizitDate = value;
            }
        }
        public int HistID { get; set; }
        public int UserID { get; set; }
        public string IP { get; set; }
        public string ClientAgent { get; set; }
        public string ClientDevice { get; set; }
    }
}
