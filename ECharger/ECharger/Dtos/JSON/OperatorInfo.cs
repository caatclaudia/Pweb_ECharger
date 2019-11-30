using System;
using System.ComponentModel.DataAnnotations;

namespace ECharger
{
    class OperatorInfo
    {
        [StringLength(255, ErrorMessage = "Title needs to be under 255 characters long!")]
        public string Title { get; set; }
    }
}
