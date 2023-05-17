using Microsoft.AspNetCore.Mvc.Filters;
using System;
namespace ServerAPI.Attribute
{
    public class AdminApiAttribute : System.Attribute, IFilterMetadata
    {
    }
}
