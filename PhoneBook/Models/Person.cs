using System;
using System.Collections.Generic;

namespace PhoneBook.Models;

public partial class Person
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string Phone { get; set; } = null!;
}
