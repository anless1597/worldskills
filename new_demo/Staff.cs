//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace new_demo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Staff
    {
        public string EmployeeCode { get; set; }
        public string Position { get; set; }
        public string FIO { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public System.DateTime LastEnter { get; set; }
        public bool EnterType { get; set; }
    
        public virtual StaffPositions StaffPositions { get; set; }
    }
}
