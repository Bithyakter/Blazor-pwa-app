using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPwaApp.Shared.Dto
{
   public class UserClaimsDto
   {
      public string UserId { get; set; }
      public string Username { get; set; }
      public string Firstname { get; set; }
      public string Lastname { get; set; }
   }
}